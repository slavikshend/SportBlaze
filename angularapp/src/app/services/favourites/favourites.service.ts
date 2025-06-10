import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { config } from '../../../main';
@Injectable({
  providedIn: 'root'
})
export class FavouritesService {

  private favouritesUrl = `${config.apiUrl}/api/favourites`;

  constructor(private http: HttpClient) { }

  addToFavourites(productId: number): Observable<void> {
    return this.http.post<void>(`${this.favouritesUrl}/${productId}`, null);
  }

  deleteFromFavourites(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.favouritesUrl}/${productId}`);
  }

  getFavoriteProducts(): Observable<any[]> {
    return this.http.get<any[]>(this.favouritesUrl);
  }
}
