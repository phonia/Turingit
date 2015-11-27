using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TuringLWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IProductService”。
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetProductList();
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetProductById(string id);
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        String RegisterProduct(String username, String product);
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string RegisterMaintanceRecord(String username,string id, String maintanceRecord);
    }
}
