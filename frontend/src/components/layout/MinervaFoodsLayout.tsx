'use client';

import {
  AppBar,
  Box,
  CssBaseline,
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Toolbar,
  Typography,
} from '@mui/material';
import Link from 'next/link';
import Image from 'next/image';
import { ReactNode } from 'react';

const drawerWidth = 240;

export default function MinervaFoodsLayout({ children }: { children: ReactNode }) {
  return (
    <Box sx={{ display: 'flex' }}>
      <CssBaseline />
      <AppBar position="fixed" sx={{ zIndex: 1201, backgroundColor: '#E84752' }}>
        <Toolbar sx={{ display: 'flex', alignItems: 'center' }}>
          <Image
            src="/minerva-logo.png" 
            alt="Minerva Logo"
            width={64}
            height={64}
            style={{ marginRight: 12 }}
          />
          <Typography variant="h6" noWrap>
           Teste Minerva Foods - Desenvolvedor: Leonardo Willians Couto
          </Typography>
        </Toolbar>
      </AppBar>

      <Drawer
        variant="permanent"
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: 'border-box' },
        }}
      >
        <Toolbar />
        <List>
          <ListItem disablePadding>
            <ListItemButton component={Link} href="/home">
              <ListItemText primary="Home" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton component={Link} href="/carnes">
              <ListItemText primary="Carnes" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton component={Link} href="/compradores">
              <ListItemText primary="Compradores" />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton component={Link} href="/pedidos">
              <ListItemText primary="Pedidos" />
            </ListItemButton>
          </ListItem>
        </List>
      </Drawer>

      <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
        <Toolbar />
        {children}
      </Box>
    </Box>
  );
}
