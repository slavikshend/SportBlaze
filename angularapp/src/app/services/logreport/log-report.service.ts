import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LogLine } from '../../interfaces/log-line';
import { config } from '../../../main';

@Injectable({
  providedIn: 'root'
})
export class LogReportService {
  private baseUrl = `${config.apiUrl}/api/logreport`;

  constructor(private http: HttpClient) { }

  getAllLogLines(): Observable<LogLine[]> {
    return this.http.get<LogLine[]>(`${this.baseUrl}/loglines`);
  }

  clearAllLogs(): Observable<any> {
    return this.http.post(`${this.baseUrl}/clearlogs`, {});
  }
}
