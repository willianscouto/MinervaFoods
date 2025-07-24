import type { NextApiRequest, NextApiResponse } from "next";
import axios from "axios";
import https from 'https';

const API_URL = process.env.NEXT_PUBLIC_API_URL;

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
  if (req.method !== "POST") {
    return res.status(405).json({ message: "Method not allowed" });
  }

  const { path, method = "GET", params, data } = req.body;

  const httpsAgent = new https.Agent({
    rejectUnauthorized: false,
  });

  try {
    const response = await axios.request({
      url: `${API_URL}/${path}`,
      method,
      params,
      data,
      httpsAgent,
    });

    //
    res.status(200).json(response.data);
  } catch (error: unknown) {
    if (axios.isAxiosError(error)) {
      res.status(error.response?.status || 500).json({
        message: "Erro ao fazer proxy da requisição",
        error: error.response?.data || error.message,
      });
    } else {
      res.status(500).json({
        message: "Erro inesperado",
        error: String(error),
      });
    }
  }
}
