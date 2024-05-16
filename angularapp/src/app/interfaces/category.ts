export interface Category {
  id: number;
  name: string;
  image: string;
}

export interface CategoryModel1 {
  id: number;
  name: string;
  image: string;
  subCategories: SubCategoryModel1[];
}

export interface SubCategoryModel1 {
  id: number;
  name: string;
}
