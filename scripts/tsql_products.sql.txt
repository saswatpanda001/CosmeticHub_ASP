-- Step 1: Declare variables for test data
DECLARE @ProductName VARCHAR(255) = 'Wireless Mouse';
DECLARE @Description VARCHAR(MAX) = 'A high-quality wireless mouse with ergonomic design';
DECLARE @Price DECIMAL(10, 2) = 29.99;
DECLARE @QuantityInStock INT = 150;
DECLARE @Category VARCHAR(100) = 'Electronics';
DECLARE @ImageUrl VARCHAR(255) = 'http://example.com/image.jpg';

-- Step 2: Insert a new product
PRINT '---- INSERTING NEW PRODUCT ----';
INSERT INTO Products (product_name, description, price, quantity_in_stock, category, image_url)
VALUES (@ProductName, @Description, @Price, @QuantityInStock, @Category, @ImageUrl);

-- Verification of insertion
IF EXISTS (SELECT 1 FROM Products WHERE product_name = @ProductName)
    PRINT 'Product insertion successful!';
ELSE
    PRINT 'Product insertion failed!';

-- Step 3: Retrieve the product to confirm the insert
PRINT '---- SELECTING PRODUCT ----';
SELECT * FROM Products WHERE product_name = @ProductName;

-- Step 4: Update the product details (price and quantity)
DECLARE @NewPrice DECIMAL(10, 2) = 24.99;
DECLARE @NewQuantity INT = 200;

PRINT '---- UPDATING PRODUCT DETAILS ----';
UPDATE Products
SET price = @NewPrice,
    quantity_in_stock = @NewQuantity,
    updated_at = GETDATE()
WHERE product_name = @ProductName;

-- Verification of update
IF EXISTS (SELECT 1 FROM Products WHERE product_name = @ProductName AND price = @NewPrice)
    PRINT 'Product update successful!';
ELSE
    PRINT 'Product update failed!';

-- Step 5: Retrieve the updated product to confirm the changes
PRINT '---- SELECTING UPDATED PRODUCT ----';
SELECT * FROM Products WHERE product_name = @ProductName;

-- Step 6: Delete the product
PRINT '---- DELETING PRODUCT ----';
DELETE FROM Products
WHERE product_name = @ProductName;

-- Verification of deletion
IF NOT EXISTS (SELECT 1 FROM Products WHERE product_name = @ProductName)
    PRINT 'Product deletion successful!';
ELSE
    PRINT 'Product deletion failed!';

-- Step 7: Attempt to retrieve the deleted product (should return no results)
PRINT '---- VERIFYING PRODUCT DELETION ----';
SELECT * FROM Products WHERE product_name = @ProductName;
