export interface Contratacao {
  id: string;
  propostaId: string;
  cpf: string;
  valorEmprestimo: number;
  taxaJuros: number;
  status: string;
  motivoReprovacao?: string;
  dataContratacao: Date;
}

export interface CriarContratacaoDto {
  propostaId: string;
}
