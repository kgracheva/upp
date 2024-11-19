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

@Injectable({
    providedIn: 'root',
  })
export class NutritionService {
    refProduct: string = "https://localhost:7171/api/Product";
    refCalendar: string = "https://localhost:7171/api/Calendar";

    constructor(private httpClient: HttpClient, private router: Router) {
    }

    public createCalendar(calendar: Calendar) {
      return this.httpClient.post(this.refCalendar, calendar);
    }

    public getProducts() {
      return this.httpClient.get<PaginatedList<Product>>(this.refProduct);
    }

    public getCalories(calories: Calories) {
      return this.httpClient.post<CaloriesByDay>(this.refCalendar + "/count-day", calories);
    }

    public createSpecialInfo(model: SpecialData) {
      return this.httpClient.put(this.refCalendar + "/special-info", model);
    }

    public getSpecialInfo(userId: number) {
      return this.httpClient.get<SpecialData>(this.refCalendar + "/special-info?userId=" + userId);
    }
}