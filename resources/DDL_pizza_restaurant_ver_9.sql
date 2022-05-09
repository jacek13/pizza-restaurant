CREATE
  TABLE account
  (
    id_account            INTEGER IDENTITY(1,1) NOT NULL ,
    e_mail                VARCHAR (128) NOT NULL ,
    login                 VARCHAR (64) NOT NULL ,
    password              VARCHAR (64) NOT NULL ,
    name                  VARCHAR (128) NOT NULL ,
    surname               VARCHAR (128) NOT NULL ,
    account_creation_date DATE ,
    phone_number          VARCHAR (11) ,
    role                  VARCHAR (64)
  )
  ON "default"
GO
ALTER TABLE account ADD CONSTRAINT account_PK PRIMARY KEY CLUSTERED (id_account
)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE client
  (
    id_client          INTEGER IDENTITY(1,1) NOT NULL ,
    points             INTEGER NOT NULL ,
    address            VARCHAR (256) ,
    account_id_account INTEGER NOT NULL
  )
  ON "default"
GO
CREATE UNIQUE NONCLUSTERED INDEX
client__IDX ON client
(
  account_id_account
)
ON "default"
GO
ALTER TABLE client ADD CONSTRAINT client_PK PRIMARY KEY CLUSTERED (id_client)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE dishes
  (
    order_id_order   INTEGER NOT NULL ,
    pizza_id_pizza   INTEGER NOT NULL ,
    historical_cost  INTEGER ,
    historical_price INTEGER ,
    amount           INTEGER
  )
  ON "default"
GO
ALTER TABLE dishes ADD CONSTRAINT dishes_PK PRIMARY KEY CLUSTERED (
order_id_order, pizza_id_pizza)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE manager
  (
    id_manager         INTEGER IDENTITY(1,1) NOT NULL ,
    salary_netto       INTEGER ,
    salary_brutto      INTEGER ,
    account_id_account INTEGER NOT NULL
  )
  ON "default"
GO
CREATE UNIQUE NONCLUSTERED INDEX
manager__IDX ON manager
(
  account_id_account
)
ON "default"
GO
ALTER TABLE manager ADD CONSTRAINT manager_PK PRIMARY KEY CLUSTERED (id_manager
)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE manager_assignment
  (
    manager_id_manager       INTEGER NOT NULL ,
    restaurant_id_restaurant INTEGER NOT NULL ,
    assignment_role          VARCHAR (128)
  )
  ON "default"
GO
ALTER TABLE manager_assignment ADD CONSTRAINT manager_assignment_PK PRIMARY KEY
CLUSTERED (manager_id_manager, restaurant_id_restaurant)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE "order"
  (
    id_order                  INTEGER IDENTITY(1,1) NOT NULL ,
    delivery_adress           VARCHAR (128) NOT NULL ,
                              DATE DATE NOT NULL ,
    status                    CHAR (1) ,
    phone_number              VARCHAR (11) ,
    "additional_information " VARCHAR (1024) ,
    name                      VARCHAR (128) ,
    surname                   VARCHAR (128) ,
    client_id_client          INTEGER ,
    restaurant_id_restaurant  INTEGER
  )
  ON "default"
GO
ALTER TABLE "order" ADD CONSTRAINT order_PK PRIMARY KEY CLUSTERED (id_order)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE pizza
  (
    id_pizza INTEGER IDENTITY(1,1) NOT NULL ,
    price    INTEGER NOT NULL ,
    cost     INTEGER NOT NULL ,
    isAvailable BIT NOT NULL ,
    type   VARCHAR (64) NOT NULL ,
    size   INTEGER ,
    points INTEGER
  )
  ON "default"
