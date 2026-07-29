# ProjetoHELIOS_API
Projeto de dotnet da global solution

Integrantes: 

   Arthur dos Santos Cabral, 566515, 2TDSA

   Bruno Martins Bettio, 564939, 2TDSA

   José Diogo da Silva Neves, 562341, 2TDSA 

   Júlia Tiziotto Buttler, 564975, 2TDSA 

   Mariana Xavier Quispe, 566357, 2TDSA 

link do repositório: https://github.com/JuliaTButtler/ProjetoHELIOS_API.git
Link do video de desenvolvimento: https://youtu.be/TpZ5cX0QgS4
Link do video pitch: https://youtu.be/oT4gtPXb6eU

# HELIOS API

## Descrição do Projeto

O **HELIOS** é uma API REST desenvolvida para gerenciamento inteligente de habitats espaciais autônomos, simulando operações realizadas em ambientes extremos como estações lunares, bases marcianas e instalações remotas.

O sistema tem como objetivo centralizar o monitoramento operacional dos habitats, controlar ocupação dos módulos, registrar leituras de sensores, gerar alertas automáticos e manter histórico de eventos críticos.

A API foi construída utilizando **ASP.NET Core Web API**, com persistência em **Oracle Database** por meio do **Entity Framework Core** e utilização de **Migrations** para gerenciamento do banco de dados.

A arquitetura foi organizada em entidades relacionadas para representar o funcionamento completo do ambiente monitorado.

---

# Desenvolvimento 

O desenvolvimento da API HELIOS foi realizado utilizando ASP.NET Core Web API seguindo arquitetura baseada em Controllers + Entity Framework Core + Oracle Database.

Inicialmente foi realizada a modelagem das entidades do sistema considerando as regras de negócio dos habitats espaciais inteligentes.

Após a modelagem:

Criação das classes de modelo (Models);
Configuração do AppDbContext;
Definição dos relacionamentos entre entidades;
Geração das migrations;
Criação automática do banco Oracle;
Implementação dos Controllers REST;
Testes via Swagger.

## Tecnologias Utilizadas

* ASP.NET Core Web API
* Entity Framework Core
* Oracle Database
* Oracle SQL Developer
* Oracle Entity Framework Core Provider
* Swagger / OpenAPI
* C#
* .NET 8

## Diagrama — Processo de Desenvolvimento da API HELIOS

```text
INÍCIO
 │
 ▼
Criar pasta do projeto
HeliosAPI
 │
 ▼
Criar repositório Git
ProjetoHELIOS_API
 │
 ▼
Abrir no VS Code
 │
 ▼
Criar projeto Web API
dotnet new webapi
 │
 ▼
Restaurar e compilar
dotnet restore
dotnet build
 │
 ▼
Ajustar versão do .NET
(net10 → net8)
 │
 ├── Editar .csproj
 ├── Atualizar pacotes
 ├── Remover OpenApi incompatível
 └── Instalar Swagger
 │
 ▼
Configurar ambiente
dotnet run
global.json
SDK 8 fixado
 │
 ▼
Criar estrutura do projeto
 │
 ├── Models
 ├── Data
 └── AppDbContext
 │
 ▼
Criar modelos iniciais
 │
 ├── Habitat
 ├── ModuloHabitacional
 └── Usuario
 │
 ▼
Instalar dependências
 │
 ├── Entity Framework Core
 ├── EF Core Design
 └── Oracle.EntityFrameworkCore
 │
 ▼
Configurar aplicação
 │
 ├── Program.cs
 └── appsettings.json
 │
 ▼
Criar modelos restantes
 │
 ├── Ocupante
 ├── Reserva
 ├── Sensor
 ├── LeituraSensor
 ├── Alerta
 ├── RegraAlerta
 ├── AcaoAutomatica
 └── LogEvento
 │
 ▼
Configurar AppDbContext
 │
 ├── DbSets
 ├── Relacionamentos
 ├── Precision
 └── Constraints
 │
 ▼
Gerar Migration
dotnet ef migrations add InitialCreate
 │
 ▼
Aplicar Banco Oracle
dotnet ef database update
 │
 ▼
Criar Controllers
 │
 ├── Habitat
 ├── ModuloHabitacional
 ├── Usuario
 ├── Ocupante
 ├── Reserva
 ├── Sensor
 ├── LeituraSensor
 ├── Alerta
 ├── RegraAlerta
 ├── AcaoAutomatica
 └── LogEvento
 │
 ▼
Executar aplicação
dotnet run
 │
 ▼
Realizar testes
Swagger
 │
 ▼
FIM
```


