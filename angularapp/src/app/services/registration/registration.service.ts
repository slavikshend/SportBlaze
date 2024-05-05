import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private http: HttpClient) { }

  register(email: string, password: string, firstName: string, lastName: string, phoneNumber: string): Observable<any> {
    const body = { email, password, firstName, lastName, phoneNumber };
    return this.http.post<any>('https://localhost:7023/api/auth/register', body);
  }
}

