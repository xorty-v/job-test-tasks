-- Запрос из задания
SELECT p.name AS product, ISNULL(c.name, 'Без категории') AS category
FROM products AS p
         LEFT JOIN productcategory AS pc ON p.id = product_id
         LEFT JOIN categories AS c ON c.id = pc.category_id;




-- Создание таблицы Products
CREATE TABLE Products
(
    ID   INT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

-- Создание таблицы Categories
CREATE TABLE Categories
(
    ID   INT PRIMARY KEY,
    Name VARCHAR(75) NOT NULL
);

-- Создание таблицы ProductCategory
CREATE TABLE ProductCategory
(
    Product_ID  INT,
    Category_ID INT,
    PRIMARY KEY (Product_ID, Category_ID),
    FOREIGN KEY (Product_ID) REFERENCES Products (ID),
    FOREIGN KEY (Category_ID) REFERENCES Categories (ID)
);

-- Вставка данных
INSERT INTO Products (id, name)
VALUES (1, 'Яблоко'),
       (2, 'Банан'),
       (3, 'Телефон'),
       (4, 'Хлеб'),
       (5, 'Ноутбук');

INSERT INTO Categories (id, name)
VALUES (1, 'Продукты'),
       (2, 'Техника');

INSERT INTO ProductCategory (Product_ID, Category_ID)
VALUES (1, 1),
       (2, 1),
       (3, 2),
       (4, 1),
       (5, 2);