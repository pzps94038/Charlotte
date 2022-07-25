import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseInputComponent } from '../base-input/base-input.component';

@Component({
  selector: 'app-dynamic-input',
  templateUrl: './dynamic-input.component.html',
  styleUrls: ['./dynamic-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR, // set NG_VALUE_ACCESSOR
      useExisting: forwardRef(() => DynamicInputComponent),
      multi: true,
    },
  ],
})
export class DynamicInputComponent
  extends BaseInputComponent
  implements OnInit
{
  @Input() type!: string;
  @Input() minLength?: number = 0;
  @Input() maxLength?: number = 0;
  @Input() min?: number = 0;
  @Input() max?: number = 0;
}
