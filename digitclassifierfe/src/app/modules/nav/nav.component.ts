import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {AlertService} from "../../services/alert.service";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  model: any = {}

  constructor(public accountService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login')
  }
}
