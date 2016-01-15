using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TuringL.ViewModels;

namespace TuringL.DServices.Messages
{
    public class GetProductInfoListRequest : RequestBase
    { }

    public class GetProductInfoListResponse : ResponseBase
    {
        public List<ViewModels.ProductInfoView> ProductInfos { get; set; }
    }

    public class RegisterProductRequest : RequestBase
    {
        public ProductInfoView ProductInfo { get; set; }
        //public RegisterProductAddtionalInfoRequest RegisterProductAddtionalInfoRequest { get; set; } 

        public string UserName { get; set; }
    }

    public class RegisterProductResponse : ResponseBase
    {
        public ProductInfoView ProductInfo { get; set; }
    }

    public class RegisterProductAddtionalInfoRequest : RequestBase
    {
        public string Id { get; set; }
        public List<ProductAddtionalInfoView> productAddtionalInfoView { get; set; }
    }

    public class RegisterProductAddtionalInfoResponse : ResponseBase
    {
        public ProductInfoView ProductInfo { get; set; }
    }

    public class GetProductInfoByIdResponse : ResponseBase
    { public ProductInfoView ProductInfoView { get; set; } }

    public class GetProductInfoByIdRequest : RequestBase
    { public string Id { get; set; } }

    public class GetProductInfoByNameResponse : ResponseBase
    {
        public List<ProductInfoView> ProductInfos { get; set; }
    }

    public class GetProductInfoByNameRequest : RequestBase
    {
        public String Name { get; set; }
    }

    public class AddProductAddtionalInfoResponse : ResponseBase
    {
        public ProductAddtionalInfoView ProductAddtionInfoView { get; set; }
    }

    public class AddProductAddtionalInfoRequest : RequestBase
    {
        public ProductAddtionalInfoView ProductAddtionalInfoView { get; set; }
        public string ProductId { get; set; }
    }

    public class DelProductInfoResponse : ResponseBase
    { }

    public class DelProductInfoRequest : RequestBase
    { public string ProductId { get; set; } }

    public class DelMaintanceRecordResponse : ResponseBase
    { }

    public class DelMaintanceRecordRequest : RequestBase
    {
        public string MaintanceRecordId { get; set; }
    }

    public class DelProductAddtionInfoResponse : ResponseBase
    { }

    public class DelProductAddtionInfoRequest : RequestBase
    { }

    public class RegisterMaintanceRecoredResponse : ResponseBase
    {
        public MaintanceRecordView MaintanceRecordView { get; set; }
    }

    public class RegisterMaintanceRecoredRequest : RequestBase
    {
        public ViewModels.MaintanceRecordView MaintanceRecordView { get; set; }
        public string ProductId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterInstallInfoRequset : RequestBase
    { 
        public string CNumber { get; set; }
        public string InstallMethod { get; set; }

        public string MaintancePeriod { get; set; }

        public string Principal { get; set; }

        public string ProductId { get; set; }

        public string Site { get; set; }

        public DateTime StartTime { get; set; }
    }

    public class RegisterInstallInfoResponse : ResponseBase
    {
        public ViewModels.InstallInfoView InstallInfo { get; set; }
    }
}
