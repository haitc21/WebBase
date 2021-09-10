import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FunctionsComponent } from './functions/functions.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { RolesComponent } from './roles/roles.component';
import { SystemsComponent } from './systems.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
  { path: '', redirectTo: 'users', pathMatch: 'prefix' },
  {
    path: 'functions',
    component: FunctionsComponent
  },
  {
    path: 'users',
    component: UsersComponent
  },
  {
    path: 'roles',
    component: RolesComponent
  },
  {
    path: 'permissions',
    component: PermissionsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SystemsRoutingModule { }
