'use client';

import { ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import theme from '../theme/theme';
import { ToastProvider } from '@/contexts/ToastContext';
import "./globals.css";




export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="pt-BR">
      <body>
        <ThemeProvider theme={theme}>
           <ToastProvider>
          <CssBaseline />
          {children}
          </ToastProvider>
        </ThemeProvider>
      </body>
    </html>
  );
}
