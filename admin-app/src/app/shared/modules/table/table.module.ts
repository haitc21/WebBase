import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableComponent } from './table.component';
import { TblCellDirective, TblColumnDirective, TblHeaderDirective } from '../../directives';
import { NzTableModule } from 'ng-zorro-antd/table';
import { FormsModule } from '@angular/forms';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzSpinModule } from 'ng-zorro-antd/spin';

@NgModule({
  imports: [
    CommonModule,
    NzTableModule,
    FormsModule,
    NzPaginationModule,
    NzSpinModule
  ],
  declarations: [
    TableComponent,
    TblColumnDirective,
    TblCellDirective,
    TblHeaderDirective
  ],
  exports: [
    TableComponent,
    TblColumnDirective,
    TblCellDirective,
    TblHeaderDirective
  ]
})
export class TableModule { }
