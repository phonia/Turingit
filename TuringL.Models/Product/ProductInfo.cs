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
            if (string.IsNullOrEmpty(this.Id)) AddBusinessRule(new BusinessRule() { Property = "Id", Rule = "产品Id不能为空" });
            if (string.IsNullOrEmpty(this.Name))AddBusinessRule(new BusinessRule() { Property = "Name", Rule = "产品名称不能为空" });
            if (string.IsNullOrEmpty(this.TypeVersion))AddBusinessRule(new BusinessRule() { Property = "TypeVersion", Rule = "产品类型不能为空" });
        }

        public void Register()
        {
            this.RState = (int)RStates.Added;
            IsValidated();
            if (ProductAddtionalInfoes != null)
            {
                foreach (var item in ProductAddtionalInfoes)
                {
                    item.ProductId = this.Id;
                    item.IsValidated();
                    item.Register();
                }
            }
        }
    }

    public interface IProductInfoRepository : IRepository<ProductInfo, string>
    { }

}
