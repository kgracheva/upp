import { Routes } from '@angular/router';
import { SignComponent } from './components/sign/sign.component';
import { JournalComponent } from './components/journal/journal.component';

export const routes: Routes = [
    { path: "", component: SignComponent},
    { path: "journal", component: JournalComponent},
];
