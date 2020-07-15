import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../app/services/employee.service';
import { Employee } from './models/employee';
import { Constants } from './constants';
import { EmployeeSearchParams } from '../app/models/employee-search-params';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit  {
  title = 'salary-management-ui';

  employees: Employee[];
  selectedEmployee:  Employee;

  minSalary = 0;
  maxSalary = 0;
  pageNumer = 1;

  constructor(private employeeService: EmployeeService) {
  }

  ngOnInit() { 
    this.employeeService
    .getEmployees(this.minSalary, this.maxSalary, this.pageNumer, Constants.pageSize)
    .subscribe(data => this.employees = data);
  }

  onNavigation(pageNumber: number): void {
    this.pageNumer = pageNumber;
    this.employeeService
    .getEmployees(this.minSalary, this.maxSalary, this.pageNumer, Constants.pageSize)
    .subscribe(data => this.employees = data);
  }

  setSelectedEmployee(employee: Employee): void {
    this.selectedEmployee = employee;
  }

  deleteEmployee(): void {
    this.employeeService.removeEmployee(this.selectedEmployee.id).subscribe(data => {      
      this.employeeService
      .getEmployees(this.minSalary, this.maxSalary, this.pageNumer, Constants.pageSize)
      .subscribe(data => this.employees = data);
    });
  }

  editEmployee(employee:Employee): void {
    this.employeeService.editEmployee(employee).subscribe(data => { 
    });
  }

  addEmployee(employee:Employee): void {
    this.employeeService.addEmployee(employee).subscribe(data => { 
    });
  }

  search(params: EmployeeSearchParams): void {
    this.minSalary = params.minSalary;
    this.maxSalary = params.maxSalary;
    this.pageNumer = 1;
    this.employeeService
      .getEmployees(this.minSalary, this.maxSalary, this.pageNumer, Constants.pageSize)
      .subscribe(data => this.employees = data);
  }
}
