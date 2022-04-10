import { GetRoleAuthResult } from "../../api/role/role.interface"

export type CheckBoxType = 'string' | 'checkBox'
export interface CheckBoxDialog{
  title: string
  rowData: GetRoleAuthResult[]
  columns: CheckBoxColumn[]
}
export interface CheckBoxColumn{
  key: string
  value: string
  type: string
}

