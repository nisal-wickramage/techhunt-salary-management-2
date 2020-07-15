import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-delete-employee',
  templateUrl: './delete-employee.component.html',
  styleUrls: ['./delete-employee.component.css']
})
export class DeleteEmployeeComponent implements OnInit {
  @Input() employeeId: string;
  @Output() deleteEmployee = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  delete(): void {
    this.deleteEmployee.emit(this.employeeId);
  }

}
