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

  constructor(private employeeService: EmployeeService) {
  }

  ngOnInit() { 
    this.employeeService.getEmployees(0, 0, 1, Constants.pageSize).subscribe(data => this.employees = data);
  }

  onNavigation(pageNumber: number): void {
    this.employeeService.getEmployees(0, 0, pageNumber, Constants.pageSize).subscribe(data => this.employees = data);
  }
}
