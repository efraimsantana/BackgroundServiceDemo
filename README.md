# Background Service Demo

Este projeto foi criado para fins de estudo da implementação de serviços em segundo plano (background services) em .NET, utilizando o padrão Worker Service.

## Descrição

O Background Service Demo é uma aplicação .NET que demonstra como implementar um Worker Service que executa tarefas em segundo plano. O projeto utiliza Redis para distribuir locks, garantindo que em ambientes com múltiplas instâncias, apenas uma instância do worker esteja processando uma tarefa específica por vez.

## Tecnologias Utilizadas

- .NET 9.0
- Worker Service
- DistributedLock.Redis para gerenciamento de locks distribuídos
- Docker e Docker Compose para containerização

## Configuração

A aplicação pode ser configurada usando variáveis de ambiente ou o arquivo `appsettings.json`:

- **ConnectionStrings__Redis**: String de conexão para o servidor Redis (formato para variáveis de ambiente)
- **Redis** (em ConnectionStrings no appsettings.json): String de conexão para o servidor Redis

## Executando o Projeto

### Usando Docker Compose

```bash
docker-compose up
