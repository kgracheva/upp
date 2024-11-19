import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignComponent } from './components/sign/sign.component';
import { HeaderComponent } from './components/header/header.component';
import { JournalComponent } from './components/journal/journal.component';
import { NutritionComponent } from './components/nutrition/nutrition.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { LkComponent } from './components/lk/lk.component';
import { FormComponent } from './components/form/form.component';
import { RequestsComponent } from './components/requests/requests.component';
import { MatTableModule } from '@angular/material/table';
import { RequestBlockComponent } from './components/request-block/request-block.component';
import { RequestComponent } from './components/request/request.component';
import { ArticlesComponent } from './components/articles/articles.component';
import { ArticleCardComponent } from './components/article-card/article-card.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent, 
    SignComponent,
    JournalComponent,
    NutritionComponent,
    LkComponent,
    FormComponent,
    RequestsComponent,
    RequestBlockComponent,
    RequestComponent,
    ArticlesComponent,
    ArticleCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatTableModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
