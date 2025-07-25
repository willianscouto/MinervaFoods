📄 README.md
md
Copiar
Editar
# 🥩 MinervaFoods - Prova Prática Full Stack

Projeto Full Stack para gestão de **Carnes, Compradores e Pedidos**, desenvolvido como parte de um desafio técnico para a MinervaFoods.

---

## 🔧 Tecnologias Utilizadas

### 🔙 Backend (.NET 8)

- ✅ .NET 8 com arquitetura **DDD (Domain-Driven Design)** e **CQRS**
- ✅ Separação em camadas: **Controller**, **Service**, **Data**, **Model**
- ✅ **Entity Framework Core** com SQL Server
- ✅ API RESTful com boas práticas REST (DTOs, validações, status codes, etc.)
- ✅ Integração com **AwesomeAPI** para conversão de moedas
- ✅ **Tratamento de erros global** com middleware
- ✅ Pronto para **Docker** e deploy

### 🧑‍🎨 Frontend (React + Next.js)

- ⚛️ **Next.js 15** com suporte a App Router (`app/`)
- 🎨 **Material UI (MUI v7)** com `@emotion` para estilização
- ✅ Formulários com `react-hook-form` + `yup` para validação
- 📅 `date-fns` para manipulação de datas
- 🔁 Comunicação com backend via `axios`
- 🔄 Feedbacks visuais com `ToastContext`
- 🧭 Navegação com layout persistente (Sidebar, layout per page)
- 🧪 Validação de campos obrigatórios e lógicos
- 🛑 Modal de confirmação antes de exclusão

#### 📦 Dependências principais do frontend

```
"dependencies": {
  "@emotion/react": "^11.14.0",
  "@emotion/styled": "^11.14.1",
  "@hookform/resolvers": "^5.1.1",
  "@mui/icons-material": "^7.2.0",
  "@mui/material": "^7.2.0",
  "axios": "^1.11.0",
  "date-fns": "^4.1.0",
  "next": "15.4.3",
  "react": "19.1.0",
  "react-dom": "19.1.0",
  "react-hook-form": "^7.61.0",
  "uuid": "^11.1.0",
  "yup": "^1.6.1"
} 
```

### 🗂 Estrutura do Projeto

```
📁 backend
├── src
│   ├── MinervaFoods.Api            # Camada Controller (Endpoints HTTP)
│   ├── MinervaFoods.Service        # Lógica de negócio
│   ├── MinervaFoods.Data           # Repositórios, EF Core, Contextos
│   └── MinervaFoods.Model          # Entidades de domínio (Carne, Comprador, Pedido)
```
```
📁 frontend
├── app/
│   ├── carnes/
│   ├── compradores/
│   ├── pedidos/
│   └── home/                      # Tela inicial com boas-vindas
├── components/                   # Componentes compartilhados
├── contexts/                     # ToastContext e outros
├── theme/                        # MUI custom theme
├── utils/                        # Formatação de datas, moedas
└── layout/                       # MinervaFoodsLayout com sidebar
```

### ▶️ Como Executar o Projeto Localmente
Pré-requisitos
Docker 

.NET 8 SDK

Node.js + npm

### 🔙 Backend (.NET 8)

cd backend

#### Apagar migrações antigas (se houver)
rm -r src/MinervaFoods.Data/Migrations

#### Aplicar as migrações e atualizar o banco
dotnet ef database update --verbose \
  --project src/MinervaFoods.Data \
  --startup-project src/MinervaFoods.Api

#### Rodar a API
dotnet run --project src/MinervaFoods.Api
A API será exposta em: https://localhost:8081 ou http://localhost:8080

### 🧑‍🎨 Frontend (React + Next.js)

cd frontend

#### Instalar dependências
npm install

#### Rodar o projeto em modo desenvolvimento
npm run dev
A aplicação estará acessível em: http://localhost:3000

#### 📚 Funcionalidades Implementadas
 CRUD de carnes com origem

 CRUD de compradores com cidade/estado

 Criação e edição de pedidos com carnes + comprador + moeda + preço

 Conversão de moeda (Dólar/Euro) via API externa

 Restrições de exclusão (bloquear carnes ou compradores com pedidos vinculados)

 Confirmação antes de excluir

 Layout persistente com sidebar de navegação

 Tela inicial com boas-vindas personalizada

#### ✨ Diferenciais Implementados
#### 🔎 Filtro por comprador e data na listagem de pedidos

#### ✅ Validações client-side e server-side (nome obrigatório, preço positivo, etc.)

#### 🧼 Código limpo, modularizado e comentado

#### 📣 Feedback visual de sucesso/erro (toast notifications)

#### 🧑‍💻 Desenvolvido por
Leonardo Willians Couto

