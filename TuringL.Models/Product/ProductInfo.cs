using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public partial class ProductInfo:EntityBase,IAggregateRoot
    {

        protected override void Validate()
        {
            if (this.Id == null || this.Id == String.Empty || this.Id.Length <= 0) 
                this.AddBusinessRule(new BusinessRule() { Property = "Id", Rule = "产品Id不能为空" });
            if (this.Name == null || this.Name == String.Empty || this.Name.Length <= 0)
                this.AddBusinessRule(new BusinessRule() { Property = "Name", Rule = "产品名称不能为空" });
            if (this.TypeVersion == null || this.TypeVersion == string.Empty || this.TypeVersion.Length <= 0)//产品类型可以为空
                this.AddBusinessRule(new BusinessRule() { Property = "TypeVersion", Rule = "产品类型不能为空" });
        }

        public void Register()
        {
            this.RState = (int)RStates.Added;
            Validate();
            foreach (var item in ProductAddtionalInfoes)
            {
                item.ProductId = this.Id;
                item.IsValidated();
                item.Register();
            }
        }
    }

    public interface IProductInfoRepository : IRepository<ProductInfo, string>
    { }

}
