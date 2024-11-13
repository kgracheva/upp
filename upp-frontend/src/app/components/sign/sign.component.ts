import { Component } from '@angular/core';

import { User } from '../../models/User';
import { AuthService } from '../../services/auth.service';
import { UserLoginDto } from '../../models/UserLoginDto';
import { Router } from '@angular/router';
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

  userLogin: UserLoginDto = {
    email: '',
    password: ''
  }

  constructor(private authService: AuthService, private router: Router) {}

  public signUp() { 
    this.authService.register(this.userModel).subscribe(x => {
      this.router.navigateByUrl('/login');
    });
  }

  public login() {
    this.authService.login(this.userLogin).subscribe(x => {
      localStorage.setItem('user', JSON.stringify(x));
      this.router.navigateByUrl('/journal');
    });
  }
}
