import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUserProfile(): Observable<User> {
    return this.http.get<User>(`https://localhost:7023/api/auth/userinfo`);
  }
    
  updateUserProfile(user: User): Observable<any> {
    return this.http.put(`https://localhost:7023/api/auth/edituser`, user);
  }
}
