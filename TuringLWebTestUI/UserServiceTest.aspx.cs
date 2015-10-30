using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Linq;
using TuringL.ViewModels;
using TuringL.Infrasturcture.Json;

namespace TuringLWebTestUI
{
    public partial class UserServiceTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserService.UserServiceClient userServiceClient = new UserService.UserServiceClient();
            string login = string.Empty;

            login = userServiceClient.Login("admin", "admin");
            Response.Write(login);
            if (login.Contains("false"))
            {
                return;
            }

            List<String> ls=new List<string>();
            ls.Add("h");
            ls.Add("y");

            List<JsonTest> list = new List<JsonTest>();
            list.Add(new JsonTest() { Name = "hy", Valuse = ls });
            list.Add(new JsonTest() { Name = "hy", Valuse = ls });
            list.Add(new JsonTest() { Name = "hy", Valuse = ls });

            //UserView loginUser = null;
            //try
            //{
            //    loginUser = JsonHelper.DeserializeObject<UserView>(login);
            //}
            //catch (Exception ex)
            //{ }

            //if (loginUser == null)
            //{
            //    Response.Write("jsonConvert error!");
            //    return;
            //}

            //string roles = userServiceClient.GetRoles();
            //Response.Write(JsonHelper.DeserializeObject<List<RoleView>>(roles).ToString());

            //string register = userServiceClient.Register(JsonConvert.SerializeObject(loginUser), JsonConvert.SerializeObject(
            //    new UserView() {
            //        Id="000",
            //        Duty="技术员",
            //        RoleId = "RegisterUser",
            //        Email="hy@turingit.com",
            //        Password="polan",
            //        Name="hy",
            //        RoleName="注册用户"
            //    }
            //    ));
            //Response.Write(JsonConvert.DeserializeObject(register));
        }
    }

    public class JsonTest
    {
        public String Name { get; set; }
        public List<String> Valuse { get; set; }
    }
}