import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PsyFormComponent } from './psy-form.component';

describe('PsyFormComponent', () => {
  let component: PsyFormComponent;
  let fixture: ComponentFixture<PsyFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PsyFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PsyFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
