import {
  HttpErrorResponse,
  HttpResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { pipe, Subject, takeUntil } from 'rxjs';
import { Token } from 'src/app/models/User/Token';
import { UserModel } from 'src/app/models/User/UserModel';
import { authService } from 'src/app/services/auth.service';
import { userService } from 'src/app/services/user.service';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
})
export class UserProfileComponent implements OnInit, OnDestroy {
  constructor(
    private _authService: authService,
    private _userService: userService,
    private router: Router
  ) {}
  faArrowLeft = faArrowLeft;
  public loadUser: boolean = false;
  public showSecuritySettings = false;
  public User!: UserModel;
  public isEdit: boolean = false;
  private $unsubscribe = new Subject<void>();
  ngOnInit(): void {
    this._authService
      .getUser()
      ?.pipe(takeUntil(this.$unsubscribe))
      .subscribe((resp) => {
        if (!(resp instanceof HttpErrorResponse)) {
          console.log(resp);
          this.User = resp;
          this.User.token = {
            refreshToken: this._authService.getRefreshTokenFromLocalStorage()!,
            accessToken: this._authService.getRefreshTokenFromLocalStorage()!,
          };
          this.loadUser = true;
        } else if (resp as HttpErrorResponse) {
          if (
            (resp as HttpErrorResponse).status === HttpStatusCode.Unauthorized
          ) {
            this._authService
              .refreshToken(
                this._authService.getRefreshTokenFromLocalStorage()!,
                this._authService.getAccessTokenFromLocalStorage()!
              )
              .pipe(takeUntil(this.$unsubscribe))
              .subscribe((resp) => {
                if (resp as HttpResponse<Token>) {
                  this.ngOnInit();
                }
              });
          }
        }
      });
  }

  public divideUserName(index: number): string {
    return this.User?.fullName.split(' ')[index] ?? '';
  }

  public getUserAvatar(): string {
    return (
      this.User.avatar ??
      'https://cdn3.iconfinder.com/data/icons/avatars-round-flat/33/avat-01-512.png'
    );
  }

  public goBack() {
    this.router.navigate(['/']);
  }

  public openSecuritySettings() {
    this.showSecuritySettings = !this.showSecuritySettings;
  }

  public edit() {
    this.isEdit = !this.isEdit;
  }

  public LogOut() {
    if (this.User) {
      this._authService
        .logout(this.User)
        .pipe(takeUntil(this.$unsubscribe))
        .subscribe(
          () => {
            this.router.navigate(['/']);
          },
          (err) => console.log(err)
        );
    }
    this._authService.setTokenInLocalStorage('', '');
  }

  public changePassword() {
    let currentPass = (document.getElementById('password')! as HTMLInputElement)
      .value;
    let newPass = (document.getElementById('new-password')! as HTMLInputElement)
      .value;
    this._userService
      .changePass(currentPass, newPass)
      .pipe(takeUntil(this.$unsubscribe))
      .subscribe(
        (resp) => {
          if (resp instanceof HttpResponse) {
            alert('password changed');
          }
        },
        (err) => console.log(err)
      );
  }
  ngOnDestroy(): void {
    this.$unsubscribe.complete();
  }
}
