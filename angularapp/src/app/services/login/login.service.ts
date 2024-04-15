import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>('https://localhost:7023/api/auth/login', { email, password }).pipe( 
      map(response => {
        if (response.successful) {
          return response.token;
        } else {
          throw new Error(response.error);
        }
      }),
      catchError(error => {
        throw new Error('Login failed');
      })
    );
  }
}
