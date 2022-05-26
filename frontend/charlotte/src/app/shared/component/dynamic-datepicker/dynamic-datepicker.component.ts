import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR, ValidatorFn } from '@angular/forms';
import { BaseInputComponent } from '../base-input/base-input.component';

@Component({
  selector: 'app-dynamic-datepicker',
  templateUrl: './dynamic-datepicker.component.html',
  styleUrls: ['./dynamic-datepicker.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR, // set NG_VALUE_ACCESSOR
      useExisting: forwardRef(() => DynamicDatepickerComponent),
      multi: true,
    },
  ],
})
export class DynamicDatepickerComponent extends BaseInputComponent implements OnInit {

}
