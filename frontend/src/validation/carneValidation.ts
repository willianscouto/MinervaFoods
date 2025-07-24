// src/validation/CarneValidation.ts

import * as yup from 'yup';
import { tipoCarneOptions } from '@/interfaces/enums/TipoCarneEnum';

export const carneValidation = yup.object({
  ean: yup
    .string()
    .required('EAN é obrigatório')
    .matches(/^\d+$/, 'EAN deve conter apenas números'),

  nome: yup
    .string()
    .required('Nome é obrigatório'),

  tipoCarne: yup
    .number()
    .oneOf(
      tipoCarneOptions.map(opt => opt.value as number),
      'Tipo inválido'
    )
    .required('Tipo é obrigatório'),

  unidadeMedida: yup
    .string()
    .required('Unidade de Medida é obrigatória')
    .default('KG'),
});
