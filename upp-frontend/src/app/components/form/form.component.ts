import { Component } from '@angular/core';
import { SpecialData } from '../../models/SpecialData';
import { NutritionService } from '../../services/nutrition.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss'
})
export class FormComponent {
  specialData: SpecialData = {
    id: JSON.parse(localStorage.getItem('user')!).userId,
    weight: 0,
    workType: 0,
    desiredWeight: 0,
    height: 0,
    sex: "",
    birthDay: new Date(),
    name: '',
    lastname: ''
  }

  constructor(private nutritionService: NutritionService, private router: Router) {}

  saveSpecialData() {
    this.nutritionService.createSpecialInfo(this.specialData).subscribe(_ => this.router.navigateByUrl("/lk"));
  }
}
