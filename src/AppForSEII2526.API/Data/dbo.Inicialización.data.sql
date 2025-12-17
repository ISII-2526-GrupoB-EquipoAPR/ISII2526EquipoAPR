
SET IDENTITY_INSERT [dbo].[Models] ON
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (9, N'Civic')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (10, N'Corolla')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (11, N'Model 3')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (12, N'CX-5')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (13, N'A-Class')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (14, N'CX-30')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (15, N'Q3')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (16, N'Golf')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (17, N'Fiesta')
SET IDENTITY_INSERT [dbo].[Models] OFF

SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (6, N'Sedan', N'Rojo', N'Compacto eficiente', N'1.8', N'Honda', CAST(18000.00 AS Decimal(10, 2)), CAST(5000.00 AS Decimal(10, 2)), 10, 7, N'Gasolina', N'16', 9)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (7, N'Harchback', N'Azul', N'Deportivo urbano', N'1.5', N'Toyota', CAST(17000.00 AS Decimal(10, 2)), CAST(2000.00 AS Decimal(10, 2)), 8,9, N'Híbrido', N'15', 10)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (8, N'Sedan', N'Blanco', N'Électrico premium', N'0', N'Tesla', CAST(38000.00 AS Decimal(10, 2)), CAST(4000.00 AS Decimal(10, 2)), 5, 6, N'Eléctrico', N'18', 11)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (9, N'SUV', N'Gris', N'Familiar espacioso', N'2', N'Mazda', CAST(26000.00 AS Decimal(10, 2)), CAST(3000.00 AS Decimal(10, 2)), 7, 6, N'Gasolina', N'17', 12)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (12, N'Compacto', N'Negro', N'Pequeño y económico', N'1.6', N'Mercedes-Benz', CAST(24000.00 AS Decimal(10, 2)), CAST(2500.00 AS Decimal(10, 2)), 6, 7, N'Diésel', N'16', 13)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (13, N'SUV', N'Azul', N'Compacto y moderno', N'2.0', N'Mazda', CAST(25000.00 AS Decimal(10,2)), CAST(3500.00 AS Decimal(10,2)), 5, 7, N'Gasolina', N'17', 14);
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (14, N'SUV', N'Blanco', N'Lujo compacto', N'2.0', N'Audi', CAST(42000.00 AS Decimal(10,2)), CAST(4500.00 AS Decimal(10,2)), 3, 8, N'Diésel', N'18', 15);
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (15, N'Hatchback', N'Rojo', N'Económico y fiable', N'1.4', N'Volkswagen', CAST(20000.00 AS Decimal(10,2)), CAST(1800.00 AS Decimal(10,2)), 6, 5, N'Gasolina', N'16', 16);
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (16, N'Hatchback', N'Negro', N'Ideal para ciudad', N'1.0', N'Ford', CAST(17000.00 AS Decimal(10,2)), CAST(1500.00 AS Decimal(10,2)), 8, 6, N'Gasolina', N'15', 17);

SET IDENTITY_INSERT [dbo].[Cars] OFF

SET IDENTITY_INSERT [dbo].[Purchases] ON
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (15, N'Elena', N'Navarro Martínez', N'Avda. España 2, Albacete', N'Laura', 0, N'2025-01-15 00:00:00', CAST(18000.00 AS Decimal(10, 2)), N'1')
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (16, N'Gregorio', N'Díaz Descalzo', N'Avda. España 25, Ciudad Real', N'María', 1, N'2025-01-16 00:00:00', CAST(34000.00 AS Decimal(10, 2)), N'2')
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (17, N'Peter', N'Jackson', N'Avda. España 75, London', N'Juan', 2, N'2025-01-17 00:00:00', CAST(38000.00 AS Decimal(10, 2)), N'3')
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (18, N'Elena', N'Navarro Martínez', N'Avda. España 2, Albacete', N'Rodrigo', 0, N'2025-02-20 00:00:00', CAST(25000.00 AS Decimal(10,2)), N'1');
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (19, N'Peter', N'Jackson', N'Avda. España 75, London', N'Mercedes', 1, N'2025-03-15 00:00:00', CAST(42000.00 AS Decimal(10,2)), N'3');
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (20, N'Gregorio', N'Díaz Descalzo', N'Avda. España 25, Ciudad Real', N'Patricia', 2, N'2025-04-10 00:00:00', CAST(20000.00 AS Decimal(10,2)), N'2');
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (21, N'Elena', N'Navarro Martínez', N'Avda. España 2, Albacete', N'Luis', 1, N'2025-05-05 00:00:00', CAST(17000.00 AS Decimal(10,2)), N'1');

SET IDENTITY_INSERT [dbo].[Purchases] OFF


INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (6, 15, CAST(18000.00 AS Decimal(10, 2)), 1)
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (7, 16, CAST(17000.00 AS Decimal(10, 2)), 2)
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (8, 17, CAST(38000.00 AS Decimal(10, 2)), 1)
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (13, 18, CAST(25000.00 AS Decimal(10,2)), 1);
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (14, 19, CAST(42000.00 AS Decimal(10,2)), 1);
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (15, 20, CAST(20000.00 AS Decimal(10,2)), 1);
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (16, 21, CAST(17000.00 AS Decimal(10,2)), 1);

