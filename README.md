# Parking Registration 

O sistema Parking Registration é essencial para o funcionamento completo da solução Pulse, ele
coordena todo o processo de registro de Pátios (Parkings) e os recursos principais para a efetivação
do mapeamento e organização do espaço: Gateways e Zonas (Zones).
Trata-se de uma API Restful desenvolvida em .NET com foco em boas práticas e automação.

---

## 📌 Descrição do Projeto

O **Pulse** é um sistema voltado para a **gestão inteligente de pátios**, oferecendo funcionalidades como:

- **Cálculo de gateways necessários**: com base na cobertura de cada dispositivo e na área disponível do pátio.  
- **Gerenciamento de zonas**: cada pátio é dividido em 4 zonas padrão, e o sistema sugere medidas ideais durante o cadastro do pátio.  
- **Controle de pátios**: permite cadastro, atualização, listagem e remoção de pátios, garantindo organização da estrutura.

---

## 🏗 Arquitetura

A escolha pela arquitetura em **camadas** foi feita para garantir **organização, manutenibilidade e escalabilidade** do projeto.  

Cada camada possui uma responsabilidade bem definida, permitindo maior desacoplamento e facilitando a evolução da aplicação:

- **API**: concentra apenas a exposição de endpoints RESTful e o retorno das respostas no formato correto (com HATEOAS e status codes adequados), mantendo essa camada limpa e sem lógica de negócio.  
- **Application**: atua como orquestradora, chamando serviços e coordenando o fluxo entre domínio e infraestrutura. Isso facilita a implementação de regras de negócio sem acoplamento direto à camada de apresentação ou persistência.  
- **Domain**: é o coração do sistema, onde ficam as entidades e regras de negócio. Essa camada não depende de outras, o que garante independência e testabilidade das regras de negócio.  
- **Infrastructure**: cuida do acesso a dados e integrações externas. Dessa forma, mudanças no banco de dados ou em provedores externos impactam apenas esta camada, sem afetar diretamente o domínio ou a API.  

Essa abordagem segue princípios do **Domain-Driven Design (DDD)** e **Clean Architecture**, assegurando que a lógica de negócio permaneça isolada e independente de tecnologias ou frameworks específicos.  

---

## 🔧 Tecnologias Utilizadas

- **.NET 8**
- **C#**
- **Entity Framework Core**
- **HATEOAS**
- **Swagger / OpenAPI**
- **Banco de dados Oracle**
- **Paginação**
- **JWT Bearer Authentication**
- **xUnit**
- **ML .NET**

---

## 🚀 Como Rodar a API

1. Clonar o repositório
```bash
git clone https://github.com/seu-usuario/pulse-api.git
cd pulse-api
```

2. Restaurar dependências
```bash
dotnet restore
```

3. Configurar string de conexão

No **appsettings.json**, configure sua conexão com o banco de dados:
```bash
"ConnectionStrings": {
    "SystemPulse": "Server=localhost;Database=PulseDB;User Id=sa;Password=senha123;"
}
```

4. Aplicar migrations
```bash
dotnet ef database update
```

5. Rodar a API
```bash
dotnet run
```

6. Acessar documentação Swagger
```bash
http://localhost:5000/swagger
```

---

## 🔗 Conexão com banco PaaS para deploy

Para realização do deploy da aplicação, a aplicação foi adaptada para se conectar ao banco Azure SQL Database, criado na plataforma azure.

**Passo a passo de conexão:**

1. Configuração da ConnectionString (appsettings.json):
```bash
"ConnectionStrings": {
    "SystemPulse": "$(urlConnection)"

  }
```

2. Criação das tabelas no banco:
Esse passo pode ser feito de duas formas, a primeira é criar as tabelas por migration localmente, e a segunda forma é criar as tabelas manualmente
na ferramenta "Query Editor" dentro do Azure.

Caso prefira criar manualmente, execute esses comandos SQL:
```bash
CREATE TABLE [dbo].[Employees]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Email] VARCHAR(150) NOT NULL,
    [Password] VARCHAR(200) NOT NULL,
    [Role] VARCHAR(50) NOT NULL,

    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[Gateways]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Model] VARCHAR(100) NOT NULL,
    [Status] INT NOT NULL,
    [MacAddress] VARCHAR(17) NOT NULL,
    [LastIP] VARCHAR(15) NOT NULL,
    [RegisterDate] DATETIME NOT NULL DEFAULT(GETDATE()),
    [MaxCoverageArea] FLOAT NOT NULL,
    [MaxCapacity] INT NOT NULL,
    [ParkingId] INT NOT NULL,

    CONSTRAINT [PK_Gateways] PRIMARY KEY ([Id]),

    CONSTRAINT [FK_Gateways_Parking]
        FOREIGN KEY ([ParkingId])
        REFERENCES [dbo].[Parking]([Id])
        ON DELETE CASCADE
);

CREATE TABLE [dbo].[Parkings]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(150) NOT NULL,
    [AvailableArea] FLOAT NOT NULL,
    [Capacity] INT NOT NULL,
    [RegisterDate] DATETIME NOT NULL DEFAULT(GETDATE()),
    [StructurePlan] VARBINARY(MAX) NOT NULL,
    [FloorPlan] VARBINARY(MAX) NOT NULL,

    -- Owned Type: Location
    [Street] VARCHAR(100) NOT NULL,
    [Complement] VARCHAR(50) NULL,
    [Neighborhood] VARCHAR(100) NOT NULL,
    [City] VARCHAR(100) NOT NULL,
    [State] VARCHAR(50) NOT NULL,
    [Cep] VARCHAR(9) NOT NULL,

    CONSTRAINT [PK_Parkings] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[Zones]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(100) NOT NULL,
    [Description] VARCHAR(500) NULL,
    [Width] FLOAT NOT NULL,
    [Length] FLOAT NOT NULL,
    [ParkingId] INT NOT NULL,

    CONSTRAINT [PK_Zones] PRIMARY KEY ([Id]),

    CONSTRAINT [FK_Zones_Parkings]
        FOREIGN KEY ([ParkingId])
        REFERENCES [dbo].[Parkings]([Id])
        ON DELETE CASCADE
);
```
---

## 👩‍💻 Grupo Desenvolvedor

- Gabriela de Sousa Reis RM558830
- Laura Amadeu Soares RM556690
- Raphael Lamaison Kim RM557914





