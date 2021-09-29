import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FunctionsComponent } from './functions/functions.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { RolesComponent } from './roles/roles.component';
import { SystemsComponent } from './systems.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'users',
    pathMatch: 'prefix'
  },
  {
    path: 'functions',
    component: FunctionsComponent,
    data: {
      customBreadcrumb: 'Chức năng'
    }
  },
  {
    path: 'users',
    component: UsersComponent,
    data: {
      customBreadcrumb: 'Người dùng'
    }
  },
  {
    path: 'roles',
    component: RolesComponent,
    data: {
      customBreadcrumb: 'Nhóm quyền'
    }
  },
  {
    path: 'permissions',
    component: PermissionsComponent,
    data: {
      customBreadcrumb: 'Quyền hạn'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SystemsRoutingModule { }
