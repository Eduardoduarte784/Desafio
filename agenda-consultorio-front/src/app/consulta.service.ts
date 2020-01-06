import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Consulta } from './consulta.type';
import { FormGroup, FormControl, Validators, ValidationErrors } from '@angular/forms';
import { stringify } from '@angular/compiler/src/util';

@Injectable({
  providedIn: 'root'
})
export class ConsultaService {
  applicationUrl = 'http://localhost:5000';
  

  constructor(
    private httpClient: HttpClient
  ) { }

  form: FormGroup = new FormGroup({
    paciente: new FormControl('', Validators.required),
    dataNascimento: new FormControl('', Validators.required),
    dataInicial: new FormControl('', Validators.required),
    horaInicial: new FormControl('', Validators.required),
    dataFinal: new FormControl('', Validators.required),
    horaFinal: new FormControl('', Validators.required),
    observacoes: new FormControl('')
  });

  getAll(): Observable<Consulta[]> {
    return this.httpClient.get<Consulta[]>(`${this.applicationUrl}/api/Consulta`);
  }

  deleteConsulta(dataInicial): Observable<string> {
    return this.httpClient.delete(`${this.applicationUrl}/api/Consulta/${dataInicial}`, {responseType:'text'});
  }

  postConsulta(consulta): Observable<string> {
    return this.httpClient.post(`${this.applicationUrl}/api/Consulta`, consulta, {responseType:'text'});
  }

  putConsulta(consultaArray): Observable<string> {
    return this.httpClient.put(`${this.applicationUrl}/api/Consulta`, consultaArray, {responseType:'text'});
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

  preencheFormGroup(consulta: Consulta){
    var dataDeInicio = new Date(consulta.dataInicial);
    var dataDeTermino = new Date(consulta.dataFinal);
    this.form.get('paciente').setValue(consulta.paciente);
    this.form.get('dataNascimento').setValue(new Date(stringify(consulta.dataNascimento)));
    this.form.get('dataInicial').setValue(new Date(stringify(consulta.dataInicial)));
    this.form.get('horaInicial').setValue(this.criaStringProCampoHora(dataDeInicio));
    this.form.get('dataFinal').setValue(new Date(stringify(consulta.dataFinal)));
    this.form.get('horaFinal').setValue(this.criaStringProCampoHora(dataDeTermino));
    this.form.get('observacoes').setValue(consulta.observacoes);
  }

  criaStringProCampoHora(data: Date): string{
    var hora: string = stringify(data.getHours());
    var minuto: string = stringify(data.getMinutes());
    if(data.getHours() < 10){
      hora = '0' + hora;
    }

    if(data.getMinutes() < 10){
      minuto = '0' + minuto;
    }

    return hora + ':' + minuto;
  }

}
