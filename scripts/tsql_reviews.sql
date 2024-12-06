
-- Example T-SQL script for testing CRUD operations

-- Step 1: Declare variables for test data
DECLARE @UserID INT = 1;  -- Assuming there's already a user with ID 1
DECLARE @ProductID INT = 101;  -- Assuming there's already a product with ID 101
DECLARE @Rating INT = 4;  -- Rating between 1 and 5
DECLARE @Comment VARCHAR(MAX) = 'Great product, highly recommend!';

-- Step 2: Insert a new review
PRINT '---- INSERTING NEW REVIEW ----';
INSERT INTO Reviews (user_id, product_id, rating, comment)
VALUES (@UserID, @ProductID, @Rating, @Comment);

-- Verification of insertion
IF EXISTS (SELECT 1 FROM Reviews WHERE user_id = @UserID AND product_id = @ProductID)
    PRINT 'Review insertion successful!';
ELSE
    PRINT 'Review insertion failed!';

-- Step 3: Retrieve the inserted review to confirm the insert
PRINT '---- SELECTING INSERTED REVIEW ----';
SELECT * FROM Reviews WHERE user_id = @UserID AND product_id = @ProductID;

-- Step 4: Update the review rating and comment
DECLARE @NewRating INT = 5;  -- New rating between 1 and 5
DECLARE @NewComment VARCHAR(MAX) = 'Absolutely love it, will buy again!';

PRINT '---- UPDATING REVIEW ----';
UPDATE Reviews
SET rating = @NewRating,
    comment = @NewComment
WHERE user_id = @UserID AND product_id = @ProductID;

-- Verification of update (convert TEXT to VARCHAR(MAX) during comparison)
IF EXISTS (SELECT 1 FROM Reviews WHERE user_id = @UserID AND product_id = @ProductID 
           AND rating = @NewRating 
           AND CAST(comment AS VARCHAR(MAX)) = @NewComment)
    PRINT 'Review update successful!';
ELSE
    PRINT 'Review update failed!';

-- Step 5: Retrieve the updated review to confirm the changes
PRINT '---- SELECTING UPDATED REVIEW ----';
SELECT * FROM Reviews WHERE user_id = @UserID AND product_id = @ProductID;

-- Step 6: Delete the review
PRINT '---- DELETING REVIEW ----';
DELETE FROM Reviews
WHERE user_id = @UserID AND product_id = @ProductID;

-- Verification of deletion
IF NOT EXISTS (SELECT 1 FROM Reviews WHERE user_id = @UserID AND product_id = @ProductID)
    PRINT 'Review deletion successful!';
ELSE
    PRINT 'Review deletion failed!';

-- Step 7: Attempt to retrieve the deleted review (should return no results)
PRINT '---- VERIFYING REVIEW DELETION ----';
SELECT * FROM Reviews WHERE user_id = @UserID AND product_id = @ProductID;
