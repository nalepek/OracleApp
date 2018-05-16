export class ProductSearchResult {
  items: Product[];
  count: number;
}

export class Product {
  product_id: number;
  type_id: number;
  name: string;
  description: string;
  price: number;

  constructor(name?: string, description?: string, price?: number, productId?: number, typeId?: number) {
    this.description = description;
    this.name = name;
    this.price = price;
    this.product_id = productId;
    this.type_id = typeId;
  }
}
