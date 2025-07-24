import { Carne } from "@/interfaces/Carne";
import { proxyRequest } from "@/services/common/proxyClient";

export const carneService = {
  getAll: async (): Promise<Carne[]> => {
    return proxyRequest<Carne[]>({
      path: "api/carnes",
      method: "GET",
    });
  },

  getById: async (id: string): Promise<Carne> => {
    return proxyRequest<Carne>({
      path: `api/carnes/${id}`,
      method: "GET",
    });
  },

  create: async (carne: Omit<Carne, "id">): Promise<Carne> => {
    return proxyRequest<Carne>({
      path: "api/carnes",
      method: "POST",
      data: carne,
    });
  },

  update: async (carne: Carne): Promise<Carne> => {
    return proxyRequest<Carne>({
      path: `api/carnes`,
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
