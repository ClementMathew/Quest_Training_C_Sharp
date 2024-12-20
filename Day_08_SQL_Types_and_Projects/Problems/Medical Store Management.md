# Medical Store Management

### Question :

      Create a Medical Store Management System for a local pharmacy.

The system needs to track various aspects such as medications, suppliers, sales, inventory, and customers. The system should also provide functionalities to manage inventory, process customer purchases, and generate reports.

Your database should be well-structured, containing several tables such as medications, suppliers, sales, inventory, and customers.
Here are the requirements and some common query operations that the medical store owner needs:

- #### Entities and Tables :

  1.  <b> Medications :</b> Stores details about the drugs or medical products available in the store.

      - <b> Fields :</b> id, name, category, price, expiry_date, supplier_id, stock_quantity.

  2.  <b> Suppliers : </b> Stores information about the suppliers who supply medications.

      - <b> Fields :</b> id, name, contact_person, phone, email, address.

  3.  <b> Customers :</b> Stores details about the customers who purchase medications.

      - <b>Fields :</b> id, name, phone, email, address.

  4.  <b>Sales :</b> Stores information about the sales transactions that take place.

      - <b>Fields :</b> id, customer_id, medication_id, quantity, sale_date, total_price.

  5.  <b>Inventory :</b> Tracks the medications and their stock levels.

      - <b>Fields :</b> id, medication_id, supplier_id, stock_in, stock_out, date_added.

<br>

---

### Application-Level Questions :

- #### Insert Operation :

  - Insert details about new medications into the medications table.

  - Insert information about new suppliers into the suppliers table.

  - Insert a new customer record into the customers table.

<br>

```
1. Write an INSERT query to add a new medication, "Paracetamol", to the medications table with a price of 2.50 per unit and a stock quantity of 100.
```

```
2. Write an INSERT query to add a new supplier "HealthCorp" into the suppliers table.
```

```
3. Write an INSERT query to register a new customer with the name "John Doe" and contact details in the customers table.
```

- #### Update Operation :

  - Update the stock level of medications as new shipments come in or after a sale.

- #### Join Operations :

  - Display a list of medications along with their supplier details.

<br>

```
4. Write a JOIN query to list all medications and their corresponding supplier names.
```

```
5. Write a query to display the sales data, including the medication name, customer name, and the total amount of the sale.
```

- #### Select and Filter Operations :

  - Track sales and filter them by customer, date, or medication.

<br>

```
6. Write a SELECT query to retrieve all sales made in the last 30 days
```

```
7. Write a SELECT query to retrieve sales details for a customer named "Alice Smith."
```

```
8. Write a query to retrieve all medications that will expire within the next 60 days.
```

- #### Aggregate Operations :

  - Generate reports such as total sales in a month or stock summary.

<br>

```
9. Write a query to calculate the total revenue generated by sales in the month of September.
```

```
10. Write a query to get the total quantity of each medication sold (group by medication).
```

```
11. Write a query to count the total number of customers who have made purchases.
```

- #### Delete Operation :

  - Delete expired medications from the inventory.

<br>

```
12. Write a DELETE query to remove all medications that have expired as of today.
```

```
13. Write a query to delete a customer who has requested to be removed from the database.
```

- #### Transaction Management :

  - When a sale is processed, ensure that the stock quantity is reduced accordingly, and the sale is recorded.

  - When a customer purchases a medication, the following operations should be performed:

    - Insert a new record into the sales table.

    - Update the medications table to reduce the stock quantity of the purchased medication.

<br>

---

### Code :

