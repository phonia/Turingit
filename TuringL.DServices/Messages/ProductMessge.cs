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
        public RegisterProductAddtionalInfoRequest RegisterProductAddtionalInfoRequest { get; set; } 
    }

    public class RegisterProductResponse : ResponseBase
    { 
        public ProductInfoView ProductInfo { get; set; }
    }

    public class RegisterProductAddtionalInfoRequest : RequestBase
    {
        public string Id { get; set; }
        public List<ProductAddtionalInfoView> productAddtionalInofView { get; set; }
    }

    public class RegisterProductAddtionalInfoResponse : ResponseBase
    {
        public ProductInfoView ProductInfo { get; set; }
    }
}
