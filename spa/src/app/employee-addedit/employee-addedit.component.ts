import { Component, OnInit } from '@angular/core';
import { FormGroup,  FormBuilder,  Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import EmployeeModel from '../models/employee-model';

@Component({
  selector: 'app-employee-addedit',
  templateUrl: './employee-addedit.component.html',
  styleUrls: ['./employee-addedit.component.css']
})
export class EmployeeAddeditComponent implements OnInit {
  angForm: FormGroup;
  mode = 'add';

  constructor(private fb: FormBuilder,
              private employeeService: EmployeeService,
              private router: Router) {
    this.createForm();
  }

  createForm() {
    this.angForm = this.fb.group({
      FirstName: ['', Validators.required ],
      MiddleName: [''],
      LastName: ['', Validators.required ],
      BirthDate: ['', Validators.required ],
      Gender: ['Male'],
      Note: [''],
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.angForm.controls; }

  ngOnInit(): void {
  }

  accept() {
    // stop here if form is invalid
    if (this.angForm.invalid) {
        return;
    }

    const model = this.angForm.value;
    model.BirthDate = this.angForm.value.BirthDate.month + '/' + this.angForm.value.BirthDate.day + '/' + this.angForm.value.BirthDate.year;
    console.log(model);
    this.employeeService.addEmployee(model).subscribe((data: any) => {
      console.log('Add employee is successful!');
  });
  }

  cancel() {
    this.router.navigate(['employees']).then( (e) => {
      if (e) {
        console.log('Navigation is successful!');
      }
    });
  }

}
