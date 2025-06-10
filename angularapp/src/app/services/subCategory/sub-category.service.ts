import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SubCategory } from '../../interfaces/subCategory';
import { config } from '../../../main';

@Injectable({
  providedIn: 'root'
})
export class SubCategoryService {

  private apiUrl = `${config.apiUrl}/api/subcategory`;

  constructor(private http: HttpClient) { }

  getAllSubCategories(): Observable<SubCategory[]> {
    return this.http.get<SubCategory[]>(this.apiUrl);
  }

  getSubCategoryById(id: number): Observable<SubCategory> {
    return this.http.get<SubCategory>(`${this.apiUrl}/${id}`);
  }

  createSubCategory(subCategory: SubCategory): Observable<SubCategory> {
    return this.http.post<SubCategory>(this.apiUrl, subCategory);
  }

  updateSubCategory(subCategory: SubCategory): Observable<SubCategory> {
    return this.http.put<SubCategory>(this.apiUrl, subCategory);
  }

  deleteSubCategory(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
