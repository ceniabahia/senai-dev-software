-- 1. Criação do Banco de Dados
CREATE DATABASE IF NOT EXISTS minha_api_db;
USE minha_api_db;

-- 2. Criação da Tabela de Produtos
CREATE TABLE IF NOT EXISTS produtos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    preco DECIMAL(10,2) NOT NULL,
    estoque INT NOT NULL DEFAULT 0,
    ativo TINYINT(1) NOT NULL DEFAULT 1
);

-- 3. Inserção de Dados Iniciais
INSERT INTO produtos (nome, preco, estoque, ativo) 
VALUES 
('Notebook', 3500.00, 10, 1),
('Mouse Gamer', 120.50, 45, 1);

-- 4. Visualizar Produtos
SELECT * FROM produtos;

-- 5. Criação da Tabela Cliente
CREATE TABLE IF NOT EXISTS cliente (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(500) NOT NULL,
    email VARCHAR(100) NOT NULL,
    cpf VARCHAR(100) NOT NULL,
    ativo TINYINT(1) NOT NULL DEFAULT 1
);

-- 6. Inserção de Cliente
INSERT INTO cliente (nome, email, cpf, ativo) 
VALUES 
('carlos', 'bolo2424@gmail.com', '674.394.930-28', 1);

-- 7. Visualizar Clientes
SELECT * FROM cliente;

CREATE TABLE IF NOT EXISTS vendas(
id int auto_increment primary key,
cliente_id INT NOT null,
produto_id INT NOT NULL,
quantidade INT NOT NULL,
valor_total DECIMAL(10,2) NOT NULL,
data_venda DATETIME NOT NULL DEFAULT current_timestamp,

 FOREIGN KEY (cliente_id) REFERENCES cliente(id),
    FOREIGN KEY (produto_id) REFERENCES produtos(id)
); 

CREATE TABLE IF NOT EXISTS fornecedores(
id INT auto_increment primary key,
nome varchar (100) not null,
email varchar (100) not null,
telefone varchar (20) NOT NULL,
    ativo TINYINT(1) NOT NULL DEFAULT 1

);

CREATE TABLE IF NOT EXISTS departamento(
id INT auto_increment primary key,
nome varchar (100) not null,
funcionario varchar(100) not null,
email varchar (100) not null, 
telefone varchar (20) not null,
ativo TINYINT(1) NOT NULL DEFAULT 1

);

INSERT INTO departamento (nome, funcionario, email, telefone, ativo) 
VALUES 
('bahia', 'carlos' ,'bolo2424@gmail.com', '2212-3213', 1);

SELECT * FROM departamento;