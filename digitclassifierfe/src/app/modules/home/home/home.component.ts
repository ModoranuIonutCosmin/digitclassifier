import {Component, OnInit} from '@angular/core';

import {User} from '../../../models/user';
import {AuthenticationService} from "../../../services/auth.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  currentUser: User;
  users: User[] | null = [];

  constructor(
    private authenticationService: AuthenticationService,
  ) {
    this.currentUser = this.authenticationService.currentUserValue;
  }

  ngOnInit() {

  }
}
