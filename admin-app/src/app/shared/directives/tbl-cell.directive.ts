import { Directive, TemplateRef } from '@angular/core';

@Directive({
  selector: '[appTblCell]'
})
export class TblCellDirective {
  constructor(
    public template: TemplateRef<any>
  ) { }

}
