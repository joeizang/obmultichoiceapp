using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;
using RektaRetailApp.Backend.GraphQL.ScalarTypes;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Backend.GraphQL.QueryTypes
{
    public class InventoryQueryType : ObjectType<Inventory>
    {
        protected override void Configure(IObjectTypeDescriptor<Inventory> descriptor)
        {
            descriptor.Field(i => i.ProductSuppliers).Type<NonNullType<ListType<SupplierQueryType>>>();
            descriptor.Field(i => i.Category).Type<NonNullType<CategoryQueryType>>();
            descriptor.Field(i => i.BatchNumber).Type<StringType>();
            descriptor.Field(i => i.Description).Type<StringType>();
            descriptor.Field(i => i.Name).Type<NonNullType<StringType>>();
            descriptor.Field(i => i.Quantity).Type<NonNullType<FloatType>>();
            descriptor.Field(i => i.SupplyDate).Type<NonNullType<DateTimeOffsetType>>();
            descriptor.Field(i => i.Id).Type<NonNullType<IdType>>();
            descriptor.Field(i => i.CreatedAt).Type<NonNullType<DateTimeOffsetType>>();
            descriptor.Field(i => i.UpdatedAt).Type<NonNullType<DateTimeOffsetType>>();
            descriptor.Field(i => i.InventorySuppliers).Type<ListType<SuppliersInventoriesQueryType>>();
        }
    }

    public class SuppliersInventoriesQueryType : ObjectType<SuppliersInventories>
    {
        protected override void Configure(IObjectTypeDescriptor<SuppliersInventories> descriptor)
        {
            descriptor.Field(si => si.InventoryId).Type<IdType>();
            descriptor.Field(si => si.SupplierId).Type<IdType>();
            descriptor.Field(si => si.ProductSupplier).Type<SupplierQueryType>();
            descriptor.Field(si => si.ProductInventory).Type<InventoryQueryType>();
        }
    }

    public class CategoryQueryType : ObjectType<Category>
    {
        protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
        {
            descriptor.Field(c => c.Description).Type<StringType>();
            descriptor.Field(c => c.Name).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
            descriptor.Field(c => c.CreatedAt).Type<NonNullType<DateTimeOffsetType>>();
            descriptor.Field(c => c.UpdatedAt).Type<NonNullType<DateTimeOffsetType>>();
        }
    }

    public class SupplierQueryType : ObjectType<Supplier>
    {
        protected override void Configure(IObjectTypeDescriptor<Supplier> descriptor)
        {
            descriptor.Field(s => s.ProductsSupplied).Type<NonNullType<ListType<ProductQueryType>>>();
            descriptor.Field(s => s.MobileNumber).Type<StringType>();
            descriptor.Field(s => s.Name).Type<NonNullType<StringType>>();
            descriptor.Field(s => s.Id).Type<IdType>();
            descriptor.Field(s => s.CreatedAt).Type<NonNullType<DateTimeOffsetType>>();
            descriptor.Field(s => s.UpdatedAt).Type<NonNullType<DateTimeOffsetType>>();
        }
    }

    public class ProductQueryType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            descriptor.Field(p => p.ItemCategory).Type<NonNullType<CategoryQueryType>>();
            descriptor.Field(p => p.Name).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.RetailPrice).Type<NonNullType<DecimalType>>();
            descriptor.Field(p => p.SuppliedPrice).Type<NonNullType<DecimalType>>();
            descriptor.Field(p => p.UnitPrice).Type<NonNullType<DecimalType>>();
            descriptor.Field(p => p.Id).Type<NonNullType<DecimalType>>();
            descriptor.Field(p => p.CategoryId).Type<NonNullType<IdType>>();
            descriptor.Field(p => p.CreatedAt).Type<NonNullType<DateTimeOffsetType>>();
            descriptor.Field(p => p.UpdatedAt).Type<NonNullType<DateTimeOffsetType>>();
        }
    }
}
