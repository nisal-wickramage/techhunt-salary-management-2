import { Component, OnInit, ViewChild } from '@angular/core';
import { EmployeeService } from 'src/app/services/employee.service';
import { HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-import-employee',
  templateUrl: './import-employee.component.html',
  styleUrls: ['./import-employee.component.css']
})
export class ImportEmployeeComponent implements OnInit {
  progress: number;
  
  constructor(private employeeService: EmployeeService) { }

  @ViewChild('closebutton') closebutton;

  ngOnInit(): void {
  }

  uploadFile(files): void {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
 
    this.employeeService.importEmployees(fileToUpload)
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress){
          this.progress = Math.round(100 * event.loaded / event.total);
        } else if (event.type === HttpEventType.Response) {
            this.progress = 0;
            this.closebutton.nativeElement.click();
        }
      });
  }
}
