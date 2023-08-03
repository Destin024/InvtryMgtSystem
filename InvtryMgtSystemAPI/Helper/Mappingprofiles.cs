using AutoMapper;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Helper
{
    public class Mappingprofiles:Profile
    {
        public  Mappingprofiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Inventory, InventoryDto>();
            CreateMap<InventoryDto, Inventory>().ForMember(dest=>dest.InventoryId,opt=>opt.MapFrom(src=>src.ProductId));

            CreateMap<Product, ProductInventoryDto>().ForMember(dest => dest.ProductInventoryId, opt => opt.MapFrom(src => src.CategoryId));
            CreateMap<ProductInventoryDto, Product>();

            CreateMap<Store, StoreDto>();
            CreateMap<StoreDto, Store>();

            CreateMap<StockTransfer, StockTransferDto>();
            CreateMap<StockTransferDto, StockTransfer>().ForMember(dest=>dest.StockTransferId,opt=>opt.MapFrom(src=>src.StoreId));

            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>().ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.InventoryId));


            //CreateMap<User, UserDto>();
            //CreateMap<UserDto, User>();
        }
    }
}
