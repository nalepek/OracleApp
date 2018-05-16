import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';
import { ProductService } from '../../../services/products/product.service';
import { Product } from '../../../models/products/product';

@Component({
  selector: 'app-add-dialog',
  templateUrl: './add-dialog.component.html',
  styleUrls: ['./add-dialog.component.css']
})
export class AddDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<AddDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Product, public productService: ProductService) { }

  formControl = new FormControl('', [
    Validators.required
  ]);

  getErrorMessage() {
    return this.formControl.hasError('required')
      ? 'Pole wymagane'
      : '';
  }

  cancel(event): void {
    this.dialogRef.close();
  }

  confirm(event): void {
    this.productService.addProduct(this.data).subscribe(data => {
        this.dialogRef.close(data);
      },
      error => {
        this.dialogRef.close(error);
      });
  }

  ngOnInit() {
  }

}
