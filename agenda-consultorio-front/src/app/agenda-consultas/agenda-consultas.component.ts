import { Component, OnInit, ViewChild } from '@angular/core';

import { ConsultaService }from '../consulta.service';
import { DataSource } from '@angular/cdk/table';
import { Observable } from 'rxjs';
import { Consulta } from '../consulta.type';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';

@Component({
  selector: 'app-agenda-consultas',
  templateUrl: './agenda-consultas.component.html',
  styleUrls: ['./agenda-consultas.component.css']
})
export class AgendaConsultasComponent implements OnInit {

  constructor(private consultaService: ConsultaService) { }

  displayedColumns = ['paciente', 'dataNascimento', 'dataInicial', 'dataFinal', 'observacoes', 'delete'];
  dataSource : MatTableDataSource<any>;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  searchKey: string;
  

  ngOnInit() {
    this.obterTodasConsultas();
  }

  obterTodasConsultas(): void {
        this.consultaService.getAll().subscribe(
      list =>{
        let array = list.map(item => item);
        this.dataSource = new MatTableDataSource(array);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      });
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

  onSearchClear(): void{
    this.searchKey= "";
    this.applyFilter();
  }

  
  applyFilter(): void{
    this.dataSource.filterPredicate = (data, filter) => (data.paciente.trim().toLowerCase().indexOf(filter.trim().toLowerCase()) !== -1);
    this.dataSource.filter = this.searchKey;
  }
}
