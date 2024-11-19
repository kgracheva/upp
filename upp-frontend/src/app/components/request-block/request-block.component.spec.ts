import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestBlockComponent } from './request-block.component';

describe('RequestBlockComponent', () => {
  let component: RequestBlockComponent;
  let fixture: ComponentFixture<RequestBlockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RequestBlockComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RequestBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
