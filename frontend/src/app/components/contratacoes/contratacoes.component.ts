import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ContratacaoService } from '../../services/contratacao.service';
import { PropostaService } from '../../services/proposta.service';
import { Contratacao } from '../../models/contratacao.model';
import { Proposta } from '../../models/proposta.model';

@Component({
  selector: 'app-contratacoes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contratacoes.component.html',
  styleUrls: ['./contratacoes.component.css']
})
export class ContratacoesComponent implements OnInit {
  contratacoes: Contratacao[] = [];
  propostasAprovadas: Proposta[] = [];
  propostaIdSelecionada: string = '';
  mensagem: string = '';
  tipoMensagem: 'success' | 'error' = 'success';
  carregando: boolean = false;

  constructor(
    private contratacaoService: ContratacaoService,
    private propostaService: PropostaService
  ) {}

  ngOnInit() {
    this.carregarContratacoes();
    this.carregarPropostasAprovadas();
  }

  carregarContratacoes() {
    this.carregando = true;
    this.contratacaoService.listarContratacoes().subscribe({
      next: (contratacoes) => {
        this.contratacoes = contratacoes;
        this.carregando = false;
      },
      error: (error) => {
        this.mostrarMensagem('Erro ao carregar contratações', 'error');
        this.carregando = false;
      }
    });
  }

  carregarPropostasAprovadas() {
    this.propostaService.listarPropostas().subscribe({
      next: (propostas) => {
        this.propostasAprovadas = propostas.filter(p => p.status === 'Aprovada');
      },
      error: (error) => {
        console.error('Erro ao carregar propostas', error);
      }
    });
  }

  criarContratacao() {
    if (!this.propostaIdSelecionada) {
      this.mostrarMensagem('Selecione uma proposta', 'error');
      return;
    }

    this.carregando = true;
    this.contratacaoService.criarContratacao({ propostaId: this.propostaIdSelecionada }).subscribe({
      next: (contratacao) => {
        this.mostrarMensagem(`Contratação criada! Status: ${contratacao.status}`, 'success');
        this.carregarContratacoes();
        this.carregarPropostasAprovadas();
        this.propostaIdSelecionada = '';
        this.carregando = false;
      },
      error: (error) => {
        const mensagemErro = error.error?.message || error.error || 'Erro ao criar contratação';
        this.mostrarMensagem(mensagemErro, 'error');
        this.carregando = false;
      }
    });
  }

  mostrarMensagem(mensagem: string, tipo: 'success' | 'error') {
    this.mensagem = mensagem;
    this.tipoMensagem = tipo;
    setTimeout(() => {
      this.mensagem = '';
    }, 5000);
  }

  getStatusClass(status: string): string {
    return `status-${status.toLowerCase()}`;
  }
}
