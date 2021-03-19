# The New Bakery: Padaria Web API

## Grupo 2: Bruno, Gilvaneide, Jéssica, Karina

Projeto final para o curso de .NET (#C) do Campinas Tech Talents / ShareRH / Performa_IT  
Instrutoras: Thamirys Gameiro e Thaise Medeiros

---

# Overview:

O grupo desenvolveu um projeto Web API com ASP.NET Core 3.1, o qual faz gestão de usuários, matérias-primas, produtos, vendas e caixas de uma padaria. Além disso, utilizamos o Entity Framework para interação com o banco de dados, fazendo uso do SQL Server.

- É possível adicionar novos usuários, cada qual tendo uma senha e um perfil, possibilitanto o login com autenticação (Bearer) e autorização com base no perfil escolhido, determinando as funcionalidades da Web API que poderão ser acessadas.
- As matérias-primas podem ser criadas, editadas e buscadas. Neste último caso, o estoque de matérias-primas é listado organizando-as por ordem alfabética de nome e buscando somente as que estão ativas. É possível optar por mostrar também as inativas e filtrar matérias-primas por nome, ou uma combinação de ambos.
- Os produtos dividem-se em produzidos ou terceirizados (revenda). No caso dos produzidos na padaria, não é possível estabelecer uma quantidade maior que zero no momento da criação do produto, uma vez que deve-se antes estabelecer as matérias-primas que são utilizadas em sua produção, o que é feito por meio da vinculação de um produto com uma matéria-prima.
- Para estabelecer esse relacionamento, é criada uma receita (ProdutoMateria), informando a quantidade necessária para a fabricação do produto, com base numa porção informada. Esta informação é essencial para que um produto possa ser produzido, ou seja, possa ter sua quantidade alterada para um número maior que zero. Feito isto, a quantidade de matéria-prima necessária para a fabricação é abatida do estoque automaticamente.
- Cada cliente recebe uma comanda no momento de entrada na padaria, na qual ficam registradas informações de data e horário, bem como uma lista dos produtos a serem comprados.
- No momento da venda, ficam registrados: o usuário (vendedor) que realizou a venda, a data, a comanda à qual a venda se refere, seu valor total (somatório do valor de todos os produtos registrados na comanda), a forma de pagamento (caso seja dinheiro, é feito o cálculo do troco), o status da venda e o caixa onde foi registrada a venda.
- Cada caixa é vinculado a um usuário (vendedor) no momento de sua abertura e só este usuário poderá fechar o próprio caixa. No momento do fechamento, é calculado o valor total de vendas diário.
- O descarte de produtos produzidos na padaria pode ser feito ao final do dia.
- Foi utilizado o Swagger para documentar os métodos da API, interações esperadas e tipos de resposta. A autenticação por Bearer Token também pode ser feita no Swagger.

# Extras:

- Mais de um caixa pode ser aberto por dia, desde que o anterior esteja fechado, possibilitanto abertura de caixas de acordo com turnos de funcionários.
- No momento do incremento da quantidade de um produto produzido na padaria (o que equivale a produzir este produto), as matérias primas vinculadas a ele são abatidas do estoque de forma proporcioal à quantidade a ser incrementada, utilizando regra de três simples com base na porção de referência. Isso permite que qualquer quantidade maior que zero seja produzida na padaria, com abatimento correto de matérias-primas.

# Estórias

Utilizando o Trello, desmembramos as estórias estabelecidas pelas instrutoras em tarefas menores, as quais foram realizadas em conjunto, em dupla, ou individualmente, com base nas habilidades de cada um.

As estórias estão listadas abaixo:

## Manipular Usuários:

Eu como administrador gostaria de incluir, atualizar, deletar e listar os usuários para ter um melhor controle das permissões de cada um.

Critérios de aceite:

- O administrador deve ter permissão para acessar essa funcionalidade;
- O Usuário deve conter E-mail (obrigatório);
- O Usuário deve conter Nome (obrigatório);
- O Usuário deve conter Senha (obrigatório);
- O Usuário deve conter Data de Nascimento (obrigatório);
- O Usuário deve conter Perfil de Acesso (obrigatório);
- Deve ser considerados os seguintes Perfis de Acesso: Administrador, Padeiro, Estoquista e Vendedor;
- Ao deletar/atualizar/incluir um usuário, deverá retornar uma mensagem confirmando a operação em caso de sucesso. Em caso de erro, deverá retornar uma mensagem informando que a operação não foi realizada;
- Ao listar os usuários, o administrador pode informar Nome ou E-mail para a busca.
- Caso não informe nada, a listagem deve retornar todos os usuários cadastrados.
- A ordenação deve ser por ordem alfabética de Nome.
- A listagem deve conter Nome, E-mail, Data de nascimento e Perfil de Acesso;

## Gestão de estoque de matéria prima

Eu como estoquista/administrador gostaria de incluir Matéria Prima no estoque para permitir que o estoque esteja atualizado para ser utilizado na fabricação dos produtos finais.

Critérios de aceite:

- O usuário deve ter permissão para acessar a funcionalidade;
- A Matéria Prima deve conter Nome (obrigatório);
- A Matéria Prima deve conter Unidade de Medida (obrigatório);
- A Matéria Prima deve conter Quantidade (obrigatório);
- A Matéria Prima deve conter uma indicação se está Ativa ou Inativa;
- Não será permitido incluir/alterar uma Matéria Prima com quantidade zerada ou negativa;
- Ao alterar uma Matéria Prima que exista no estoque, a nova quantidade deverá ser somada a anterior;
- Não deve permitir Inativar se a Matéria Prima estiver vinculada a um Produto Final;
- Ao incluir uma Matéria Prima que não exista, esta deverá ser criada com a respectiva quantidade informada;
- Não será permitido excluir uma Matéria Prima.

## Gestão de produtos finais do tipo Produzidos na Padaria

Eu como padeiro/administrador gostaria de controlar os produtos Produzidos na Padaria a fim de manter um estoque atualizado com o que pode ser vendido.

Critérios de aceite:

- O usuário deve ter permissão para acessar a funcionalidade;
- O Produto Final deve do tipo: Produzido na Padaria;
- O Produto Final deve ter Nome (obrigatório);
- O Produto Final deve ter a Unidade de Medida (obrigatório);
- O Produto Final deve ter Quantidade (obrigatório);
- O Produto Final deve ter Valor (obrigatório);
- O Produto Final deve ter a indicação de Ativo ou Inativo;
- O Produto Final, deve informar quais Matérias Primas foram utilizadas na fabricação assim como a Quantidade de cada uma e não devem estar Inativas;
- Ao alterar quantidade do produto Final, deve ser verificado se existe estoque das Matérias Primas que são utilizadas na fabricação;

## Gestão de produtos finais do tipo Terceirizado

Eu como estoquista/administrador gostaria de controlar os produtos comprados a fim de manter um estoque atualizado com o que pode ser vendido.

Critérios de aceite:

- O usuário deve ter permissão para acessar a funcionalidade;
- O Produto Final deve do tipo Terceirizado;
- O Produto Final deve ter Nome (obrigatório);
- O Produto Final deve ter a Unidade de Medida (obrigatório);
- O Produto Final deve ter Quantidade (obrigatório);
- O Produto Final deve ter Valor (obrigatório);
- O Produto Final deve ter a indicação de Ativo ou Inativo;
- Ao alterar um Produto Final, a quantidade informada deve ser somada a anterior;

## Acompanhamento de estoque de matéria prima

Eu como estoquista/padeiro/administrador gostaria de acompanhar o estoque de Matéria Prima para poder controlar a quantidade dos produtos.

Critérios de aceite:

- O usuário deve ter acesso a esta funcionalidade;
- Ao listar as Matérias Primas, o usuário pode informar o Nome e se deseja retornar as Matérias Primas Inativas. Caso o usuário não informe nada em Nome, a lista não deve considerar o filtro de Nome. Caso o usuário não informe a opção de trazer as Inativas, deve-se considerar apenas as Matérias Primas Ativas, caso contrário, deve-se retornar as Ativas e Inativas. A listagem deve conter Nome, Unidade de Medida e Quantidade. A listagem deve estar ordenada por ordem alfabética de Nome;

## Acompanhamento de estoque de produtos finais

Eu como estoquista/padeiro/administrador gostaria de acompanhar o estoque de Produto Final para poder controlar a quantidade dos produtos.

Critérios de aceite:

- O usuário deve ter acesso a esta funcionalidade;
- Ao listar os Produtos Finais, o usuário pode informar o Nome e se deseja retornar as Produtos Finais Inativos. Caso o usuário não informe nada em Nome, a lista não deve considerar o filtro de Nome. Caso o usuário não informe a opção de trazer as Inativos, deve-se considerar apenas os Produtos Finais Ativos, caso contrário, deve-se retornar as Ativas e Inativas. A listagem deve conter Nome, Tipo, Unidade de Medida e Quantidade. A listagem deve estar ordenada por ordem alfabética de Nome;

## Abertura de Caixa

Eu como vendedor/administrador gostaria de finalizar o caixa do dia atual para conseguir acompanhar as vendas realizadas.

Critérios de aceite:

- O usuário deve ter permissão de acesso a esta funcionalidade;
- O caixa deve estar associado a um usuário;
- O usuário não pode abrir o caixa sem fechar o anterior;

## Realizar Venda

Eu como vendedor/administrador gostaria de realizar uma venda para que haja movimentação financeira na padaria.

Critério de aceite:

- O usuário deve ter permissão para acessar a funcionalidade;
- A Venda deve conter Data sempre considerando a data atual no formato dd/MM/yyyy (obrigatório);
- A Venda deve conter uma lista de Produtos Finais (obrigatório);
- A Venda deve conter um Valor que corresponde ao somatório dos valores da lista de Produtos Finais (obrigatório);
- A Venda deve conter o Usuário responsável por efetuar a venda (obrigatório);
- A Venda deve conter qual Tipo de Pagamento: Dinheiro, Débito ou Crédito (obrigatório);
- Não será permitido realizar uma venda do dia anterior;
- Não será permitido realizar uma venda com valor zerado;
- Não será permitido realizar uma venda de algum Produto Final sem estoque;
- Não será permitido realizar uma venda sem ter um caixa aberto;
- Ao realizar uma venda com sucesso, está deve fazer a baixa automática dos Produtos Finais informados;
- Ao realizar uma venda com dinheiro, deve ser informado o valor pago pelo cliente e retornar o valor do troco;
- Ao realizar uma venda com Débito ou Crédito com sucesso, deve retornar uma mensagem informando que a operação foi realizada com sucesso;
- Caso ocorra algum erro, deve retornar uma mensagem informando que a operação não foi finalizada com sucesso;

## Fechamento de Caixa

Eu como vendedor/administrador gostaria de finalizar o caixa do dia atual para conseguir acompanhar as vendas realizadas.

Critérios de aceite:

- O usuário deve ter permissão de acesso a esta funcionalidade;
- Ao fechar o caixa, deve retornar o valor total das vendas em Dinheiro, Débito e Crédito;
- O caixa deve estar associado a um usuário;
- Ao fechar o caixa, o usuário deve informar o descarte de Produtos Finais do tipo Produzidos na Padaria fazendo a baixa automaticamente do estoque.
