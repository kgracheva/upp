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
import { PhyschologistDto } from "../models/PhyschologistDto";
import { StatusDto } from "../models/StatusDto";

@Injectable({
  providedIn: "root",
})
export class UserService {
  refUser: string = "https://localhost:7171/api/User";

  constructor(private httpClient: HttpClient, private router: Router) {}

  public getUsers() {
    return this.httpClient.get<PaginatedList<PhyschologistDto>>(this.refUser);
  }

  public changeWorkStatus(dto: StatusDto) {
    return this.httpClient.post(this.refUser, dto);
  }
}
