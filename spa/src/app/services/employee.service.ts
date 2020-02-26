import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import EmployeeModel from '../models/employee-model';
import QualificationModel from '../models/Qualification-model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addEmployee(objEmployee: EmployeeModel) {
    return this.http.post(`${this.baseUrl}/employee/add`, objEmployee);
  }

  editEmployee(objEmployee: EmployeeModel) {
    return this.http.post(`${this.baseUrl}/employee/edit`, objEmployee);
  }

  listEmployee(txtKey: string, orderByField: string, sortOrder: string, page: number, pageSize: number) {
    return this
           .http
           // tslint:disable-next-line:max-line-length
           .get(`${this.baseUrl}/employee?txtFirstOrLastName=${txtKey}&orderByField=${orderByField}&sortOrder=${sortOrder}&page=${page}&pageSize=${pageSize}`);
  }

  getEmployee(employeeId: number) {
    return this
           .http
           // tslint:disable-next-line:max-line-length
           .get(`${this.baseUrl}/employee/getbyid/${employeeId}`);
  }

  deleteEmployee(employeeId: number) {
    return this
           .http
           .delete(`${this.baseUrl}/employee/${employeeId}`);
  }

  addQualification(objQualification: QualificationModel) {
    return this.http.post(`${this.baseUrl}/qualification/add`, objQualification);
  }
}
