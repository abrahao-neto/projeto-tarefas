import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { TarefasCadastroComponent } from '../tarefas-cadastro/tarefas-cadastro.component';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-tarefas-consulta',
  standalone: true,
  imports: [CommonModule, TarefasCadastroComponent, FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: './tarefas-consulta.component.html',
  styleUrl: './tarefas-consulta.component.css'
})
export class TarefasConsultaComponent implements OnInit {

  //variáveis
  tarefas: any[] = [];
  mensagem: string = '';
  httpHeaders: HttpHeaders | null = null;

  //método construtor (injeção de dependência)
  constructor(
    private httpClient: HttpClient
  ) { }

  //método executado no momento em que o componente é carregado
  ngOnInit(): void {
     //capturar os dados gravados na local storage
     const auth = JSON.parse(localStorage.getItem('auth') as string);
     //adicionando o token jwt no HttpHeaders
     this.httpHeaders = new HttpHeaders({
       Authorization: 'Bearer ' + auth.accessToken
     });

  }

  // objeto do formulario
  form = new FormGroup({
    dataMin: new FormControl('', [Validators.required]),
    dataMax: new FormControl('', [Validators.required]),
  });

  //função auxiliar para exibir os erros de validação
  get f(): any {
    return this.form.controls;
  }

  // função para capturar o submit do formulário
  onSubmit(): void {
    const dataMin = this.form.value.dataMin;
    const dataMax = this.form.value.dataMax;

    if (this.httpHeaders != null) {
      //executando a consulta de tarefas na API
      this.httpClient.get(`http://localhost:5169/api/tarefas/${dataMin}/${dataMax}`, { headers: this.httpHeaders })
        .subscribe({ //capturando a resposta obtida da API
          next: (data) => {
            //armazenar os dados obtidos da consulta
            this.tarefas = data as any[];
            if (this.tarefas.length > 0) {
              this.mensagem = 'Consulta realizada com sucesso';
            } else {
              this.mensagem = 'Nenhum registro foi encontrado para as datas selecionadas';
            }
          }
      });
    }
  }

  //função para exclusão da tarefa
  onDelete(id: string): void {
    if (this.httpHeaders != null) {
      if (confirm('Deseja realmente excluir a tarefa?')) {
        this.httpClient.delete(`http://localhost:5169/api/tarefas/${id}`, { headers: this.httpHeaders }).subscribe({
          next: (data) => {
            this.onSubmit(); //recarrega a consulta
            this.mensagem = 'Tarefa excluída com sucesso';
          },
          error: (e) => {
            this.mensagem = 'Erro ao excluir tarefa';
            console.log(e.error);
          }
        });
      }
    }
  }
}
