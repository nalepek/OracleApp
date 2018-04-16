import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from './product';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError, retry } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http/src/response';

@Injectable()
export class ProductService {

  constructor(private http: HttpClient) { }

  private productsUrl = 'http://localhost:53721/api/product';

  getProducts(): Observable<HttpResponse<Product>> {
    return this.http.get<Product>(this.productsUrl, { observe: 'response' });
  }
}
