    -- Step 1: Declare variables for test data
    DECLARE @Username VARCHAR(255) = 'jane_doe';
    DECLARE @Password VARCHAR(255) = 'password123';
    DECLARE @FirstName VARCHAR(255) = 'Jane';
    DECLARE @LastName VARCHAR(255) = 'Doe';
    DECLARE @Email VARCHAR(255) = 'jane.doe@example.com';
    DECLARE @PhoneNumber VARCHAR(20) = '1234567890';
    DECLARE @Address VARCHAR(MAX) = '456 Oak Street'; -- Use VARCHAR(MAX) instead of TEXT
    DECLARE @UserType VARCHAR(50) = 'admin';

    -- Step 2: Insert a new user
    PRINT '---- INSERTING NEW USER ----';
    INSERT INTO Users (username, password, first_name, last_name, email, phone_number, address, user_type)
    VALUES (@Username, @Password, @FirstName, @LastName, @Email, @PhoneNumber, @Address, @UserType);

    -- Verification of insertion
    IF EXISTS (SELECT 1 FROM Users WHERE username = @Username)
        PRINT 'User insertion successful!';
    ELSE
        PRINT 'User insertion failed!';

    -- Step 3: Retrieve the user to confirm the insert
    PRINT '---- SELECTING USER ----';
    SELECT * FROM Users WHERE username = @Username;

    -- Step 4: Update the user's phone number and address
    DECLARE @NewPhoneNumber VARCHAR(20) = '9876543210';
    DECLARE @NewAddress VARCHAR(MAX) = '789 Pine Street'; -- Use VARCHAR(MAX) instead of TEXT

    PRINT '---- UPDATING USER DETAILS ----';
    UPDATE Users
    SET phone_number = @NewPhoneNumber,
        address = @NewAddress,
        updated_at = GETDATE()
    WHERE username = @Username;

    -- Verification of update
    IF EXISTS (SELECT 1 FROM Users WHERE username = @Username AND phone_number = @NewPhoneNumber)
        PRINT 'User update successful!';
    ELSE
        PRINT 'User update failed!';

    -- Step 5: Retrieve the updated user to confirm the changes
    PRINT '---- SELECTING UPDATED USER ----';
    SELECT * FROM Users WHERE username = @Username;

    -- Step 6: Delete the user
    PRINT '---- DELETING USER ----';
    DELETE FROM Users
    WHERE username = @Username;

    -- Verification of deletion
    IF NOT EXISTS (SELECT 1 FROM Users WHERE username = @Username)
        PRINT 'User deletion successful!';
    ELSE
        PRINT 'User deletion failed!';

    -- Step 7: Attempt to retrieve the deleted user (should return no results)
    PRINT '---- VERIFYING USER DELETION ----';
    SELECT * FROM Users WHERE username = @Username;
