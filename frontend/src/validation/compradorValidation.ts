import * as yup from "yup";
import { Comprador } from "@/interfaces/Comprador";

// Validador de e-mail reutilizável
const emailValidator = yup
  .string()
  .email("Informe um e-mail válido")
  .max(100, "O e-mail deve ter no máximo 100 caracteres");

export const compradorValidation = yup.object().shape({
  nome: yup
    .string()
    .required("O nome é obrigatório")
    .max(200, "O nome deve ter no máximo 200 caracteres"),
    
  documento: yup
    .string()
    .required("O documento é obrigatório")
    .max(20, "O documento deve ter no máximo 20 caracteres"),
    
  email: emailValidator.required("O e-mail é obrigatório"),
    
  telefone: yup
    .string()
    .nullable()
    .max(20, "O telefone deve ter no máximo 20 caracteres"),
    
  logradouro: yup
    .string()
    .nullable()
    .max(200, "O logradouro deve ter no máximo 200 caracteres"),
    
  numero: yup
    .string()
    .nullable()
    .max(20, "O número deve ter no máximo 20 caracteres"),
    
  complemento: yup
    .string()
    .nullable()
    .max(100, "O complemento deve ter no máximo 100 caracteres"),
    
  bairro: yup
    .string()
    .nullable()
    .max(100, "O bairro deve ter no máximo 100 caracteres"),
    
  cidade: yup
    .string()
    .nullable()
    .max(100, "A cidade deve ter no máximo 100 caracteres"),
    
  estado: yup
    .string()
    .nullable()
    .max(5, "O estado deve ter no máximo 5 caracteres"),
    
  cep: yup
    .string()
    .nullable()
    .max(10, "O CEP deve ter no máximo 10 caracteres"),
    
  pais: yup
    .string()
    .required("O país é obrigatório")
    .max(100, "O país deve ter no máximo 100 caracteres"),
    
  dataNascimento: yup
    .string()
    .nullable()
    .test("data-anterior", "A data de nascimento deve ser anterior à data atual", (value) => {
      if (!value) return true;
      const dataNasc = new Date(value);
      const hoje = new Date();
      return dataNasc < hoje;
    })
}) as yup.ObjectSchema<Comprador>;