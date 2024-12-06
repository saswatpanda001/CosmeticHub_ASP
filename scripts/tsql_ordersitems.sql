-- Step 1: Declare variables for test data
DECLARE @OrderID INT = 1;  -- Assuming there's already an order with ID 1
DECLARE @ProductID INT = 101;  -- Assuming there's already a product with ID 101
DECLARE @Quantity INT = 2;
DECLARE @PriceEach DECIMAL(10, 2) = 49.99;

-- Step 2: Insert a new order item
PRINT '---- INSERTING NEW ORDER ITEM ----';
INSERT INTO Order_Items (order_id, product_id, quantity, price_each)
VALUES (@OrderID, @ProductID, @Quantity, @PriceEach);

-- Verification of insertion
IF EXISTS (SELECT 1 FROM Order_Items WHERE order_id = @OrderID AND product_id = @ProductID)
    PRINT 'Order item insertion successful!';
ELSE
    PRINT 'Order item insertion failed!';

-- Step 3: Retrieve the inserted order item to confirm the insert
PRINT '---- SELECTING INSERTED ORDER ITEM ----';
SELECT * FROM Order_Items WHERE order_id = @OrderID AND product_id = @ProductID;

-- Step 4: Update the order item quantity and price
DECLARE @NewQuantity INT = 3;
DECLARE @NewPriceEach DECIMAL(10, 2) = 59.99;

PRINT '---- UPDATING ORDER ITEM ----';
UPDATE Order_Items
SET quantity = @NewQuantity,
    price_each = @NewPriceEach
WHERE order_id = @OrderID AND product_id = @ProductID;

-- Verification of update
IF EXISTS (SELECT 1 FROM Order_Items WHERE order_id = @OrderID AND product_id = @ProductID AND quantity = @NewQuantity AND price_each = @NewPriceEach)
    PRINT 'Order item update successful!';
ELSE
    PRINT 'Order item update failed!';

-- Step 5: Retrieve the updated order item to confirm the changes
PRINT '---- SELECTING UPDATED ORDER ITEM ----';
SELECT * FROM Order_Items WHERE order_id = @OrderID AND product_id = @ProductID;

-- Step 6: Delete the order item
PRINT '---- DELETING ORDER ITEM ----';
DELETE FROM Order_Items
WHERE order_id = @OrderID AND product_id = @ProductID;

-- Verification of deletion
IF NOT EXISTS (SELECT 1 FROM Order_Items WHERE order_id = @OrderID AND product_id = @ProductID)
    PRINT 'Order item deletion successful!';
ELSE
    PRINT 'Order item deletion failed!';

-- Step 7: Attempt to retrieve the deleted order item (should return no results)
PRINT '---- VERIFYING ORDER ITEM DELETION ----';
SELECT * FROM Order_Items WHERE order_id = @OrderID AND product_id = @ProductID;