# Funcionalidades Implementadas

A API HELIOS possui as seguintes funcionalidades:

### Gestão de Habitats

* Cadastro de habitats espaciais;
* Consulta por ID;
* Consulta por tipo de habitat;
* Consulta por status operacional;
* Atualização e remoção de registros.

---

### Gestão de Módulos Habitacionais

* Cadastro de módulos vinculados a habitats;
* Controle de ocupação;
* Classificação por nível de risco;
* Consulta por habitat;
* Consulta por status.

---

### Gestão de Usuários

* Cadastro de usuários administrativos e operacionais;
* Controle de nível de acesso;
* Validação de e-mail único.

---

### Gestão de Ocupantes

* Registro de ocupantes do habitat;
* Controle de status;
* Associação com reservas.

---

### Gestão de Reservas

* Criação de reservas para módulos;
* Controle de período de ocupação;
* Associação entre ocupantes e módulos.

---

### Monitoramento por Sensores

* Cadastro de sensores;
* Definição de limites mínimos e máximos;
* Controle de frequência de leitura.

---

### Registro de Leituras

* Armazenamento de leituras dos sensores;
* Registro temporal dos dados coletados;
* Identificação do status das leituras.

---

### Sistema de Alertas

* Geração e gerenciamento de alertas;
* Classificação por criticidade;
* Registro de ações corretivas.

---

### Regras de Alerta

* Configuração de limites e pesos de risco;
* Ativação e desativação de regras.

---

### Automação Operacional

* Registro de ações automáticas executadas pelo sistema;
* Associação com alertas gerados.

---

### Histórico de Eventos

* Registro de eventos operacionais;
* Armazenamento de origem e nível dos eventos.

---

# Objetivo da API

A API HELIOS foi desenvolvida com o objetivo de fornecer uma solução centralizada para gerenciamento operacional e monitoramento de habitats espaciais.

A proposta do sistema é permitir:

* Controle da infraestrutura dos habitats;
* Monitoramento contínuo de condições ambientais;
* Gestão de ocupação dos módulos;
* Identificação preventiva de situações de risco;
* Registro histórico de ocorrências;
* Apoio à tomada de decisão baseada em dados.

O sistema busca simular cenários de automação e suporte operacional em ambientes críticos e de alta complexidade.

---

# Documentação das Rotas

---

## Habitat

| Método | Rota                       | Descrição         |
| ------ | -------------------------- | ----------------- |
| GET    | `/habitat`                 | Listar habitats   |
| GET    | `/habitat/{id}`            | Buscar por ID     |
| GET    | `/habitat/status/{status}` | Buscar por status |
| GET    | `/habitat/tipo/{tipo}`     | Buscar por tipo   |
| POST   | `/habitat`                 | Criar habitat     |
| PUT    | `/habitat/{id}`            | Atualizar habitat |
| DELETE | `/habitat/{id}`            | Remover habitat   |

---

## ModuloHabitacional

| Método | Rota                                  |
| ------ | ------------------------------------- |
| GET    | `/modulohabitacional`                 |
| GET    | `/modulohabitacional/{id}`            |
| GET    | `/modulohabitacional/habitat/{id}`    |
| GET    | `/modulohabitacional/status/{status}` |
| POST   | `/modulohabitacional`                 |
| PUT    | `/modulohabitacional/{id}`            |
| DELETE | `/modulohabitacional/{id}`            |

---

## Usuario

