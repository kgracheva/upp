import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { SignComponent } from './components/sign/sign.component';
import { RouterModule, RouterOutlet } from '@angular/router';

import {MatDatepickerModule} from '@angular/material/datepicker';
import { CommonModule } from '@angular/common';      
import { routes } from './app.routes';
import { NutritionComponent } from './components/nutrition/nutrition.component';
import { JournalComponent } from './components/journal/journal.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SignComponent,
    NutritionComponent,
    JournalComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    RouterOutlet,
    CommonModule,
    MatDatepickerModule,
    RouterModule
  ],
  exports: [ 
    HeaderComponent,
    SignComponent,
    NutritionComponent,
    JournalComponent
    ],
  providers: [],
})
export class AppModule { }