import { Component, EventEmitter, forwardRef, OnInit, Output } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { BaseInputComponent } from '../base-input/base-input.component';

@Component({
  selector: 'app-dynamic-checkbox',
  templateUrl: './dynamic-checkbox.component.html',
  styleUrls: ['./dynamic-checkbox.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR, // set NG_VALUE_ACCESSOR
      useExisting: forwardRef(() => DynamicCheckboxComponent),
      multi: true,
    },
  ],
})
export class DynamicCheckboxComponent extends BaseInputComponent implements OnInit {
  @Output() change: EventEmitter<MatCheckboxChange> = new EventEmitter<MatCheckboxChange>();
  changeEvent(e:MatCheckboxChange){
    this.change.emit(e)
  }
}
