# Restaurant

Desafio para avaliação profissional em processo seletivo da Aditum.

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
