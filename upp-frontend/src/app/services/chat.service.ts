import { BehaviorSubject } from 'rxjs';

import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { MessageDto } from '../models/MessageDto';
import { AuthService } from './auth.service';
import { HubConnection, IHttpConnectionOptions } from '@microsoft/signalr';
import { HttpClient } from '@angular/common/http';
import { PaginatedList } from '../models/PaginatedList';
import { ChatDto } from '../models/ChatDto';
import { CreateSimpleChatDto } from '../models/CreateSimpleChatDto';
import { GetMessagesDto } from '../models/GetMessagesDto';
import { ShortUserDto } from '../models/UserShortDto';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  refChat: string = "https://localhost:7171/api/Chat";
  constructor(private authService: AuthService, private httpClient: HttpClient) {}

  messageReceived: BehaviorSubject<MessageDto> =
    new BehaviorSubject<MessageDto>({id: 0,  text: "",
        userId: 0,
        isRead: false,
        chatId: 0,
        created: new Date()
    });

  chatRead: BehaviorSubject<number> = new BehaviorSubject<number>(0);

  private hubConnection!: signalR.HubConnection;
  private hubOptions: IHttpConnectionOptions = {
    withCredentials: true,
    skipNegotiation: true,  // skipNegotiation as we specify WebSockets
    transport: signalR.HttpTransportType.WebSockets,
    accessTokenFactory: async () => {
      return this.authService.getToken()!;
  }}

  public startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7171/hub', this.hubOptions)
      .build();
    this.addRecieveListener();
    return this.hubConnection
      .start()
      .then(() => {console.log('Connection started')})
      .catch((err) => console.log('Error while starting connection: ' + err));
  }

  addRecieveListener() {
    this.hubConnection.on(
      'messageReceived',
      (userId: number, chatId: number, message: string) => {
        this.messageReceived.next({
          chatId: chatId,
          id: 0,
          isRead: false,
          text: message,
          userId: userId,
          created: new Date(),
        });
      }
    );

    this.hubConnection.on('read', (chatId: number) => {
      this.chatRead.next(chatId);
    });
  }

  async send(chatId: number, message: string) {
    await this.hubConnection.send('SendMessage', chatId, message);
  }

  async readMessages(chatId: number) {
    await this.hubConnection.send('SetMessageRead', chatId);
  }

  getChats() {
    return this.httpClient.get<ChatDto[]>(this.refChat);
  }



  createChat(dto: CreateSimpleChatDto) {
    return this.httpClient.post(this.refChat + "/simple", dto);
  }

  openChat(dto: GetMessagesDto) {
    return this.httpClient.get<MessageDto[]>(this.refChat + "/open?chatId=" + dto.chatId);
  }

  getUsers() {
    return this.httpClient.get<ShortUserDto[]>(this.refChat + "/users");
  }
}
