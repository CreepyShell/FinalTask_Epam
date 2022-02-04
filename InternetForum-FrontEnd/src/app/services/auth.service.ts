import {
  HttpErrorResponse,
  HttpResponse,
  HttpStatusCode,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AuthUserModel } from '../models/User/AuthUserModel';
import { UserModel } from '../models/User/UserModel';
import { httpService } from './http.service';
import { userService } from './user.service';

@Injectable()
export class authService {
  constructor(
    private _httpService: httpService,
    private _userServie: userService
  ) {}
  public registerUser(user: AuthUserModel) {
    return this._httpService.postRequest<UserModel>(
      '/api/auth/register',
      this._httpService.setHttpHeader([], []),
      user
    );
  }

  public loginUser(user: AuthUserModel) {
    return this._httpService
      .postRequest<UserModel>(
        '/api/auth/login',
        this._httpService.setHttpHeader([], []),
        user
      )
      .pipe(
        map((resp) => {
          this._userServie.setUserModel(resp.body!);
          return resp;
        }),
        catchError((err) => {
          return of(err as HttpErrorResponse);
        })
      );
  }
}
