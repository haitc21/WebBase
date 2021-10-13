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
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker'
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzTransferModule } from 'ng-zorro-antd/transfer';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzSpinModule } from 'ng-zorro-antd/spin';


import { SystemsComponent } from './systems.component';
import { FunctionsComponent } from './functions/functions.component';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { NotificationService, PermissionModule, TableModule, ValidationMsgModule } from '../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// ngx-bootstrap/modal
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';

// primeng
import { PanelModule } from 'primeng/panel';
import { TableModule as primeTblModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { CalendarModule } from 'primeng/calendar';
import { CheckboxModule } from 'primeng/checkbox';
import { KeyFilterModule } from 'primeng/keyfilter';
import { TreeTableModule } from 'primeng/treetable';
import { DropdownModule } from 'primeng/dropdown';



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
    NzIconModule,
    NzDatePickerModule,
    NzBreadCrumbModule,
    NzTransferModule,
    NzTableModule,
    NzSpinModule,
    PermissionModule,

    ModalModule,
    PanelModule,
    primeTblModule,
    PaginatorModule,
    BlockUIModule,
    InputTextModule,
    ProgressSpinnerModule,
    CalendarModule,
    CheckboxModule,
    KeyFilterModule,
    TreeTableModule,
    DropdownModule
  ],
  providers: [
    NotificationService,
    DatePipe,
    BsModalService
  ]
})
export class SystemsModule { }
