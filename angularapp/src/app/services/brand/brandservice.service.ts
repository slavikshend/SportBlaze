import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Brand } from '../../interfaces/brand';
import { config } from '../../../main';
@Injectable({
  providedIn: 'root'
})
export class BrandService {
  private apiUrl = `${config.apiUrl}/api/brand`;

  constructor(private http: HttpClient) { }

  getAllBrands(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  addBrand(brand: Brand): Observable<any> {
    return this.http.post<any>(this.apiUrl, brand);
  }

  editBrand(brand: Brand): Observable<any> {
    return this.http.put<any>(this.apiUrl, brand);
  }

  deleteBrand(id: number): Observable<any> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<any>(url);
  }
}

