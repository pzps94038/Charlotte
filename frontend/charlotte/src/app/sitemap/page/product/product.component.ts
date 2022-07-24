import { Component, OnDestroy, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { filter, finalize, forkJoin, map, mergeMap, Observable, of, Subject, switchMap, takeUntil, tap } from 'rxjs';
import { ApiService } from 'src/app/shared/api/api.service';
import { FactoryService } from 'src/app/shared/api/factory/factory.service';
import { GetProductResult as Product } from 'src/app/shared/api/product/product.interface';
import { ProductService } from 'src/app/shared/api/product/product.service';
import { ProductTypeService } from 'src/app/shared/api/productType/product-type.service';
import { BaseDataTable, DataTableInfo, InitDataTableFunction } from 'src/app/shared/component/data-table/data.table.model';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { ProductDialogComponent } from 'src/app/shared/dialog/product-dialog/product-dialog.component';
import { SharedService } from 'src/app/shared/service/shared.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent extends BaseDataTable<Product> implements OnInit,  InitDataTableFunction<Product>, OnDestroy {

  destroy$: Subject<any> = new Subject<any>();
  constructor(
    private productService: ProductService,
    private sharedService: SharedService,
    private apiService: ApiService,
    private dialog: MatDialog,
    private swalService: SwalService,
    private productTypeService: ProductTypeService,
    private factoryService: FactoryService
  ) {
    super();
  }

  ngOnInit(): void {
    this.columns = this.createColumns()
    this.getProducts()
  }

  ngOnDestroy(): void {
    this.destroy$.next(null)
    this.destroy$.complete()
  }

  createColumns(): { key: string; value: string | number; }[] {
    const columns =
    [
      {
        key: 'productId',
        value: '產品ID'
      },
      {
        key: 'productName',
        value: '產品名稱'
      },
      {
        key: 'type',
        value: '產品類型'
      },
      {
        key: 'inventory',
        value: '庫存量'
      },
      {
        key: 'costPrice',
        value: '成本價',
      },
      {
        key: 'sellPrice',
        value: '售價',
      },
      {
        key: 'factoryName',
        value: '廠商名稱'
      }
    ]
    return columns
  }

  getProducts(info?: DataTableInfo){
    this.loading$.next(true)
    this.productService.getProducts(info).pipe(
      takeUntil(this.destroy$),
      finalize(()=> this.loading$.next(false))
    ).subscribe((res)=>{
      this.tableDataList = res.data.tableDataList
      this.tableTotalCount = res.data.tableTotalCount
    })
  }

  refresh(): void {
    this.getProducts()
  }
  create(): void {
    const typeOptions$ = this.createProductTypeOptions()
    const factoryOptions$ = this.createFactoryOptions()
    forkJoin([typeOptions$, factoryOptions$]).pipe(
      switchMap(([typeOptions, factoryOptions])=>{
        const dialog = this.dialog.open(ProductDialogComponent,{
          data: {
            title: '新建產品',
            dataList:[
              {
                controlName: 'productName',
                labelText: '產品名稱',
                type: 'text',
                valids: [Validators.required]
              },
              {
                controlName: 'productTypeId',
                labelText: '產品類型',
                type: 'select',
                options: typeOptions,
                icon: '',
                value: '',
                valids: [Validators.required],
              },
              {
                controlName: 'inventory',
                labelText: '庫存量',
                type: 'number',
                valids: [Validators.required]
              },
              {
                controlName: 'costPrice',
                labelText: '成本價',
                type: 'number',
                valids: [Validators.required]
              },
              {
                controlName: 'sellPrice',
                labelText: '售價',
                type: 'number',
                valids: [Validators.required]
              },
              {
                controlName: 'factoryId',
                labelText: '廠商',
                type: 'select',
                options: factoryOptions,
                value: '',
                valids: [Validators.required]
              },
              {
                controlName: 'productImgPath',
                labelText: '圖片路徑',
                type: '',
                valids: [Validators.required]
              }
            ]
          }
        })
        return dialog.afterClosed()
      }),
      filter(data=> !this.sharedService.isNullorEmpty(data)),
      switchMap(data=> {
        const files = data.files as FileList
        const formData = new FormData()
        Array.from(files).forEach(a=> { formData.append('files', a) })
        return forkJoin([this.productService.fileUpload(formData), of(data)])
      }),
      filter(([pathData])=> this.apiService.judgeSuccess(pathData)),
      switchMap(([pathData, data])=>{
        return this.productService.createProduct({
          productName: data.productName,
          productTypeId: data.productTypeId,
          inventory: data.inventory,
          costPrice: data.costPrice,
          sellPrice: data.sellPrice,
          factoryId: data.factoryId,
          productImgPath: pathData.data
        })
      }),
      filter((data)=> this.apiService.judgeSuccess(data)),
    ).subscribe(()=> this.refresh())
  }

  createProductTypeOptions(): Observable<{ text: string; value: number; }[]>{
    return this.productTypeService.getProductTypes({}).pipe(
      map(res=> res.data.tableDataList.map(a=> {
        return { text: a.type, value: a.productTypeId }
      })),
    )
  }

  createFactoryOptions(): Observable<{ text: string; value: number; }[]>{
    return this.factoryService.getFactorys({}).pipe(
      map(res=> res.data.tableDataList.map(a=> {
        return { text: a.factoryName, value: a.factoryId }
      }))
    )
  }

  modify(row: Product): void {
    const typeOptions$ = this.createProductTypeOptions()
    const factoryOptions$ = this.createFactoryOptions()
    forkJoin([typeOptions$, factoryOptions$]).pipe(
      switchMap(([typeOptions, factoryOptions])=>{
        const dialog = this.dialog.open(ProductDialogComponent,{
          data: {
            title: '新建產品',
            dataList:[
              {
                controlName: 'productName',
                labelText: '產品名稱',
                type: 'text',
                valids: [Validators.required],
                value: row.productName
              },
              {
                controlName: 'productTypeId',
                labelText: '產品類型',
                type: 'select',
                options: typeOptions,
                value: typeOptions.find(a=> a.text === row.type)?.value,
                valids: [Validators.required],
              },
              {
                controlName: 'inventory',
                labelText: '庫存量',
                type: 'number',
                valids: [Validators.required],
                value: row.inventory
              },
              {
                controlName: 'costPrice',
                labelText: '成本價',
                type: 'number',
                valids: [Validators.required],
                value: row.costPrice
              },
              {
                controlName: 'sellPrice',
                labelText: '售價',
                type: 'number',
                valids: [Validators.required],
                value: row.sellPrice
              },
              {
                controlName: 'factoryId',
                labelText: '廠商',
                type: 'select',
                options: factoryOptions,
                value: factoryOptions.find(a=> a.text === row.factoryName)?.value,
                valids: [Validators.required]
              },
              {
                controlName: 'productImgPath',
                labelText: '圖片路徑',
                value: row.productImgPath
              }
            ]
          }
        })
        return dialog.afterClosed()
      }),
      filter(data=> !this.sharedService.isNullorEmpty(data)),
      switchMap(data=> {
        const files = data.files as FileList
        if(files.length > 0){
          const formData = new FormData()
          Array.from(files).forEach(a=> { formData.append('files', a) })
          return forkJoin([of(data), this.productService.fileUpload(formData)])
        }
        else
          return forkJoin([of(data)])
      }),
      filter(([data, pathData])=>{
        if(pathData)
          return this.apiService.judgeSuccess(pathData)
        else
          return true
      }),
      switchMap(([data, pathData])=>{
        return this.productService.modifyProduct(row.productId,{
          productName: data.productName,
          productTypeId: data.productTypeId,
          inventory: data.inventory,
          costPrice: data.costPrice,
          sellPrice: data.sellPrice,
          factoryId: data.factoryId,
          productImgPath: pathData?.data ?? data.url
        })
      }),
      filter((data)=> this.apiService.judgeSuccess(data, true)),
    ).subscribe(()=> this.refresh())
  }
  delete(row: Product): void {
    this.swalService.delete().pipe(
      filter(data=> data.isConfirmed),
      switchMap(()=> this.productService.deleteProduct(row.productId)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=> this.refresh())
  }
  multipleDelete(rows: Product[]): void {
    this.swalService.multipleDelete(rows).pipe(
      map(()=> rows.map(a=> a.productId)),
      switchMap((deleteArr)=> this.productService.batchDeleteProduct(deleteArr)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=> this.refresh())
  }
  filterTable(info: DataTableInfo): void {
    this.getProducts(info)
  }
}
