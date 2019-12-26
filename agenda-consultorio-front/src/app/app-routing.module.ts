import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AgendaComponent } from './agenda/agenda.component';
import { AgendaConsultasComponent } from './agenda-consultas/agenda-consultas.component';


const routes: Routes = [
  { path: '', component: AgendaConsultasComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
