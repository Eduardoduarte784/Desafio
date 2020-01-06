import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-caixa-confirmacao',
  templateUrl: './caixa-confirmacao.component.html',
  styleUrls: ['./caixa-confirmacao.component.css']
})
export class CaixaConfirmacaoComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data,
  public dialogRef: MatDialogRef<CaixaConfirmacaoComponent>) { }

  ngOnInit() {
  }

  closeDialog(){
    this.dialogRef.close(false);
  }

}
