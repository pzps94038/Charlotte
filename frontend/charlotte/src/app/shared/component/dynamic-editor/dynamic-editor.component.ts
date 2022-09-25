import { map, Observable, tap, filter } from 'rxjs';
import { Component, forwardRef, OnInit, Optional } from '@angular/core';
import { ControlContainer, NG_VALUE_ACCESSOR } from '@angular/forms';
import { AngularEditorConfig, UploadResponse } from '@kolkov/angular-editor';
import { BaseInputComponent } from '../base-input/base-input.component';
import { HttpClient, HttpEvent } from '@angular/common/http';
import { ProductService } from '../../api/product/product.service';
import { ApiUrl } from '../../api/api.url';
import { ResultModel } from '../../api/api.interface';
import { ApiService } from '../../api/api.service';

@Component({
  selector: 'app-dynamic-editor',
  templateUrl: './dynamic-editor.component.html',
  styleUrls: ['./dynamic-editor.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR, // set NG_VALUE_ACCESSOR
      useExisting: forwardRef(() => DynamicEditorComponent),
      multi: true,
    },
  ],
})
export class DynamicEditorComponent extends BaseInputComponent {
  constructor(
    @Optional() public override controlContainer: ControlContainer,
    private http: HttpClient,
    private apiService: ApiService
  ) {
    super(controlContainer);
  }
  config: AngularEditorConfig = {
    editable: true,
    toolbarHiddenButtons: [['insertVideo']],
    sanitize: false,
    upload: (file: File) => {
      const formData = new FormData();
      formData.append('files', file);
      return this.http
        .post(ApiUrl.productEditorFileUpload, formData, {
          observe: 'events',
        })
        .pipe(
          map((res) => res as any),
          filter((res) => res.body?.code),
          filter((res) => this.apiService.judgeSuccess(res.body)),
          map((res) => {
            const path = res.body.data;
            return {
              ...res,
              body: {
                imageUrl: `${ApiUrl.baseUrl}/${path}`,
              },
            };
          })
        );
    },
  };
}