| Método | Rota                     |
| ------ | ------------------------ |
| GET    | `/usuario`               |
| GET    | `/usuario/{id}`          |
| GET    | `/usuario/email/{email}` |
| GET    | `/usuario/tipo/{tipo}`   |
| POST   | `/usuario`               |
| PUT    | `/usuario/{id}`          |
| DELETE | `/usuario/{id}`          |

---

## Ocupante

| Método | Rota                        |
| ------ | --------------------------- |
| GET    | `/ocupante`                 |
| GET    | `/ocupante/{id}`            |
| GET    | `/ocupante/status/{status}` |
| POST   | `/ocupante`                 |
| PUT    | `/ocupante/{id}`            |
| DELETE | `/ocupante/{id}`            |

---

## Reserva

| Método | Rota                     |
| ------ | ------------------------ |
| GET    | `/reserva`               |
| GET    | `/reserva/{id}`          |
| GET    | `/reserva/ocupante/{id}` |
| GET    | `/reserva/modulo/{id}`   |
| POST   | `/reserva`               |
| PUT    | `/reserva/{id}`          |
| DELETE | `/reserva/{id}`          |

---

## Sensor

| Método | Rota                      |
| ------ | ------------------------- |
| GET    | `/sensor`                 |
| GET    | `/sensor/{id}`            |
| GET    | `/sensor/modulo/{id}`     |
| GET    | `/sensor/status/{status}` |
| POST   | `/sensor`                 |
| PUT    | `/sensor/{id}`            |
| DELETE | `/sensor/{id}`            |

---

## LeituraSensor

| Método | Rota                         |
| ------ | ---------------------------- |
| GET    | `/leiturasensor`             |
| GET    | `/leiturasensor/{id}`        |
| GET    | `/leiturasensor/sensor/{id}` |
| POST   | `/leiturasensor`             |
| PUT    | `/leiturasensor/{id}`        |
| DELETE | `/leiturasensor/{id}`        |

---

## Alerta

| Método | Rota                      |
| ------ | ------------------------- |
| GET    | `/alerta`                 |
| GET    | `/alerta/{id}`            |
| GET    | `/alerta/modulo/{id}`     |
| GET    | `/alerta/status/{status}` |
| POST   | `/alerta`                 |
| PUT    | `/alerta/{id}`            |
| DELETE | `/alerta/{id}`            |

---

## RegraAlerta

| Método | Rota                          |
| ------ | ----------------------------- |
| GET    | `/regraalerta`                |
| GET    | `/regraalerta/{id}`           |
| GET    | `/regraalerta/ativo/{status}` |
| POST   | `/regraalerta`                |
| PUT    | `/regraalerta/{id}`           |
| DELETE | `/regraalerta/{id}`           |

---

## AcaoAutomatica

| Método | Rota                          |
| ------ | ----------------------------- |
| GET    | `/acaoautomatica`             |
| GET    | `/acaoautomatica/{id}`        |
| GET    | `/acaoautomatica/alerta/{id}` |
| POST   | `/acaoautomatica`             |
| PUT    | `/acaoautomatica/{id}`        |
| DELETE | `/acaoautomatica/{id}`        |

---

## LogEvento

| Método | Rota                     |
| ------ | ------------------------ |
| GET    | `/logevento`             |
| GET    | `/logevento/{id}`        |
| GET    | `/logevento/tipo/{tipo}` |
| POST   | `/logevento`             |
| PUT    | `/logevento/{id}`        |
| DELETE | `/logevento/{id}`        |

# Swagger

A documentação completa da API pode ser acessada via Swagger após executar o projeto:

https://localhost:5263/swagger

# Instalação e Execução

## Pré-requisitos

Antes de executar o projeto, é necessário possuir instalado:

- .NET 8 SDK
- Visual Studio 2022
- Oracle Database
- Oracle SQL Developer

---

# Clonar o repositório

git clone https://github.com/JuliaTButtler/ProjetoHELIOS_API.git

---

# Abrir o projeto

Abra o arquivo no Visual Studio 2022.

Os pacotes NuGet utilizados no projeto serão restaurados automaticamente ao abrir a solução.

