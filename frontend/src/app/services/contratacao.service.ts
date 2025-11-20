import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contratacao, CriarContratacaoDto } from '../models/contratacao.model';

@Injectable({
  providedIn: 'root'
})
export class ContratacaoService {
  private apiUrl = 'http://localhost:5002/api/contratacoes';

  constructor(private http: HttpClient) { }

  criarContratacao(dto: CriarContratacaoDto): Observable<Contratacao> {
    return this.http.post<Contratacao>(this.apiUrl, dto);
  }

  obterContratacao(id: string): Observable<Contratacao> {
    return this.http.get<Contratacao>(`${this.apiUrl}/${id}`);
  }

  listarContratacoes(): Observable<Contratacao[]> {
    return this.http.get<Contratacao[]>(this.apiUrl);
  }

  obterContratacoesPorCpf(cpf: string): Observable<Contratacao[]> {
    return this.http.get<Contratacao[]>(`${this.apiUrl}/cpf/${cpf}`);
  }
}
