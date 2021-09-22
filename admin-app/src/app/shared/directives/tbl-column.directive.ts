import { ContentChild, Directive, EventEmitter, Input, Output } from '@angular/core';
import { COL_DATA_TYPE, SortFunc, SortOrder } from '../types';
import { TblCellDirective } from './tbl-cell.directive';
import { TblHeaderDirective } from './tbl-header.directive';

@Directive({
  selector: 'app-tbl-column'
})

export class TblColumnDirective {
  @Input() header = '';
  @Input() key = '';
  @Input() renderKey = '';
  @Input() dataType = COL_DATA_TYPE.TEXT;
  @Input() sortable = false;
  @Input() sortOrder: SortOrder = null;
  @Input() sortFn: SortFunc | null = null;
  @Output() sortChange = new EventEmitter<{ key: string, order: SortOrder }>();

  @ContentChild(TblCellDirective, { static: true }) tplCell?: TblCellDirective;
  @ContentChild(TblHeaderDirective, { static: true }) tplHeader?: TblHeaderDirective;

  constructor() { }
}
