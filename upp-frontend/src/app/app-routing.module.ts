import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignComponent } from './components/sign/sign.component';
import { JournalComponent } from './components/journal/journal.component';
import { AuthGuard } from './services/auth.guard';
import { NutritionComponent } from './components/nutrition/nutrition.component';

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
  { path: "nutrition", component: NutritionComponent},];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
