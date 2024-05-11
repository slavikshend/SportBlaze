import { Feedback } from "./feedback";
import { Feature } from "./feature";

export interface ProductDetails {
  id: number;
  name: string;
  description: string;
  imageUrl: string;
  quantity: number;
  price: number;
  discount: number;
  brand: string;
  subCategory: string;
  feedbacks: Feedback[];
  features: Feature[];
}
