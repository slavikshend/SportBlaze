import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../../interfaces/product';
import { SimplifiedProduct } from '../../interfaces/simplified-product';
import { ProductDetails } from '../../interfaces/product-details'; // Import the product details interface

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:7023/api/product';

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http.put<Product>(this.apiUrl, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getSpecialOfferProducts(): Observable<SimplifiedProduct[]> {
    return this.http.get<SimplifiedProduct[]>(`${this.apiUrl}/special-offers`);
  }

  getSpecialOfferProductsAnon(): Observable<SimplifiedProduct[]> {
    return this.http.get<SimplifiedProduct[]>(`${this.apiUrl}/special-offers-anon`);
  }

  getProductDetailsById(id: number): Observable<ProductDetails> {
    return this.http.get<ProductDetails>(`${this.apiUrl}/${id}/details`);
  }

  searchProductsByName(name: string): Observable<SimplifiedProduct[]> {
    return this.http.get<SimplifiedProduct[]>(`${this.apiUrl}/search?name=${name}`);
  }

  loadProductsBySubcategory(subcategory: string): Observable<SimplifiedProduct[]> {
    return this.http.get<SimplifiedProduct[]>(`${this.apiUrl}/subcategory?subcategory=${subcategory}`);
  }

  getPersonalizedRecommendations(): Observable<SimplifiedProduct[]> {
    return this.http.get<SimplifiedProduct[]>(`${this.apiUrl}/personalized-recommendations`);
  }
}
