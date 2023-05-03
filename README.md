# Credit API

Este é um projeto criado para incrementar o portfolio afim de participar de processos seletivos. Portanto este projeto conta com overengineering propositalmente para simular um ambiente complexo de uma grande empresa.

## Sobre o projeto

Foi desenvolvido uma API REST em .NET Core 6.0 utilizando conceitos de arquitetura Hexagonal, Clean Architecture, DDD, SOLID e testes unitários automatizados. O banco de dados utilizado foi o SQL Server. 

Foi criado dois adapters, um utilizando Dapper e Stored Procedures e outro utilizando Entity Framework Core. A intenção era, além de demonstrar conhecimento nestas tecnologias, deixar que a API decida qual implementação irá utilizar, usando o conceito de inversão de dependência. O Program.cs configura e injeta conforme quiser a implementação dos adapters.

Todo o projeto seguiu o GitFlow, contendo os branches Main e Develop de onde futuramente poderia ser criado análises de código pelo SonarQube e implementar os pipelines de CI/CD para realizar deploy nos ambientes de homologação e produção.
