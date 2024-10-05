import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AfterViewInit, Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { PersonService } from '../services/person.service';
import { Person } from '../models/person';
import { MatCardModule } from '@angular/material/card';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { CommonModule } from '@angular/common';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { FormsModule } from '@angular/forms';
import { GenderTextPipe } from '../pipes/gender-text.pipe';
import { ErrorDialogComponent } from '../../../shared/components/error-dialog/error-dialog.component';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-person-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatSortModule,
    FormsModule,
    GenderTextPipe,
    ErrorDialogComponent
  ],
  templateUrl: './person-list.component.html',
  styleUrl: './person-list.component.scss'
})
export class PersonListComponent implements OnInit, AfterViewInit  {
  persons: MatTableDataSource<Person> = new MatTableDataSource<Person>();
  displayedColumns: string[] = [
    'fullName',
    'dateOfBirth',
    'email',
    'phoneNumber',
    'address',
    'gender',
    'nationality',
    'occupation',
    'actions'
  ];
  pageIndex: number = 0;
  pageSize: number = 2;
  totalCount: number = 0;
  isLoading: boolean = true;
  searchTerm: string = '';  // Variable para almacenar el término de búsqueda
  errorMessage: string | null = null;

  @ViewChild(MatSort) sort!: MatSort;  // Ordenamiento
  @ViewChild(MatPaginator) paginator!: MatPaginator;  // Paginador

  @Output() actionTriggered = new EventEmitter<{ action: string, id?: string }>();

  constructor(private personService: PersonService) {}

  ngOnInit(): void {
    this.loadPersons();
  }

  ngAfterViewInit(): void {
    this.persons.sort = this.sort;
  }

  loadPersons(): void {
    this.isLoading = true;
    this.personService.getPersons(this.pageIndex, this.pageSize)
    .pipe(
      finalize(() => {
        this.isLoading = false;
      })
    ).subscribe({
      next: (result: any) => {
        this.persons = new MatTableDataSource(result.persons.data);
        this.totalCount = result.persons.count;
        this.isLoading = false;

        // Aplicar paginador y ordenamiento
        this.persons.sort = this.sort;
        this.persons.paginator = this.paginator;

        // Configurar filtro de búsqueda
        this.persons.filterPredicate = (data: Person, filter: string): boolean => {
          const filterLowerCase = filter.toLowerCase();
          return (data.fullName?.toLowerCase().includes(filterLowerCase) || false) ||
                 (data.email?.toLowerCase().includes(filterLowerCase) || false) ||
                 (data.phoneNumber?.toLowerCase().includes(filterLowerCase) || false) ||
                 (data.address?.toLowerCase().includes(filterLowerCase) || false);
        };
      },
      error: (error) => {
        this.errorMessage = 'Ha habido un inconveniente, por favor intente más tarde.';
        console.error(error);
      }
    });
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.persons.filter = filterValue.trim().toLowerCase();
  }

  editPerson(id: string): void {
    this.actionTriggered.emit({ action: 'edit', id });
    }

  deletePerson(id: string): void {
    if (confirm('¿Estás seguro de que deseas eliminar esta persona?')) {
      this.personService.deletePerson(id).subscribe({
        next: () => {
          this.loadPersons();
        },
        error: (error) => {
          this.errorMessage = 'Ha habido un inconveniente, por favor intente más tarde.';
          console.error(error);
        }
      });
    }
  }

  onPageChange(event: any): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadPersons();
  }
}
