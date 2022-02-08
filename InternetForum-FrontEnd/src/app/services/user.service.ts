import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Token } from '../models/User/Token';
import { UserModel } from '../models/User/UserModel';
import { authService } from './auth.service';
import { httpService } from './http.service';

@Injectable()
export class userService {
  constructor(
    private _httpService: httpService,
    private _authService: authService
  ) {}

  public changePass(currentPass: string, newPassword: string) {
    let headers: HttpHeaders = this._httpService.setHttpHeader(
      ['currentPassword', 'newPassword', 'Authorization'],
      [
        currentPass,
        newPassword,
        `Bearer ${this._authService.getAccessTokenFromLocalStorage()}`,
      ]
    );
    return this._httpService
      .putRequest<Token>('/api/auth/changepass', headers, null)
      .pipe(
        map((resp) => {
          this._authService.setTokenInLocalStorage(
            resp.body!.accessToken,
            resp.body!.refreshToken
          );
          return resp;
        })
      );
  }
}
