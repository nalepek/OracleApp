import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Product } from './product';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError, retry } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http/src/response';
import { ProductSearchCriteria } from './product-search-criteria';

@Injectable()
export class ProductService {

  constructor(private http: HttpClient) { }

  private productsUrl = 'http://localhost:53721/api/product';

  criteria = new ProductSearchCriteria();

  getProducts(): Observable<HttpResponse<Array<Product>>> {
    let url = this.productsUrl + "/search";    
    return this.http.post<Array<Product>>(url, {}, { observe: 'response'});
  }

  getProduct(): Observable<HttpResponse<Product>> {

    this.criteria.ProductId = 1;
    let url = this.productsUrl + "/get";    
    return this.http.post<Product>(url, this.criteria, { observe: 'response' });
  }
}
