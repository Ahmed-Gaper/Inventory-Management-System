# BethanysPieShop.InventoryManagement

## Overview
This project is an inventory management system for Bethany's Pie Shop. It allows users to manage products, orders, and settings.

## Features
- Inventory management: Add, view, and clone products.
- Order management: Create and fulfill orders.
- Settings: Change stock threshold.

## File Structure
BethanysPieShop.InventoryManagement/  
├── Domain/  
│   ├── Contracts/  
│   │   └── ISaveable.cs  
│   ├── General/  
│   │   ├── Currency.cs  
│   │   ├── Price.cs  
│   │   └── UnitType.cs  
│   ├── Order/  
│   │   ├── Order.cs  
│   │   └── OrderItem.cs  
│   └── ProductManagement/  
│       ├── Product.cs  
│       ├── BoxedProduct.cs  
│       ├── BulkProduct.cs  
│       ├── FreshProducts.cs  
│       ├── RegularProduct.cs  
│       └── ProductExtensions.cs  
├── Repositories/  
│   └── ProductRepo.cs  
├── Utilities.cs  
└── Program.cs  
