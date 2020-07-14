import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { MenuPanelComponent } from './components/menu-panel/menu-panel.component';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EditEmployeeComponent } from './components/edit-employee/edit-employee.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    MenuPanelComponent,
    SearchBarComponent,
    EmployeeListComponent,
    EditEmployeeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
