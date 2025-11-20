import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PropostaService } from '../../services/proposta.service';
import { Proposta, CriarPropostaDto } from '../../models/proposta.model';

@Component({
  selector: 'app-propostas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './propostas.component.html',
  styleUrls: ['./propostas.component.css']
})
export class PropostasComponent implements OnInit {
  propostas: Proposta[] = [];
  novaProposta: CriarPropostaDto = {
    cpf: '',
    nome: '',
    idade: 0,
    renda: 0,
    score: 0,
    valorSolicitado: 0
  };
  mensagem: string = '';
  tipoMensagem: 'success' | 'error' = 'success';
  carregando: boolean = false;

  constructor(private propostaService: PropostaService) {}

  ngOnInit() {
    this.carregarPropostas();
  }

  carregarPropostas() {
    this.carregando = true;
    this.propostaService.listarPropostas().subscribe({
      next: (propostas) => {
        this.propostas = propostas;
        this.carregando = false;
      },
      error: (error) => {
        this.mostrarMensagem('Erro ao carregar propostas', 'error');
        this.carregando = false;
      }
    });
  }

  criarProposta() {
    this.carregando = true;
    this.propostaService.criarProposta(this.novaProposta).subscribe({
      next: (proposta) => {
        this.mostrarMensagem(`Proposta criada com sucesso! Status: ${proposta.status}`, 'success');
        this.carregarPropostas();
        this.limparFormulario();
        this.carregando = false;
      },
      error: (error) => {
        this.mostrarMensagem('Erro ao criar proposta', 'error');
        this.carregando = false;
      }
    });
  }

  limparFormulario() {
    this.novaProposta = {
      cpf: '',
      nome: '',
      idade: 0,
      renda: 0,
      score: 0,
      valorSolicitado: 0
    };
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