GO
ALTER TABLE pizza ADD CONSTRAINT pizza_PK PRIMARY KEY CLUSTERED (id_pizza)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE reservation
  (
    id_reservation INTEGER IDENTITY(1,1) NOT NULL ,
                   DATE DATE NOT NULL ,
    start_of_reservation TIME NOT NULL ,
    end_of_reservation TIME NOT NULL ,
    name                           VARCHAR (128) NOT NULL ,
    phone                          VARCHAR (11) NOT NULL ,
    manager_id_manager             INTEGER ,
    client_id_client               INTEGER ,
    table_id_table                 INTEGER NOT NULL ,
    table_restaurant_id_restaurant INTEGER
  )
  ON "default"
GO
ALTER TABLE reservation ADD CONSTRAINT reservation_PK PRIMARY KEY CLUSTERED (
id_reservation)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE restaurant
  (
    id_restaurant INTEGER IDENTITY(1,1) NOT NULL ,
    address       VARCHAR (256) NOT NULL ,
    phone_number  VARCHAR (11) ,
    e_mail        VARCHAR (128)
  )
  ON "default"
GO
ALTER TABLE restaurant ADD CONSTRAINT restaurant_PK PRIMARY KEY CLUSTERED (
id_restaurant)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

CREATE
  TABLE "table"
  (
    id_table                 INTEGER IDENTITY(1,1) NOT NULL ,
    capacity                 INTEGER ,
    restaurant_id_restaurant INTEGER NOT NULL
  )
  ON "default"
GO
ALTER TABLE "table" ADD CONSTRAINT table_PK PRIMARY KEY CLUSTERED (id_table,
restaurant_id_restaurant)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO

ALTER TABLE dishes
ADD CONSTRAINT FK_ASS_3 FOREIGN KEY
(
order_id_order
)
REFERENCES "order"
(
id_order
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE dishes
ADD CONSTRAINT FK_ASS_4 FOREIGN KEY
(
pizza_id_pizza
)
REFERENCES pizza
(
id_pizza
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE manager_assignment
ADD CONSTRAINT FK_ASS_7 FOREIGN KEY
(
manager_id_manager
)
REFERENCES manager
(
id_manager
)
ON
DELETE CASCADE ON
UPDATE NO ACTION
GO

ALTER TABLE manager_assignment
ADD CONSTRAINT FK_ASS_8 FOREIGN KEY
(
restaurant_id_restaurant
)
REFERENCES restaurant
(
id_restaurant
)
ON
DELETE CASCADE ON
UPDATE NO ACTION
GO

ALTER TABLE client
ADD CONSTRAINT client_account_FK FOREIGN KEY
(
account_id_account
)
REFERENCES account
(
id_account
)
ON
DELETE CASCADE ON
UPDATE NO ACTION
GO

ALTER TABLE manager
ADD CONSTRAINT manager_account_FK FOREIGN KEY
(
account_id_account
)
REFERENCES account
(
id_account
)
ON
DELETE CASCADE ON
UPDATE NO ACTION
GO

ALTER TABLE "order"
ADD CONSTRAINT order_client_FK FOREIGN KEY
(
client_id_client
)
REFERENCES client
(
id_client
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE "order"
ADD CONSTRAINT order_restaurant_FK FOREIGN KEY
(
restaurant_id_restaurant
)
REFERENCES restaurant
(
id_restaurant
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE reservation
ADD CONSTRAINT reservation_client_FK FOREIGN KEY
(
client_id_client
)
REFERENCES client
(
id_client
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE reservation
ADD CONSTRAINT reservation_manager_FK FOREIGN KEY
(
manager_id_manager
)
REFERENCES manager
(
id_manager
)
ON
DELETE
  NO ACTION ON
UPDATE NO ACTION
GO

ALTER TABLE reservation
ADD CONSTRAINT reservation_table_FK FOREIGN KEY
(
table_id_table,
table_restaurant_id_restaurant
)
REFERENCES "table"
(
id_table ,
restaurant_id_restaurant
)
ON
DELETE CASCADE ON
UPDATE NO ACTION
GO

ALTER TABLE "table"
ADD CONSTRAINT table_restaurant_FK FOREIGN KEY
(
restaurant_id_restaurant
)
REFERENCES restaurant
(
id_restaurant
)
ON
DELETE CASCADE ON
UPDATE NO ACTION
GO
