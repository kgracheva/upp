import { Routes } from '@angular/router';
import { SignComponent } from './components/sign/sign.component';
import { JournalComponent } from './components/journal/journal.component';
import { NutritionComponent } from './components/nutrition/nutrition.component';
import { AuthGuard } from './services/auth.guard';

export const routes: Routes = [
    { path: "", component: SignComponent},
    { 
        path: "journal", 
        component: JournalComponent, 
        canActivate: [AuthGuard],
        data: {
          roles: ['Admin', 'Moderator', 'Operator']
        },
    },
    { path: "nutrition", component: NutritionComponent},
];
