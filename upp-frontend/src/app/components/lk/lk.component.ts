import { Component } from '@angular/core';
import { User } from '../../models/User';
import { AuthService } from '../../services/auth.service';
import { NutritionService } from '../../services/nutrition.service';
import { SpecialData } from '../../models/SpecialData';

@Component({
  selector: 'app-lk',
  templateUrl: './lk.component.html',
  styleUrl: './lk.component.scss'
})
export class LkComponent {
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

  public isLoading: boolean = false;
  isAdmin: boolean = false;

  constructor(private nutritionService: NutritionService) {
    this.isAdmin = JSON.parse(localStorage.getItem('user')!).roles[0] == "Admin";
    if(!this.isAdmin) {
      this.getSpecialData();
    }
  }
  
  getSpecialData() {
    this.nutritionService.getSpecialInfo(JSON.parse(localStorage.getItem('user')!).userId).subscribe(x => this.specialData = x);
  }

}
