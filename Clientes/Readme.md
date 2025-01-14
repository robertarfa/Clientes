# Projeto de API de Clientes

Este projeto é uma API de gerenciamento de clientes construída com **ASP.NET Core** e **Blazor WebAssembly**. A API permite operações CRUD (_Create, Read, Update, Delete_) sobre clientes e seus logradouros, além de autenticação e autorização utilizando **JWT**.

---

## **Tecnologias Utilizadas**
- **ASP.NET Core 8**
- **Entity Framework Core**
- **Blazor WebAssembly**
- **JWT (JSON Web Token)** para autenticação
- **Swagger** para documentação da API

---

## **Configuração do Projeto**

### **Pré-requisitos**
- **.NET 8 SDK**
- **Visual Studio 2022** ou superior
- **SQL Server** (ou outro banco de dados configurado no `ApplicationDbContext`)

### **Passos para Configuração**

1. Clone o repositório:


``` 
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   
``` 

2. Configure o banco de dados:
Atualize a string de conexão no arquivo `appsettings.json`:

```   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=seu-servidor;Database=sua-base-de-dados;User Id=seu-usuario;Password=sua-senha;"
     },
     "Jwt": {
       "Key": "sua-chave-secreta-de-no-minimo-32-caracteres",
       "Issuer": "seu-issuer",
       "Audience": "sua-audience"
     }
   }
   ```

   
3. Aplique as migrações e crie o banco de dados:
No terminal do Visual Studio, execute:

``` 
   dotnet ef database update
```


4. Execute o projeto:
No Visual Studio, pressione `F5` ou execute o comando:

``` 
   dotnet run
```


---

## **Utilizando o Swagger**

O Swagger é uma ferramenta para documentação e teste interativo da API.

### Passos:
1. Inicie a aplicação:
Certifique-se de que a aplicação está em execução.

2. Acesse o Swagger:
Abra o navegador e navegue até:

``` 
   https://localhost:7146/swagger
```


3. Explore a API:
- Visualize todos os endpoints disponíveis.
- Teste requisições diretamente na interface.
- Visualize as respostas da API.

---

## **Endpoints Principais**

### **Autenticação**
- `POST /api/clientes/login`: Autentica um cliente e retorna um token JWT.

### **Clientes**
- `GET /api/clientes`: Lista todos os clientes.
- `GET /api/clientes/{id}`: Obtém um cliente pelo ID.
- `POST /api/clientes`: Cria um novo cliente.
- `PUT /api/clientes/{id}`: Atualiza um cliente existente.
- `DELETE /api/clientes/{id}`: Remove um cliente pelo ID.

### **Logradouros**
- `GET /api/logradouros`: Lista todos os logradouros.
- `GET /api/logradouros/{id}`: Obtém um logradouro pelo ID.
- `POST /api/logradouros`: Cria um novo logradouro.
- `PUT /api/logradouros/{id}`: Atualiza um logradouro existente.
- `DELETE /api/logradouros/{id}`: Remove um logradouro pelo ID.

---

Com este guia, você poderá configurar, executar e explorar a API de Clientes utilizando as ferramentas fornecidas!
