import { ContentChild, Directive, EventEmitter, Input, Output } from '@angular/core';
import { COL_DATA_TYPE, SortFunc, SortOrder } from '../types';
import { CellDirective } from './cell.directive';
import { HeaderDirective } from './header.directive';

@Directive({
  selector: 'app-column'
})
export class ColumnDirective {
  @Input() header = '';
  @Input() key = '';
  @Input() renderKey = '';
  @Input() dataType = COL_DATA_TYPE.TEXT;
  @Input() sortable = false;
  @Input() sortOrder: SortOrder = null;
  @Input() sortFn: SortFunc | null = null;
  @Output() sortChange = new EventEmitter<{ key: string, order: SortOrder }>();

  @ContentChild(CellDirective, { static: true }) tplCell?: CellDirective;
  @ContentChild(HeaderDirective, { static: true }) tplHeader?: HeaderDirective;

  constructor() { }
}
