import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {MaterialModule} from './material.module';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AgendaCadastroComponent } from './agenda-cadastro/agenda-cadastro.component';
import { AgendaConsultasComponent } from './agenda-consultas/agenda-consultas.component';
import { ConsultaService } from './consulta.service';
import { MAT_DATE_LOCALE } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    AgendaCadastroComponent,
    AgendaConsultasComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [{provide: MAT_DATE_LOCALE, useValue: 'pt-Br'},ConsultaService],
  bootstrap: [AppComponent]
})
export class AppModule { }
