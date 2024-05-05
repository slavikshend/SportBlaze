import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PasswordService {

  constructor(private http: HttpClient) { }

  changePassword(oldPassword: string, newPassword: string): Observable<void> {
    const url = 'https://localhost:7023/api/auth/changePassword';
    const body = { oldPassword, newPassword };

    return this.http.post<void>(url, body).pipe(
      catchError(error => {
        let errorMessage = 'An error occurred while changing the password';
        if (error.error && error.error.message) {
          errorMessage = error.error.message;
        }
        return throwError({ message: errorMessage });
      })
    );
  }
}
