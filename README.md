# Controle Estoque MVC
WebApp em AspNet Core MVC para controle de estoque consumindo duas api's distribuidas, sendo uma de identidade e uma de catalogo, cada api com seu banco de dados rodando em um container diferente. 

Principais implementações:

- Autenticação com Cookies no MVC consumindo uma API com JWT.
- Criação de Interface IUser para obtenção de informações via IHttpContextAccessor de usuário logado no aplicação
- Utilização de HttpClient e System.Text.Json para consumo da API de Identidade
- Tratamento de Erros de Request com criação de Middleware para interceptar exceptions de HttpRequest
- ViewComponent de Sumário para mostrar ao usuário ao Erros
- Utilização de Autenticação e Autorização via JWT Bearer
- Validação de acesso a endpoints com Claims
- DelegatingHandler, adicionando autenticação a HttpServices
- Globalização da aplicação, com validações baseadas na cultura

Comando para subir container do banco de dados:

- docker run --name identidade-db -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=senhaRoot@ -e MYSQL_DATABASE=identidade mysql:latest
- docker run --name catalogo-db -d -p 3307:3306 -e MYSQL_ROOT_PASSWORD=senhaRoot@ -e MYSQL_DATABASE=identidade mysql:latest
