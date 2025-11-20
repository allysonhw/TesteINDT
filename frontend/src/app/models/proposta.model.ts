export interface Proposta {
  id: string;
  cpf: string;
  nome: string;
  idade: number;
  renda: number;
  score: number;
  valorSolicitado: number;
  status: string;
  taxaJuros?: number;
  motivoReprovacao?: string;
  dataCriacao: Date;
}

export interface CriarPropostaDto {
  cpf: string;
  nome: string;
  idade: number;
  renda: number;
  score: number;
  valorSolicitado: number;
}
