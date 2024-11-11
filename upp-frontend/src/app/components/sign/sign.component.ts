import { Component } from '@angular/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { CommonModule } from '@angular/common';      
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-sign',
  standalone: true,
  imports: [MatDatepickerModule, CommonModule, MatInputModule, MatFormFieldModule, FormsModule],
  templateUrl: './sign.component.html',
  styleUrl: './sign.component.scss'
})
export class SignComponent {
  isSignUp: boolean = false;
}
