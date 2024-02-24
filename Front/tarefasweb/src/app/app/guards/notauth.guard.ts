import { Injectable } from "@angular/core";
import { Router } from "@angular/router";


@Injectable({
    providedIn: 'root'
})
export class NotAuthGuard {


    constructor(
        private router: Router
    ) {}

    /*
        Método para verificar se o usuário
        está ou não autenticado
    */
   canActivate() {
    //ler os dados da local storage
    var auth = localStorage.getItem('auth');
    if(auth != null) {
        const data = JSON.parse(auth);
        if(data.accessToken != null) {
            const dataAtual = new Date();
            const dataExpiracao = new Date(data.dataHoraExpiracao);
            if(dataExpiracao > dataAtual) {
                this.router.navigate(['/pages/tarefa-consulta']);
                return false;
            }
        }
    }

    return true;    
   }
}



