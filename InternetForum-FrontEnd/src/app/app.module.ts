import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { MainPageComponent } from './components/main-page/main-page.component';
import { RegisterComponent } from './components/register/register.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import {  MatToolbarModule} from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { httpService } from './services/http.service';
import { authService } from './services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { userService } from './services/user.service';
import { authorizedGuard, nonAuthorizedGuard } from './guards/authGuard';
import { LoggedInErrorComponent } from './components/logged-in-error/logged-in-error.component';
import { PostComponent } from './components/post/post.component';
import { CommentComponent } from './components/comment/comment.component';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    RegisterComponent,
    LoginComponent,
    UserProfileComponent,
    LoggedInErrorComponent,
    PostComponent,
    CommentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    HttpClientModule,
    MatButtonToggleModule,
    FontAwesomeModule
  ],
  providers: [httpService, authService, userService, authorizedGuard, nonAuthorizedGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
