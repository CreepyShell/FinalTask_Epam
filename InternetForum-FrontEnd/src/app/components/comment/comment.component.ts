import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { commentModel } from 'src/app/models/CommentModel';
import { UserModel } from 'src/app/models/User/UserModel';
import { authService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
})
export class CommentComponent implements OnInit, OnDestroy {
  @Input() User!: UserModel;
  @Input() Comment!: commentModel;
  @Input() answersToComment: commentModel[] = [];
  constructor(private _authService:authService) {}

  private $unsubscribe = new Subject<void>();
  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.$unsubscribe.next();
    this.$unsubscribe.complete();
  }
}
