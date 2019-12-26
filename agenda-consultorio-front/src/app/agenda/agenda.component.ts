import { Component, OnInit } from '@angular/core';
import { ConsultaService } from '../consulta.service';
import { Consulta } from '../consulta.type';

@Component({
  selector: 'app-agenda',
  templateUrl: './agenda.component.html',
  styleUrls: ['./agenda.component.css']
})
export class AgendaComponent implements OnInit {

  Model:Consulta[];

  constructor(
    private service: ConsultaService
  ) { }

  ngOnInit() {
    console.log(this.obterTodasConsultas());
  }

  obterTodasConsultas(): void {
        this.service
          .getAll()
          .subscribe(data => {
            this.Model = data;
            console.log(data);
          });
      }
}
