using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TuringL.Models;
using TuringL.ViewModels;
using TuringL.DServices.Messages;
using TuringL.Repository;
using AutoMapper;
using TuringL.Infrasturcture.Log;

namespace TuringL.DServices
{
    public class ProductInfoService
    {
        public GetProductInfoListResponse GetProductInfo(GetProductInfoListRequest request)
        {
            GetProductInfoListResponse response = new GetProductInfoListResponse();
            using (IUnitOfWork unitOfWork = RepositoryFactory.GetUnitOfWork())
            {
                try
                {
                    if (request == null) throw new Exception("null input!");
                    IProductInfoRepository productInfoRepository = RepositoryFactory.Get(typeof(IProductInfoRepository), unitOfWork) as IProductInfoRepository;

                    unitOfWork.Commit();
                    response.IsSucess = true;
                }
                catch (Exception ex)
                {
                    response.IsSucess = false;
                    response.Message = ex.Message;
                }
            }
            return response;
        }

        public RegisterProductResponse RegisterProduct(RegisterProductRequest request)
        {
            RegisterProductResponse response = new RegisterProductResponse();
            using (IUnitOfWork unitOfWork = RepositoryFactory.GetUnitOfWork())
            {
                try
                {
                    if (request == null || request.ProductInfo == null) throw new Exception("null Input!");
                    ProductInfo registerProductInfo = Mapper.Map<ProductInfoView, ProductInfo>(request.ProductInfo);
                    if (request.RegisterProductAddtionalInfoRequest != null && request.RegisterProductAddtionalInfoRequest.productAddtionalInofView != null)
                    {
                        foreach (var item in request.RegisterProductAddtionalInfoRequest.productAddtionalInofView)
                        {
                            registerProductInfo.ProductAddtionalInfoes.Add(Mapper.Map<ProductAddtionalInfoView, ProductAddtionalInfo>(item));
                        }
                    }
                    registerProductInfo.Register();
                    IProductInfoRepository productInfoRepository = RepositoryFactory.Get(typeof(IProductInfoRepository), unitOfWork) as IProductInfoRepository;
                    if (productInfoRepository.GetAll().Where(it => it.Id.Equals(registerProductInfo.Id)).FirstOrDefault() != null)
                    {
                        response.IsSucess = false;
                        response.Message = "已存在的产品Id！";
                    }
                    else
                    {
                        productInfoRepository.Add(registerProductInfo);
                        unitOfWork.Commit();
                        response.IsSucess = true;
                    }

                }
                catch (Exception ex)
                {
                    response.IsSucess = false;
                    response.Message = ex.Message;
                }
            }

            return response;
        }
    }
}
