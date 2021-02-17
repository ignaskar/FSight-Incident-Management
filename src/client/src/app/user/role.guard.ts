import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import decode from 'jwt-decode';
import {IToken} from '../shared/models/token';
import {map} from 'rxjs/operators';
import {SnackService} from '../services/snack.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private as: AuthService, private snack: SnackService) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {

      const expectedRole = route.data.expectedRole;

      const token = localStorage.getItem('token');
      if (token === null) {
        this.snack.authError();
        return false;
      }

      const tokenPayload = decode<IToken>(token);

      if (tokenPayload.role !== expectedRole) {
        this.snack.roleError();
        return false;
      }

      return true;
  }

}
