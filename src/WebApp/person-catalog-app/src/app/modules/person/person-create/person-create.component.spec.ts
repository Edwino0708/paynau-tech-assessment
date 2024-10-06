import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { PersonCreateComponent } from './person-create.component';
import { PersonService } from '../services/person.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { of, throwError } from 'rxjs';

class MockPersonService {
  createPerson = jasmine.createSpy('createPerson').and.returnValue(of({})); // Simulando un observable exitoso
}

class MockRouter {
  navigate = jasmine.createSpy('navigate');
}

class MockMatDialog {
  open = jasmine.createSpy('open').and.returnValue({ close: jasmine.createSpy('close') });
}

describe('PersonCreateComponent', () => {
  let component: PersonCreateComponent;
  let fixture: ComponentFixture<PersonCreateComponent>;
  let personService: PersonService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, PersonCreateComponent], // Importa el componente aquí
      providers: [
        { provide: PersonService, useClass: MockPersonService },
        { provide: Router, useClass: MockRouter },
        { provide: MatDialog, useClass: MockMatDialog },
        { provide: MatSnackBar, useValue: {} }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonCreateComponent);
    component = fixture.componentInstance;
    personService = TestBed.inject(PersonService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should create a person when form is valid', () => {
    component.personForm.setValue({
      fullName: 'John Doe',
      dateOfBirth: '1990-01-01',
      email: 'john.doe@example.com',
      phoneNumber: '1234567890',
      address: '123 Main St',
      gender: 'Male',
      nationality: 'American',
      occupation: 'Engineer'
    });

    component.createPerson();

    expect(personService.createPerson).toHaveBeenCalled();
    expect(personService.createPerson).toHaveBeenCalledWith(jasmine.any(Object));
    expect((component as any).router.navigate).toHaveBeenCalledWith(['/services/person']);
  });

  it('should set error message on service error', () => {
    (personService.createPerson as jasmine.Spy).and.returnValue(throwError('Error'));

    component.personForm.setValue({
      fullName: 'John Doe',
      dateOfBirth: '1990-01-01',
      email: 'john.doe@example.com',
      phoneNumber: '1234567890',
      address: '123 Main St',
      gender: 'Male',
      nationality: '',
      occupation: ''
    });

    component.createPerson();

    expect(component.errorMessage).toBe('Ha habido un inconveniente, por favor intente más tarde.');
  });

  it('should not create a person when form is invalid', () => {
    component.personForm.setValue({
      fullName: '',
      dateOfBirth: '',
      email: '',
      phoneNumber: '',
      address: '',
      gender: '',
      nationality: '',
      occupation: ''
    });

    component.createPerson();

    expect(personService.createPerson).not.toHaveBeenCalled();
  });
});
