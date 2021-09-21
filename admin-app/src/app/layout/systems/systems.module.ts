import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { SystemsRoutingModule } from './systems-routing.module';
import { TranslateModule } from '@ngx-translate/core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

// ant design
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzLayoutModule } from 'ng-zorro-antd/layout';


import { SystemsComponent } from './systems.component';
import { FunctionsComponent } from './functions/functions.component';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { PermissionsComponent } from './permissions/permissions.component';
import {  NotificationService, TableModule } from '../../shared';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    SystemsComponent,
    FunctionsComponent,
    UsersComponent,
    RolesComponent,
    PermissionsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    SystemsRoutingModule,
    TranslateModule,
    NgbDropdownModule,
    NzButtonModule,
    NzModalModule,
    NzPaginationModule,
    NzLayoutModule,
    TableModule
  ],
  providers: [
    NotificationService,
    DatePipe
  ]
})
export class SystemsModule { }
