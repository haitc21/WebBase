import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SystemsComponent } from './systems.component';
import { SystemsRoutingModule } from './systems-routing.module';
import { FunctionsComponent } from './functions/functions.component';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { TranslateModule } from '@ngx-translate/core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NzButtonModule } from 'ng-zorro-antd/button';

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
    SystemsRoutingModule,
    TranslateModule,
    NgbDropdownModule,
    NzButtonModule
  ]
})
export class SystemsModule { }
