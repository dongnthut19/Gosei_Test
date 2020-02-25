import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { ConfirmationDialogService } from '../services/confirmation-dialog.service';
import EmployeeModel from '../models/employee-model';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: EmployeeModel[];
  total = 0;
  currentPage = 0;
  pageSize = 5;
  sortField = 'Id';
  sortOrder = 'Asc';
  txtKey = null;

  constructor(private employeeService: EmployeeService,
              private confirmationDialogService: ConfirmationDialogService) { }

  ngOnInit(): void {
    this.getEmployee();
  }

  getEmployee(page?: number) {
    page = page || 0;
    this.employeeService
      .listEmployee(this.txtKey, this.sortField, this.sortOrder, page, this.pageSize)
      .subscribe((data: any) => {
        this.employees = data.items;
        this.total = data.total;
    });
  }

  doSort(field: string) {
    this.sortField = field;
    this.sortOrder = this.sortOrder === 'Asc' ? 'Desc' : 'Asc';
    this.getEmployee(this.currentPage);
  }

  pageChanged(currentPage: number) {
    this.currentPage = currentPage;
    this.getEmployee(currentPage - 1);
  }

  openConfirmationDialog(employeeId: number) {
    this.confirmationDialogService.confirm('Please confirm..', 'Do you really want to delete this employee?')
    .then((confirmed) => {
      console.log('User confirmed:', confirmed);
      if (confirmed) {
        this.deleteEmployee(employeeId);
      }
    })
    .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
  }

  deleteEmployee(employeeId: number) {
    this.employeeService.deleteEmployee(employeeId).subscribe(data => {
      this.getEmployee(this.currentPage);
    });
  }

}
