import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material';


@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(public snackBar: MatSnackBar) { }

  config: MatSnackBarConfig = {
    duration: 3000,
    horizontalPosition: 'center',
    verticalPosition: 'top'
    
  }

  success(msg){
    this.config['panelClass'] = ['notification', 'success'];
    this.snackBar.open(msg, '', this.config);
  }
    
  fail(msg){
    this.config['panelClass'] = ['notification', 'fail'];
    this.snackBar.open(msg, '', this.config);
  }

  deleted(msg){
    this.config['panelClass'] = ['notification', 'deleted'];
    this.snackBar.open(msg, '', this.config);
  }
}
