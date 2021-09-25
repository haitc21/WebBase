import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { SystemsRoutingModule } from './systems-routing.module';
import { TranslateModule } from '@ngx-translate/core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

// ant design
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';


import { SystemsComponent } from './systems.component';
import { FunctionsComponent } from './functions/functions.component';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { PermissionsComponent } from './permissions/permissions.component';
import {  NotificationService, TableModule, ValidationMsgModule } from '../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



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
    TableModule,
    NzButtonModule,
    NzModalModule,
    NzLayoutModule,
    NzFormModule,
    NzInputModule,
    ReactiveFormsModule,
    ValidationMsgModule,
    NzIconModule
  ],
  providers: [
    NotificationService,
    DatePipe
  ]
})
export class SystemsModule { }
