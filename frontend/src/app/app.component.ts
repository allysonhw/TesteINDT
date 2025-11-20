import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  template: `
    <header>
      <div class="container">
        <h1>Sistema de Empréstimos - INDT</h1>
        <nav>
          <a routerLink="/propostas" routerLinkActive="active">Propostas</a>
          <a routerLink="/contratacoes" routerLinkActive="active">Contratações</a>
        </nav>
      </div>
    </header>
    <main class="container">
      <router-outlet></router-outlet>
    </main>
  `,
  styles: [`
    header {
      background: #007bff;
      color: white;
      padding: 20px 0;
      margin-bottom: 30px;
    }

    h1 {
      margin-bottom: 15px;
      font-size: 28px;
    }

    nav {
      display: flex;
      gap: 20px;
    }

    nav a {
      color: white;
      text-decoration: none;
      padding: 8px 16px;
      border-radius: 4px;
      transition: background 0.3s;
    }

    nav a:hover,
    nav a.active {
      background: rgba(255,255,255,0.2);
    }
  `]
})
export class AppComponent {
  title = 'Sistema de Empréstimos';
}
