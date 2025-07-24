// src/components/Toast.tsx
import React from 'react';
import { Snackbar, Alert, AlertColor } from '@mui/material';

interface ToastProps {
  open: boolean;
  message: string;
  severity?: AlertColor; // 'error' | 'success' | 'info' | 'warning'
  onClose: () => void;
  duration?: number;
}

export default function Toast({
  open,
  message,
  severity = 'info',
  onClose,
  duration = 4000,
}: ToastProps) {
  return (
    <Snackbar
      open={open}
      autoHideDuration={duration}
      onClose={onClose}
      anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
    >
      <Alert onClose={onClose} severity={severity} sx={{ width: '100%' }}>
        {message}
      </Alert>
    </Snackbar>
  );
}
