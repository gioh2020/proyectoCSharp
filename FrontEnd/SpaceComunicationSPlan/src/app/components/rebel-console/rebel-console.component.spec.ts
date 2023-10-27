import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RebelConsoleComponent } from './rebel-console.component';

describe('RebelConsoleComponent', () => {
  let component: RebelConsoleComponent;
  let fixture: ComponentFixture<RebelConsoleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RebelConsoleComponent]
    });
    fixture = TestBed.createComponent(RebelConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
