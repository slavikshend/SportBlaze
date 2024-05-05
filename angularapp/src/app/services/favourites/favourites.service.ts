import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FavouritesService {

  private favouritesUrl = 'https://localhost:7023/api/favourites';

  constructor(private http: HttpClient) { }

  addToFavourites(productId: number): Observable<void> {
    return this.http.post<void>(`${this.favouritesUrl}/${productId}`, null);
  }

  deleteFromFavourites(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.favouritesUrl}/${productId}`);
  }
}
