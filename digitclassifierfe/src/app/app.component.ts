import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {AuthenticationService} from "./services/auth.service";
import {User} from "./models/user";
import {environment} from "../environments/environment";
import {LoadingService} from "./services/loading.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'digitclassifierfe';
  currentUser?: User;
  loading$ = this.loader.loading$;

  constructor(
    public loader: LoadingService,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }
}
