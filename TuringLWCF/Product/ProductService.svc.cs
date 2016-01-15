using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using TuringL.DServices;
using TuringL.DServices.Messages;
using TuringL.Infrasturcture.Json;
using TuringL.ViewModels;

namespace TuringLWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ProductService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ProductService.svc 或 ProductService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProductService : IProductService
    {
        private ProductInfoService _productInfoService = new ProductInfoService();
        public ProductService()
        {
            AutoMapperBootStrapper.ConfigurationMapper();
        }

        public string GetProductList()
        {
            GetProductInfoListResponse response = _productInfoService.GetProductInfos(new GetProductInfoListRequest());
            if (response.IsSucess == false || response.ProductInfos == null)
                return JsonHelper.SerializeObject("false:NoData");
            return JsonHelper.SerializeObject(response.ProductInfos);
        }

        public string RegisterProduct(String username, String product)
        {
            ProductInfoView registerProduct = JsonHelper.DeserializeObject<ProductInfoView>(product);
            if (registerProduct == null)
                return JsonHelper.SerializeObject("false: NoInput!");
            RegisterProductResponse response = _productInfoService.RegisterProduct(new RegisterProductRequest()
            {
                ProductInfo = registerProduct,
                UserName = username
            });
            if (response.IsSucess == false || response.ProductInfo == null)
                return JsonHelper.SerializeObject("false:" + response.Message);
            return JsonHelper.SerializeObject(response.ProductInfo);
        }

        public string GetProductById(string id)
        {
            GetProductInfoByIdResponse response = _productInfoService.GetProductById(new GetProductInfoByIdRequest() { Id = id });
            if (response.IsSucess == false || response.ProductInfoView == null)
                return JsonHelper.SerializeObject("false:" + response.Message);
            return JsonHelper.SerializeObject(response.ProductInfoView);
        }

        public string RegisterMaintanceRecord(string username,string id, string maintanceRecord)
        {
            MaintanceRecordView ff = JsonHelper.DeserializeObject<MaintanceRecordView>(maintanceRecord);
            RegisterMaintanceRecoredResponse response = _productInfoService
                .RegisterMaintanceRecored(new RegisterMaintanceRecoredRequest() 
                { 
                    ProductId = id, 
                    MaintanceRecordView = JsonHelper.DeserializeObject<MaintanceRecordView>(maintanceRecord),
                    UserName=username!=null?username:""
                });
            if (response.IsSucess != false && response.MaintanceRecordView != null)
                return JsonHelper.SerializeObject(response.MaintanceRecordView);
            return JsonHelper.SerializeObject("false:" + response.Message+" input: "+username+id+maintanceRecord);
        }

        public string GetProductType()
        {
            List<ProductTypeView> productTypes = new List<ProductTypeView>();
            productTypes.Add(new ProductTypeView() { Name = "TRAVC-180接收单元", Code = "TRAVC-180-RX" });
            productTypes.Add(new ProductTypeView() { Name = "TRAVC-180发射单元", Code = "TRAVC-180-TX" });
            return JsonHelper.SerializeObject(productTypes);
        }

        public string RegisterInstallInfo(string principal, string cNumber, string installMethod, string maintancePeriod, string productId, string site,string startTime)
        {
            RegisterInstallInfoResponse response = (RegisterInstallInfoResponse)_productInfoService
                .RegisterInstallInfo(new RegisterInstallInfoRequset() { 
                    CNumber=cNumber,
                    InstallMethod=installMethod,
                    MaintancePeriod=maintancePeriod,
                    Principal=principal,
                    ProductId=productId,
                    Site=site,
                    StartTime =Convert.ToDateTime(startTime)
                });
            if (response == null)
                return JsonHelper.SerializeObject("false:null return!");
            if (response.IsSucess == false)
                return JsonHelper.SerializeObject("false:" + response.Message);
            if (response.InstallInfo == null)
                return JsonHelper.SerializeObject("false:null installinfo return!");
            return JsonHelper.SerializeObject(response.InstallInfo);
        }

        public string DelProduct(string productId)
        {
            DelProductInfoResponse response = (DelProductInfoResponse)_productInfoService.DelProductInfo(
                new DelProductInfoRequest() { ProductId = productId });
            if (response == null)
                return JsonHelper.SerializeObject("false:null return!");
            if (response.IsSucess == false)
                return JsonHelper.SerializeObject("false:" + response.Message);
            return JsonHelper.SerializeObject("sucessfull");
        }

        public string DelMaintanceRecord(string maintanceRecordId)
        {
            DelMaintanceRecordResponse response = (DelMaintanceRecordResponse)_productInfoService.DelMaintanceRecord(
                new DelMaintanceRecordRequest() { MaintanceRecordId = maintanceRecordId });
            if (response == null)
                return JsonHelper.SerializeObject("false:null return!");
            if (response.IsSucess == false)
                return JsonHelper.SerializeObject("false:" + response.Message);
            return JsonHelper.SerializeObject("sucessfull");
        }

        public string GetProductByName(string name)
        {
            GetProductInfoByNameResponse response = (GetProductInfoByNameResponse)_productInfoService
                .GetProductByName(new GetProductInfoByNameRequest() { Name = name });
            if (response == null) return JsonHelper.SerializeObject("false:Null Return");
            if (response.IsSucess == false) return JsonHelper.SerializeObject("false:"+response.Message);
            if(response.ProductInfos==null||response.ProductInfos.Count<=0)
                return JsonHelper.SerializeObject("false:没有查询到结果");
            return JsonHelper.SerializeObject(response.ProductInfos);
        }
    }
}
