import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
  MatProgressSpinnerModule,
  MatIconModule,
  MatIconRegistry,
  MatInputModule,
  MatDialogModule,
  MatButtonModule,
  MatMenuModule
} from '@angular/material';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { ProductsComponent } from './products/products.component';
import { ProductService } from './services/products/product.service';
import { EditDialogComponent } from './dialogs/products/edit-dialog/edit-dialog.component';
import { DeleteDialogComponent } from './dialogs/products/delete-dialog/delete-dialog.component';
import { AddDialogComponent } from './dialogs/products/add-dialog/add-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    EditDialogComponent,
    DeleteDialogComponent,
    AddDialogComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatMenuModule,
    MatIconModule,
    FormsModule,
    MatInputModule,
    MatDialogModule
  ],
  entryComponents: [
    EditDialogComponent,
    DeleteDialogComponent,
    AddDialogComponent
  ],
  providers: [ProductService, MatIconRegistry],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(public matIconRegistry: MatIconRegistry) {
    matIconRegistry.registerFontClassAlias('fontawesome', 'fa');
  }
  
}
