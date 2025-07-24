export enum TipoCarneEnum {
  Unknown = 0,
  Bovina = 1,
  Suina = 2,
  Aves = 3,
  Peixe = 4,
  Outra = 5,
}

export const tipoCarneOptions = [
  { label: 'Selecione um tipo de carne', value: TipoCarneEnum.Unknown },
  { label: 'Bovina', value: TipoCarneEnum.Bovina },
  { label: 'Suína', value: TipoCarneEnum.Suina },
  { label: 'Aves', value: TipoCarneEnum.Aves },
  { label: 'Peixe', value: TipoCarneEnum.Peixe },
  { label: 'Outra', value: TipoCarneEnum.Outra },
];