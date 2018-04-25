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

  getProducts(sort: string, order: string, page: number): Observable<HttpResponse<Array<Product>>> {
    const url = this.productsUrl + "/search";
    const requestUrl = `${url}?sort=${sort}&order=${order}&page=${page}`;

    //const href = 'https://api.github.com/search/issues';
    //const requestUrl =
      //`${href}?q=repo:angular/material2&sort=${sort}&order=${order}&page=${page + 1}`;

    //return this.http.get<GithubApi>(requestUrl);

    return this.http.get<Array<Product>>(requestUrl, { observe: 'response'});
  }

  getProduct(): Observable<HttpResponse<Product>> {

    this.criteria.ProductId = 1;
    let url = this.productsUrl + "/get";    
    return this.http.post<Product>(url, this.criteria, { observe: 'response' });
  }
}
