import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { SlimLoadingBarModule } from 'ng2-slim-loading-bar';
import { NgxPaginationModule } from 'ngx-pagination';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { EmployeeService } from './services/employee.service';
import { ConfirmationDialogService } from './services/confirmation-dialog.service';
import { AppComponent } from './app.component';
import { ConfirmationDialogComponent } from './directives/confirmation-dialog/confirmation-dialog.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { EmployeeAddeditComponent } from './employee-addedit/employee-addedit.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeeListComponent,
    ConfirmationDialogComponent,
    EmployeeAddeditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SlimLoadingBarModule,
    NgxPaginationModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot() // ToastrModule added
  ],
  providers: [
    EmployeeService,
    ConfirmationDialogService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
