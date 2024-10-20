# Student Grades API

## Descrição
A **Student Grades API** permite gerenciar estudantes e suas notas médias em diferentes disciplinas, oferecendo uma interface RESTful para operações CRUD.

## Funcionalidades
- Gerenciar Estudantes (CRUD)
- Gerenciar Notas (CRUD)
- Calcular Média de Notas

## Tecnologias
- ASP.NET Core
- Entity Framework Core
- Moq (para testes)
- MySQL

## Endpoints Principais

### Obter Todos os Estudantes
`GET /api/students`

### Obter Estudante por ID
`GET /api/students/{id}`

### Criar Novo Estudante
`POST /api/students`

### Atualizar Estudante
`PUT /api/students/{id}`

### Deletar Estudante
`DELETE /api/students/{id}`


### Obter Todas as Notas
`GET /api/grades`

### Obter Nota por ID
`GET /api/grades/{id}`

### Criar Nova Nota
`POST /api/grades`

### Atualizar Nota
`PUT /api/grades/{id}`

### Deletar Nota
`DELETE /api/grades/{id}`

