import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../product';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  displayedColumns = ['created', 'state', 'number', 'title'];

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
    //this.getProducts(this.sort.active, this.sort.direction, this.paginator.pageIndex);

    //this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
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
