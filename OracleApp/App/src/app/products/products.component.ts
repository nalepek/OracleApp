import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../product';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { merge } from 'rxjs/observable/merge';
import { map } from 'rxjs/operators/map';
import { startWith } from 'rxjs/operators/startWith';
import { switchMap } from 'rxjs/operators/switchMap';
import { catchError } from 'rxjs/operators/catchError';
import { of as observableOf } from 'rxjs/observable/of';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  displayedColumns = ['product_id', 'name', 'description', 'price'];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  dataSource: MatTableDataSource<Product>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  products: Array<Product>;
  product: Product;
  headers;
  error;

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.getProducts('name', 'asc', 0);

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    //merge(this.sort.sortChange, this.paginator.page)
    //  .pipe(
    //  startWith({}),
    //  switchMap(() => {
    //    this.isLoadingResults = true;
    //    return this.getProducts(
    //      this.sort.active, this.sort.direction, this.paginator.pageIndex);
    //  }),
    //  map(data => {
    //    // Flip flag to show that loading has finished.
    //    this.isLoadingResults = false;
    //    this.isRateLimitReached = false;
    //    this.resultsLength = data.total_count;

    //    return data.items;
    //  }),
    //  catchError(() => {
    //    this.isLoadingResults = false;
    //    // Catch if the GitHub API has reached its rate limit. Return empty data.
    //    this.isRateLimitReached = true;
    //    return observableOf([]);
    //  })
    //  ).subscribe(data => this.dataSource.data = data);
  }

  getProducts(sort: string, order: string, page: number) {
    return this.productService.getProducts(sort, order, page).subscribe(response => {
      const keys = response.headers.keys();
      this.headers = keys.map(key => { '${key}: ${response.headers.get(key)}' });
      this.products = response.body;
    },
      error => {
        this.error = error
      });
  }

  getProduct() {
    return this.productService.getProduct().subscribe(response => {
      const keys = response.headers.keys();
      this.headers = keys.map(key => { '${key}: ${response.headers.get(key)}' });
      this.product = response.body;
    },
      error => {
        this.error = error
      });
  }
}
