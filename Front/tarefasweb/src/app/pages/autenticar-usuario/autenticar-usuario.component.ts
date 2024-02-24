import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-autenticar-usuario',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './autenticar-usuario.component.html',
  styleUrl: './autenticar-usuario.component.css'
})
export class AutenticarUsuarioComponent {

  //variáveis
  message: string = '';

  constructor(private httpClient: HttpClient) {}

  //Estrutura do formulário
  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    senha: new FormControl('', [Validators.required, Validators.minLength(8)])
  });

  //exibir as mensagens de validação
  get f() {
    return this.form.controls
  }

  //função para enviar o SUBMIT do formulário
  onSubmit(): void {
    this.httpClient.post('http://localhost:5191/api/Usuarios/Autenticar', this.form.value).subscribe({
      next: (data) => {
        //salvar os dados do usuário autenticado na local storage
        localStorage.setItem('auth', JSON.stringify(data));
        //redirecionar para a página de consulta de contatos
        location.href = '/pages/tarefa-consulta';
      },
      error: (err) => {
        this.message = err.error.message;

      }
    })
  }

}
