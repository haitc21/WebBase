import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { SystemsRoutingModule } from './systems-routing.module';
import { TranslateModule } from '@ngx-translate/core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

// ant design
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';

import { SystemsComponent } from './systems.component';
import { FunctionsComponent } from './functions/functions.component';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { RoleDetailComponent } from './roles/role-detail/role-detail.component';
import { NotificationService } from '../../shared';


@NgModule({
  declarations: [
    SystemsComponent,
    FunctionsComponent,
    UsersComponent,
    RolesComponent,
    RoleDetailComponent,
    PermissionsComponent
  ],
  imports: [
    CommonModule,
    SystemsRoutingModule,
    TranslateModule,
    NgbDropdownModule,
    NzButtonModule,
    NzModalModule,
    NzTableModule,
    NzPaginationModule
  ],
  providers: [
    NotificationService,
    DatePipe
  ]
})
export class SystemsModule { }
