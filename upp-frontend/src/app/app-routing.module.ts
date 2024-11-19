import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignComponent } from './components/sign/sign.component';
import { JournalComponent } from './components/journal/journal.component';
import { AuthGuard } from './services/auth.guard';
import { NutritionComponent } from './components/nutrition/nutrition.component';
import { LkComponent } from './components/lk/lk.component';
import { FormComponent } from './components/form/form.component';

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
      roles: ['Client']
    },
  },
  {
    path: "form",
    component: FormComponent, 
    canActivate: [AuthGuard],
    data: {
      roles: ['Client']
    },
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
