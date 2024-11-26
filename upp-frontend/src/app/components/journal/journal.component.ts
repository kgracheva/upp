import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Calories } from "../../models/Calories";
import { NutritionService } from "../../services/nutrition.service";
import { CaloriesByDay } from "../../models/CaloriesByDay";

@Component({
  selector: "app-journal",
  templateUrl: "./journal.component.html",
  styleUrl: "./journal.component.scss",
})
export class JournalComponent {
  isLoading: boolean = false;

  calories: Calories = {
    userId: JSON.parse(localStorage.getItem("user")!).userId,
    date: new Date(),
  };

  caloriesByDay: CaloriesByDay | null = null;

  constructor(
    private router: Router,
    private nutritionService: NutritionService
  ) {
    this.getCountCalories();
  }

  public addProduct(mealType: number) {
    this.router.navigateByUrl("/nutrition?mealType=" + mealType);
  }

  public getCountCalories() {
    this.nutritionService.getCalories(this.calories).subscribe((x) => {
      this.caloriesByDay = x;
      this.isLoading = true;
    });
  }

  getNumber(value: any) {
    return Math.floor(value);
  }
}
