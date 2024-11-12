import { Component } from '@angular/core';
import { HeaderComponent } from "./components/header/header.component";
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'upp-frontend';
}
