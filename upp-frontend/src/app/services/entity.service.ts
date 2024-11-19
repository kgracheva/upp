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

@Injectable({
    providedIn: 'root',
  })
export class EntityService {
    refArticle: string = "https://localhost:7171/api/Article";
    refRecipe: string = "https://localhost:7171/api/Recipe";
    refTraining: string = "https://localhost:7171/api/Training";

    constructor(private httpClient: HttpClient, private router: Router) {
    }



    public getArticles() {
      return this.httpClient.get<PaginatedList<ArticleDto>>(this.refArticle);
    }
}