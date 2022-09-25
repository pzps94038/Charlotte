import { ValidationErrors, ValidatorFn } from '@angular/forms';
export interface FormDialog {
  title: string;
  dataList: BaseInput[];
  editor?: {
    controlName: string;
    valids: ValidatorFn[];
    placeholder: string;
    value: any;
    disabled?: boolean;
  };
}
export type InputType =
  | 'text'
  | 'select'
  | 'password'
  | 'email'
  | 'date'
  | 'toggle';
export interface BaseInput {
  controlName: string;
  type: string;
  labelText: string;
  icon: string;
  placeholder: string;
  value?: any;
  minLength?: number;
  maxLength?: number;
  min?: number;
  max?: number;
  disabled: boolean;
  options?: { text: string; value: any }[];
  valids: ValidatorFn[];
}
