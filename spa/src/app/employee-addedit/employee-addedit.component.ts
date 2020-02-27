import { Component, OnInit } from '@angular/core';
import { FormGroup,  FormBuilder,  Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModalConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from '../services/employee.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-employee-addedit',
  templateUrl: './employee-addedit.component.html',
  styleUrls: ['./employee-addedit.component.css'],
  providers: [DatePipe, NgbModalConfig, NgbModal]
})
export class EmployeeAddeditComponent implements OnInit {
  angForm: FormGroup;
  qualificationForm: FormGroup;
  mode = 'add';
  title = 'New Employee';
  employeeId;
  qualifications = [];

  constructor(private fb: FormBuilder,
              private employeeService: EmployeeService,
              private router: Router,
              private route: ActivatedRoute,
              private datePipe: DatePipe,
              config: NgbModalConfig,
              private modalService: NgbModal,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) {
    this.createForm();
    config.backdrop = 'static';
    config.keyboard = false;
  }

  createForm() {
    this.angForm = this.fb.group({
      FirstName: ['', Validators.required ],
      MiddleName: [''],
      LastName: ['', Validators.required ],
      BirthDate: [new Date(), Validators.required ],
      Gender: ['Male'],
      Note: [''],
      Email: ['']
    });

    this.qualificationForm = this.fb.group({
      Name: ['', Validators.required ],
      Institution: ['', Validators.required],
      City: [''],
      ValidFrom: [''],
      ValidTo: ['']
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.angForm.controls; }

  ngOnInit(): void {
    this.route
      .queryParams
      .subscribe(params => {
        // Defaults to 0 if no query param provided.
        this.employeeId = +params.id || 0;
        this.mode = params.mode || 'add';

        if (this.mode === 'edit') {
          this.loadEmployee();
        }
      });
  }

  accept() {
    // stop here if form is invalid
    if (this.angForm.invalid) {
        return;
    }

    const model = this.angForm.value;

    // tslint:disable-next-line:max-line-length
    const selectDate = this.angForm.value.BirthDate.year + '-' + this.angForm.value.BirthDate.month + '-' + this.angForm.value.BirthDate.day;
    model.BirthDate = new Date(selectDate);
    console.log('this.angForm.value.BirthDate ', this.angForm.value.BirthDate);
    this.spinner.show();

    if (this.mode === 'add') {
      this.employeeService.addEmployee(model).subscribe((data: any) => {
        this.spinner.hide();
        this.toastr.success('Add employee is successful!', 'Success');
        this.redirectToEmployee();
      });
    } else if (this.mode === 'edit') {
      model.Id = this.employeeId;
      this.employeeService.editEmployee(model).subscribe((data: any) => {
        this.spinner.hide();
        this.toastr.success('Edit employee is successful!', 'Success');
        this.redirectToEmployee();
      });
    }
  }

  redirectToEmployee() {
    this.router.navigate(['employees']).then( (e) => {
      if (e) {
        console.log('Navigation is successful!');
      }
    });
  }

  loadEmployee() {
    this.spinner.show();
    this.employeeService.getEmployee(this.employeeId).subscribe((data: any) => {
      const birthDate = new Date(data.birthDate);
      console.log('birthDate = ', birthDate);

      const birthYear =  Number(this.datePipe.transform(data.birthDate, 'yyyy'));
      const birthMonth =  Number(this.datePipe.transform(data.birthDate, 'MM'));
      const birthDay =  Number(this.datePipe.transform(data.birthDate, 'dd'));

      this.angForm.setValue({
        FirstName: data.firstName,
        MiddleName: data.middleName,
        LastName: data.lastName,
        Note: data.note,
        Gender: data.gender,
        Email: data.email,
        BirthDate: {
          year: birthYear,
          month: birthMonth,
          day: birthDay,
        },
      });
      this.qualifications = data.employeeQualification;
      this.title = `${data.firstName} ${data.middleName} ${data.lastName}`;
      this.spinner.hide();
    });
  }

  openQualificationAdding(content) {
    this.modalService.open(content);
  }

  saveQualification() {
    if (this.mode === 'edit') {
      if (this.qualificationForm.invalid) {
        return;
      }
      const model = this.qualificationForm.value;
      model.EmployeeId = this.employeeId;
      this.employeeService.addQualification(model).subscribe((data: any) => {
        this.toastr.success('Add Qualification is successful!', 'Success');
        this.modalService.dismissAll();
        this.loadEmployee();
      });
    }
  }
}
