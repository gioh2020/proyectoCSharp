import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImperialConsoleComponent } from './imperial-console.component';

describe('ImperialConsoleComponent', () => {
  let component: ImperialConsoleComponent;
  let fixture: ComponentFixture<ImperialConsoleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImperialConsoleComponent]
    });
    fixture = TestBed.createComponent(ImperialConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
