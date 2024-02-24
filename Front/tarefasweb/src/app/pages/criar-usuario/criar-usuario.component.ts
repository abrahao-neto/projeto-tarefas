import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';


@Component({
  selector: 'app-criar-usuario',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './criar-usuario.component.html',
  styleUrl: './criar-usuario.component.css'
})
export class CriarUsuarioComponent {


  //atributos
  mensagemSucesso: string = '';
  mensagemErro: string = '';


  //método construtor
  constructor(
    private httpClient: HttpClient
  ) {}


  //estrutura do formulário
  form = new FormGroup({
    nome: new FormControl('', [
      Validators.required, Validators.minLength(8), Validators.maxLength(100)
    ]),
    email : new FormControl('', [
      Validators.required, Validators.email
    ]),
    senha: new FormControl('', [
      Validators.required,
      Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
    ]),
    senhaConfirmacao: new FormControl('', [
      Validators.required
    ])
  })


  //função para exibir as mensagens de erro de validação
  get f() {
    return this.form.controls;
  }


  //função para capturar o SUBMIT do formulário
  onSubmit(): void {

    //limpar as mensagens
    this.mensagemSucesso = '';
    this.mensagemErro = '';


    //verificando se as senhas estão iguais
    if(this.form.value.senha == this.form.value.senhaConfirmacao) {


      //enviando uma requisição POST para a API de usuários (criar usuário)
      this.httpClient.post('http://localhost:5191/api/usuarios/criar', this.form.value)
        .subscribe({
          next: (data) => {
            this.mensagemSucesso = 'Usuário cadastrado com sucesso.';
            this.form.reset();
          },
          error: (e) => {
            if(e.status == 400){
              this.mensagemErro = e.error.message;
            }
            else{
              this.mensagemErro = 'Falha ao cadastrar o usuário.';
            }
          }
        });
    }
    else {
      this.mensagemErro = 'Senhas não conferem, por favor verifique.';
    }


  }


}





