import {Component, OnInit} from '@angular/core';
import {AuthenticationService} from "../../../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-header-menu-content',
  templateUrl: './header-menu-content.component.html',
  styleUrls: ['./header-menu-content.component.scss']
})
export class HeaderMenuContentComponent implements OnInit {

  constructor(private authService: AuthenticationService,private router: Router) {
  }


  ngOnInit(): void {
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);

  }
}
