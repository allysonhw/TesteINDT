import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Proposta, CriarPropostaDto } from '../models/proposta.model';

@Injectable({
  providedIn: 'root'
})
export class PropostaService {
  private apiUrl = 'http://localhost:5001/api/propostas';

  constructor(private http: HttpClient) { }

  criarProposta(dto: CriarPropostaDto): Observable<Proposta> {
    return this.http.post<Proposta>(this.apiUrl, dto);
  }

  obterProposta(id: string): Observable<Proposta> {
    return this.http.get<Proposta>(`${this.apiUrl}/${id}`);
  }

  listarPropostas(): Observable<Proposta[]> {
    return this.http.get<Proposta[]>(this.apiUrl);
  }

  obterPropostasPorCpf(cpf: string): Observable<Proposta[]> {
    return this.http.get<Proposta[]>(`${this.apiUrl}/cpf/${cpf}`);
  }
}
