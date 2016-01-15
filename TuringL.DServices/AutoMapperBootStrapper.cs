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
                .ForMember(d=>d.InstallInfos,opt=>opt.MapFrom(s=>s.InstallInfoes))
                .ForMember(d=>d.MaintanceRecords,opt=>opt.MapFrom(s=>s.MaintanceRecords))
                .ForMember(d => d.ProductAddtionalInfos, opt => opt.MapFrom(s => s.ProductAddtionalInfoes));
            Mapper.CreateMap<ProductInfoView, ProductInfo>()
                .ForMember(d=>d.InstallInfoes,opt=>opt.MapFrom(s=>s.InstallInfos))
                .ForMember(d=>d.MaintanceRecords,opt=>opt.MapFrom(s=>s.MaintanceRecords))
                .ForMember(d => d.ProductAddtionalInfoes, opt => opt.MapFrom(s => s.ProductAddtionalInfos));
            Mapper.CreateMap<ProductAddtionalInfo, ProductAddtionalInfoView>();
            Mapper.CreateMap<ProductAddtionalInfoView, ProductAddtionalInfo>();
            Mapper.CreateMap<MaintanceRecord, MaintanceRecordView>()
                .ForMember(d => d.MiantanceOverTime, opt => opt.MapFrom(s => s.MiantanceOverTime.ToString("yyyy/MM/dd")))
                .ForMember(d=>d.MaintanceStartTime,opt=>opt.MapFrom(s=>s.MaintanceStartTime.ToString("yyyy/MM/dd")));
            Mapper.CreateMap<MaintanceRecordView, MaintanceRecord>()
                .ForMember(d => d.MaintanceStartTime, opt => opt.MapFrom(s => s.MaintanceStartTime != null ? Convert.ToDateTime(s.MaintanceStartTime) : DateTime.Now));
            Mapper.CreateMap<InstallInfo, InstallInfoView>()
                .ForMember(d => d.CheckTime, opt => opt.MapFrom(s => s.CheckTime.ToString("yyyyy/MM/dd")))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime.ToString("yyyyy/MM/dd")))
                .ForMember(d => d.OverTime, opt => opt.MapFrom(s => s.OverTime.ToString("yyyyy/MM/dd")));
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
