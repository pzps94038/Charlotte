import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ProductDialogComponent } from '../shared/dialog/product-dialog/product-dialog.component';
@Component({
  selector: 'app-sitemap',
  templateUrl: './sitemap.component.html',
  styleUrls: ['./sitemap.component.scss'],

})
export class SitemapComponent implements OnInit {

  constructor(private dialog: MatDialog){}

  ngOnInit(): void {
    this.dialog.open(ProductDialogComponent)
  }
}
