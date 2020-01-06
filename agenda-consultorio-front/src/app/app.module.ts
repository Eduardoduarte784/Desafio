import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MaterialModule } from './material.module';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AgendaCadastroComponent } from './agenda-cadastro/agenda-cadastro.component';
import { AgendaConsultasComponent } from './agenda-consultas/agenda-consultas.component';
import { ConsultaService } from './consulta.service';
import { MAT_DATE_LOCALE, MatDialogRef } from '@angular/material';
import { CaixaConfirmacaoComponent } from './caixa-confirmacao/caixa-confirmacao.component';

@NgModule({
  declarations: [
    AppComponent,
    AgendaCadastroComponent,
    AgendaConsultasComponent,
    CaixaConfirmacaoComponent
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
  providers: [{
    provide: MatDialogRef,
    useValue: {}
  },{provide: MAT_DATE_LOCALE, useValue: 'pt-Br'},ConsultaService],
  bootstrap: [AppComponent],
  entryComponents: [AgendaCadastroComponent, CaixaConfirmacaoComponent]
})
export class AppModule { }
