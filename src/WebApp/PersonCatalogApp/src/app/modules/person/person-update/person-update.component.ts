import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { PersonService } from '../services/person.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { GenderStatus } from '../models/genderStatus';
import { GenderTextPipe } from '../pipes/gender-text.pipe';
import { SuccessDialogComponent } from '../../../shared/components/success-dialog/success-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from '../../../shared/components/error-dialog/error-dialog.component';

@Component({
  selector: 'app-person-update',
  standalone: true,
  imports:
  [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    GenderTextPipe,
    SuccessDialogComponent,
    ErrorDialogComponent
  ],
  templateUrl: './person-update.component.html',
  styleUrl: './person-update.component.scss'
})
export class PersonUpdateComponent implements OnInit {
  personForm: FormGroup;
  errorMessage: string | null = null;

  @Input({ required: true }) personId?: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private personService: PersonService,
    private router : Router,
    private dialog: MatDialog
  ) {
    this.personForm = this.formBuilder.group({
      id: ['', Validators.required],
      fullName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      address: ['', Validators.required],
      gender: ['', Validators.required],
      nationality: [''], // Opcional
      occupation: [''], // Opcional
    });
  }

  ngOnInit(): void {
    if (this.personId) {
      this.getPersonDetails(this.personId);
    } else {
      console.error('No se proporcionó un ID de persona válido.');
    }
  }

  getPersonDetails(id: string): void {
    this.personService.getPersonById(id).subscribe({
      next: (result: any) =>
        {
          const dateOfBirth = result.person.dateOfBirth
            ? new Date(result.person.dateOfBirth).toISOString().split('T')[0] : '';

           this.personForm.patchValue({
              ...result.person,
               dateOfBirth
            });
        },
        error: (error) => {
          this.errorMessage = 'Ha habido un inconveniente, por favor intente más tarde.';
          console.error(error);
        }
    });
  }

  updatePerson(): void {
    if (this.personForm.valid) {
      const data = { person: { ...this.personForm.value } };

      const date = new Date(data.person.dateOfBirth);
      data.person.dateOfBirth = date.toISOString().split('T')[0];

      this.personService.updatePerson(data).subscribe({
        next: () => {
          this.router.navigate(['/services/person']).then(() => {

            const dialogRef = this.dialog.open(SuccessDialogComponent, {
              width: '400px', // Puedes ajustar el tamaño según lo desees
            });

            setTimeout(() => {
              dialogRef.close(); // Cerrar el diálogo
              location.reload(); // Recargar la página
            }, 2000);
          });

        },
        error: (error) => {
          this.errorMessage = 'Ha habido un inconveniente, por favor intente más tarde.';
          console.error(error);
        }
      });
    }
  }

  genderKeys(): Array<string> {
    return Object.keys(GenderStatus).filter(key => isNaN(Number(key))); // Solo las claves string
  }
}
