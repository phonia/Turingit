using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TuringL.Models;

namespace TuringL.Repository
{
    public class ProductAddtionalInfoUnitOfWorkRepository:Repository<ProductAddtionalInfo,System.Guid>,IProductAddtionalInfoRepository
    {
        public ProductAddtionalInfoUnitOfWorkRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override ProductAddtionalInfo GetByKey(Guid Id)
        {
            ProductAddtionalInfo productAddtinoalInfo= (_dataContext.ProductAddtionalInfoes.Where(it => it.Id.Equals(Id))).FirstOrDefault();
            return productAddtinoalInfo;
        }
    }
}
