import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material'
import { CaixaConfirmacaoComponent } from './caixa-confirmacao/caixa-confirmacao.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationService {

  constructor(private dialog:MatDialog) { }

  abrirCaixaDeConfirmacao(msg){
    return this.dialog.open(CaixaConfirmacaoComponent,{
      width: '390px',
      panelClass: 'confirm-dialog-container',
      disableClose: true,
      position: { top: "70px" },
      data: {
        message: msg
      }
    });
  }
}
