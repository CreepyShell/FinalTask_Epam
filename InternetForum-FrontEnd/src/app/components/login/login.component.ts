import {
  HttpErrorResponse,
  HttpResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faEyeSlash, faEye } from '@fortawesome/free-solid-svg-icons';
import { catchError, map, Subject, takeUntil } from 'rxjs';
import { UserModel } from 'src/app/models/User/UserModel';
import { authService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit, OnDestroy {
  faNotShowPass = faEyeSlash;
  faShowPass = faEye;
  public isUsernameChosen: boolean = true;
  public selectedValue: string = 'username';
  public showpassword: boolean = false;
  public errorMessage: string | undefined = undefined;
  private $unsubscribe = new Subject<void>();

  constructor(private route: Router, private _authService: authService) {}

  public onValChange(val: string) {
    this.selectedValue == val;
    if (val == 'email') {
      this.isUsernameChosen = false;
      return;
    }
    this.isUsernameChosen = true;
  }
  ngOnInit(): void {}

  public login() {
    let username: string | null = (
      document.getElementById('username') as HTMLInputElement
    )?.value;
    let email: string | null = (
      document.getElementById('mail') as HTMLInputElement
    )?.value;
    let password: string = (document.getElementById('pass') as HTMLInputElement)
      .value;
    this._authService
      .loginUser({
        password: password,
        username: username,
        email: email,
      })
      .pipe(takeUntil(this.$unsubscribe))
      .subscribe(
        (resp) => {
          console.log(resp.status);
          if (resp.status === HttpStatusCode.Ok) {
            this.route.navigate(['/']);
          } else {
            if (resp.status == HttpStatusCode.NotFound) {
              this.errorMessage = this.isUsernameChosen
                ? 'Did not find user with this username'
                : 'Did not find user with this email';
            }
            if (resp.status == HttpStatusCode.BadRequest) {
              this.errorMessage = (resp as HttpErrorResponse).error;
            }
            if (resp.status == HttpStatusCode.NotAcceptable) {
              this.errorMessage = (resp as HttpErrorResponse).error;
            }
            setTimeout(() => {
              this.errorMessage = undefined;
            }, 10000);
          }
        },
        (err) => console.log(err)
      );
  }

  public showPassword() {
    this.showpassword = !this.showpassword;
    let el = document.getElementById('pass') as HTMLInputElement;
    if (el!.type === 'password') {
      el!.type = 'text';
    } else {
      el!.type = 'password';
    }
  }
  ngOnDestroy(): void {
    this.$unsubscribe.unsubscribe();
  }
}
