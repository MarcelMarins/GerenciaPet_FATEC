-- -----------------------------------------------------
-- CRIANDO A TABELA DE ESTADOS 
-- -----------------------------------------------------
CREATE TABLE `estados` (
  `est_id` INT NOT NULL AUTO_INCREMENT,
  `est_nome` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`est_id`));

-- -----------------------------------------------------
-- CRIANDO A TABELA DE CIDADES 
-- -----------------------------------------------------
CREATE TABLE `cidades` (
  `cid_id` INT NOT NULL,
  `cid_nome` VARCHAR(45) NOT NULL,
  `cid_est_id` INT NOT NULL,
  PRIMARY KEY (`cid_id`, `cid_est_id`),
  INDEX `fk_cidades_estados1_idx` (`cid_est_id` ASC),
  CONSTRAINT `fk_cidades_estados1`
    FOREIGN KEY (`cid_est_id`)
    REFERENCES `estados` (`est_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- -----------------------------------------------------
-- CRIANDO A TABELA DE CLIENTE, JÁ COLOCANDO A CHAVE 
-- ESTRANGEIRA DA TABELA CIDADES
-- -----------------------------------------------------
CREATE TABLE `clientes` (
  `cli_id` INT NOT NULL AUTO_INCREMENT,
  `cli_nome` VARCHAR(45) NOT NULL,
  `cli_cpf` VARCHAR(11) NOT NULL,
  `cli_telefone` VARCHAR(45) NOT NULL,
  `cli_endereco` VARCHAR(45) NOT NULL,
  `cli_cep` VARCHAR(45) NOT NULL,
  `cli_cid_id` INT NOT NULL,
  `cli_complemento` VARCHAR(45) NOT NULL,
  `cli_celular` VARCHAR(45) NOT NULL,
  `cli_numero` VARCHAR(45) NOT NULL,
  `cli_email` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`cli_id`, `cli_cid_id`),
  INDEX `fk_clientes_cidades1_idx` (`cli_cid_id` ASC),
  CONSTRAINT `fk_clientes_cidades1`
    FOREIGN KEY (`cli_cid_id`)
    REFERENCES `cidades` (`cid_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

-- -----------------------------------------------------
-- CRIANDO A TABELA DE FORMAS DE PAGAMENTO. É POSSÍVEL
-- COLOCAR UMA TAXA PARA A FORMA DE PAGAMENTO, POR ISSO
-- O CAMPO ESTÁ COMO DECIMAL
-- -----------------------------------------------------
CREATE TABLE `pagamento` (
    `pag_id` INT PRIMARY KEY AUTO_INCREMENT,
    `pag_nome` VARCHAR(45),
    `pag_taxa` DECIMAL(10 , 2 ),
    `pag_status` TINYINT DEFAULT 1
);
  
-- -----------------------------------------------------
-- CRIANDO A TABELA DE VENDAS E JÁ USANDO A CHAVE 
-- ESTRANGEIRA DA TABELA FORMA DE PAGAMENTO
-- -----------------------------------------------------
CREATE TABLE `vendas` (
  `ven_id` INT PRIMARY KEY AUTO_INCREMENT,
  `ven_data` DATETIME,
  `ven_desconto` DECIMAL(10,2),
  `ven_valortotal` DECIMAL(10,2),
  `ven_pag_id` INT,
  `ven_cli_id` INT,
  `ven_finalizada` TINYINT DEFAULT 1,
  `ven_status` TINYINT DEFAULT 0
);
-- -----------------------------------------------------
--  CRIANDO A TABELA DE CATEGORIA
-- -----------------------------------------------------
CREATE TABLE `categoria` (
    `cat_id` INT PRIMARY KEY AUTO_INCREMENT,
    `cat_nome` VARCHAR(45),
    `cat_status` TINYINT DEFAULT 1
);


-- -----------------------------------------------------
-- CRIANDO A TABELA DE TIPOS
-- -----------------------------------------------------
CREATE TABLE `tipos` (
  `tip_id` INT PRIMARY KEY AUTO_INCREMENT,
  `tip_nome` VARCHAR(45),
  `tip_status` TINYINT DEFAULT 1
);


-- -----------------------------------------------------
-- CRIANDO A TABELA DE PRODUTOS JÁ COM A CHAVE
-- ESTRANGEIRA DA TABELA tipos E categorias
-- -----------------------------------------------------
CREATE TABLE `produtos` (
    `pro_id` INT PRIMARY KEY AUTO_INCREMENT,
    `pro_descricao` VARCHAR(45),
    `pro_unidade` VARCHAR(45),
    `pro_cat_id` INT,
    `pro_tip_id` INT,
    `pro_estoqueminimo` VARCHAR(45),
    `pro_estoqueatual` VARCHAR(45),
    `pro_estoquemaximo` VARCHAR(45),
    `pro_controlarestoque` TINYINT,
    `pro_precocusto` DECIMAL(10),
    `pro_margem` DECIMAL(10),
    `pro_precovenda` DECIMAL(10),
    `pro_for_id` INT,
    `pro_solicitarquantidade` TINYINT,
    `pro_status` TINYINT DEFAULT 1
);


-- -----------------------------------------------------
-- CRIANDO A TABELA DE FORNECEDORES COM A CHAVE PRIMARIA
-- DA TABELA DE cidades
-- -----------------------------------------------------
CREATE TABLE `fornecedores` (
    `for_id` INT PRIMARY KEY AUTO_INCREMENT,
    `for_nomefantasia` VARCHAR(45),
    `for_razao` VARCHAR(45),
    `for_cnpj` VARCHAR(45),
    `for_ie` VARCHAR(45),
    `for_isento` TINYINT,
    `for_cep` VARCHAR(45),
    `for_cid_id` INT,
    `for_endereco` VARCHAR(45),
	`for_bairro` VARCHAR(45),
    `for_celular` VARCHAR(45),
    `for_numero` VARCHAR(45),
    `for_email` VARCHAR(45),
    `for_status` TINYINT DEFAULT 1
);


-- -----------------------------------------------------
-- CRIANDO A TABELA DE VENDAS_DETALHES. ELA USA AS CHAVES
-- ESTRANGEIRAS DAS TABELAS produto E vendas PARA ESPECIFICAR
-- OS PRODUTOS USADOS NAS VENDAS - POR ISSO SE CHAMA vendas_detalhes
-- **É UMA TABELA CRIADA A PARTIR DE UMA RELAÇÃO N por N ENTRE AS 
-- TABELAS vendas E produtos
-- -----------------------------------------------------
CREATE TABLE `vendas_detalhes` (
  `vendet_pro_id` INT NOT NULL,
  `vendet_ven_id` INT NOT NULL,
  `vendet_quantidade` DECIMAL(10) NOT NULL,
  PRIMARY KEY (`vendet_pro_id`, `vendet_ven_id`),
  INDEX `fk_produtos_has_vendas_vendas1_idx` (`vendet_ven_id` ASC),
  INDEX `fk_produtos_has_vendas_produtos1_idx` (`vendet_pro_id` ASC),
  CONSTRAINT `fk_produtos_has_vendas_produtos1`
    FOREIGN KEY (`vendet_pro_id`)
    REFERENCES `produtos` (`pro_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_produtos_has_vendas_vendas1`
    FOREIGN KEY (`vendet_ven_id`)
    REFERENCES `vendas` (`ven_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


-- -----------------------------------------------------
-- ESSA TABELA SERVE PARA OS PRODUTOS QUE TEM MAIS DE UM
-- FORNECEDOR. ELA FUNCIONA QUEM NEM A vendas_deltalhes
-- POR QUE É UMA TABELA CRIADA A PARTIR DE UMA RELAÇÃO 
-- N por N ENTRE AS TABELAS produtos E fornecedores
-- SERVE PRA ESPECIFICAR QUAIS FORNECEDORES FORNECEM
-- DETERMINADOS PRODUTOS, POIS UM PRODUTO PODE SER
-- FORNECIDO POR MAIS DE UM FORNECEDOR
-- -----------------------------------------------------
CREATE TABLE `produtos_fornecedores` (
    `profor_id` INT PRIMARY KEY AUTO_INCREMENT,
    `profor_pro_id` INT,
    `profor_for_id` INT
);
  
  
  