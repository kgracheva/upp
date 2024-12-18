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
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LkComponent } from './components/lk/lk.component';
import { FormComponent } from './components/form/form.component';
import { RequestsComponent } from './components/requests/requests.component';
import { MatTableModule } from '@angular/material/table';
import { RequestBlockComponent } from './components/request-block/request-block.component';
import { RequestComponent } from './components/request/request.component';
import { ArticlesComponent } from './components/articles/articles.component';
import { ArticleCardComponent } from './components/article-card/article-card.component';
import { ArticleComponent } from './components/article/article.component';
import { TrainingsComponent } from './components/trainings/trainings.component';
import { TrainingComponent } from './components/training/training.component';
import { RecipesComponent } from './components/recipes/recipes.component';
import { RecipeComponent } from './components/recipe/recipe.component';
import { SafePipe } from 'safe-pipe';
import { ChatComponent } from './components/chat/chat.component';
import { JwtInterceptor } from './services/jwt.interceptor';
import { ChatWindowComponent } from './components/chat-window/chat-window.component';
import { UsersComponent } from './components/users/users.component';
import { PsyFormComponent } from './components/psy-form/psy-form.component';
import { MatDialogModule } from '@angular/material/dialog';
import { CreateChatDialogComponent } from './components/dialogs/create-chat-dialog/create-chat-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatButtonModule } from '@angular/material/button';


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
    ArticleCardComponent,
    ArticleComponent,
    TrainingsComponent,
    TrainingComponent,
    RecipesComponent,
    RecipeComponent,
    ChatComponent,
    ChatWindowComponent,
    UsersComponent,
    PsyFormComponent
    ChatWindowComponent,
    CreateChatDialogComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatTableModule,
    MatDialogModule,
    MatButtonModule,
    SafePipe
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}, provideAnimationsAsync() ],
  bootstrap: [AppComponent]
})
export class AppModule { }
