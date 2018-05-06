import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../product.service';
import { Product, ProductSearchResult } from '../product';
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

  constructor(private productService: ProductService) { }

  ngOnInit() {
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
        let a = error;
        this.isLoadingResults = false;
        // Catch if the GitHub API has reached its rate limit. Return empty data.
        this.isRateLimitReached = true;
        return observableOf([]);
      })
    ).subscribe(data => {
      this.dataSource.data = data;
    });
  }

  //ngAfterViewInit() {
  //  this.dataSource.paginator = this.paginator;
  //}

  //getProducts(sort: string, order: string, page: number) {
  //  return this.productService.getProducts(sort, order, page).subscribe(response => {
  //    const keys = response.headers.keys();
  //    this.headers = keys.map(key => { '${key}: ${response.headers.get(key)}' });
  //    this.products = response.body;
  //  },
  //    error => {
  //      this.error = error
  //    });
  //}

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

  edit(event, product) {
    //edit -> ok
    //delete -> cancel
    //editable fields
    product.editMode = true;
  }

  delete(event, product) {
    //are you sure
  }

  save(event, product) {

    product.editMode = false;
    let a = this.productService.updateProduct(product).subscribe(data => {
      let x = data;
    });
    let b = 1;
  }

  cancel(event, product) {

    product.editMode = false;
  }
}
