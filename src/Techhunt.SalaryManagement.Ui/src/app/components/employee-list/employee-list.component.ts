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
  
  isPreviousPageAvailable = false;
  isNextPageAvailable = false;
  currentPage = 1;
  
  constructor() { }
  ngOnChanges(changes: SimpleChanges): void {
    if(this.employees)
    {
      console.log(this.employees.length);
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

  ngOnInit(): void {
  }

}
