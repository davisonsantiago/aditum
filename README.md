# Restaurant

Desafio para avaliação profissional em processo seletivo da Aditum.

A Aplicação foi desenvolvida dividida em 2 projetos: um WebAPI em .net core 3.1 e o front em VueJS.

Elementos visuais não foram explorados, pois durante a conversa não parecia ser o propósito do desafio.

O Front conta apenas com o input para informar a data e um botão para executar a busca. Caso a hora informada esteja dentro do intervalo de algum dos restaurantes, os resultados serão apresentados numa tabela.

O BackEnd conta com um HostedService configurado para executar a cada 30 minutos com o objetivo de sincronizar os dados do arquivo excel com os registrados na base sqlite incluida no projeto.

O EntityFramework foi utilizado para manipulação dos dados.

## Instalação

Acessar pasta do projeto no terminal e executar os comando do Docker para criar o container.

### API:
```bash
docker build -t restaurant-api-001 .

docker run -it -p 5000:80 -p 5001:443 --rm --name restaurant-api restaurant-api-001
```
### FRONT:

```bash
docker build -t vuejs/restaurants-app .

docker run -it -p 8080:80 --rm --name restaurant-web vuejs/restaurants-app
```
