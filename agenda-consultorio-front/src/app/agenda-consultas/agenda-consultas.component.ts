import { Component, OnInit } from '@angular/core';

import { ConsultaService }from '../consulta.service';
import { DataSource } from '@angular/cdk/table';
import { Observable } from 'rxjs';
import { Consulta } from '../consulta.type';
import { stringify } from 'querystring';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-agenda-consultas',
  templateUrl: './agenda-consultas.component.html',
  styleUrls: ['./agenda-consultas.component.css']
})
export class AgendaConsultasComponent implements OnInit {

  constructor(private consultaService: ConsultaService) { }

  displayedColumns = ['paciente', 'dataNascimento', 'dataInicial', 'dataFinal', 'observacoes', 'delete'];
  dataSource : MatTableDataSource<any>;

  ngOnInit() {
    this.obterTodasConsultas();
  }

  obterTodasConsultas(): void {
        this.consultaService.getAll().subscribe(
      list =>{
        let array = list.map(item => item);
        this.dataSource = new MatTableDataSource(array);
      }
    );
  }

  deletarConsulta(nome): void{
        this.consultaService.deleteConsulta(nome).subscribe();
        const itemIndex = this.dataSource.data.findIndex(obj => obj.paciente == nome);
        this.dataSource.data.splice(itemIndex, 1);
        this.dataSource._updateChangeSubscription();
  }

  isEmpty(dataSource): boolean{
    if(dataSource.data.length==0){
      return true;
    }
    else{
      return false;
    }
  }
}
