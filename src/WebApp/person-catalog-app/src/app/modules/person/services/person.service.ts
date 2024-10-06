import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from '../models/person';
import { environment } from '../../../../environments/environment';
import { PaginateResult } from '../../../models/paginate-result';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
  private apiUrl = `${environment.apiUrl}persons/`; // Cambia esto a la URL de tu API

  constructor(private http: HttpClient) {}

  // Configura encabezados para trabajar con JSON
  private getJsonHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    });
  }

  getPersons(pageIndex: number, pageSize: number): Observable<PaginateResult<Person>> {
    let url = `${this.apiUrl}?pageIndex=${pageIndex.toString()}&pageSize=${pageSize.toString()}`;
    return this.http.get<PaginateResult<Person>>(url, { headers: this.getJsonHeaders() });
  }

  createPerson(data: any): Observable<Person> {
    return this.http.post<Person>(this.apiUrl, data, { headers: this.getJsonHeaders() });
  }

  updatePerson(data: any): Observable<Person> {
    return this.http.put<Person>(`${this.apiUrl}`, data, { headers: this.getJsonHeaders() });
  }

  deletePerson(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}${id}`, { headers: this.getJsonHeaders() });
  }

  getPersonByEmail(email: string): Observable<Person> {
    return this.http.get<Person>(`${this.apiUrl}ByEmail/${email}`, { headers: this.getJsonHeaders() });
  }

  getPersonById(id: string): Observable<Person> {
    return this.http.get<Person>(`${this.apiUrl}ById/${id}`, { headers: this.getJsonHeaders() });
  }
}
