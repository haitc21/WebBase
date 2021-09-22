import { Directive, TemplateRef } from '@angular/core';

@Directive({
  selector: '[appTblHeader]'
})
export class TblHeaderDirective {
  constructor(
    public template: TemplateRef<any>
  ) { }
}
