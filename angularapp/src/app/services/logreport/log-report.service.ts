import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LogLine } from '../../interfaces/log-line';

@Injectable({
  providedIn: 'root'
})
export class LogReportService {
  private baseUrl = 'https://localhost:7023/api/logreport';

  constructor(private http: HttpClient) { }

  getAllLogLines(): Observable<LogLine[]> {
    return this.http.get<LogLine[]>(`${this.baseUrl}/loglines`);
  }

  clearAllLogs(): Observable<any> {
    return this.http.post(`${this.baseUrl}/clearlogs`, {});
  }
}
