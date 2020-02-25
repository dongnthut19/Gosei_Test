import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeAddeditComponent } from './employee-addedit/employee-addedit.component';


const routes: Routes = [
  {
    path: 'employeesCreate',
    component: EmployeeAddeditComponent
  },
  // {
  //   path: 'employees/:id',
  //   component: EmployeeAddeditComponent
  // },
  {
    path: 'employees',
    component: EmployeeListComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
