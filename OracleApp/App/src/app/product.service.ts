import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Product, ProductSearchResult } from './product';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError, retry } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http/src/response';
import { ProductSearchCriteria } from './product-search-criteria';

@Injectable()
export class ProductService {

  constructor(private http: HttpClient) { }

  private _headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
  private productsUrl = 'http://localhost:53721/api/product';

  criteria = new ProductSearchCriteria();

  getProducts(sort: string, order: string, page: number): Observable<ProductSearchResult> {
    const url = this.productsUrl + "/search";
    const requestUrl = `${url}?sort=${sort}&order=${order}&page=${page}`;

    return this.http.get<ProductSearchResult>(requestUrl);
  }

  getProduct(): Observable<HttpResponse<Product>> {

    this.criteria.ProductId = 1;
    let url = this.productsUrl + "/get";    
    return this.http.post<Product>(url, this.criteria, { observe: 'response' });
  }

  updateProduct(product) : Observable<HttpResponse<Product>> {
    const url = this.productsUrl + '/update';

    let a = this.http.post<Product>(url, product, { headers: this._headers, observe: 'response' });
    return a;
  }
}
