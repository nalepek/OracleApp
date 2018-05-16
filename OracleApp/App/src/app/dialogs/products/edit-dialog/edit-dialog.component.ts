import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';
import { ProductService } from '../../../services/products/product.service';
import { Product } from '../../../models/products/product';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.css']
})
export class EditDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<EditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Product, public productService: ProductService) { }

  formControl = new FormControl('', [
    Validators.required
  ]);

  getErrorMessage() {
    return this.formControl.hasError('required')
      ? 'Pole wymagane'
      : '';
      //: this.formControl.hasError('x')
      //? 'Not a valid'
      //: '';
  }

  cancel(event): void {
    this.dialogRef.close();
  }

  save(event): void {
    this.productService.updateProduct(this.data).subscribe(data => {
      this.dialogRef.close(data);
    },
      error => {
        this.dialogRef.close(error);
      });
  }

  ngOnInit() {
  }

}
