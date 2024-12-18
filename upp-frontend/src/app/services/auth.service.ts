
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { User } from '../models/User';
import { HttpBackend, HttpClient } from '@angular/common/http';
import { UserLoginDto } from '../models/UserLoginDto';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  // private readonly STORAGE_KEY = 'user';
  private _user: AuthUserDto | null = null;
  ref: string = "https://localhost:7171/api/Auth";
  private httpClient: HttpClient;

  constructor( handler: HttpBackend, private router: Router) {
    this.httpClient = new HttpClient(handler);
  }


  public register(model: User) {
    return this.httpClient.post(this.ref, model);
  }

  public registerPsy(model: User) {
    return this.httpClient.post(this.ref + "/psy", model);
  }

  public login(model: UserLoginDto, returnUrl: string = '/') {
    return this.httpClient.post(this.ref + "/login", model);
  }



  // public loginVk(vkId: number, returnUrl: string = '/') {
  //   return this.authClient.authVk(new VkLoginDto({ vkId: vkId })).pipe(
  //     tap(async (response) => {
  //       if (response.isSuccess) {
  //         localStorage.setItem(
  //           this.STORAGE_KEY,
  //           JSON.stringify(response.result)
  //         );
  //         await this.router.navigateByUrl('');
  //       }
  //     })
  //   );
  // }

  // public generateRecoveryCode(
  //   model: ResetPasswordDto
  // ): Observable<IAuthResultOfResetPasswordDto> {
  //   return this.authClient.generatePasswordResetCode(model).pipe(
  //     tap(async (response) => {
  //       return response;
  //     })
  //   );
  // }

  // resetPassword(
  //   model: ResetPasswordDto,
  //   returnUrl: string = '/'
  // ): Observable<IAuthResultOfAuthResponseDto> {
  //   return this.authClient.resetPassword(model).pipe(
  //     tap(async (response) => {
  //       if (response.isSuccess) {
  //         localStorage.setItem(
  //           this.STORAGE_KEY,
  //           JSON.stringify(response.result)
  //         );
  //         await this.router.navigateByUrl(returnUrl);
  //       }
  //     })
  //   );
  // }

  // public checkVerificationCode(
  //   model: ResetPasswordDto
  // ): Observable<IAuthResultOfResetPasswordDto> {
  //   return this.authClient.verifyCode(model).pipe(
  //     tap(async (response) => {
  //       return response;
  //     })
  //   );
  // }

  // public impersonate(userId: number, returnUrl: string = '/') {
  //   return this.authClient.impersonate(new ImpersonateDto({userId})).pipe(
  //     tap(async (response) => {
  //       if (response.isSuccess) {
  //         localStorage.setItem(
  //           this.STORAGE_KEY,
  //           JSON.stringify(response.result)
  //         );
  //         await this.router.navigateByUrl(returnUrl);
  //         location.reload();
  //       }
  //     })
  //   );
  // }

  public getToken(): string | null {
    return JSON.parse(localStorage.getItem('user')!).key;
  }

  public getRoles(): string[] {
    return JSON.parse(localStorage.getItem('user')!).roles;
  }

  public user(): AuthUserDto {
    if (this._user) {
      return this._user;
    }

    this._user = JSON.parse(localStorage.getItem('user') ?? 'null');

    return this._user!;
  }

  // public logout(returnUrl: string = '/login'): void {
  //   this._user = null;
  //   localStorage.removeItem(this.STORAGE_KEY);
  //   this.router.navigateByUrl(returnUrl);
  // }


}

export interface AuthUserDto {
  key: string;
  userId: number;
  roles: string[];
}
