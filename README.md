# Arquitetura da Solução

A arquitetura da solução é composta por duas camadas: o **front-end** e o **back-end**.

## Front-End (MVC)
- **ASP.NET Core MVC**: Utilizado para criar uma aplicação web interativa e responsiva. O MVC (Model-View-Controller) permite organizar o código de forma modular e escalável.
- **Views**: A aplicação é dividida em _Views_ reutilizáveis, como formulários de cadastro, listas de clientes.
- **Comunicação com o Back-End**: Utiliza HTTP para se comunicar com a API RESTful no back-end.

## Back-End (ASP.NET Core)
- **API RESTful**: Implementada usando ASP.NET Core, fornece endpoints para operações CRUD (_Create, Read, Update, Delete_) sobre os clientes.
- **Serviços**: Contém a lógica de negócios encapsulada em serviços, como `IClienteService`, que gerencia operações relacionadas aos clientes.
- **Autenticação e Autorização**: Utiliza JWT (_JSON Web Token_) para autenticação e autorização, gerenciado pelo `TokenService`.
- **Banco de Dados**: Utiliza Entity Framework Core para interagir com o banco de dados, mapeando entidades como `Cliente`, `Logradouro`, e `Usuario`.


## Decisões de Design

1. **ASP.NET Core MVC**: Escolhido para o front-end devido à sua capacidade de organizar a aplicação em _Models_, _Views_ e _Controllers_, promovendo separação de responsabilidades.
2. **ASP.NET Core**: Utilizado no back-end para criar uma API RESTful robusta e escalável, com suporte a autenticação e autorização via JWT.
3. **Entity Framework Core**: Escolhido para o acesso ao banco de dados devido à sua integração com ASP.NET Core e suporte a migrações, facilitando o gerenciamento do esquema do banco de dados.
4. **Componentização no MVC**: A aplicação é dividida em _Views_ reutilizáveis e organizadas por funcionalidades, promovendo modularidade e facilitando a manutenção do código.
5. **Serviços no Back-End**: A lógica de negócios é encapsulada em serviços, promovendo a separação de preocupações e facilitando a testabilidade.

## Como Atendem aos Requisitos do Projeto

- **Interatividade e Responsividade**: ASP.NET Core MVC permite criar uma interface de usuário rica e interativa, atendendo aos requisitos de usabilidade.
- **Segurança**: A utilização de JWT para autenticação e autorização garante que apenas usuários autenticados possam acessar determinadas funcionalidades, atendendo aos requisitos de segurança.
- **Escalabilidade**: A arquitetura baseada em serviços e a utilização de uma API RESTful permitem que a aplicação seja facilmente escalável e extensível.
- **Manutenibilidade**: A separação de preocupações entre front-end (_Views_, _Controllers_) e back-end (_Serviços_, _Repositórios_), bem como a organização modular da aplicação, facilitam sua manutenção e evolução.

Essa arquitetura proporciona uma base sólida para o desenvolvimento de uma aplicação web moderna, segura e escalável, atendendo aos requisitos do projeto de forma eficiente.

