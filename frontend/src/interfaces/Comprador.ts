export interface Comprador {
  id: string;
  nome: string;
  documento: string;
  email: string;
  telefone?: string;
  logradouro?: string;
  numero?: string;
  complemento?: string;
  bairro?: string;
  cidade?: string;
  estado?: string;
  cep?: string;
  pais?: string;
  dataNascimento?: string; 
}