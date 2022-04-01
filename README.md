# Sistema Cálculo de Juros
Sistema para cálculo de juros, onde é informado um valor inicial, meses e taxa de juro. O resultado é o valor inicial acrescido de juros.

## Descrição
Os projetos foram nomeados utilizando um prefixo "Sp." e possuem métodos e classes para cálculo de juros e conexão entre API.
Para buscar taxas de juros pela API Sp.TaxaJuros.Web é necessário informar a propriedade "UrlJuros" no appsettings.json da aplicação Sp.CalculaJuros.Web.
Para visualizar respostas do Swagger basta adicionar /Swagger no hot de cada aplicação Web.

### Sp.CalculaJuros.Web
Projeto ASP.NET Core Web Application onde possui endpoint que recebe valores e retorna valor acrescido de juros.
Em caso de ERRO retorna valor em branco

### Sp.TaxaJuros.Web
Projeto ASP.NET Core Web Application onde possui endpoint que retorna percentual de juros.

### Sp.CalculaJuros.Domain
Projeto Class Library que possui classes de cobrança e cálculo de juros de acordo com tipo de objeto.

### Sp.CalculaJuros.Test
Projeto xUnit Test Project que possui classes e métodos para testes unitários e serviços

## Getting Started

### Dependencies

* .Net Core 5

## Version History

* 1.0
    * Versão Inicial

