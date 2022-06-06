import { Component, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, FormControl, ValidatorFn } from '@angular/forms';

@Component({
  selector: 'app-base-input',
  templateUrl: './base-input.component.html',
  styleUrls: ['./base-input.component.scss']
})
export abstract class BaseInputComponent implements OnInit, ControlValueAccessor {
  @Input() icon: string = ''
  @Input() labelText: string = ''
  @Input() valids?: ValidatorFn[] = []
  @Input() placeholder: string = ''


  formControl: FormControl = new FormControl()
  constructor() { }
  ngOnInit(): void
  {
    if(this.valids)
      this.formControl.setValidators(this.valids)
  }
  writeValue(val: any): void {
    this.formControl.setValue(val)
  }
  registerOnChange(fn: any): void {
    this.formControl.valueChanges.subscribe(fn)
  }
  registerOnTouched(fn: (_: any) => void): void {};



}
