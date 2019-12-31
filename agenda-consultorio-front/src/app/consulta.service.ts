import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Consulta } from './consulta.type';
import { FormGroup, FormControl, Validators, ValidationErrors } from '@angular/forms';
import { stringify } from 'querystring';

@Injectable({
  providedIn: 'root'
})
export class ConsultaService {
  applicationUrl = 'http://localhost:5000';
  inicioprop: Date = new Date("December 17, 1995 03:24:00");
  finalprop: Date = new Date("December 17, 1995 03:24:00");

  constructor(
    private httpClient: HttpClient
  ) { }

  form: FormGroup = new FormGroup({
    paciente: new FormControl('', Validators.required),
    dataNascimento: new FormControl(''),
    dataInicial: new FormControl('', Validators.required),
    horaInicial: new FormControl('', [Validators.required, this.validaHora]),
    dataFinal: new FormControl('', Validators.required),
    horaFinal: new FormControl('', Validators.required),
    observacoes: new FormControl('')
  });

  getAll(): Observable<Consulta[]> {
    return this.httpClient.get<Consulta[]>(`${this.applicationUrl}/api/Consulta`);
  }

  deleteConsulta(paciente: string): Observable<void> {
    return this.httpClient.delete<void>(`${this.applicationUrl}/api/Consulta/${paciente}`);
  }

  postConsulta(consulta): Observable<void> {
    return this.httpClient.post<void>(`${this.applicationUrl}/api/Consulta`, consulta);
  }

  validaHora(input: FormControl){
      if(Number(input.value) != null){
        return null;
      }
      else{
        return { horainvalida: true }
      }
  }

}
