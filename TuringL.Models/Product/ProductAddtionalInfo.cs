using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public partial class ProductAddtionalInfo:EntityBase,IAggregateRoot
    {

        protected override void Validate()
        {

        }

        public void Register()
        {
            this.RState = (int)RStates.Added;
        }
    }

    public interface IProductAddtionalInfoRepository : IRepository<ProductAddtionalInfo, System.Guid>
    { }
}
