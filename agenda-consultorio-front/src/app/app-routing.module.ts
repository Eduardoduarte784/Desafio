import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AgendaCadastroComponent } from './agenda-cadastro/agenda-cadastro.component';
import { AgendaConsultasComponent } from './agenda-consultas/agenda-consultas.component';



const routes: Routes = [
  { path: '', component: AgendaConsultasComponent },
  { path: 'cadastro', component: AgendaCadastroComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
