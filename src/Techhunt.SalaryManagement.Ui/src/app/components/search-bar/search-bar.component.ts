import { Component, OnInit, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { EmployeeSearchParams } from '../../models/employee-search-params';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {
  @Output() salaryParamChanged = new EventEmitter<EmployeeSearchParams>();  

  searchBar:FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.searchBar = new FormGroup({
      minSalary : new FormControl(''),
      maxSalary : new FormControl(''),
    });
    
    this.searchBar.valueChanges.pipe(
      debounceTime(1000),
      distinctUntilChanged()).subscribe(data => {
        let params = new EmployeeSearchParams();
        params.minSalary = data.minSalary;
        params.maxSalary = data.maxSalary;
        this.salaryParamChanged.emit(params);
      });
  }
}
