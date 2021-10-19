import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FunctionsComponent } from './functions/functions.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { RolesComponent } from './roles/roles.component';
import { UsersComponent } from './users/users.component';
import { AuthGuard } from '../../shared/guard';

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
      customBreadcrumb: 'Chức năng',
      functionCode: 'SYSTEM_FUNCTION'
    },
    canActivate: [AuthGuard]
  },
  {
    path: 'users',
    component: UsersComponent,
    data: {
      customBreadcrumb: 'Người dùng',
      functionCode: 'SYSTEM_USER'
    },
    canActivate: [AuthGuard]
  },
  {
    path: 'roles',
    component: RolesComponent,
    data: {
      customBreadcrumb: 'Nhóm quyền',
      functionCode: 'SYSTEM_ROLE'
    },
    canActivate: [AuthGuard]
  },
  {
    path: 'permissions',
    component: PermissionsComponent,
    data: {
      customBreadcrumb: 'Quyền hạn',
      functionCode: 'SYSTEM_PERMISSION'
    },
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SystemsRoutingModule { }
