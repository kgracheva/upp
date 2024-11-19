import { ArticleDto } from "./ArticleDto";
import { RecipeDto } from "./RecipeDto";
import { TrainingDto } from "./TrainingDto";

export interface CreateRequestDto {
    article: ArticleDto | null;
    recipe: RecipeDto | null;
    training: TrainingDto | null;
}