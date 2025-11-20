import { Routes } from '@angular/router';
import { PropostasComponent } from './components/propostas/propostas.component';
import { ContratacoesComponent } from './components/contratacoes/contratacoes.component';

export const routes: Routes = [
  { path: '', redirectTo: '/propostas', pathMatch: 'full' },
  { path: 'propostas', component: PropostasComponent },
  { path: 'contratacoes', component: ContratacoesComponent }
];
