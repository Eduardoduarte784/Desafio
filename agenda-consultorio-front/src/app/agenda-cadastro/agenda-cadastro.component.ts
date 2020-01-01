import { Component, OnInit } from '@angular/core';
import { ConsultaService } from '../consulta.service';
import { Consulta } from '../consulta.type';
import { stringify } from '@angular/compiler/src/util';
import { Validators } from '@angular/forms';



@Component({
  selector: 'app-agenda-cadastro',
  templateUrl: './agenda-cadastro.component.html',
  styleUrls: ['./agenda-cadastro.component.css']
})
export class AgendaCadastroComponent implements OnInit {

  arrayLocalDeConsultas : Consulta[] = {} as Consulta[];

  constructor(                                              
    private service: ConsultaService
  ) { }
  
  ngOnInit() {
    this.obterTodasConsultas();
  }

  obterTodasConsultas(): void {
        this.service.getAll().subscribe(                  //pega todas as consultas cadastradas no banco e joga pra um
      list =>{                                            //array local que também receberá as novas consultas e será
        let array = list.map(item => item);               //usado para a validação do range de datas. Validação essa que
        this.arrayLocalDeConsultas = array;               //será realizada novamente no backend.
      });
  }

  onClear(){
    this.service.form.reset();
    this.service.inicializaFormGroup();
  }

  onSubmit(){
    if(this.service.form.valid){
      var dataDeInicio: Date = this.criaDataAPartirDosInputs("dataInicial","horaInicial");
      var dataDeTermino: Date = this.criaDataAPartirDosInputs("dataFinal","horaFinal");
      if(dataDeTermino.getTime() > dataDeInicio.getTime()) {
        if(this.verificaSeHorarioEstaVago(dataDeInicio, dataDeTermino)){
          dataDeInicio = this.corrigeZonaHorariaProPost(dataDeInicio);
          dataDeTermino = this.corrigeZonaHorariaProPost(dataDeTermino);
          this.service.postConsulta(this.criaObjetoProPost(dataDeInicio, dataDeTermino)).subscribe(r => {console.log(r)});
          this.arrayLocalDeConsultas.push(this.criaObjetoProArrayLocal(dataDeInicio, dataDeTermino));
          this.limparOsInputs();
        }
        else{
          console.log("Horário Indisponível");
        }
      }else{
        console.log("Data Final não pode ser menor do que a Inicial")
      } 
    }
  }

  criaDataAPartirDosInputs(nomeDoDataInput: string, nomeDoHoraInput: string): Date{
    var data: Date = new Date(stringify(this.service.form.get(nomeDoDataInput).value));  //recebe a data do Datepicker.
    var horaEMinuto: string = this.service.form.get(nomeDoHoraInput).value;       //recebe as horas e minutos do input.
    var dia: string = stringify(data.getUTCDate());
    var mes: string = stringify(data.getUTCMonth()+1);        //divide a data do Datepicker em partes para posterior
    var ano: string = stringify(data.getUTCFullYear());       //junção com a hora.
    if(Number(dia) < 10){
      dia = "0" + dia;                  //caso o dia tenha apenas um digito adiciona um 0 na frente.
    }
    if(Number(mes) < 10){
      mes = "0" + mes;                  //caso o mês tenha apenas um digito adiciona um 0 na frente.
    }
    data = new Date(ano + "-" + mes + "-" + dia + "T" + horaEMinuto + ":00");   //faz a junção da data e da hora.
    return data;
  }

  verificaSeHorarioEstaVago(dataDeInicio: Date, dataDeTermino: Date): boolean{
    for(let cs of this.arrayLocalDeConsultas){                          //iteração das consultas no array local.
      var dataInicialAuxiliar = new Date(stringify(cs.dataInicial));    //variáveis auxiliares para usar o metodo getTime
      var dataFinalAuxiliar = new Date(stringify(cs.dataFinal));        //do tipo Date sem causar erros de execução.
      
      if((dataDeInicio.getTime() < dataFinalAuxiliar.getTime())         //Estrutura de Condição que verifica se a nova consulta
      && (dataDeTermino.getTime() > dataInicialAuxiliar.getTime())){    //começa ou termina no range de duração das outras.
        console.log(cs.paciente);
        console.log(dataDeInicio);
        console.log(dataDeTermino);
        console.log(dataInicialAuxiliar);
        console.log(dataFinalAuxiliar);
        return false;
      }
    }
    return true;
  }

