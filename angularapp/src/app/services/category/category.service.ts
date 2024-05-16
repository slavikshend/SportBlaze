import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category, CategoryModel1 } from '../../interfaces/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = 'https://localhost:7023/api/category';

  constructor(private http: HttpClient) { }

  getAllCategories1(): Observable<CategoryModel1[]> {
    const url = `${this.apiUrl}/catalog`;
    return this.http.get<CategoryModel1[]>(url);
  }

  getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.apiUrl);
  }

  addCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.apiUrl, category);
  }

  editCategory(category: Category): Observable<Category> {
    return this.http.put<Category>(this.apiUrl, category);
  }

  deleteCategory(id: number): Observable<any> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<any>(url);
  }


}
