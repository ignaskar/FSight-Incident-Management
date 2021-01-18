import { Component, OnInit } from '@angular/core';
import {AuthService} from './user/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'client';

  constructor(private as: AuthService) {
  }

  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.as.loadCurrentUser(token).subscribe(() => {
        console.log('Loaded user.');
      }, err => {
        console.log(err);
      });
    }
  }
}
