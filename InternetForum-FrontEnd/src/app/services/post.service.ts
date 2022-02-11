import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, of } from 'rxjs';
import { postModel } from '../models/PostModel';
import { authService } from './auth.service';
import { httpService } from './http.service';

@Injectable()
export class postService {
  constructor(
    private _httpService: httpService,
    private _authService: authService
  ) {}
  public getPostsById(id:string) {
    this._httpService.getRequest<postModel>(
      `/api/posts/${id}`,
      this._httpService.setHttpHeader([], []),
      null
    );
  }
  public getAllPosts() {
    return this._httpService
      .getRequest<postModel[]>('/api/posts', this._httpService.setHttpHeader([], []), null)
      .pipe(catchError((err) => of(err as HttpErrorResponse)));
  }
}
