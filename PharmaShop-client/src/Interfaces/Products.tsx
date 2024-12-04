import { ReactNode } from "react";

export default interface Products {
  ProductName: string;
  ProductDescription: string;
  Price: number;
  CategoryId: number;
  BrandId: number;
  discountId: number;
  images: ReactNode;
}
