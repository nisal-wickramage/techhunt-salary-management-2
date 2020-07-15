import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Employee } from 'src/app/models/employee';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {
  @Output() employeeAdded = new EventEmitter<Employee>();
  
  addForm:FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.addForm = new FormGroup({
      id : new FormControl(''),
      login : new FormControl(''),
      name : new FormControl(''),
      salary : new FormControl(''),
    });
  }
  
  saveChanges(): void {
    let addedEmployee = new Employee();
    addedEmployee.id = this.addForm.value.id;
    addedEmployee.login = this.addForm.value.login;
    addedEmployee.name = this.addForm.value.name;
    addedEmployee.salary = this.addForm.value.salary;
    this.employeeAdded.emit(addedEmployee);
  }

}
