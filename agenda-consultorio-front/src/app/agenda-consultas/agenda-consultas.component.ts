import { Component, OnInit } from '@angular/core';

import { ConsultaService }from '../consulta.service';
import { DataSource } from '@angular/cdk/table';
import { Observable } from 'rxjs';
import { Consulta } from '../consulta.type';
import { stringify } from 'querystring';

@Component({
  selector: 'app-agenda-consultas',
  templateUrl: './agenda-consultas.component.html',
  styleUrls: ['./agenda-consultas.component.css']
})
export class AgendaConsultasComponent implements OnInit {

  Model:Consulta[];

  constructor(private consultaService: ConsultaService) { }

  displayedColumns = ['paciente', 'dataNascimento', 'dataInicial', 'dataFinal', 'observacoes', 'delete'];
  dataSource = new PostDataSource(this.consultaService);

  ngOnInit() {
    console.log(this.obterTodasConsultas());
  }

  obterTodasConsultas(): void {
        this.consultaService
          .getAll()
          .subscribe(data => {
            this.Model = data;
            console.log(data);
          });
      }

  deletarConsulta(paciente): void{
        this.consultaService.deleteConsulta(paciente).subscribe();
        this.dataSource = new PostDataSource(this.consultaService);
  }

}

export class PostDataSource extends DataSource<any> {
  constructor(private consultaService: ConsultaService) {
    super();
  }

  connect(): Observable<Consulta[]> {
    return this.consultaService.getAll();
  }

  disconnect() {
  }
}
