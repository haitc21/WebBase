import { UserService, UserModel } from './../../../shared';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit, OnDestroy {

  subscription: Subscription;
  public users: UserModel[];
  public errorMeg: any;;

  constructor(private _userService: UserService) { }

  ngOnInit() {
    this.subscription = this._userService.getAll().subscribe(
      data => {
        this.users = data;
        console.log(this.users);
      },
      error => this.errorMeg = error);
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
