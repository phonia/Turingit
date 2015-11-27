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
            Mapper.CreateMap<ProductInfo, ProductInfoView>()
                .ForMember(d=>d.MaintanceRecords,opt=>opt.MapFrom(s=>s.MaintanceRecords))
                .ForMember(d => d.ProductAddtionalInfos, opt => opt.MapFrom(s => s.ProductAddtionalInfoes));
            Mapper.CreateMap<ProductInfoView, ProductInfo>()
                .ForMember(d=>d.MaintanceRecords,opt=>opt.MapFrom(s=>s.MaintanceRecords))
                .ForMember(d => d.ProductAddtionalInfoes, opt => opt.MapFrom(s => s.ProductAddtionalInfos));
            Mapper.CreateMap<ProductAddtionalInfo, ProductAddtionalInfoView>();
            Mapper.CreateMap<ProductAddtionalInfoView, ProductAddtionalInfo>();
            
        }
    }

    public static class MapHelper
    {
        public static TResult Map<TResult,TSource>(this TSource source)
        {
            if (source == null) return default(TResult);
            return Mapper.Map<TSource, TResult>(source);
        }

        public static List<TResult> Map<TResult, TSource>(this List<TSource> source)
        {
            if (source == null) return null;
            return Mapper.Map<List<TSource>, List<TResult>>(source);
        }
    }
}
