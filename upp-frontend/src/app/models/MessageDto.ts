export interface MessageDto {
    id: number;
    text: string | null;
    userId: number;
    isRead: boolean;
    chatId: number;
    created: Date;
}