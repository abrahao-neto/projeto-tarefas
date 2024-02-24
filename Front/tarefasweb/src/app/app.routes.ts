import { Routes } from '@angular/router';
import { TarefasCadastroComponent } from './pages/tarefas-cadastro/tarefas-cadastro.component';
import { TarefasConsultaComponent } from './pages/tarefas-consulta/tarefas-consulta.component';
import { CriarUsuarioComponent } from './pages/criar-usuario/criar-usuario.component';
import { AutenticarUsuarioComponent } from './pages/autenticar-usuario/autenticar-usuario.component';
import { TarefasEdicaoComponent } from './pages/tarefas-edicao/tarefas-edicao.component';
import { AuthGuard } from './app/guards/auth.guard';
import { NotAuthGuard } from './app/guards/notauth.guard';

export const routes: Routes = [
  {
    path: '', pathMatch: 'full', redirectTo: '/pages/autenticar-usuario'
  },
  {
    path: 'pages/autenticar-usuario', component: AutenticarUsuarioComponent, canActivate: [NotAuthGuard]
  },
  {
    path: 'pages/criar-usuario', component: CriarUsuarioComponent, canActivate: [NotAuthGuard]
  },
  {
    path: 'pages/tarefa-cadastro', component: TarefasCadastroComponent, canActivate: [AuthGuard]
  },
  {
    path: 'pages/tarefa-consulta', component: TarefasConsultaComponent, canActivate: [AuthGuard]
  },
  {
    path: 'pages/tarefa-edicao/:id', component: TarefasEdicaoComponent, canActivate: [AuthGuard]
  }
];
