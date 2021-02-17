import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {UserManagementComponent} from './user-management/user-management.component';

const routes: Routes = [
  { path: 'console', component: UserManagementComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
