# Desafio ESX

Projeto desenvolvido em .net core 2.1

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

Para executar o projeto incialmente execute todos os scripts que se encontram na pasta 
