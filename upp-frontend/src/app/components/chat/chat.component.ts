import { Component, OnInit } from '@angular/core';
import { ChatDto } from '../../models/ChatDto';
import { User } from '../../models/User';
import { ChatService } from '../../services/chat.service';
import { AuthService, AuthUserDto } from '../../services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { from } from 'rxjs';
import { MessageDto } from '../../models/MessageDto';
import { CreateSimpleChatDto } from '../../models/CreateSimpleChatDto';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent implements OnInit {
  public chats: ChatDto[] = [];
  public currentUser: AuthUserDto | null = null;
  public currentOpenChat: ChatDto | null = null;
  public openChatId: number | null = null;

  constructor(private chatService: ChatService,  private authService: AuthService, private route: ActivatedRoute)
  {
  }

  async ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.openChatId = params["id"];
    });

    from(this.chatService.startConnection()).subscribe((_) => {
      this.currentUser = this.authService.user();
      this.loadChats();
    });
    //this.chatService.addRecieveListener();
  }

  public openChat(chat: ChatDto) {
    this.currentOpenChat = chat;
  }

  public getUser(chat: ChatDto) {
    const last = chat.lastMessage?.userId;

    const a = chat.users.find(x => x.userId == last) || chat.users.find(x => x.userId != +this.currentUser!.userId);
    return a;
  }

  public getText(last: MessageDto) {
    return last?.text || "";

  }

  public updateChat(chat: ChatDto) {
    this.currentOpenChat!.unreadCount = chat.unreadCount;
  }

  public isMessageOld(date: Date) {
    var newDate = new Date(date);
    newDate.setDate(date?.getDate() + 1);
    return newDate < new Date();
  }

  private loadChats() {
    this.chatService.getChats().subscribe(x => {
      this.chats = x;
      if (this.openChatId !== null && this.openChatId != undefined) {
        var chat = this.chats.find(x => x.id == this.openChatId);
        if (chat != null) this.openChat(chat);
      }
      this.subscribeAll();
    })
  }

  private subscribeAll() {
    this.chatService.messageReceived.subscribe(x => {
      if (!x) return;
      const c = this.chats.find(chat => chat.id == x.chatId);
      c!.unreadCount++;
      c!.lastMessage = {
        id: x.id,
        isRead: x.isRead,
        text: x.text,
        userId: x.userId,
        created: x.created,
        chatId: x.chatId
      };
    })
  }

  public createChat() {
    let dto: CreateSimpleChatDto = {
      userIds: [1, 7]
    }
    this.chatService.createChat(dto).subscribe(x => {});
  }
}
