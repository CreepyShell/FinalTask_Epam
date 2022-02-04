import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';
import { postModel } from 'src/app/models/PostModel';
import { UserModel } from 'src/app/models/User/UserModel';
import { userService } from 'src/app/services/user.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
})
export class MainPageComponent implements OnInit, OnDestroy {
  private $unsubscribe = new Subject<void>();
  constructor(private _userService:userService) {}
  ngOnDestroy(): void {
    this.$unsubscribe.unsubscribe();
  }
  public route: string = '';
  public Posts: postModel[] = [];
  public User: UserModel  | undefined = this._userService.getUser();
  ngOnInit(): void {}
  public GoToRegisterPage() {
    this.route = '/register';
  }
  public GoToLoginPage() {
    this.route = '/login';
  }
  public LogOut() {
    this.User = undefined;
    this._userService.setUserModel(this.User);
  }
}