```sql

-- Creating Suppliers Table --

CREATE TABLE Suppliers (
id INT PRIMARY KEY IDENTITY(1,1),
name VARCHAR(100) NOT NULL,
contact_person VARCHAR(100),
phone VARCHAR(15),
email VARCHAR(100) UNIQUE,
address VARCHAR(255)
);

-- Creating Customers Table --

CREATE TABLE Customers (
id INT PRIMARY KEY IDENTITY(1,1),
name VARCHAR(100) NOT NULL,
phone VARCHAR(15),
email VARCHAR(100) UNIQUE,
address VARCHAR(255)
);

-- Creating Medications Table --

CREATE TABLE Medications (
id INT PRIMARY KEY IDENTITY(1,1),
name VARCHAR(100) NOT NULL,
category VARCHAR(50),
price DECIMAL(10, 2) NOT NULL,
expiry_date DATE,
supplier_id INT,
stock_quantity INT NOT NULL,
FOREIGN KEY (supplier_id) REFERENCES Suppliers(id)
);

-- Creating Sales Table --

CREATE TABLE Sales (
id INT PRIMARY KEY IDENTITY(1,1),
customer_id INT,
medication_id INT,
quantity INT NOT NULL,
sale_date DATE DEFAULT GETDATE(),
total_price DECIMAL(10, 2),
FOREIGN KEY (customer_id) REFERENCES Customers(id),
FOREIGN KEY (medication_id) REFERENCES Medications(id) ON DELETE CASCADE
);

-- Creating Inventory Table --

CREATE TABLE Inventory (
id INT PRIMARY KEY IDENTITY(1,1),
medication_id INT NOT NULL,
supplier_id INT NOT NULL,
stock_in INT,
stock_out INT,
date_added DATE DEFAULT GETDATE(),
FOREIGN KEY (medication_id) REFERENCES Medications(id) ON DELETE CASCADE,
FOREIGN KEY (supplier_id) REFERENCES Suppliers(id)
);

-- Inserting values to Suppliers --

INSERT INTO Suppliers (name, contact_person, phone, email, address)
VALUES
('Pharma Suppliers', 'John Mark', '12345678', 'john@pharmasuppliers.com', 'Bangalore');

-- Inserting values to Customers --

INSERT INTO Customers (name, phone, email, address)
VALUES
('Clement Mathew', '22345678', 'clement@email.com', 'Kerala');

-- Updating Medications --

EXEC update_Medications 'Dollo', 'Allopathy', 60, '2025-08-01', 1, 'stock_in', 30;

EXEC update_Medications 'Paracetamol', 'Allopathy', 120, '2025-07-01', 1, 'stock_in', 30;

EXEC update_Medications 'Paracetamol', 'Allopathy', 120, '2025-07-01', 1, 'stock_out', 30;

-- Inserting values to Sales --

INSERT INTO Sales (customer_id, medication_id, quantity, total_price)
VALUES
(1, 1, 2, 100);

-- Inserting values to Inventory --

INSERT INTO Inventory (medication_id, supplier_id, stock_in, stock_out)
VALUES
(1, 1, 100, 0);

-- Listing all medications and their suppliers --

SELECT
Medications.name AS MedicationName,
Medications.category,
Medications.price,
Medications.expiry_date,
Medications.stock_quantity,
Suppliers.name AS SupplierName
FROM
Medications
INNER JOIN
Suppliers
ON
Medications.supplier_id = Suppliers.id;

-- Display sales data including medication name, customer name, and total amount of sale --

SELECT
Sales.id AS SaleID,
Customers.name AS CustomerName,
Medications.name AS MedicationName,
Sales.quantity,
Sales.total_price AS TotalAmount,
Sales.sale_date
FROM
Sales
INNER JOIN
Customers
ON
Sales.customer_id = Customers.id
INNER JOIN
Medications
ON
Sales.medication_id = Medications.id;

-- All sales in last 30 days --

SELECT
Sales.id AS SaleID,
Customers.name AS CustomerName,
Medications.name AS MedicationName,
Sales.quantity,
Sales.total_price AS TotalAmount,
Sales.sale_date
FROM
Sales
INNER JOIN
Customers ON Sales.customer_id = Customers.id
INNER JOIN
Medications ON Sales.medication_id = Medications.id
WHERE
Sales.sale_date >= DATEADD(DAY, -30, GETDATE());

-- Sales details of customer Clement Mathew --

SELECT
Sales.id AS SaleID,
Customers.name AS CustomerName,
Medications.name AS MedicationName,
Sales.quantity,
Sales.total_price AS TotalAmount,
Sales.sale_date
FROM
Sales
INNER JOIN
Customers ON Sales.customer_id = Customers.id
INNER JOIN
Medications ON Sales.medication_id = Medications.id
WHERE
Customers.name = 'Clement Mathew';

-- All medications expire in 60 days --

SELECT
id AS MedicationID,
name AS MedicationName,
category,
price,
expiry_date,
stock_quantity
FROM
Medications
WHERE
expiry_date <= DATEADD(DAY, 60, GETDATE());

-- Total revenue generated by sales in month of October --

SELECT
SUM(total_price) AS TotalRevenue
FROM
Sales
WHERE
MONTH(sale_date) = 10;

-- Total quantity of each medication sold --

SELECT
M.id AS MedicationID,
M.name AS MedicationName,
SUM(S.quantity) AS TotalQuantitySold
FROM
Medications M
JOIN
Sales S ON M.id = S.medication_id
GROUP BY
M.id, M.name;

-- Total number of customers made purchases --

SELECT
COUNT(DISTINCT customer_id) AS TotalCustomers
FROM
Sales;

-- Remove all medications that have expired as of today --

DELETE FROM Medications
WHERE expiry_date < GETDATE();

-- Delete a customer who has requested to be removed from the database --

DELETE FROM Customers
WHERE name = 'Clement Mathew';

-- Process Sale --

EXEC ProcessSale @customer_id = 2, @medication_id = 2, @quantity = 2;

```

```sql

-- Updating values to Medications --

CREATE PROCEDURE update_Medications
    @name VARCHAR(100),
    @category VARCHAR(50),
    @price DECIMAL(10, 2),
    @expiry_date DATE,
    @supplier_id INT,
    @type VARCHAR(20),
    @quantity INT
AS
BEGIN

    DECLARE @present INT;

    SELECT @present = COUNT(*)
    FROM Medications
    WHERE name = @name;

    IF @present > 0
    BEGIN
        IF @type = 'stock_in'
        BEGIN
            UPDATE Medications
            SET stock_quantity = stock_quantity + @quantity
            WHERE name = @name;
        END

        IF @type = 'stock_out'
        BEGIN
            UPDATE Medications
            SET stock_quantity = stock_quantity - @quantity
            WHERE name = @name;
        END
    END

    ELSE
    BEGIN
        INSERT INTO Medications (name, category, price, expiry_date, supplier_id, stock_quantity)
        VALUES
        (@name, @category, @price, @expiry_date, @supplier_id, @quantity);
    END
END

```

```sql
CREATE PROCEDURE ProcessSale
    @customer_id INT,
    @medication_id INT,
    @quantity INT
AS
BEGIN
    DECLARE @currentStock INT;
    DECLARE @price DECIMAL(10, 2);

    -- Check current stock quantity

    SELECT @currentStock = stock_quantity,
           @price = price
    FROM Medications
    WHERE id = @medication_id;

    -- Ensure there is enough stock to process the sale

    IF @currentStock >= @quantity
    BEGIN
        -- Insert the sale record

        INSERT INTO Sales (customer_id, medication_id, quantity, total_price)
        VALUES (@customer_id, @medication_id, @quantity, @quantity * @price);

        -- Update the stock quantity in the Medications table

        UPDATE Medications
        SET stock_quantity = stock_quantity - @quantity
        WHERE id = @medication_id;

        PRINT 'Sale processed successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Not enough stock available for this sale.';
    END
END;

```
