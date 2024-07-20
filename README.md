# Minimal API em .NET Core 8

Este projeto é uma Minimal API desenvolvida em .NET Core 8 que utiliza um banco de dados em memória, Swagger para documentação e IHttpClientFactory para realizar chamadas em outra API.

## Tecnologias Utilizadas

- **.NET Core 8**: Framework para construção da API.
- **Entity Framework Core**: Utilizado para gerenciar o banco de dados em memória.
- **Swagger**: Ferramenta para documentação e teste da API.
- **IHttpClientFactory**: Utilizado para realizar chamadas HTTP para outra API externa.

## Endpoints

### Produtos

1. **GET /products**: Retorna todos os produtos.
2. **GET /fruitlist/{id}**: Retorna um produto pelo seu ID.
3. **POST /product**: Adiciona um novo produto à lista.

### Frutas

1. **GET /fruits**: Faz uma chamada a uma API externa para obter uma lista de frutas.

## Configuração do Ambiente

1. **Banco de Dados em Memória**: Utilizamos o Entity Framework Core com um banco de dados em memória para facilitar o desenvolvimento e testes.
2. **Swagger**: Disponível em ambiente de desenvolvimento para facilitar a documentação e o teste dos endpoints.
3. **IHttpClientFactory**: Utilizado para criar clientes HTTP configuráveis para fazer chamadas a APIs externas.

## Como Executar

1. Clone o repositório.
2. Navegue até o diretório do projeto.
3. Execute o comando `dotnet run` para iniciar a aplicação.
4. Acesse `https://localhost:5111/swagger` para visualizar a documentação e testar a API.

## Observações

- Este projeto é um exemplo simples para fins de estudo e demonstração de uso de Minimal APIs, banco de dados em memória, Swagger e IHttpClientFactory no .NET Core 8.
