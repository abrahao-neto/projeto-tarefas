import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor(private router: Router) {}

  //Metodo para verificar se usuário esta autenticado
  canActivate() {
    //ler os dados da local storage
    var auth = localStorage.getItem('auth');
    if (auth != null) {
      const data = JSON.parse(auth);
      if (data.accessToken != null) {
        const dataAtual = new Date();
        const dataExpiracao = new Date(data.dataHoraExpiracao);
        if (dataExpiracao > dataAtual)
          return true;
      }
    }

    //redirecionar de volta para a página de autenticação
    this.router.navigate(['/pages/autenticar-usuario']);
    return false;
  }
}





