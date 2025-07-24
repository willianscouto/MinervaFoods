import { Comprador } from "@/interfaces/Comprador";
import { proxyRequest } from "@/services/common/proxyClient";

export const compradorService = {
  getAll: async (): Promise<Comprador[]> => {
    return proxyRequest<Comprador[]>({
      path: "api/compradores",
      method: "GET",
    });
  },

  getById: async (id: string): Promise<Comprador> => {
    return proxyRequest<Comprador>({
      path: `api/compradores/${id}`,
      method: "GET",
    });
  },

  create: async (carne: Omit<Comprador, "id">): Promise<Comprador> => {
    return proxyRequest<Comprador>({
      path: "api/compradores",
      method: "POST",
      data: carne,
    });
  },

  update: async (carne: Comprador): Promise<Comprador> => {
    return proxyRequest<Comprador>({
      path: `api/compradores`,
      method: "PUT",
      data: carne,
    });
  },

  delete: async (id: string): Promise<void> => {
    await proxyRequest<null>({
      path: `api/carnes/${id}`,
      method: "DELETE",
    });
  },
};
