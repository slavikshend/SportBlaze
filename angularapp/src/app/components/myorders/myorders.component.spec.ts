import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyOrdersComponent } from './myorders.component';

describe('MyordersComponent', () => {
  let component: MyOrdersComponent;
  let fixture: ComponentFixture<MyOrdersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyOrdersComponent]
    });
    fixture = TestBed.createComponent(MyOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
