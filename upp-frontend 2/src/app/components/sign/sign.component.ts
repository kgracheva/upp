import { Component } from '@angular/core';

import { User } from '../../models/User';
import { AuthService } from '../../services/auth.service';
@Component({
  selector: 'app-sign',
  templateUrl: './sign.component.html',
  styleUrl: './sign.component.scss'
})
export class SignComponent {
  isSignUp: boolean = false;
  userModel: User = {
    id: 0,
    email: '',
    password: '',
    name: '',
    lastname: ''
  };

  constructor(private authService: AuthService) {}

  public signUp() { 
    this.authService.register(this.userModel).subscribe(x => console.log(x));
  }
}
