import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feedback } from '../../interfaces/feedback';
import { config } from '../../../main';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  private feedbacksUrl = `${config.apiUrl}/api/feedbacks`;

  constructor(private http: HttpClient) { }

  getAllProductFeedbacks(productId: number): Observable<Feedback[]> {
    return this.http.get<Feedback[]>(`${this.feedbacksUrl}/${productId}`);
  }

  addFeedback(productId: number, feedback: Feedback): Observable<Feedback> {
    return this.http.post<Feedback>(`${this.feedbacksUrl}/${productId}`, feedback);
  }

  deleteFeedback(feedbackId: number): Observable<void> {
    return this.http.delete<void>(`${this.feedbacksUrl}/${feedbackId}`);
  }
}
