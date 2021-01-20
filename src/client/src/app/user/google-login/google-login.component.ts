import { Component } from '@angular/core';
import {GoogleLoginProvider, SocialAuthService} from 'angularx-social-login';
import {AuthService} from '../auth.service';

@Component({
  selector: 'app-google-login',
  templateUrl: './google-login.component.html',
  styleUrls: ['./google-login.component.scss']
})
export class GoogleLoginComponent {
  isLogging = false;

  constructor(private as: AuthService, private sas: SocialAuthService) { }

  signInWithGoogle(): void {
    this.isLogging = true;
    this.sas.signIn(GoogleLoginProvider.PROVIDER_ID).then(googleUser => {
      this.as.googleLogin(googleUser)
        .subscribe(data => {
          this.isLogging = false;
        });
    }, err => {
      this.isLogging = false;
    });
  }


}