---

# Configurar conexão com o banco Oracle
No arquivo:

appsettings.json

configure sua string de conexão Oracle:

"ConnectionStrings": {
  "DefaultConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=localhost:1521/XEPDB1;"
}

---

# Executar as migrations

As tabelas e migrations já foram criadas previamente durante o desenvolvimento do projeto, caso queria recriar manualmente digite no terminal:

dotnet ef migrations add InitialCreate

Caso deseje recriar o banco manualmente, utilize:

dotnet ef database update

no terminal.

---

# Executar a aplicação

Via Visual Studio:

F5

Ou via terminal:

dotnet run

---

# Acessar Swagger

Após executar o projeto, a documentação da API estará disponível em:

https://localhost:5263/swagger

---

# Estrutura do Projeto

O projeto segue arquitetura baseada em:

* Controllers
* Models
* Entity Framework Core
* DbContext
* Oracle Database

---

# Observações

* O projeto utiliza ASP.NET Core Web API seguindo padrão REST.
* O banco de dados utilizado é Oracle.
* O Swagger foi configurado para documentação e testes dos endpoints.

# EXEMPLOS DE TESTES 


# Habitat (POST `/api/habitat`)

### Exemplo 2

```json
{
  "id": 2,
  "nome": "Artemis Beta",
  "localizacao": "Lua - Setor Sul",
  "tipoHabitat": "PESQUISA",
  "capacidadeTotal": 80,
  "statusOperacional": "OPERACIONAL"
}
```

### Exemplo 3

```json
{
  "id": 3,
  "nome": "Mars One",
  "localizacao": "Marte - Cratera Gale",
  "tipoHabitat": "EXPLORACAO",
  "capacidadeTotal": 120,
  "statusOperacional": "MANUTENCAO"
}
```

---

# ModuloHabitacional (POST `/api/modulohabitacional`)

### Exemplo 2

```json
{
  "id": 2,
  "habitatId": 1,
  "nomeModulo": "Laboratório Norte",
  "tipoModulo": "PESQUISA",
  "capacidadeOcupantes": 15,
  "ocupacaoAtual": 8,
  "statusModulo": "ATIVO",
  "nivelRisco": "MEDIO",
  "indiceRisco": 31.70
}
```

### Exemplo 3

```json
{
  "id": 3,
  "habitatId": 2,
  "nomeModulo": "Dormitório Sul",
  "tipoModulo": "RESIDENCIAL",
  "capacidadeOcupantes": 30,
  "ocupacaoAtual": 18,
  "statusModulo": "ATIVO",
  "nivelRisco": "BAIXO",
  "indiceRisco": 8.50
}
```

---

# Usuario (POST `/api/usuario`)

### Exemplo 2

```json
{
  "id": 2,
  "nome": "Maria Costa",
  "email": "maria@helios.com",
  "senha": "senha456",
  "tipoUsuario": "OPERADOR",
  "statusUsuario": "ATIVO",
  "nivelAcesso": 6
}
```

### Exemplo 3

```json
{
  "id": 3,
  "nome": "Carlos Mendes",
  "email": "carlos@helios.com",
  "senha": "abc123",
  "tipoUsuario": "ANALISTA",
  "statusUsuario": "INATIVO",
  "nivelAcesso": 4
}
```

---

# Ocupante (POST `/api/ocupante`)

### Exemplo 2

```json
{
  "id": 2,
  "nome": "Ana Rodrigues",
  "funcao": "Engenheira",
  "statusOcupante": "ATIVO"
}
```

### Exemplo 3

```json
{
  "id": 3,
  "nome": "Pedro Lima",
  "funcao": "Médico",
  "statusOcupante": "EM_MISSAO"
}
```

---

# Reserva (POST `/api/reserva`)

### Exemplo 2

```json
{
  "id": 2,
  "ocupanteId": 2,
  "moduloId": 2,
  "dataInicio": "2026-06-10T08:00:00",
  "dataFim": "2026-06-20T08:00:00",
  "statusReserva": "ATIVA"
}
```

