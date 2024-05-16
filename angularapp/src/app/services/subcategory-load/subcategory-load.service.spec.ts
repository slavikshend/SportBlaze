import { TestBed } from '@angular/core/testing';

import { SubcategoryLoadService } from './subcategory-load.service';

describe('SubcategoryLoadService', () => {
  let service: SubcategoryLoadService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubcategoryLoadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
