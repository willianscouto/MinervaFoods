import { TipoCarneEnum } from './enums/TipoCarneEnum';

export interface Carne {
  id: string;           
  ean: string;         
  nome: string;        
  tipoCarne: TipoCarneEnum; 
  unidadeMedida: string;   
}