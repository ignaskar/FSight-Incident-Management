import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent {
  currentUser$ = this.as.currentUser$;

  constructor(public as: AuthService) { }

  logout(): void {
    this.as.logout();
  }
}
