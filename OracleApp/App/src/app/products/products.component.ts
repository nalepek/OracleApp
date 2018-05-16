import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../services/products/product.service';
import { Product, ProductSearchResult } from '../models/products/product';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { merge } from 'rxjs/observable/merge';
import { map } from 'rxjs/operators/map';
import { startWith } from 'rxjs/operators/startWith';
import { switchMap } from 'rxjs/operators/switchMap';
import { catchError } from 'rxjs/operators/catchError';
import { of as observableOf } from 'rxjs/observable/of';
import { EditDialogComponent } from '../dialogs/products/edit-dialog/edit-dialog.component';
import { AddDialogComponent } from '../dialogs/products/add-dialog/add-dialog.component';
import { DeleteDialogComponent } from '../dialogs/products/delete-dialog/delete-dialog.component';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  displayedColumns = ['name', 'description', 'price', 'actions'];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  dataSource = new MatTableDataSource();
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  products: ProductSearchResult;
  product: Product;
  headers;
  error;

  constructor(private productService: ProductService,
    public dialog: MatDialog) { }

  ngOnInit() {
    this.loadData();
  }

  private refreshTable() {

  }

  public loadData() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page).pipe(
      startWith({}),
      switchMap(() => {
        this.isLoadingResults = true;
        return this.productService!.getProducts(
          this.sort.active, this.sort.direction, this.paginator.pageIndex);
      }),
      map(data => {
        // Flip flag to show that loading has finished.
        this.isLoadingResults = false;
        this.isRateLimitReached = false;
        this.resultsLength = data.count;

        return data.items;
      }),
      catchError(error => {
        this.isLoadingResults = false;
        // Catch if the GitHub API has reached its rate limit. Return empty data.
        this.isRateLimitReached = true;
        return observableOf([]);
      })
    ).subscribe(data => {
      this.dataSource.data = data;
    });
  }

  getProduct() {
    return this.productService.getProduct().subscribe(response => {
      const keys = response.headers.keys();
      this.headers = keys.map(key => { '${key}: ${response.headers.get(key)}' });
      this.product = response.body;
    },
      error => {
        this.error = error;
      });
  }

  edit(event, product: Product) {
    const dialogRef = this.dialog.open(EditDialogComponent,
      {
        data: new Product(product.name, product.description, product.price, product.product_id, product.type_id),
        maxHeight: '90%'
      });

    dialogRef.afterClosed().subscribe(result => {
      this.loadData();
    },
      error => {
        this.error = error;
      });
  }

  delete(event, product: Product) {
    const dialogRef = this.dialog.open(DeleteDialogComponent,
      {
        data: product
      });

    dialogRef.afterClosed().subscribe(result => {

      this.loadData();
    });
  }

  add(event) {
    const dialogRef = this.dialog.open(AddDialogComponent,
      {
        data: new Product()
      });

    dialogRef.afterClosed().subscribe(result => {
      this.loadData();
    });
  }

}
