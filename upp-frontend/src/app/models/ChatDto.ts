import { ChatUserDto } from "./ChatUserDto";
import { MessageDto } from "./MessageDto";

export interface ChatDto {
    id: number;
    unreadCount: number;
    lastMessage: MessageDto | null;
    users: ChatUserDto[];
}