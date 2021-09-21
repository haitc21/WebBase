import { Component, ContentChildren, EventEmitter, Input, OnChanges, OnInit, Output, QueryList, SimpleChanges } from '@angular/core';
import { ColumnDirective } from '../../directives';
import { COL_DATA_TYPE, Dictionary } from '../../types';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit, OnChanges {

  totalPages = 10;
  COL_DATA_TYPE = COL_DATA_TYPE;

  @Input() loading = false;
  @Input() rows: Dictionary<any>[] = [];
  @Input() hasPagination = false;
  @Input() pageIndex = 1;
  @Input() pageSize = 10;
  @Input() totalRecords = 0;

  @Output() pageIndexChange = new EventEmitter<number>();
  @Output() pageSizeChange = new EventEmitter<number>();

  @ContentChildren(ColumnDirective) columns!: QueryList<ColumnDirective>;


  constructor() { }

  ngOnChanges(changes: SimpleChanges): void {
    this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
  }

  ngOnInit(): void {
  }
  view(entity: any) {
    console.log(entity);
  }
  delete(entity: any) {
    console.log(entity);
  }
  edit(entity: any) {
    console.log(entity);
  }
}
