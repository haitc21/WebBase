import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableComponent } from './table.component';
import { CellDirective, ColumnDirective, HeaderDirective } from '../../directives';
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
    ColumnDirective,
    CellDirective,
    HeaderDirective
  ],
  exports: [
    TableComponent,
    ColumnDirective,
    CellDirective,
    HeaderDirective
  ]
})
export class TableModule { }
