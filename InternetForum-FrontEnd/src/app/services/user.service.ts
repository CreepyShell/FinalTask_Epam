import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { UserModel } from '../models/User/UserModel';

@Injectable()
export class userService {
  private userModel: UserModel | undefined = undefined;

  public getUser(): UserModel | undefined{
    return this.userModel;
  }
  public setUserModel(user: UserModel | undefined) {
    this.userModel = user;
  }
}
