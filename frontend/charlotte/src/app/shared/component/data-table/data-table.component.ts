import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BehaviorSubject } from 'rxjs';
import { SwalService } from '../../service/swal/swal.service';
import { UserAuth } from '../../service/userInfo/userInfo.interface';
import { UserInfoService } from '../../service/userInfo/userInfo.service';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss']
})
export class DataTableComponent implements OnInit {
  @Input() columns: {key:string, value: any}[] = [] // 表頭名稱
  @Input() dataList: any[] = [] // 資料來源
  @Input() functionShow: boolean = true // 要不要開起多選刪除跟搜尋功能
  @Input() deteailBtnShow: boolean = false // 明細按鈕
  dataSource = new MatTableDataSource<any>() // Table的資料來源設定
  displayedColumns: string[] = []
  userAuth: UserAuth
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator // 分頁
  @ViewChild(MatSort) sort!: MatSort; // 排序
  @Output() createAction: EventEmitter<any> = new EventEmitter<any>()
  @Output() deleteAction: EventEmitter<any> = new EventEmitter<any>()
  @Output() modifyAction: EventEmitter<any> = new EventEmitter<any>()
  @Output() detailAction: EventEmitter<any> = new EventEmitter<any>()
  @Output() checkBox: EventEmitter<any> = new EventEmitter<any>()
  @Output() multipleDeleteAction: EventEmitter<any> = new EventEmitter<any>()
  @Output() refreshAction: EventEmitter<any> = new EventEmitter<any>()
  selection = new SelectionModel<any>(true, []);

  constructor(
    private swalService: SwalService<null>,
    private userInfoService: UserInfoService){
    this.userAuth = this.userInfoService.UserAuth$.value
  }

  ngOnInit(): void
  {
    this.displayedColumns = this.createDisplayedColumns()
    this.bindingTable(this.dataList)
    this.initPaginatorLabel()
  }

  ngAfterViewInit()
  {
    this.dataSource.sort = this.sort// set sort
    this.dataSource.paginator = this.paginator// set pagination
  }

  /** 搜尋事件 */
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  /** 初始化設定Table */
  bindingTable(dataList: any): void
  {
    this.dataSource = new MatTableDataSource<any>(dataList);
  }

  /**
   * 創建表頭
   * @param columns 表頭資訊
   * @returns 表頭Key陣列
   */
  createDisplayedColumns(): string[]{
    if(this.functionShow)this.columns = [ {key: 'Checkbox',value: ''}, ...this.columns, {key: 'Action', value: ''}]
    else if(this.functionShow)this.columns = [...this.columns, {key: 'Action', value: ''}]
    let disColumns: string[] = []
    for(let item of this.columns)
      disColumns.push(item.key)
    return disColumns
  }
  /** 新增按鈕點擊 */
  createClick(): void{
    this.createAction.emit()
  }
  /**
   * 修改按鈕點擊
   * @param row 整條Row資料
   */
  modifyClick(row: any): void {
    this.modifyAction.emit(row)
  }
  /**
   * 刪除按鈕點擊
   * @param row 整條Row資料
   */
  deleteClick(row: any){
    this.deleteAction.emit(row)
  }
  detailClick(row: any){
    this.detailAction.emit(row)
  }

  /** 點擊多選刪除按鈕 */
  multipleDeleteClick(){
    if(this.selection.selected.length === 0)
    {
      this.swalService.alert
      (
        {
          text: '未選取任何東西',
          icon: 'warning',
          confirmButtonText: '確認'
        }
      )
    }else
      this.multipleDeleteAction.emit(this.selection.selected)
  }

  /** 點擊刷新按鈕 */
  refreshClick(){
    this.refreshAction.emit()
  }

  /**
   * 點擊checkBox
   * @param row 整條Row資料
   */
  checkBoxChange(row: any){
    this.selection.toggle(row)
  }

  /** 設定分頁標籤文字 */
  initPaginatorLabel()
  {
    this.paginator._intl.itemsPerPageLabel= '每頁數量'
    this.paginator._intl.firstPageLabel = '第一頁'
    this.paginator._intl.nextPageLabel = '下一頁'
    this.paginator._intl.previousPageLabel = '上一頁'
    this.paginator._intl.lastPageLabel = '最後一頁'
  }
}
