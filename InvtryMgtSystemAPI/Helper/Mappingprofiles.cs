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

            CreateMap<Inventory, InventoryDto>().ForMember(dest => dest.InventoryId, act => act.MapFrom(src => src.ProductId))
                                                .ForMember(dest => dest.InventoryId, act => act.MapFrom(src => src.StoreId));
            CreateMap<InventoryDto, Inventory>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Store, StoreDto>();
            CreateMap<StoreDto, Store>();

            CreateMap<Transfer, TransferDto>();
            CreateMap<TransferDto, Transfer>().ForMember(dest=>dest.TransferId,act=>act.MapFrom(src=>src.StoreId));

            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>().ForMember(dest => dest.TransactionId, act => act.MapFrom(src => src.UserId))
                                                    .ForMember(dest => dest.TransactionId, act => act.MapFrom(src => src.InventoryId));

            //CreateMap<User, UserDto>();
            //CreateMap<UserDto, User>();
        }
    }
}
