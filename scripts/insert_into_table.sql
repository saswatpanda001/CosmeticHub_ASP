INSERT INTO Users (username, password, first_name, last_name, email, phone_number, address, user_type)
VALUES 
('michael_doe', 'canada@123', 'Michael', 'Doe', 'michael.doe@example.com', '1234567890', '123 Elm Street, City', 'customer'),
('admin_master', 'canada@123', 'Admin', 'Master', 'admin.master@example.com', '0987654321', '456 Maple Avenue, City', 'admin'),
('emily_smith', 'canada@123', 'Emily', 'Smith', 'emily.smith@example.com', '1122334455', '789 Oak Road, City', 'customer'),
('david_jones', 'canada@123', 'David', 'Jones', 'david.jones@example.com', '6677889900', '101 Pine Lane, City', 'customer'),
('mark_brown', 'canada@123', 'Mark', 'Brown', 'mark.brown@example.com', '9988776655', '202 Birch Way, City', 'customer'),
('susan_white', 'canada@123', 'Susan', 'White', 'susan.white@example.com', '3344556677', '303 Cedar Blvd, City', 'customer');


INSERT INTO Products (product_name, description, price, quantity_in_stock, category, image_url)
VALUES 
('Hair Gel', 'Strong hold hair gel for styling.', 8.99, 90, 'Hair Care', 'https://example.com/hair_gel.jpg'),
('Face Wash', 'Gentle face wash for oily skin.', 12.00, 75, 'Skincare', 'https://example.com/face_wash.jpg'),
('Shaving Cream', 'Rich shaving cream for a smooth shave.', 5.99, 200, 'Men\'s Care', 'https://example.com/shaving_cream.jpg'),
('Deodorant', 'Long-lasting deodorant with a fresh scent.', 6.50, 180, 'Men\'s Care', 'https://example.com/deodorant.jpg'),
('Body Lotion', 'Moisturizing body lotion for soft skin.', 9.00, 150, 'Skincare', 'https://example.com/body_lotion.jpg'),
('Aftershave', 'Soothing aftershave for a fresh feel.', 10.99, 130, 'Men\'s Care', 'https://example.com/aftershave.jpg');


INSERT INTO Reviews (user_id, product_id, rating, comment)
VALUES 
(1, 1, 5, 'Great lipstick! Long-lasting and smooth texture.'),
(2, 3, 4, 'Good shampoo, but I wish it smelled better.'),
(3, 5, 5, 'Amazing face cream, my skin feels so soft.'),
(4, 2, 3, 'Decent moisturizer, but a bit too greasy for my liking.'),
(5, 4, 5, 'Love the vibrant colors of the nail polish!'),
(6, 6, 4, 'Fragrance is nice, but not as strong as I expected.');

INSERT INTO Order_Items (order_id, product_id, quantity, price_each)
VALUES 
(1, 1, 2, 15.99),
(1, 2, 1, 22.49),
(2, 3, 3, 10.99),
(2, 4, 2, 7.99),
(3, 5, 2, 30.50),
(4, 6, 1, 45.00);


INSERT INTO Orders (user_id, total_price, order_status, shipping_address)
VALUES 
(1, 75.96, 'Pending', '123 Elm Street, City'),
(2, 120.48, 'Shipped', '456 Maple Avenue, City'),
(3, 45.47, 'Pending', '789 Oak Road, City'),
(4, 25.99, 'Delivered', '101 Pine Lane, City'),
(5, 52.48, 'Shipped', '202 Birch Way, City'),
(6, 92.50, 'Delivered', '303 Cedar Blvd, City');


INSERT INTO Products (product_name, description, price, quantity_in_stock, category, image_url)
VALUES 
('Lipstick', 'Long-lasting matte lipstick in various shades.', 15.99, 100, 'Makeup', ''),
('Moisturizer', 'Hydrating moisturizer for all skin types.', 22.49, 200, 'Skincare', ''),
('Shampoo', 'Shampoo for dry and damaged hair, sulfate-free.', 10.99, 150, 'Hair Care', ''),
('Nail Polish', 'Bright and vibrant nail polish in multiple colors.', 7.99, 120, 'Makeup', ''),
('Face Cream', 'Anti-aging face cream with SPF 30.', 30.50, 80, 'Skincare', ''),
('Perfume', 'Luxury fragrance with floral notes.', 45.00, 50, 'Fragrance', '');





select * from Users;
select * from Reviews;
select * from Products;
select * from Order_Items;
select * from Orders;




