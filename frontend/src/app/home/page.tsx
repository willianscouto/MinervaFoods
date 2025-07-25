'use client';

import { Box, Typography, Paper } from '@mui/material';

export default function HomePage() {
  return (
    <Box
      sx={{
        minHeight: '80vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        background: 'linear-gradient(135deg, #e3f2fd 0%, #ffffff 100%)',
        padding: 4,
        borderRadius: 2,
      }}
    >
      <Paper
        elevation={4}
        sx={{
          padding: 6,
          textAlign: 'center',
          maxWidth: 600,
          backgroundColor: '#ffffffcc',
          backdropFilter: 'blur(4px)',
          borderRadius: 3,
        }}
      >
        <Typography variant="h4" gutterBottom>
          Bem-vindo ao Teste MinervaFoods
        </Typography>
        <Typography variant="h6" color="text.secondary">
          Desenvolvedor: Leonardo Willians Couto
        </Typography>
      </Paper>
    </Box>
  );
}
