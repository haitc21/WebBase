import { UserService, UserModel } from './../../../shared';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  public users: UserModel[];
  public errorMeg: any;;

  constructor(private _userService: UserService) { }

  ngOnInit() {
    this._userService.getAll().subscribe(
      data => {
        this.users = data;
        console.log(this.users);
      },
      error => this.errorMeg = error);
  }

}
