import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { config } from '../../../main';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private http: HttpClient) { }

  register(email: string, password: string, firstName: string, lastName: string, phoneNumber: string): Observable<any> {
    const body = { email, password, firstName, lastName, phoneNumber };
    return this.http.post<any>(`${config.apiUrl}/api/auth/register`, body);
  }
}

