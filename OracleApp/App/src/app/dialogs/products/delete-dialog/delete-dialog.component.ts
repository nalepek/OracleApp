import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ProductService } from '../../../services/products/product.service';
import { Product } from '../../../models/products/product';


@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.css']
})
export class DeleteDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeleteDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: Product, public productService: ProductService) { }

  ngOnInit() {
  }

  cancel(event): void {
    this.dialogRef.close();
  }

  confirm(event): void {
    this.productService.deleteProduct(this.data.product_id).subscribe(data => {
        this.dialogRef.close(data);
      },
      error => {
        this.dialogRef.close(error);
      });
  
  }
}
