import { ValidationErrors, ValidatorFn } from "@angular/forms"
export interface FormDialog{
  title: string
  dataList: BaseInput[]
}
export type InputType = 'text' | 'select' | 'password' | 'email' | 'date' | 'toggle'
export interface BaseInput{
  controlName: string
  type: string
  labelText: string
  icon: string
  placeholder: string
  value?: any
  minLength?: number
  maxLength?: number
  disabled: boolean
  valids: ValidatorFn[]
}
