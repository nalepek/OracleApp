export class ProductApi {
  items: Product[];
  total_count: number;
}

export class Product {
  product_id: number;
  type_id: number;
  name: string;
  description: string;
  price: number;
}
