using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TuringL.Infrasturcture;
using TuringL.Infrasturcture.AutoMap;

namespace InfrastructureTest
{
    /// <summary>
    /// AutoMapperTest 的摘要说明
    /// </summary>
    [TestClass]
    public class AutoMapperTest
    {
        public AutoMapperTest()
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

        [TestMethod]
        public void Test()
        {
            try
            {
                List<Product> products = new List<Product>();
                for (int i = 1; i < 3; i++)
                {
                    products.Add(new Product() { Id = i.ToString(), ProductType = (ProductType)i });
                }
                AutoMapper autoMapper = new AutoMapper();
                autoMapper.AddGenericMap(it => autoMapper.ListMap<Nature, NatureView>(it), typeof(Nature).Name + typeof(NatureView).Name);
                Person person = new Person() { Id = 1, Name = "person",ProductType=1};
                List<Nature> list = new List<Nature>();
                for (int i = 0; i < 2; i++) list.Add(new Nature() { Id = 2, Name = "nature" });
                person.Natures = list;
                PersonView pv = autoMapper.Map<Person, PersonView>(person);
            }
            catch (Exception ex)
            {
 
            }
        }
    }

    public enum ProductType
    {
        type1,
        type2,
        type3
    }

    public class Product
    {
        public string Id { get; set; }
        public ProductType ProductType { get; set; }
    }

    public class Person
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public List<Nature> Natures { get; set; }
        public int ProductType { get; set; }
    }

    public class PersonView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<NatureView> Natures { get; set; }
        public ProductType ProductType { get; set; }
    }

    public class Nature
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
    }

    public class NatureView
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
