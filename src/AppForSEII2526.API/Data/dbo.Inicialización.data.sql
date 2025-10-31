INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'12', N'guest', N'guest', N'guest', N'guest', N'test@test.com', N'test@test.com', 1, N'guest', N'guest', N'guest', N'123', 1, 0, N'12/12/2024 0:00:00 +01:00', 1, 12)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'13', N'test', N'test', N'test', N'test', N'tester@test.com', N'tester@test.com', 1, N'test', N'test', N'test', N'1234', 1, 0, N'31/10/2025 0:00:00 +01:00', 1, 13)

SET IDENTITY_INSERT [dbo].[Models] ON
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (9, N'Civic')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (10, N'Corolla')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (11, N'Model 3')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (12, N'CX-5')
INSERT INTO [dbo].[Models] ([Id], [Name]) VALUES (13, N'A-Class')
SET IDENTITY_INSERT [dbo].[Models] OFF

SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (6, N'Sedan', N'Rojo', N'Compacto eficiente', N'1.8', N'Honda', CAST(18000.00 AS Decimal(10, 2)), CAST(5000.00 AS Decimal(10, 2)), 10, 1, N'Gasolina', N'16', 9)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (7, N'Harchback', N'Azul', N'Deportivo urbano', N'1.5', N'Toyota', CAST(17000.00 AS Decimal(10, 2)), CAST(2000.00 AS Decimal(10, 2)), 8, 1, N'Híbrido', N'15', 10)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (8, N'Sedan', N'Blanco', N'Électrico premium', N'0', N'Tesla', CAST(38000.00 AS Decimal(10, 2)), CAST(4000.00 AS Decimal(10, 2)), 5, 1, N'Eléctrico', N'18', 11)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (9, N'SUV', N'Gris', N'Familiar espacioso', N'2', N'Mazda', CAST(26000.00 AS Decimal(10, 2)), CAST(3000.00 AS Decimal(10, 2)), 7, 1, N'Gasolina', N'17', 12)
INSERT INTO [dbo].[Cars] ([Id], [CarClass], [Color], [Description], [EngDisplacement], [Manufacturer], [PurchasingPrice], [RentingPrice], [QuantityForPurchasing], [QuantityForRenting], [FuelType], [RimSize], [ModelId]) VALUES (12, N'Compacto', N'Negro', N'Pequeño y económico', N'1.6', N'Mercedes-Benz', CAST(24000.00 AS Decimal(10, 2)), CAST(2500.00 AS Decimal(10, 2)), 6, 1, N'Diésel', N'16', 13)
SET IDENTITY_INSERT [dbo].[Cars] OFF

INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (6, 15, CAST(18000.00 AS Decimal(10, 2)), 1)
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (7, 16, CAST(17000.00 AS Decimal(10, 2)), 2)
INSERT INTO [dbo].[PurchaseItems] ([CarId], [PurchaseId], [Price], [Quantity]) VALUES (8, 17, CAST(38000.00 AS Decimal(10, 2)), 1)

SET IDENTITY_INSERT [dbo].[Purchases] ON
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (15, N'test', N'test', N'test address', N'4 días', 0, N'2025-01-15 00:00:00', CAST(18000.00 AS Decimal(10, 2)), N'13')
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (16, N'guest', N'guest', N'guest address', N'5 días', 1, N'2025-01-16 00:00:00', CAST(34000.00 AS Decimal(10, 2)), N'12')
INSERT INTO [dbo].[Purchases] ([Id], [CustomerUserName], [CustomerNameSurname], [DeliveryAddress], [DeliveryCarDealer], [PaymentMethod], [PurchasingDate], [PurchasingPrice], [ApplicationUserId]) VALUES (17, N'guest', N'guest', N'guest address', N'3 días', 2, N'2025-01-17 00:00:00', CAST(38000.00 AS Decimal(10, 2)), N'12')
SET IDENTITY_INSERT [dbo].[Purchases] OFF

INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (6, 4, 1, CAST(55.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (7, 5, 2, CAST(80.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[RentalItems] ([CarId], [RentalId], [Quantity], [PriceForRenting]) VALUES (8, 8, 1, CAST(85.00 AS Decimal(18, 2)))


SET IDENTITY_INSERT [dbo].[Rentals] ON
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (4, N'test address', N'test', N'test', N'Inmediata', N'2025-06-01 00:00:00', N'2025-06-07 00:00:00', N'2025-05-28 00:00:00', 0, CAST(330.00 AS Decimal(10, 2)), N'13')
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (5, N'guest address', N'guest', N'guest', N'Aeropuesto', N'2025-07-15 00:00:00', N'2025-07-25 00:00:00', N'2025-07-10 00:00:00', 1, CAST(1120.00 AS Decimal(10, 2)), N'12')
INSERT INTO [dbo].[Rentals] ([Id], [DeliveryAddress], [CustomerUserName], [CustomerNameSurname], [DeliveryCarDealer], [EndDate], [StartDate], [RentingDate], [PaymentMethod], [RentingPrice], [ApplicationUserId]) VALUES (8, N'test address', N'test', N'test', N'Entrega hotel', N'2025-08-10 00:00:00', N'2025-08-15 00:00:00', N'2025-08-05 00:00:00', 2, CAST(425.00 AS Decimal(10, 2)), N'13')
SET IDENTITY_INSERT [dbo].[Rentals] OFF

SET IDENTITY_INSERT [dbo].[Reviews] ON
INSERT INTO [dbo].[Reviews] ([Id], [Created], [Country], [DriverType], [ApplicationUserId]) VALUES (1, N'2025-01-20 00:00:00', N'España', 0, N'12')
INSERT INTO [dbo].[Reviews] ([Id], [Created], [Country], [DriverType], [ApplicationUserId]) VALUES (2, N'2025-02-25 00:00:00', N'Francia', 1, N'13')
SET IDENTITY_INSERT [dbo].[Reviews] OFF

INSERT INTO [dbo].[ReviewItems] ([CarId], [ReviewId], [Description], [Rating]) VALUES (6, 1, N'Excelento coche, económico y fiable', 5)
INSERT INTO [dbo].[ReviewItems] ([CarId], [ReviewId], [Description], [Rating]) VALUES (7, 2, N'Muy buen coche híbrido', 4)
