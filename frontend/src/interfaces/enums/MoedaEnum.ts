export enum MoedaEnum {
  Unknown = 0,
  Real = 1,
  Dolar = 2,
  Euro = 3,
}

export const moedaOptions = [
  { label: 'Selecione uma moeda', value: MoedaEnum.Unknown },
  { label: 'Real', value: MoedaEnum.Real },
  { label: 'Dólar', value: MoedaEnum.Dolar },
  { label: 'Euro', value: MoedaEnum.Euro },
];
