
import {
  AfterViewChecked,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { ChatDto } from '../../models/ChatDto';
import { User } from '../../models/User';
import { MessageDto } from '../../models/MessageDto';
import { AuthService } from '../../services/auth.service';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-chat-window',
  templateUrl: './chat-window.component.html',
  styleUrls: ['./chat-window.component.scss'],
})
export class ChatWindowComponent implements OnInit, AfterViewChecked {
  private _chat: ChatDto | null = null;
  public currentUser!: User;
  public needToScroll = true;
  public isLoading = true;
  public isAccepted = false;

  public messages: MessageDto[] = [];
  sidebar = false

  public currentText = '';
  @ViewChild('chatWindow') private chatContainer!: ElementRef;

  @Input()
  set chat(value: ChatDto) {
    
    this._chat = value;
    this.loadMessages();
  }

  get chat(): ChatDto | null {
    return this._chat;
  }

  @Output() chatChange = new EventEmitter<ChatDto>();

  constructor(
    private authService: AuthService,
    private chatService: ChatService,
  ) {
    this.currentUser = this.authService.user();
  }

  getName() {
    console.log("current" + this.currentUser.id);
    var user = this._chat?.users.find((x) => x.userId != this.currentUser.id);
    console.log(this._chat?.users);
    return user!.name;
  }

  ngOnInit() {
    
    this.chatService.messageReceived.subscribe((message) => {
      

      if (!message) return;

      if (message.chatId == this.chat!.id) {
        this.messages.push(message);
        this.chatService.readMessages(this.chat!.id).then((_) => {});
        this._chat!.unreadCount = 0;
        this.chatChange.emit(this.chat!);

        console.log(message);
      }
    });

    this.chatService.chatRead.subscribe((chatId) => {
      if (this.chat!.id == chatId) {
        this.messages
          .filter((x) => x.userId == +this.currentUser.id && x.isRead == false)
          .forEach((x) => (x.isRead = true));
      }
    });
    this.isLoading = false;
  }

  ngAfterViewChecked(): void {    
    
    this.scrollToBottom();
  }



  getRole(): string {
    return JSON.parse(localStorage.getItem('user')!).roles;
  }

  getUrl(id: number) {
    return '/api/file/' + id;
  }

  loadMessages() {
    if (!this._chat?.id) return;
    this.chatService.openChat({
      chatId: this._chat!.id,
      take: 0,
      lastMessageId: null
    }).subscribe((x) => {
      this.messages = x;
      this.chat!.unreadCount = 0;
      this.chatChange.emit(this.chat!);
      this.chatService.readMessages(this._chat!.id).then((_) => {});
    });
  }

  getUser(userId: number) {
    return this.chat!.users.find((x) => x.userId == userId);
  }

  async send() {
    const message: MessageDto = {
      chatId: this.chat!.id,
      isRead: false,
      text: this.currentText,
      userId: +this.currentUser.id,
      created: new Date(),
      id: 0
    };
    this.currentText = '';
    this.needToScroll = true;
    const sentMessage = message;
    this.messages.push(sentMessage);
    this.scrollToBottom();
    this.chat!.lastMessage = sentMessage;

    await this.chatService.send(this.chat!.id, message);
  }

  scrollToBottom(): void {
    try {
      this.needToScroll = false;
      this.chatContainer.nativeElement.scrollTop =
        this.chatContainer.nativeElement.scrollHeight;
    } catch (err) {}
  }
}
