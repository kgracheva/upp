import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignComponent } from './components/sign/sign.component';
import { JournalComponent } from './components/journal/journal.component';
import { AuthGuard } from './services/auth.guard';
import { NutritionComponent } from './components/nutrition/nutrition.component';
import { LkComponent } from './components/lk/lk.component';
import { FormComponent } from './components/form/form.component';
import { RequestsComponent } from './components/requests/requests.component';
import { RequestComponent } from './components/request/request.component';
import { ArticlesComponent } from './components/articles/articles.component';
import { ArticleComponent } from './components/article/article.component';
import { TrainingComponent } from './components/training/training.component';
import { TrainingsComponent } from './components/trainings/trainings.component';
import { RecipesComponent } from './components/recipes/recipes.component';
import { RecipeComponent } from './components/recipe/recipe.component';
import { ChatComponent } from './components/chat/chat.component';
import { UsersComponent } from './components/users/users.component';
import { PsyFormComponent } from './components/psy-form/psy-form.component';

const routes: Routes = [
  {
    path: "", component: SignComponent
  },
  {
      path: "journal",
      component: JournalComponent,
      canActivate: [AuthGuard],
      data: {
        roles: ['Client']
      },
  },
  { path: "nutrition", component: NutritionComponent},
  {
    path: "lk",
    component: LkComponent,
    canActivate: [AuthGuard],
    data: {
      roles: ['Client', 'Admin']
    },
  },
  {
    path: "form",
    component: FormComponent,
    canActivate: [AuthGuard],
    data: {
      roles: ['Client']
    },
  },
  {
    path: "requests",
    component: RequestsComponent,
    canActivate: [AuthGuard],
    data: {
      roles: ['Client', "Admin", 'Psychologist']
    },
  },
  {
    path: "request",
    component: RequestComponent,
    canActivate: [AuthGuard],
    data: {
      roles: ['Client', "Admin", 'Psychologist']
    },
  },
  {
    path: "articles",
    component: ArticlesComponent,
    data: {
      roles: ['Client', "Admin"]
    },
  },
  {
    path: "article/:id",
    component: ArticleComponent,
    data: {
      roles: ['Client', "Admin"]
    },
  },

  {
    path: "trainings",
    component: TrainingsComponent,
    data: {
      roles: ['Client', "Admin"]
    },
  },
  {
    path: "training/:id",
    component: TrainingComponent,
    data: {
      roles: ['Client']
    },
  },

  {
    path: "recipes",
    component: RecipesComponent,
    data: {
      roles: ['Client']
    },
  },
  {
    path: "recipe/:id",
    component: RecipeComponent,
    canActivate: [AuthGuard],
    data: {
      roles: ['Client']
    },
  },
  {
    path: "chat",
    component: ChatComponent,
    canActivate: [AuthGuard],
    data: {
      roles: ['Client', 'Admin']
    },
  },
  {
    path: "users",
    component: UsersComponent,
    canActivate: [AuthGuard],
    data: {
      roles: ['Admin']
    },
  },
  {
    path: "psy",
    component: PsyFormComponent,
    canActivate: [AuthGuard],
    data: {
      roles: ['Psychologist']
    },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
