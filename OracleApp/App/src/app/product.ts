export interface ProductSearchResult {
  items: Product[];
  total_count: number;
}

export interface Product {
  product_id: number;
  type_id: number;
  name: string;
  description: string;
  price: number;
}
