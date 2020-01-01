import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Consulta } from './consulta.type';
import { FormGroup, FormControl, Validators, ValidationErrors } from '@angular/forms';

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
    horaInicial: new FormControl('', Validators.required),
    dataFinal: new FormControl('', Validators.required),
    horaFinal: new FormControl('', Validators.required),
    observacoes: new FormControl('')
  });

  getAll(): Observable<Consulta[]> {
    return this.httpClient.get<Consulta[]>(`${this.applicationUrl}/api/Consulta`);
  }

  deleteConsulta(dataInicial: string): Observable<string> {
    return this.httpClient.delete(`${this.applicationUrl}/api/Consulta/${dataInicial}`, {responseType:'text'});
  }

  postConsulta(consulta): Observable<string> {
    return this.httpClient.post(`${this.applicationUrl}/api/Consulta`, consulta, {responseType:'text'});
  }

  inicializaFormGroup(){
    this.form.setValue({
      paciente: '',
      dataNascimento: '',
      dataInicial: '',
      horaInicial: '',
      dataFinal: '',
      horaFinal: '',
      observacoes: ''
    });
  }

}
