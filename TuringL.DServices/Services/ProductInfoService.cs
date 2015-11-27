using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TuringL.Models;
using TuringL.ViewModels;
using TuringL.DServices.Messages;
using TuringL.Repository;
using TuringL.Infrasturcture.Log;

namespace TuringL.DServices
{
    public class ProductInfoService
    {
        public GetProductInfoListResponse GetProductInfos(GetProductInfoListRequest request)
        {
            try
            {
                using (IUnitOfWork unitOfWork = RepositoryFactory.GetUnitOfWork())
                {
                    if (request == null) return new GetProductInfoListResponse() { IsSucess = false, Message = "NoInput" };
                    IProductInfoRepository productInfoRepository = RepositoryFactory.Get(typeof(IProductInfoRepository), unitOfWork) as IProductInfoRepository;
                    var ret = productInfoRepository.GetAll()
                        .Select(it => new
                        {
                            Id = it.Id,
                            Name = it.Name,
                            TypeVersion = it.TypeVersion,
                            ProductAddtionalInfoes = it.ProductAddtionalInfoes.ToList(),
                            Note=it.Note,
                            Rstate=it.RState,
                            MaintanceRecords=it.MaintanceRecords.ToList() 
                        }).ToList();
                    if (ret == null) return new GetProductInfoListResponse() { IsSucess = false, Message = "No Products!" };
                    List<ProductInfo> list = ret.Select(it => new ProductInfo()
                    {
                        Id = it.Id,
                        Name = it.Name,
                        TypeVersion = it.TypeVersion,
                        ProductAddtionalInfoes = it.ProductAddtionalInfoes.ToList(),
                        Note=it.Note,
                        MaintanceRecords=it.MaintanceRecords,
                        RState=it.Rstate
                    }).ToList();

                    unitOfWork.Commit();
                    return new GetProductInfoListResponse() { IsSucess = true, ProductInfos = list.Map<ProductInfoView, ProductInfo>() };
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                return new GetProductInfoListResponse() { IsSucess = false, Message = ex.Message };
            }
        }

        public GetProductInfoByIdResponse GetProductById(GetProductInfoByIdRequest request)
        {            
            try
            {
                using (var unitOfWork = RepositoryFactory.GetUnitOfWork())
                {
                    if (request == null) return new GetProductInfoByIdResponse() { IsSucess = false, Message = "NoInput" };
                    IProductInfoRepository productInfoRepository = RepositoryFactory.Get(typeof(IProductInfoRepository), unitOfWork) as IProductInfoRepository;
                    var ret = productInfoRepository.GetAll()
                        .Where(it => it.Id.Equals(request.Id))
                        .Select(it => new
                        {
                            Id = it.Id,
                            Name = it.Name,
                            TypeVersion = it.TypeVersion,
                            ProductAddtionalInfoes = it.ProductAddtionalInfoes.ToList(),
                            Note = it.Note,
                            Rstate = it.RState,
                            MaintanceRecords = it.MaintanceRecords.ToList()
                        }).ToList();
                    if (ret == null) return new GetProductInfoByIdResponse() { IsSucess = false, Message = "No Products!" };
                    List<ProductInfo> list = ret.Select(it => new ProductInfo()
                    {
                        Id = it.Id,
                        Name = it.Name,
                        TypeVersion = it.TypeVersion,
                        ProductAddtionalInfoes = it.ProductAddtionalInfoes.ToList(),
                        Note = it.Note,
                        MaintanceRecords = it.MaintanceRecords,
                        RState = it.Rstate
                    }).ToList();
                    unitOfWork.Commit();
                    return new GetProductInfoByIdResponse() { IsSucess = true, ProductInfoView = list.Map<ProductInfoView, ProductInfo>().FirstOrDefault() };
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                return new GetProductInfoByIdResponse() { IsSucess = false, Message = ex.Message };
            }
        }

        public RegisterProductResponse RegisterProduct(RegisterProductRequest request)
        {
            RegisterProductResponse response = new RegisterProductResponse();
            try
            {
                using (IUnitOfWork unitOfWork = RepositoryFactory.GetUnitOfWork())
                {
                    if (request != null && request.ProductInfo != null)
                    {
                        ProductInfo registerProductInfo = request.ProductInfo.Map<ProductInfo, ProductInfoView>();
                        registerProductInfo.Register();
                        IProductInfoRepository productInfoRepository = RepositoryFactory.Get(typeof(IProductInfoRepository), unitOfWork) as IProductInfoRepository;
                        if (productInfoRepository.GetAll().Where(it => it.Id.Equals(registerProductInfo.Id)).FirstOrDefault() == null)
                        {
                            productInfoRepository.Add(registerProductInfo);
                            unitOfWork.Commit();
                            response.IsSucess = true;
                            response.ProductInfo = registerProductInfo.Map<ProductInfoView, ProductInfo>();
                        }
                        else
                        {
                            response.IsSucess = false;
                            response.Message = "已存在的产品Id！";
                        }
                    }
                    else
                    {
                        response.IsSucess = false;
                        response.Message = "No Input!";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                response.IsSucess = false;
                response.Message = ex.Message;
            }            
            return response;
        }

        public AddProductAddtionalInfoResponse AddProductAddtional(AddProductAddtionalInfoRequest request)
        {
            AddProductAddtionalInfoResponse response = new AddProductAddtionalInfoResponse();
            try
            {
                using (var unitOfWork = RepositoryFactory.GetUnitOfWork())
                {
                    if (request != null && request.ProductAddtionalInfoView != null)
                    {
                        IProductAddtionalInfoRepository productAddtionalInfoRepository = (IProductAddtionalInfoRepository)RepositoryFactory
                            .Get(typeof(IProductAddtionalInfoRepository), unitOfWork);
                        ProductAddtionalInfo productAddtionalInfo = request.ProductAddtionalInfoView.Map<ProductAddtionalInfo, ProductAddtionalInfoView>();
                        productAddtionalInfo.ProductId = request.ProductId;
                        productAddtionalInfo.Register();
                        productAddtionalInfoRepository.Add(productAddtionalInfo);
                        unitOfWork.Commit();
                        response.IsSucess = true;
                        response.ProductAddtionInfoView = productAddtionalInfo.Map<ProductAddtionalInfoView, ProductAddtionalInfo>();
                    }
                    else
                    {
                        response.IsSucess = false;
                        response.Message = "No Input!";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public RegisterMaintanceRecoredResponse RegisterMaintanceRecored(RegisterMaintanceRecoredRequest request)
        {
            RegisterMaintanceRecoredResponse response = new RegisterMaintanceRecoredResponse();
            try
            {
                using (var unitOfWork = RepositoryFactory.GetUnitOfWork())
                {
                    IMaintanceRecordRepository maintanceRepository = (IMaintanceRecordRepository)RepositoryFactory.Get(typeof(IMaintanceRecordRepository), unitOfWork);
                    MaintanceRecord registerMaintanceRecord = request.MaintanceRecordView.Map<MaintanceRecord, MaintanceRecordView>();
                    registerMaintanceRecord.ProductId = request.ProductId;
                    registerMaintanceRecord.Register();
                    unitOfWork.Commit();
                    response.IsSucess = true;
                    response.MaintanceRecordView = registerMaintanceRecord.Map<MaintanceRecordView, MaintanceRecord>();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                response.IsSucess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public DelProductInfoResponse DelProductInfo(DelProductInfoRequest request)
        {
            DelProductInfoResponse response = new DelProductInfoResponse();

            return response;
        }

        public DelProductAddtionInfoResponse DelProductAddtionInfo(DelProductAddtionInfoRequest request)
        {
            DelProductAddtionInfoResponse response = new DelProductAddtionInfoResponse();
            return response;
        }
    }
}
