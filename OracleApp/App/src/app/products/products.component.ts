import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.getProducts();
  }

  products: Product;
  headers;
  error;

  getProducts() {
    return this.productService.getProducts().subscribe(response => {
      const keys = response.headers.keys();
      this.headers = keys.map(key => { '${key}: ${response.headers.get(key)}' });
      this.products = response.body;
    },
      error => {
        this.error = error
      });
  }
}
