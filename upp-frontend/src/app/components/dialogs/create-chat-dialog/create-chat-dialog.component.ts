import { AuthService } from "./../../../services/auth.service";
import { Component } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";
import { ChatService } from "../../../services/chat.service";
import { ShortUserDto } from "../../../models/UserShortDto";

@Component({
  selector: "app-create-chat-dialog",
  templateUrl: "./create-chat-dialog.component.html",
  styleUrl: "./create-chat-dialog.component.scss",
})
export class CreateChatDialogComponent {
  public users: ShortUserDto[] = [];
  public user: string | undefined = undefined;
  constructor(
    private ref: MatDialogRef<CreateChatDialogComponent>,
    private chatService: ChatService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.chatService.getUsers().subscribe((x) => {
      this.users = x;
    });
  }

  create() {
    if (this.user) {
      const currentUser = this.authService.user();
      if (currentUser.userId == Number.parseInt(this.user)) return;
      this.chatService.createChat({
        userIds: [Number.parseInt(this.user), currentUser.userId],
      }).subscribe(res => {
        this.ref.close(true);
      })
    }
  }
}
