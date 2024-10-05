import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonMenuComponent } from './person-menu.component';

describe('PersonMenuComponent', () => {
  let component: PersonMenuComponent;
  let fixture: ComponentFixture<PersonMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PersonMenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
