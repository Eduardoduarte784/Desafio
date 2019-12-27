import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {MaterialModule} from './material.module';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AgendaComponent } from './agenda/agenda.component';
import { AgendaConsultasComponent } from './agenda-consultas/agenda-consultas.component';
import {ConsultaService} from './consulta.service';

@NgModule({
  declarations: [
    AppComponent,
    AgendaComponent,
    AgendaConsultasComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    FormsModule,
  ],
  providers: [ConsultaService],
  bootstrap: [AppComponent]
})
export class AppModule { }
