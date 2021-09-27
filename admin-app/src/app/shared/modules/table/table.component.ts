import { Component, ContentChildren, EventEmitter, Input, OnChanges, OnInit, Output, QueryList, SimpleChanges } from '@angular/core';
import { Console } from 'console';
import { TblColumnDirective } from '../../directives';
import { ACTION_TYPE, COL_DATA_TYPE, Dictionary } from '../../types';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit, OnChanges {

  totalPages = 10;
  count = 0;
  COL_DATA_TYPE = COL_DATA_TYPE;
  ACTION_TYPE = ACTION_TYPE;

  @Input() loading = false;
  @Input() rows: Dictionary<any>[] = [];
  @Input() hasPagination = false;
  @Input() pageIndex = 1;
  @Input() pageSize = 10;
  @Input() totalRecords = 0;
  @Input() actions: number[] = [];
  @Input() functionName: string = '';


  @Output() pageIndexChange = new EventEmitter<number>();
  @Output() pageSizeChange = new EventEmitter<number>();

  @Output() actionDetails = new EventEmitter<Dictionary<any>>();
  @Output() actionEdit = new EventEmitter<Dictionary<any>>();
  @Output() actionDelete = new EventEmitter<Dictionary<any>>();
  @Output() actionApprove = new EventEmitter<Dictionary<any>>();

  @ContentChildren(TblColumnDirective) columns!: QueryList<TblColumnDirective>;


  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
  }

  ngOnInit(): void {
  }
  details(entity: any) {
    this.actionDetails.emit(entity);
  }
  edit(entity: any) {
    this.actionEdit.emit(entity);
  }
  delete(entity: any) {
    this.actionDelete.emit(entity);
  }
  approve(entity: any) {
    this.actionApprove.emit(entity);
  }
}
