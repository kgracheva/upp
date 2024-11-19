export interface FindRequestsDto {
    date: string | null;
    creatorId: number;
    requestType: number;
    name: string;
    page: number;
    size: number;
}