import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { Employee } from 'src/app/models/employee';
import { Constants } from 'src/app/constants';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit, OnChanges {

  @Input() employees: Employee[];
  @Output() navigated = new EventEmitter<number>();
  @Output() selectedEmployee = new EventEmitter<Employee>();
  
  isPreviousPageAvailable = false;
  isNextPageAvailable = false;
  currentPage = 1;
  
  constructor() { }
  
  ngOnChanges(changes: SimpleChanges): void {
    if(this.employees)
    {
      if(this.employees.length === Constants.pageSize)
      {
        this.isNextPageAvailable = true;
      }
      else
      {
        this.isNextPageAvailable = false;
      }
    }
    
  }

  moveToNextPage(): void
  {
    this.currentPage++;
    this.isPreviousPageAvailable = true;
    this.navigated.emit(this.currentPage);
  }
  
  moveToPreviousPage(): void
  {
    this.currentPage--;
    this.isNextPageAvailable = true;
    this.navigated.emit(this.currentPage);
  }

  selectEmployee(employee: Employee): void {
    this.selectedEmployee.emit(employee);
  }

  ngOnInit(): void {
    this.selectedEmployee.emit(new Employee());
  }

}
