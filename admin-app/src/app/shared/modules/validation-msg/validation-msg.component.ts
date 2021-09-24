import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-validation-msg',
  templateUrl: './validation-msg.component.html',
  styleUrls: ['./validation-msg.component.scss']
})
export class ValidationMsgComponent implements OnInit {
  @Input() entityForm: FormGroup;
  @Input() fieldName: string;
  @Input() validationMessages: any;
  constructor() { }

  ngOnInit() {
  }

}
