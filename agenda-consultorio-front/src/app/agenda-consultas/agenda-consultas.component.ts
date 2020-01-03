import { Component, OnInit, ViewChild } from '@angular/core';

import { ConsultaService }from '../consulta.service';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { AgendaCadastroComponent } from '../agenda-cadastro/agenda-cadastro.component';
import { MatDialogRef } from '@angular/material';
import { Consulta } from '../consulta.type';
import {
  MatToolbarModule,
  MatIconModule,
  MatSidenavModule,
  MatListModule,
  MatCardModule,
  MatTableModule,
  MatButtonModule,
  MatPaginatorModule,
  MatSortModule,
  MatFormFieldModule,
  MatInputModule,
  MatGridListModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatSnackBarModule,
  MatDialogModule } from  '@angular/material';

@Component({
  selector: 'app-agenda-consultas',
  templateUrl: './agenda-consultas.component.html',
  styleUrls: ['./agenda-consultas.component.css']
})
export class AgendaConsultasComponent implements OnInit {

  dialogRef: MatDialogRef<AgendaCadastroComponent>

  constructor(
    private consultaService: ConsultaService,
    private dialog : MatDialog
    ) { }

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

  deletarConsulta(data): void{
    this.consultaService.deleteConsulta(data).subscribe(r => {console.log(r)});
    const itemIndex = this.dataSource.data.findIndex(obj => obj.dataInicial == data);
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

  onCreate(){
    this.consultaService.inicializaFormGroup();
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '80%';
    try {
    this.dialogRef = this.dialog.open(AgendaCadastroComponent, dialogConfig);
    this.dialogRef.afterClosed().subscribe(result => {
      if(result != 'sem resultado'){
        console.log(result);
        this.dataSource.data.push(result);
        this.dataSource._updateChangeSubscription();
      }
    });
    } catch (error) {
      console.log(error);
    }
  }

  onUpdate(consulta: Consulta){
    this.consultaService.preencheFormGroup(consulta);
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '80%';
    try {
    this.dialogRef = this.dialog.open(AgendaCadastroComponent, dialogConfig);
    this.dialogRef.afterClosed().subscribe(result => {
      if(result != 'sem resultado'){
        console.log(result);
        const itemIndex = this.dataSource.data.findIndex(obj => obj.dataInicial == consulta.dataInicial);
        this.dataSource.data.splice(itemIndex, 1, result);
        this.dataSource._updateChangeSubscription();
      }
    });
    } catch (error) {
      console.log(error);
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
