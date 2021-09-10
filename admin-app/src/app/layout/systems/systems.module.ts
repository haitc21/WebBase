import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SystemsComponent } from './systems.component';
import { SystemsRoutingModule } from './systems-routing.module';

@NgModule({
  imports: [
    CommonModule,
    SystemsRoutingModule
  ],
  declarations: [SystemsComponent]
})
export class SystemsModule { }
