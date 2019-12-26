import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Consulta } from './consulta.type';

@Injectable({
  providedIn: 'root'
})
export class ConsultaService {
  applicationUrl = 'http://localhost:5000';

  constructor(
    private httpClient: HttpClient
  ) { }

  getAll(): Observable<Consulta[]> {
    return this.httpClient.get<Consulta[]>(`${this.applicationUrl}/api/Consulta`);
  }

  deleteConsulta(paciente: string): Observable<void> {
    const url = `${this.applicationUrl}/api/Consulta/${paciente}`;
    return this.httpClient.delete<void>(`${this.applicationUrl}/api/Consulta/${paciente}`);
  }
}
