export interface GetMessagesDto {
    chatId: number;
    take: number;
    lastMessageId: number | null;
}