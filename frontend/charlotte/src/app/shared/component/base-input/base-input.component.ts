import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Optional,
  Output,
} from '@angular/core';
import {
  ControlContainer,
  ControlValueAccessor,
  FormControl,
  ValidatorFn,
} from '@angular/forms';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-base-input',
  templateUrl: './base-input.component.html',
  styleUrls: ['./base-input.component.scss'],
})
export abstract class BaseInputComponent
  implements OnInit, ControlValueAccessor, OnDestroy
{
  @Input() icon: string = '';
  @Input() labelText: string = '';
  @Input() valids?: ValidatorFn[] = [];
  @Input() placeholder: string = '';
  @Input() formControlName!: string;
  @Output() changeEmitter: EventEmitter<any> = new EventEmitter();
  get touched() {
    return this.controlContainer.control?.get(this.formControlName)?.touched;
  }
  private onChange?: (val: any) => void;
  private onTouched?: (val: any) => void;
  private destory$ = new Subject<any>();
  formControl: FormControl = new FormControl();
  constructor(@Optional() public controlContainer: ControlContainer) {
    this.formControl.valueChanges.subscribe((val) => {
      if (this.onChange && this.onTouched) {
        this.changeEmitter.emit(val);
        this.onChange(val);
        this.onTouched(true);
      }
    });
  }
  ngOnDestroy(): void {
    this.destory$.next(null);
    this.destory$.complete();
  }

  ngOnInit(): void {
    if (this.valids) {
      this.formControl.setValidators(this.valids);
    }
  }
  writeValue(val: any): void {
    this.formControl.setValue(val);
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
}
