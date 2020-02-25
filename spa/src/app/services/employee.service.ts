import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  uri = 'http://localhost:5000/api/employee';
  constructor(private http: HttpClient) { }

  addProduct(ProductName, ProductDescription, ProductPrice) {
    const obj = {
      ProductName,
      ProductDescription,
      ProductPrice
    };
    console.log(obj);
    this.http.post(`${this.uri}/add`, obj)
        .subscribe(res => console.log('Done'));
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