SET IDENTITY_INSERT [dbo].[Rentals] ON
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (4, N'Avda. España 2, Albacete', N'Elena', N'Navarro Martínez', N'Luis', N'2025-06-08 00:00:00', N'2025-06-01 00:00:00', N'2025-05-28 00:00:00', 0, CAST(330.00 AS Decimal(10, 2)), N'1')
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (5, N'Avda. España 25, Ciudad Real', N'Gregorio', N'Díaz Descalzo', N'Mercedes', N'2025-07-25 00:00:00', N'2025-07-15 00:00:00', N'2025-07-10 00:00:00', 1, CAST(1120.00 AS Decimal(10, 2)), N'2')
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (8, N'Avda. España 75, London', N'Peter', N'Jackson', N'Adela', N'2025-08-15 00:00:00', N'2025-08-10 00:00:00', N'2025-08-05 00:00:00', 2, CAST(425.00 AS Decimal(10, 2)), N'3')
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (9, N'Avda. España 2, Albacete', N'Elena', N'Navarro Martínez', N'Patricia', N'2025-08-10 00:00:00', N'2025-08-01 00:00:00', N'2025-07-28 00:00:00', 0, CAST(450.00 AS Decimal(10,2)), N'1');
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (10, N'Avda. España 75, London', N'Peter', N'Jackson', N'Laura', N'2025-09-15 00:00:00', N'2025-09-10 00:00:00', N'2025-09-05 00:00:00', 1, CAST(700.00 AS Decimal(10,2)), N'3');
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (11, N'Avda. España 25, Ciudad Real', N'Gregorio', N'Díaz Descalzo', N'Miguel', N'2025-10-05 00:00:00', N'2025-09-28 00:00:00', N'2025-09-25 00:00:00', 2, CAST(300.00 AS Decimal(10,2)), N'2');
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (12, N'Avda. España 2, Albacete', N'Elena', N'Navarro Martínez', N'Rodrigo', N'2025-11-10 00:00:00', N'2025-11-01 00:00:00', N'2025-10-28 00:00:00', 1, CAST(250.00 AS Decimal(10,2)), N'1');

SET IDENTITY_INSERT [dbo].[Rentals] OFF

INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (6, 4, 1, CAST(55.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (7, 5, 2, CAST(80.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (8, 8, 1, CAST(85.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (13, 9, 1, CAST(55.00 AS Decimal(10,2)));
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (14, 10, 1, CAST(90.00 AS Decimal(10,2)));
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (15, 11, 1, CAST(45.00 AS Decimal(10,2)));
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (16, 12, 1, CAST(35.00 AS Decimal(10,2)));


SET IDENTITY_INSERT [dbo].[Reviews] ON
INSERT INTO [dbo].[Reviews] ([Id], [Created], [Country], [DriverType], [ApplicationUserId]) VALUES (1, N'2025-01-20 00:00:00', N'España', 0, N'1')
INSERT INTO [dbo].[Reviews] ([Id], [Created], [Country], [DriverType], [ApplicationUserId]) VALUES (2, N'2025-02-25 00:00:00', N'Francia', 1, N'3')
INSERT INTO [dbo].[Reviews] ([Id], [Created], [Country], [DriverType], [ApplicationUserId]) VALUES (3, N'2025-03-05 00:00:00', N'Italia', 0, N'2');
INSERT INTO [dbo].[Reviews] ([Id], [Created], [Country], [DriverType], [ApplicationUserId]) VALUES (4, N'2025-04-12 00:00:00', N'Alemania', 1, N'1');
INSERT INTO [dbo].[Reviews] ([Id], [Created], [Country], [DriverType], [ApplicationUserId]) VALUES (5, N'2025-05-20 00:00:00', N'Portugal', 0, N'2');
INSERT INTO [dbo].[Reviews] ([Id], [Created], [Country], [DriverType], [ApplicationUserId]) VALUES (6, N'2025-06-18 00:00:00', N'España', 1, N'3');

SET IDENTITY_INSERT [dbo].[Reviews] OFF

INSERT INTO [dbo].[ReviewItems] ([CarId], [ReviewId], [Description], [Rating]) VALUES (6, 1, N'Excelento coche, económico y fiable', 5)
INSERT INTO [dbo].[ReviewItems] ([CarId], [ReviewId], [Description], [Rating]) VALUES (7, 2, N'Muy buen coche híbrido', 4)
INSERT INTO [dbo].[ReviewItems] ([CarId], [ReviewId], [Description], [Rating]) VALUES (13, 3, N'Muy cómodo para viajes largos', 5);
INSERT INTO [dbo].[ReviewItems] ([CarId], [ReviewId], [Description], [Rating]) VALUES (14, 4, N'Lujo y confort excelentes', 4);
INSERT INTO [dbo].[ReviewItems] ([CarId], [ReviewId], [Description], [Rating]) VALUES (15, 5, N'Muy eficiente y económico', 4);
INSERT INTO [dbo].[ReviewItems] ([CarId], [ReviewId], [Description], [Rating]) VALUES (16, 6, N'Perfecto para ciudad, fácil de aparcar', 5);

