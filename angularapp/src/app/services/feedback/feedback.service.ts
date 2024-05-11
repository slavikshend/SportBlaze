import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feedback } from '../../interfaces/feedback';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  private feedbacksUrl = 'https://localhost:7023/api/feedbacks';

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
