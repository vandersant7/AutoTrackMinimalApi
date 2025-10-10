# AutoTrack Minimal API - Veículos com Autenticação JWT

Este projeto é uma **Minimal API** desenvolvida com **ASP.NET Core**, que permite realizar a autenticação de administradores via **JWT (JSON Web Token)** e a gestão de veículos (Cadastro, Atualização, Deleção e Consulta).

## Funcionalidades

* **Autenticação JWT** para administradores e usuários com roles específicas (Admin, Editor).
* **Cadastro de veículos** com validações de dados.
* **Consultas de veículos**, com filtros e paginação.
* **Atualização e remoção de veículos**.
* **Cadastro de administradores** e login com **token JWT**.

---

## Tecnologias Utilizadas

* **ASP.NET Core 6.0** ou superior.
* **JWT (JSON Web Token)** para autenticação.
* **Swagger** para documentação e testes da API.
* **Entity Framework Core** para interação com o banco de dados.
* **MySQL/InMemoryDatabase** (conforme o ambiente de execução).
* **AutoMapper** e **DTOs** para a transferência de dados.

---

## Como Rodar o Projeto

### Pré-requisitos

* **.NET 6.0** ou superior.
* **MySQL** (se usar banco real) ou ambiente de **InMemoryDatabase** para testes.
* **Postman** ou outra ferramenta para testar APIs REST (caso não utilize o Swagger).

### Passos para rodar o projeto:

1. Clone o repositório:

   ```bash
   git clone https://github.com/vandersant7/AutoTrackMinimalApi
   cd AutoTrackMinimalApi
   ```

2. Instale as dependências:

   ```bash
   dotnet restore
   ```

3. Crie o banco de dados (caso não use o **InMemoryDatabase**):
   Configure sua string de conexão no arquivo `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "server=localhost;database=auto_track_db;user=root;password=senha"
   }
   ```

4. Para rodar o projeto em modo de desenvolvimento, use:

   ```bash
   dotnet run
   ```

   Ou, se estiver usando o Visual Studio, apenas clique em "Run".

---

## Endpoints

### Autenticação de Administradores

* **POST** `/administrator/login`

  * Realiza o login e retorna um **JWT** para autenticação futura.
  * Corpo da requisição:

  ```json
  {
    "email": "admin@exemplo.com",
    "password": "senha123"
  }
  ```

  * Resposta:

  ```json
  {
    "email": "admin@exemplo.com",
    "profile": "Admin",
    "token": "seu_token_jwt_aqui"
  }
  ```

### Administração de Veículos

* **POST** `/veiculos` - Adicionar um novo veículo.

  * Requer autenticação com role `Admin`.
  * Corpo da requisição:

  ```json
  {
    "name": "Fusca",
    "model": "1980",
    "yearFabrication": 1980
  }
  ```

* **GET** `/veiculos` - Listar todos os veículos com paginação.

  * Exemplo de requisição:

  ```bash
  GET /veiculos?page=1
  ```

* **GET** `/veiculos/{id}` - Consultar um veículo pelo **ID**.

* **PUT** `/veiculos/{id}` - Atualizar as informações de um veículo.

  * Requer autenticação com roles `Admin` ou `Editor`.

* **DELETE** `/veiculos/{id}` - Remover um veículo.

  * Requer autenticação com role `Admin`.

### Administração de Administradores

* **GET** `/administrators` - Listar todos os administradores.

  * Requer autenticação com role `Admin`.

* **GET** `/administrator/{id}` - Consultar um administrador pelo **ID**.

* **POST** `/administrator` - Cadastrar um novo administrador.

  * Requer autenticação com role `Admin`.
  * Corpo da requisição:

  ```json
  {
    "email": "novoadmin@exemplo.com",
    "password": "senha123",
    "profile": "Admin"
  }
  ```

---

## Estrutura do Projeto

### 1. **Domain**

* **DTOs**: Objetos de transferência de dados, como `VehicleDTO`, `AdministratorDTO`.
* **Entities**: Definições de entidades, como `Vehicle` e `Administrator`.
* **Interfaces**: Interfaces para serviços de administração e veículos.
* **ModelViews**: Modelos de visualização que são usados para formar as respostas da API.

### 2. **Infrastructure**

* **Database**: Configuração do contexto de banco de dados (`AppDbContext`) e migrações.
* **Services**: Implementações dos serviços que manipulam as regras de negócio, como `VehiclesServices` e `AdministratorServices`.

### 3. **API**

* **Endpoints**: Definição dos endpoints da API.
* **Autenticação**: Configuração de autenticação JWT.

### 4. **Swagger**

* Usado para gerar a documentação da API e facilitar os testes.

---

## Testes

O projeto conta com endpoints bem definidos e pode ser testado utilizando o **Swagger UI** que estará disponível no endereço `http://localhost:<PORTA>/swagger`. Você pode testar os endpoints diretamente pela interface gerada ou usar ferramentas como **Postman**.

---

## Contribuindo

Se você deseja contribuir com este projeto, siga os passos abaixo:

1. Faça um **fork** do repositório.
2. Crie uma branch com suas modificações:

   ```bash
   git checkout -b feature/novas-funcionalidades
   ```
3. Faça as alterações necessárias e **commit**:

   ```bash
   git commit -am "Descrição das alterações"
   ```
4. Envie as alterações para o seu fork:

   ```bash
   git push origin feature/novas-funcionalidades
   ```
5. Abra um **pull request** para o repositório principal.

---
