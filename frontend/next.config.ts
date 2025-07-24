import type { NextConfig } from 'next';

const nextConfig: NextConfig = {
  async rewrites() {
    return [
      {
        source: '/api', // Chamada POST para /api
        destination: '/api/proxy',
      },
      {
        source: '/api/', // Com barra no final
        destination: '/api/proxy',
      },
    ];
  },
};

export default nextConfig;
