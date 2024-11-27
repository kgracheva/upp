import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Calendar } from "../models/Calendar";
import { Observable } from "rxjs";
import { Product } from "../models/Product";
import { PaginatedList } from "../models/PaginatedList";
import { Calories } from "../models/Calories";
import { CaloriesByDay } from "../models/CaloriesByDay";
import { SpecialData } from "../models/SpecialData";
import { ArticleDto } from "../models/ArticleDto";
import { TrainingDto } from "../models/TrainingDto";
import { RecipeDto } from "../models/RecipeDto";

@Injectable({
  providedIn: "root",
})
export class EntityService {
  refArticle: string = "https://localhost:7171/api/Article";
  refRecipe: string = "https://localhost:7171/api/Recipe";
  refTraining: string = "https://localhost:7171/api/Training";

  constructor(private httpClient: HttpClient, private router: Router) {}

  public getArticles() {
    return this.httpClient.get<PaginatedList<ArticleDto>>(this.refArticle);
  }

  public getArticle(id: any) {
    return this.httpClient.get<ArticleDto>(this.refArticle + "/" + id);
  }

  public getTrainings() {
    return this.httpClient.get<PaginatedList<TrainingDto>>(this.refTraining);
  }

  public getTraining(id: any) {
    return this.httpClient.get<TrainingDto>(this.refTraining + "/" + id);
  }

  public getRecipes() {
    return this.httpClient.get<PaginatedList<RecipeDto>>(this.refRecipe);
  }

  public getRecipe(id: any) {
    return this.httpClient.get<RecipeDto>(this.refRecipe + "/" + id);
  }

  public editArticle(dto: ArticleDto) {
    return this.httpClient.put(this.refArticle, dto);
  }
  public editRecipe(dto: RecipeDto) {
    return this.httpClient.put(this.refRecipe, dto);
  }

  public editTraining(dto: TrainingDto) {
    return this.httpClient.put(this.refTraining, dto);
  }
}
