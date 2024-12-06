-- Create Users Table
CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing primary key
    username VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    email VARCHAR(255),
    phone_number VARCHAR(20),
    address TEXT,
    user_type VARCHAR(50),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- Create Products Table
CREATE TABLE Products (
    product_id INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing primary key
    product_name VARCHAR(255) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    quantity_in_stock INT NOT NULL,
    category VARCHAR(100),
    image_url VARCHAR(255),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);

-- Create Orders Table
CREATE TABLE Orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing primary key
    user_id INT NOT NULL,
    total_price DECIMAL(10, 2) NOT NULL,
    order_status VARCHAR(50),
    shipping_address TEXT,
    placed_at DATETIME DEFAULT GETDATE(),
    shipped_at DATETIME
);

-- Create Order_Items Table
CREATE TABLE Order_Items (
    order_item_id INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing primary key
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price_each DECIMAL(10, 2) NOT NULL
);

-- Create Reviews Table
CREATE TABLE Reviews (
    review_id INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing primary key
    user_id INT NOT NULL,
    product_id INT NOT NULL,
    rating INT NOT NULL CHECK (rating BETWEEN 1 AND 5),  -- Rating between 1 and 5
    comment TEXT,
    created_at DATETIME DEFAULT GETDATE()
);
