import { Component, forwardRef, OnInit } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseInputComponent } from '../base-input/base-input.component';

@Component({
  selector: 'app-dynamic-silde-toggle',
  templateUrl: './dynamic-silde-toggle.component.html',
  styleUrls: ['./dynamic-silde-toggle.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR, // set NG_VALUE_ACCESSOR
      useExisting: forwardRef(() => DynamicSildeToggleComponent),
      multi: true,
    },
  ],
})
export class DynamicSildeToggleComponent extends BaseInputComponent implements OnInit {

}
