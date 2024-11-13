import { Component, inject } from '@angular/core';
import { Calendar } from '../../models/Calendar';
import { NutritionService } from '../../services/nutrition.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Product } from '../../models/Product';
import { Observable } from 'rxjs';
import { Calories } from '../../models/Calories';

@Component({
  selector: 'app-nutrition',
  templateUrl: './nutrition.component.html',
  styleUrl: './nutrition.component.scss'
})
export class NutritionComponent {
  public productsList: Product[] = []; 

  calendar: Calendar = {
    id: 0,
    userId: JSON.parse(localStorage.getItem('user')!).userId,
    productId: 0,
    productCount: 100,
    mealTypeId: 0
  }

  
  constructor(private nutritionService: NutritionService, private route:ActivatedRoute, private router: Router) {
    this.getProducts();
    
    route.queryParams.subscribe(queryParam => {
         this.calendar.mealTypeId = queryParam['mealType'];
      }
  );
  }

  public createCalendar(productId: number) {
    this.calendar.productId = productId;
    console.log(this.route.snapshot.params);
    this.nutritionService.createCalendar(this.calendar).subscribe(x => this.router.navigateByUrl('/journal'));
  }

  public getProducts() {
    this.nutritionService.getProducts().subscribe(x => this.productsList = x.items); 
  }
}
