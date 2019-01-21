# Desafio ESX

Projeto desenvolvido em .net core 2.1, todos os acessos a dados foram feitos utilizando Stored Procedures utilizando *Dapper*.
Construido em camadas e utilizando IoC, autenticacao feita via JWT e mapeamento dos modelos com AutoMapper.

Para acesso na api ser feito de maneira mais facil foi utilizado o Swagger, permitindo que o usuario faca o consumo da api de modo mais rapido e facil.

## O mesmo tem como objetivo ter os seguintes itens:
 
**Patrimônio**

*Campos*:
- Nome - obrigatório
- MarcaId - obrigatório
- Descrição
- Nº do tombo

*Endpoints*:

- GET patrimonios - Obter todos os patrimônios
- GET patrimonios/{id} - Obter um patrimônio por ID
- POST patrimonios - Inserir um novo patrimônio
- PUT patrimonios/{id} - Alterar os dados de um patrimônio
- DELETE patrimonios/{id} - Excluir um patrimônio

*Regras*:

- O nº do tombo deve ser gerado automaticamente pelo sistema, e não pode ser alterado pelos usuários. 

**Marca**

*Campos*:

- Nome – obrigatório
- MarcaId - obrigatório

*Endpoints*:

- GET marcas - Obter todas as marcas
- GET marcas/{id} - Obter uma marca por ID
- GET marcas/{id}/patrimônios – Obter todos os patrimônios de uma marca
- POST marcas - Inserir uma nova marca
- PUT marca/{id} - Alterar os dados de uma marca
- DELETE marca/{id} - Excluir uma marca

*Regras*:
- Não deve permitir a existência de duas marcas com o mesmo nome.

# Instalando projeto

Para executar o projeto incialmente execute todos os scripts que se encontram no caminho [Scritps](https://github.com/1bberto/esx/blob/master/Scritps/DataBase.sql).

Apos realizar a criacao do banco de dados, faca a carga inicial na tabela `tblUsuario`, caso queira pode utilizar o script que se encontra no caminho [Carga](https://github.com/1bberto/esx/blob/master/Scritps/Carga.sql).

Altere o caminho do banco de dados no arquivo `appsettings.json`:

```
"Configuracoes": {
    "Connection": "Data Source=[DATABASE];Initial Catalog=dbESX;Persist Security Info=True;Integrated Security=SSPI"
  }
```

Ao realizar isso basta executar o projeto utilizando o visual studio, nao se esqueca de ter a versao do .net core 2.1 instalado em sua maquina, caso nao tenha basta baixar o mesmo neste [link](https://dotnet.microsoft.com/download/dotnet-core/2.1)
