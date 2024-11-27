import { Component, inject } from '@angular/core';
import { Calendar } from '../../models/Calendar';
import { NutritionService } from '../../services/nutrition.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Product } from '../../models/Product';
import { Observable } from 'rxjs';
import { Calories } from '../../models/Calories';
import { FindCalendarsDto } from '../../models/FindCalendarsDto';

@Component({
  selector: 'app-nutrition',
  templateUrl: './nutrition.component.html',
  styleUrl: './nutrition.component.scss'
})
export class NutritionComponent {
  public productsList: Product[] = [];
  public currentProductsList: Calendar[] = [];

  calendar: Calendar = {
    id: 0,
    userId: JSON.parse(localStorage.getItem('user')!).userId,
    productId: 0,
    productCount: 100,
    mealTypeId: 0,
    productName: ''
  }

  isNew: boolean = false;

  constructor(private nutritionService: NutritionService, private route:ActivatedRoute, private router: Router) {
    this.getProducts();

    route.queryParams.subscribe(queryParam => {
         this.calendar.mealTypeId = queryParam['mealType'];
         this.calendarsDto.mealTypeId = queryParam['mealType'];

         this.getCurrentProducts();
      }
  );
  }

  public createCalendar(productId: number) {
    this.calendar.productId = productId;
    this.nutritionService.createCalendar(this.calendar).subscribe(x => this.router.navigateByUrl('/journal'));
  }

  public getProducts() {
    this.nutritionService.getProducts().subscribe(x => this.productsList = x.items);
  }
  productDto: Product = {
    id: 0,
    name: '',
    proteinsCount: 0,
    fatsCount: 0,
    carbsCount: 0,
    caloriesCount: 0,
    creatorId: JSON.parse(localStorage.getItem('user')!).userId
  }
  public createProduct() {
    this.nutritionService.createProduct(this.productDto).subscribe(x => this.getProducts());
  }

  public new() {
    this.isNew = !this.isNew;
  }

  calendarsDto: FindCalendarsDto = {
    date: new Date(),
    userId: JSON.parse(localStorage.getItem('user')!).userId,
    mealTypeId: 0,
    page: 0,
    size: 0
  }

  public getCurrentProducts() {
    this.nutritionService.getCalendars(this.calendarsDto).subscribe(x => this.currentProductsList = x.items);
  }

  public deleteCalendar(id: number) {
    this.nutritionService.deleteCalendar(id).subscribe(x => this.getCurrentProducts());
  }
}
