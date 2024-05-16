import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubcategoryLoadService {
  private selectedSubcategorySource = new BehaviorSubject<string>('');
  selectedSubcategory$ = this.selectedSubcategorySource.asObservable();
  constructor() { }

  setSelectedSubcategory(subcategory: string) {
    this.selectedSubcategorySource.next(subcategory);
  }
}