  criaObjetoProPost(dataDeInicio: Date, dataDeTermino: Date): Consulta{
    var consulta: Consulta = {} as Consulta;
    consulta.paciente = this.service.form.get("paciente").value;;
    consulta.dataNascimento = this.service.form.get("dataNascimento").value;
    consulta.dataInicial = dataDeInicio;
    consulta.dataFinal = dataDeTermino;
    consulta.observacoes = this.service.form.get("observacoes").value;
    return consulta;
  }

  criaObjetoProArrayLocal(dataDeInicio: Date, dataDeTermino: Date): Consulta{
    var consulta: Consulta = {} as Consulta;
    dataDeInicio = this.corrigeZonaHorariaProArrayLocal(dataDeInicio);
    dataDeTermino = this.corrigeZonaHorariaProArrayLocal(dataDeTermino);
    consulta.paciente = this.service.form.get("paciente").value;;
    consulta.dataNascimento = this.service.form.get("dataNascimento").value;
    consulta.dataInicial = dataDeInicio;
    consulta.dataFinal = dataDeTermino;
    consulta.observacoes = this.service.form.get("observacoes").value;
    return consulta;
  }

  corrigeZonaHorariaProPost(dataParametro): Date{
    var dataRetorno : Date = dataParametro;
    if (dataRetorno.getHours() > dataRetorno.getUTCHours()) {
        if (dataRetorno.getDate() > dataRetorno.getUTCDate()) {
            if (dataRetorno.getMonth() > dataRetorno.getUTCMonth()) {
                dataRetorno.setUTCFullYear(dataRetorno.getFullYear(), dataRetorno.getMonth(), dataRetorno.getDate());
            }else{
                dataRetorno.setUTCFullYear(dataRetorno.getUTCFullYear(), dataRetorno.getMonth(), dataRetorno.getDate());
            }
        }else{
            dataRetorno.setUTCFullYear(dataRetorno.getUTCFullYear(), dataRetorno.getUTCMonth(), dataRetorno.getDate());
        }
    }
    dataRetorno.setUTCHours(dataRetorno.getHours());
    
    return dataRetorno;
  }

  corrigeZonaHorariaProArrayLocal(dataParametro):Date{
    var dataRetorno : Date = dataParametro;
    if (dataRetorno.getHours() > dataRetorno.getUTCHours()) {
        if (dataRetorno.getDate() > dataRetorno.getUTCDate()) {
            if (dataRetorno.getMonth() > dataRetorno.getUTCMonth()) {
                dataRetorno.setFullYear(dataRetorno.getUTCFullYear(), dataRetorno.getUTCMonth(), dataRetorno.getUTCDate());
            }else{
                dataRetorno.setFullYear(dataRetorno.getFullYear(), dataRetorno.getUTCMonth(), dataRetorno.getUTCDate());
            }
        }else{
            dataRetorno.setFullYear(dataRetorno.getFullYear(), dataRetorno.getMonth(), dataRetorno.getUTCDate());
        }
    }
    dataRetorno.setHours(dataRetorno.getUTCHours());

    return dataRetorno;
  }

  limparOsInputs(){
    this.service.form.reset();
    this.service.inicializaFormGroup();
    this.resetarValidators('paciente');
    this.resetarValidators('dataInicial');
    this.resetarValidators('horaInicial');
    this.resetarValidators('dataFinal');
    this.resetarValidators('horaFinal'); 
  }

  resetarValidators(nomedoInput){
    this.service.form.get(nomedoInput).clearValidators();
    this.service.form.get(nomedoInput).updateValueAndValidity();
    this.service.form.get(nomedoInput).setValidators(Validators.required);
  }

  aplicarValidacao(nomeDoInput){
    this.service.form.get(nomeDoInput).updateValueAndValidity();
  }

  camposVazios(): boolean{
    if(this.service.form.get('paciente').value=='' ||
    this.service.form.get('dataInicial').value=='' ||
    this.service.form.get('horaInicial').value=='' ||
    this.service.form.get('dataFinal').value=='' ||
    this.service.form.get('horaFinal').value==''){
      return true;
    }
    return false;
  }

}
