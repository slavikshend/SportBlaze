import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShopLogComponent } from './shop-log.component';

describe('ShopLogComponent', () => {
  let component: ShopLogComponent;
  let fixture: ComponentFixture<ShopLogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShopLogComponent]
    });
    fixture = TestBed.createComponent(ShopLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
