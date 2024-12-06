-- Step 1: Declare variables for test data
DECLARE @UserID INT = 1;  -- Assuming there's already a user with ID 1
DECLARE @TotalPrice DECIMAL(10, 2) = 149.99;
DECLARE @OrderStatus VARCHAR(50) = 'Pending';
DECLARE @ShippingAddress VARCHAR(MAX) = '1234 Elm Street, Springfield, IL';
DECLARE @PlacedAt DATETIME = GETDATE();

-- Step 2: Insert a new order
PRINT '---- INSERTING NEW ORDER ----';
INSERT INTO Orders (user_id, total_price, order_status, shipping_address, placed_at)
VALUES (@UserID, @TotalPrice, @OrderStatus, @ShippingAddress, @PlacedAt);

-- Verification of insertion
IF EXISTS (SELECT 1 FROM Orders WHERE user_id = @UserID AND total_price = @TotalPrice)
    PRINT 'Order insertion successful!';
ELSE
    PRINT 'Order insertion failed!';

-- Step 3: Retrieve the inserted order to confirm the insert
PRINT '---- SELECTING INSERTED ORDER ----';
SELECT * FROM Orders WHERE user_id = @UserID AND total_price = @TotalPrice;

-- Step 4: Update the order status and shipping address
DECLARE @NewOrderStatus VARCHAR(50) = 'Shipped';
DECLARE @NewShippingAddress VARCHAR(MAX) = '5678 Oak Avenue, Springfield, IL';

PRINT '---- UPDATING ORDER DETAILS ----';
UPDATE Orders
SET order_status = @NewOrderStatus,
    shipping_address = @NewShippingAddress,
    shipped_at = GETDATE()
WHERE user_id = @UserID AND total_price = @TotalPrice;

-- Verification of update
IF EXISTS (SELECT 1 FROM Orders WHERE user_id = @UserID AND order_status = @NewOrderStatus)
    PRINT 'Order update successful!';
ELSE
    PRINT 'Order update failed!';

-- Step 5: Retrieve the updated order to confirm the changes
PRINT '---- SELECTING UPDATED ORDER ----';
SELECT * FROM Orders WHERE user_id = @UserID AND order_status = @NewOrderStatus;

-- Step 6: Delete the order
PRINT '---- DELETING ORDER ----';
DELETE FROM Orders
WHERE user_id = @UserID AND total_price = @TotalPrice;

-- Verification of deletion
IF NOT EXISTS (SELECT 1 FROM Orders WHERE user_id = @UserID AND total_price = @TotalPrice)
    PRINT 'Order deletion successful!';
ELSE
    PRINT 'Order deletion failed!';

-- Step 7: Attempt to retrieve the deleted order (should return no results)
PRINT '---- VERIFYING ORDER DELETION ----';
SELECT * FROM Orders WHERE user_id = @UserID AND total_price = @TotalPrice;
