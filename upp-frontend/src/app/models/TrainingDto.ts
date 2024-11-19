import { BlockDto } from "./BlockDto";

export interface TrainingDto {
    id: number;
    name: string;
    type: string;
    statusTypeId: number;
    creatorId: number;
    videoRef: string;
    isDeleted: boolean;
    blocks: BlockDto[];
}