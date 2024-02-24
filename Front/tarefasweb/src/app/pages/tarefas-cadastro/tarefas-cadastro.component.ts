import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-tarefas-cadastro',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './tarefas-cadastro.component.html',
  styleUrl: './tarefas-cadastro.component.css'
})
export class TarefasCadastroComponent implements OnInit {

  //atributos
  categorias : any[] = [];
  mensagem: string = '';
  httpHeaders: HttpHeaders | null = null;

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    //capturar os dados gravados na local storage
    const auth = JSON.parse(localStorage.getItem('auth') as string);
    //adicionando o token jwt no HttpHeaders
    this.httpHeaders = new HttpHeaders({
      Authorization: 'Bearer ' + auth.accessToken
    });


    this.httpClient.get('http://localhost:5169/api/categorias', { headers: this.httpHeaders })
      .subscribe({
        next: (data) => {
          this.categorias = data as any[];
        },
        error: (e) => {
          console.log(e);
        }
      })
  }

  //criando a estrutura do formulário
  form = new FormGroup({
    nome : new FormControl('', [Validators.required]),
    descricao : new FormControl('', [Validators.required]),
    dataHora : new FormControl('', [Validators.required]),
    categoriaId : new FormControl('', [Validators.required])
  });


  //função para verificar as validações dos campos
  get f() : any {
    return this.form.controls;
  }

  //função para capturar o SUBMIT do formulário
  submit(): void {

    if(this.httpHeaders != null) {
      this.httpClient.post('http://localhost:5169/api/tarefas', this.form.value, { headers: this.httpHeaders })
      .subscribe({
        next: (data) => {
          this.mensagem = 'Tarefa cadastrada com sucesso.';
          this.form.reset(); //limpar os campos do formulário
        },
        error: (e) => {
          this.mensagem = 'Erro ao cadastrar tarefa.';
        }
      })
    }
  }
}




