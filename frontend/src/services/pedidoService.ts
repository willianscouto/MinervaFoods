import { Pedido } from "@/interfaces/Pedido";
import { proxyRequest } from "@/services/common/proxyClient";

export const pedidoService = {
  getAll: async (): Promise<Pedido[]> => {
    return proxyRequest<Pedido[]>({
      path: "api/pedidos",
      method: "GET",
    });
  },

  getById: async (id: string): Promise<Pedido> => {
    return proxyRequest<Pedido>({
      path: `api/pedidos/${id}`,
      method: "GET",
    });
  },

  create: async (pedido: Omit<Pedido, "id">): Promise<Pedido> => {
    return proxyRequest<Pedido>({
      path: "api/pedidos",
      method: "POST",
      data: pedido,
    });
  },

  update: async (pedido: Pedido): Promise<Pedido> => {
    return proxyRequest<Pedido>({
      path: "api/pedidos",
      method: "PUT",
      data: pedido,
    });
  },

  delete: async (id: string): Promise<void> => {
    await proxyRequest<null>({
      path: `api/pedidos/${id}`,
      method: "DELETE",
    });
  },
};
