import { BlockDto } from "./BlockDto";

export interface ArticleDto {
    id: number;
    name: string;
    creatorId: number;
    creatorName: string;
    statusTypeId: number;
    blocks: BlockDto[];
}