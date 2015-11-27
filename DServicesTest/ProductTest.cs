using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TuringL.DServices;
using TuringL.DServices.Messages;
using TuringL.ViewModels;

namespace DServicesTest
{
    /// <summary>
    /// ProductTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ProductTest
    {
        public ProductTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        private ProductInfoService _service = null;

        [TestInitialize]
        public void Init()
        {
            AutoMapperBootStrapper.ConfigurationMapper();
            _service = new ProductInfoService();
        }

        [TestMethod]
        public void RegiseterProductInfo()
        {
            try
            {
                RegisterProductAddtionalInfoRequest par = new RegisterProductAddtionalInfoRequest();
                List<ProductAddtionalInfoView> list = new List<ProductAddtionalInfoView>();
                list.Add(new ProductAddtionalInfoView()
                {
                    Name = "测试附件",
                    Path = "~~~",
                    Rtype = 1,
                });
                par.productAddtionalInfoView = list;
                RegisterProductRequest requset = new RegisterProductRequest()
                {
                    ProductInfo = new ProductInfoView()
                    {
                        Id = "614141999996",
                        Name = "测试项",
                        TypeVersion = "测试项",
                        ProductAddtionalInfos=list
                    }
                    //RegisterProductAddtionalInfoRequest = par
                };

                RegisterProductResponse response = _service.RegisterProduct(requset);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void GetProductInfoList()
        {
            try
            {
                List<ProductInfoView> list = _service.GetProductInfos(new GetProductInfoListRequest()).ProductInfos;
            }
            catch (Exception ex)
            { }
        }
    }
}
