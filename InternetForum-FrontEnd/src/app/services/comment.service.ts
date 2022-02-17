import { Injectable } from '@angular/core';
import { commentModel } from '../models/CommentModel';
import { authService } from './auth.service';
import { httpService } from './http.service';

@Injectable()
export class commentService {
  constructor(
    private _httpService: httpService,
    private _authService: authService
  ) {}
  public getCommentsByPostId(postId: string, count: number) {
    let accessToken = this._authService.getAccessTokenFromLocalStorage();
    return this._httpService.getRequest<commentModel[]>(
      `/api/comments/${postId}/${count}`,
      this._httpService.setHttpHeader(
        ['Authorization'],
        [`Bearer ${accessToken}`]
      ),
      null
    );
  }
}
