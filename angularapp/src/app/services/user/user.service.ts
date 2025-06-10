import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../interfaces/user';
import { config } from '../../../main';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUserProfile(): Observable<User> {
    return this.http.get<User>(`${config.apiUrl}/api/auth/userinfo`);
  }
    
  updateUserProfile(user: User): Observable<any> {
    return this.http.put(`${config.apiUrl}/api/auth/edituser`, user);
  }
}
