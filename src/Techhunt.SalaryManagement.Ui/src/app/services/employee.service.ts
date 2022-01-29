import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { IConfig } from '../models/config';
import { Employee } from '../models/employee';
import { Constants} from '../constants';
import config from '../../assets/config.json';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private config: IConfig;
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
    })
  };

  constructor(private httpClient: HttpClient) { 
    this.config = { employeeUrl: config.employeeUrl};
  }

  getEmployees(minSalary: number, maxSalary: number, page: number, size: number): Observable<Employee[]> {
    let url = `${this.config.employeeUrl}?minSalary=${minSalary}&maxSalary=${maxSalary}&offset=${(page - 1)*size}&limit=${size}&sort=+name`;
    return this.httpClient.get<Employee[]>(url);
  }

  removeEmployee(id: string): Observable<any> {
    let url = `${this.config.employeeUrl}/${id}`;
    return this.httpClient.delete(url);
  }

  editEmployee(employee: Employee): Observable<any> {
    let url = `${this.config.employeeUrl}/${employee.id}`;
    return this.httpClient.patch(url,employee, this.httpOptions);
  }

  addEmployee(employee: Employee): Observable<any> {
    let url = `${this.config.employeeUrl}/${employee.id}`;
    return this.httpClient.post(url,employee, this.httpOptions);
  }

  importEmployees(file:File): Observable<any> {
    let url = `${this.config.employeeUrl}/upload`;
    const formData = new FormData();
    formData.append('file', file, file.name);
    return this.httpClient.post(url, formData, {reportProgress: true, observe: 'events'});
  }
}
