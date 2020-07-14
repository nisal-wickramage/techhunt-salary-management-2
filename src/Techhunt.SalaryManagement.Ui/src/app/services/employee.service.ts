import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { IConfig } from '../models/config';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private config: IConfig;
  private configUrl = 'assets/config.json';
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
    })
  };

  constructor(private httpClient: HttpClient) { 
  }

  private getConfig(): void 
  {
    this.httpClient.get(this.configUrl)
      .subscribe((data: IConfig) => this.config = data);
  }

  getEmployees(minSalary: number, maxSalary: number, page: number, size: number): Observable<Array<Employee>> {
    let url = `http://localhost:5001/users?minSalary=${minSalary}&maxSalary=${maxSalary}&offset${page*size}&limit=${size}`;
    return this.httpClient.get<Array<Employee>>(url);
  }

  removeEmployee(id: string): Observable<any> {
    let url = `http://localhost:5001/users/${id}`;
    return this.httpClient.delete(url);
  }

  editEmployee(employee: Employee): Observable<any> {
    let url = `http://localhost:5001/users/${id}`;
    return this.httpClient.patch(url,employee, this.httpOptions);
  }

  addEmployee(employee: Employee): Observable<any> {
    let url = `http://localhost:5001/users/${id}`;
    return this.httpClient.post(url,employee, this.httpOptions);
  }


}
