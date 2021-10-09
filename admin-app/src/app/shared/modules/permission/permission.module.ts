import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PermissionDirective } from '../../directives';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [PermissionDirective],
  exports: [PermissionDirective]
})
export class PermissionModule { }
