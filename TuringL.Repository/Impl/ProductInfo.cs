using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TuringL.Models;
using TuringL.Repository;

namespace TuringL.Repository
{
    public class ProductInfoUnitOfWorkRepository:Repository<ProductInfo,string>,IProductInfoRepository
    {
        public ProductInfoUnitOfWorkRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override ProductInfo GetByKey(string Id)
        {
            ProductInfo productInfo = (GetAll() as IQueryable<ProductInfo>).Where(it => it.Id == Id).FirstOrDefault();
            return productInfo;
        }
    }
}
