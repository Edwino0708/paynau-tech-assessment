import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PersonService } from '../services/person.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { GenderTextPipe } from '../pipes/gender-text.pipe';
import { SuccessDialogComponent } from '../../../shared/components/success-dialog/success-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { GenderStatus } from '../models/genderStatus';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ErrorDialogComponent } from '../../../shared/components/error-dialog/error-dialog.component';

@Component({
  selector: 'app-person-create',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    GenderTextPipe,
    SuccessDialogComponent,
    ErrorDialogComponent
  ],
  templateUrl: './person-create.component.html',
  styleUrls: ['./person-create.component.scss']
})
export class PersonCreateComponent {
  personForm: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private personService: PersonService,
    private router: Router,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.personForm = this.formBuilder.group({
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

  createPerson(): void {
    if (this.personForm.valid) {
      const data = { person: { ...this.personForm.value } };

      const date = new Date(data.person.dateOfBirth);
      data.person.dateOfBirth = date.toISOString().split('T')[0];

      this.personService.createPerson(data).subscribe({
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
