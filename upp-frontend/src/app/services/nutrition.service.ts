import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Calendar } from "../models/Calendar";
import { Observable } from "rxjs";
import { Product } from "../models/Product";
import { PaginatedList } from "../models/PaginatedList";
import { Calories } from "../models/Calories";
import { CaloriesByDay } from "../models/CaloriesByDay";

@Injectable({
    providedIn: 'root',
  })
export class NutritionService {
    refProduct: string = "http://localhost:5214/api/Product";
    refCalendar: string = "http://localhost:5214/api/Calendar";

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
}