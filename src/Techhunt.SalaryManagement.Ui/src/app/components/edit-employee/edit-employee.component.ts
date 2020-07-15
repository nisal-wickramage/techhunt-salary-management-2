import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { Employee } from 'src/app/models/employee';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit, OnChanges {
  @Input() employee: Employee;
  @Output() employeeEdited = new EventEmitter<Employee>();

  editForm:FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.editForm = new FormGroup({
      id : new FormControl({value: '', disabled: true}),
      login : new FormControl(''),
      name : new FormControl(''),
      salary : new FormControl(''),
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.editForm){
      this.editForm.setValue({
        id : this.employee.id,
        login : this.employee.login,
        name : this.employee.name,
        salary : this.employee.salary,
      });
    }
  }

  saveChanges(): void {
    let editedEmployee = this.employee;
    editedEmployee.login = this.editForm.value.login;
    editedEmployee.name = this.editForm.value.name;
    editedEmployee.salary = this.editForm.value.salary;
    this.employeeEdited.emit(editedEmployee);
  }

}
