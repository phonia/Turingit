using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringL.ViewModels;
using TuringL.Models;
using AutoMapper;

namespace TuringL.DServices
{
    public class AutoMapperBootStrapper
    {
        public static void ConfigurationMapper()
        {
            //
            Mapper.CreateMap<ProductInfo, ProductInfoView>();
            Mapper.CreateMap<ProductInfoView, ProductInfo>();
            Mapper.CreateMap<ProductAddtionalInfo, ProductAddtionalInfoView>();
            Mapper.CreateMap<ProductAddtionalInfoView, ProductAddtionalInfo>();
            Mapper.CreateMap<List<ProductAddtionalInfoView>, List<ProductAddtionalInfo>>();
            
        }
    }
}
