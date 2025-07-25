import * as yup from "yup";
import { MoedaEnum } from "@/interfaces/enums/MoedaEnum";
import { StatusPedidoEnum } from "@/interfaces/enums/StatusPedidoEnum";
import { PedidoItem } from "@/interfaces/PedidoItem";
import { Pedido } from "@/interfaces/Pedido";


const valoresMoeda = Object.values(MoedaEnum).filter(
  (v) => typeof v === "number" && v !== MoedaEnum.Unknown
) as MoedaEnum[];

const statusPedidoValores = Object.values(StatusPedidoEnum).filter(
  (v) => typeof v === "number" && v !== StatusPedidoEnum.Unknown
) as StatusPedidoEnum[];

// Validação dos itens do pedido
const pedidoItemValidation = yup.object().shape({
  id: yup.string().notRequired(),
  pedidoId: yup.string().notRequired(),
  carneId: yup.string().required("Carne é obrigatória"),
  quantidade: yup
    .number()
    .required("Quantidade é obrigatória")
    .moreThan(0, "Quantidade deve ser maior que zero"),
  moeda: yup
    .mixed<MoedaEnum>()
    .oneOf(valoresMoeda)
    .required("Moeda é obrigatória"),
  precoUnitario: yup
    .number()
    .required("Preço unitário é obrigatório")
    .min(0, "Preço unitário não pode ser negativo"),
  total: yup.number().nullable().default(null),
  cotacao: yup.number().nullable().notRequired(),
  valorTotalCotacao: yup.number().nullable().notRequired(),
}) as yup.ObjectSchema<PedidoItem>;

// Validação do pedido completo
export const pedidoValidation =yup.object().shape({
  numeroPedido: yup.string().nullable().notRequired().default(null),
  compradorId: yup.string().required("Comprador é obrigatório"),
  dataPedido: yup
    .string()
    .nullable()
    .test("max-date", "A data do pedido não pode ser no futuro", (value) => {
      return !value || new Date(value) <= new Date();
    })
    .default(null),
  statusPedido: yup
    .mixed<StatusPedidoEnum>()
    .oneOf(statusPedidoValores, "Status Pedido inválida")
    .required("Status Pedido é obrigatória"),
  pedidoItem: yup
    .array()
    .of(pedidoItemValidation)
    .min(1, "O pedido deve conter pelo menos um item")
    .required("Itens são obrigatórios"),
  valorTotal: yup
    .number()
    .typeError("Valor total deve ser um número")
    .min(0, "Valor total não pode ser negativo")
    .notRequired()
    .default(null),
  observacoes: yup
    .string()
    .max(500, "Observações devem ter no máximo 500 caracteres")
    .notRequired()
    .default(null),
}) as yup.ObjectSchema<Pedido>;
