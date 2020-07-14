import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../app/services/employee.service';
import { Employee } from './models/employee';

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
    this.employeeService.getEmployees(0, 0, 1, 30).subscribe(data => this.employees = data);
  }
}
