import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../app/services/employee.service';
import { Employee } from './models/employee';
import { Constants } from './constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit  {
  title = 'salary-management-ui';

  employees: Employee[];
  selectedEmployeeId:string;

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
    this.employeeService
    .getEmployees(this.minSalary, this.maxSalary, this.pageNumer, Constants.pageSize)
    .subscribe(data => this.employees = data);
  }

  selectedEmployee(id: string): void {
    this.selectedEmployeeId = id;
  }

  deleteEmployee(): void {
    this.employeeService.removeEmployee(this.selectedEmployeeId).subscribe(data => {      
      this.employeeService
      .getEmployees(this.minSalary, this.maxSalary, this.pageNumer, Constants.pageSize)
      .subscribe(data => this.employees = data);
    });
  }
}
