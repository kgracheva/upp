import { BlockDto } from "./BlockDto";

export interface RecipeDto {
    id: number;
    name: string;
    proteinsCount: number;
    fatsCount: number;
    carbsCount: number;
    caloriesCount: number;
    creatorId: number;
    isDeleted: boolean;
    statusTypeId: number;
    blocks: BlockDto[] | null;
}