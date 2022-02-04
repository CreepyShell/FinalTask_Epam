import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';
import { AuthUserModel } from 'src/app/models/User/AuthUserModel';
import { UserModel } from 'src/app/models/User/UserModel';
import { authService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  public authUser:UserModel | undefined = undefined;
 constructor(private _authService: authService) {}
  ngOnDestroy(): void {
    this.$unsubscribe.unsubscribe();
  }
   private $unsubscribe = new Subject<void>();
  // public registerUser(authUser:AuthUserModel){
  //   this._authService.registerUser(authUser);
  //   this.authUser = this._authService.getAuthUser();
  // }
  ngOnInit(): void {}
}
