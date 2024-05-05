export interface Product {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
  quantity: number;
  price: number;
  discount: number;
  brandId: number;
  brandName: string;
  brandImageUrl: string;
  subCategoryId: number;
  subCategoryName: string;
  subCategoryImageUrl: string;
  features: Feature[];
}

export interface Feature {
  name: string;
  value: string;
}
