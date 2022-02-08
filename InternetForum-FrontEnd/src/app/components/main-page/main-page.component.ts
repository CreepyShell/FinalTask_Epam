import {
  HttpErrorResponse,
  HttpResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { pipe, takeUntil } from 'rxjs';
import { Subject } from 'rxjs/internal/Subject';
import { postModel } from 'src/app/models/PostModel';
import { Token } from 'src/app/models/User/Token';
import { UserModel } from 'src/app/models/User/UserModel';
import { authService } from 'src/app/services/auth.service';
import { userService } from 'src/app/services/user.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
})
export class MainPageComponent implements OnInit, OnDestroy {
  ngOnInit(): void {
    this.getUserFromToken();
  }
  constructor(
    private _authService: authService
  ) {}
  public route: string = '';
  public Posts: postModel[] = [];
  public User: UserModel | undefined;
  public isLoadData:boolean = false;
  private $unsubscribe = new Subject<void>();
  public GoToRegisterPage() {
    this.route = '/register';
  }
  public GoToLoginPage() {
    this.route = '/login';
  }

  private getUserFromToken() {
    let accessToken: string | null = localStorage.getItem('accessToken');
    let refreshToken: string | null = localStorage.getItem('refreshToken');
    if (accessToken && !this.User && refreshToken) {
      this._authService
        .getUserFromToken()!
        .pipe(takeUntil(this.$unsubscribe))
        .subscribe(
          (resp) => {
            if (!(resp instanceof HttpErrorResponse)) {
              this.User = resp!;
              this.User.token = {
                accessToken: accessToken!,
                refreshToken: refreshToken!,
              };
              this.isLoadData = true;
              return;
            } else if (
              (resp as HttpErrorResponse).status === HttpStatusCode.Unauthorized
            ) {
              this._authService
                .refreshToken(refreshToken!, accessToken!)
                .pipe(takeUntil(this.$unsubscribe))
                .subscribe(
                  (resp) => {
                    if (resp instanceof HttpResponse) {
                      this.getUserFromToken();
                    }
                  },
                  (err) => console.log(err)
                );
            }
          },
          (err) => console.log(err)
        );
    }
  }

  ngOnDestroy(): void {
    this.$unsubscribe.unsubscribe();
  }
}
