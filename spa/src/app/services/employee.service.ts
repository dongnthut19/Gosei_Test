import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import EmployeeModel from '../models/employee-model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  uri = 'http://localhost:5000/api/employee';
  constructor(private http: HttpClient) { }

  addEmployee(objEmployee: EmployeeModel) {
    console.log('EmployeeModel = ', EmployeeModel);
    return this.http.post(`${this.uri}`, EmployeeModel);
  }

  listEmployee(txtKey: string, orderByField: string, sortOrder: string, page: number, pageSize: number) {
    return this
           .http
           // tslint:disable-next-line:max-line-length
           .get(`${this.uri}?txtFirstOrLastName=${txtKey}&orderByField=${orderByField}&sortOrder=${sortOrder}&page=${page}&pageSize=${pageSize}`);
  }

  deleteEmployee(employeeId: number) {
    return this
           .http
           .delete(`${this.uri}/${employeeId}`);
  }
}