### Exemplo 3

```json
{
  "id": 3,
  "ocupanteId": 3,
  "moduloId": 3,
  "dataInicio": "2026-07-01T09:00:00",
  "dataFim": null,
  "statusReserva": "PENDENTE"
}
```

---

# Sensor (POST `/api/sensor`)

### Exemplo 2

```json
{
  "id": 2,
  "moduloId": 2,
  "nomeSensor": "Sensor Oxigênio",
  "tipoSensor": "OXIGENIO",
  "statusSensor": "ATIVO",
  "unidadeMedida": "%",
  "limiteMinimo": 18,
  "limiteMaximo": 22,
  "intervaloLeituraSegundos": 30
}
```

### Exemplo 3

```json
{
  "id": 3,
  "moduloId": 3,
  "nomeSensor": "Sensor Pressão",
  "tipoSensor": "PRESSAO",
  "statusSensor": "ATIVO",
  "unidadeMedida": "kPa",
  "limiteMinimo": 95,
  "limiteMaximo": 110,
  "intervaloLeituraSegundos": 45
}
```

---

# LeituraSensor (POST `/api/leiturasensor`)

### Exemplo 2

```json
{
  "id": 2,
  "sensorId": 2,
  "valorLeitura": 20.5,
  "statusLeitura": "NORMAL"
}
```

### Exemplo 3

```json
{
  "id": 3,
  "sensorId": 3,
  "valorLeitura": 112,
  "statusLeitura": "CRITICO"
}
```

---

# Alerta (POST `/api/alerta`)

### Exemplo 2

```json
{
  "id": 2,
  "moduloId": 2,
  "sensorId": 2,
  "tipoAlerta": "OXIGENIO",
  "mensagem": "Nível abaixo do ideal",
  "nivelCriticidade": "MEDIO",
  "statusAlerta": "ABERTO",
  "acaoCorretiva": "Liberar oxigênio"
}
```

### Exemplo 3

```json
{
  "id": 3,
  "moduloId": 3,
  "sensorId": 3,
  "tipoAlerta": "PRESSAO",
  "mensagem": "Pressão crítica",
  "nivelCriticidade": "CRITICO",
  "statusAlerta": "EM_ANALISE",
  "acaoCorretiva": "Isolar módulo"
}
```

---

# RegraAlerta (POST `/api/regraalerta`)

### Exemplo 2

```json
{
  "id": 2,
  "tipoSensor": "OXIGENIO",
  "valorMinimo": 18,
  "valorMaximo": 22,
  "nivelCriticidade": "MEDIO",
  "pesoRisco": 7,
  "mensagemPadrao": "Oxigênio fora do padrão",
  "ativo": "S"
}
```

### Exemplo 3

```json
{
  "id": 3,
  "tipoSensor": "PRESSAO",
  "valorMinimo": 95,
  "valorMaximo": 110,
  "nivelCriticidade": "CRITICO",
  "pesoRisco": 10,
  "mensagemPadrao": "Pressão perigosa",
  "ativo": "S"
}
```

---

# AcaoAutomatica (POST `/api/acaoautomatica`)

### Exemplo 2

```json
{
  "id": 2,
  "alertaId": 2,
  "descricao": "Ativar reserva de oxigênio",
  "statusAcao": "EXECUTADA"
}
```

### Exemplo 3

```json
{
  "id": 3,
  "alertaId": 3,
  "descricao": "Fechamento automático do módulo",
  "statusAcao": "PENDENTE"
}
```

---

# LogEvento (POST `/api/logevento`)

### Exemplo 2

```json
{
  "id": 2,
  "tipoEvento": "SENSOR",
  "descricao": "Nova leitura registrada",
  "origemEvento": "Sensor Oxigênio",
  "nivelEvento": "NORMAL"
}
```

### Exemplo 3

```json
{
  "id": 3,
  "tipoEvento": "ACAO",
  "descricao": "Sistema executou contenção",
  "origemEvento": "Módulo Central",
  "nivelEvento": "CRITICO"
}
```



