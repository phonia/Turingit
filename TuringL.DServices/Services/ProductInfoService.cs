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
        private ResponseBase Service<T>(Func<T> action)where T:ResponseBase
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                ResponseBase response = (ResponseBase)Activator.CreateInstance(typeof(T));
                response.IsSucess = false;
                response.Message = ex.Message;
                return response;
            }
        }

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
                            MaintanceRecords = it.MaintanceRecords.ToList(),
                            InstallInfoes=it.InstallInfoes.ToList()
                        }).ToList();
                    if (ret == null) return new GetProductInfoByIdResponse() { IsSucess = false, Message = "No Products!" };
                    List<ProductInfo> list = ret.Select(it => new ProductInfo()
                    {
                        Id=it.Id,
                        Name = it.Name,
                        TypeVersion = it.TypeVersion,
                        ProductAddtionalInfoes = it.ProductAddtionalInfoes.ToList(),
                        Note = it.Note,
                        MaintanceRecords = it.MaintanceRecords,
                        RState = it.Rstate,
                        InstallInfoes=it.InstallInfoes
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

        public ResponseBase GetProductByName(GetProductInfoByNameRequest request)
        {
            return Service<GetProductInfoByNameResponse>(() => {
                if (request == null) throw new Exception("NUll Input!");
                using (IUnitOfWork unitOfWork = RepositoryFactory.GetUnitOfWork())
                {
                    IProductInfoRepository productInfoRepository = (IProductInfoRepository)RepositoryFactory
                        .Get(typeof(IProductInfoRepository), unitOfWork);
                    var ret= productInfoRepository.GetAll().Where(re => re.Name.Contains(request.Name))
                        .Select(re => new
                        {
                            Id = re.Id,
                            Name = re.Name,
                            TypeVersion = re.TypeVersion,
                            ProductAddtionalInfoes = re.ProductAddtionalInfoes.ToList(),
                            Note = re.Note,
                            Rstate = re.RState,
                            MaintanceRecords = re.MaintanceRecords.ToList(),
                            InstallInfoes = re.InstallInfoes.ToList()
                        }).ToList();
                    if (ret == null) throw new Exception("未查询到数据");
                    List<ProductInfo> list = ret.Select(re => new ProductInfo()
                    {
                        Id = re.Id,
                        Name = re.Name,
                        TypeVersion = re.TypeVersion,
                        ProductAddtionalInfoes = re.ProductAddtionalInfoes.ToList(),
                        Note = re.Note,
                        MaintanceRecords = re.MaintanceRecords,
                        RState = re.Rstate,
                        InstallInfoes = re.InstallInfoes
                    }).ToList();
                    unitOfWork.Commit();
                    return new GetProductInfoByNameResponse() { IsSucess = true, Message = "", ProductInfos = list.Map<ProductInfoView, ProductInfo>() };
                }
            });
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
                    registerMaintanceRecord.MiantanceUser = request.UserName;
                    registerMaintanceRecord.Register();
                    maintanceRepository.Add(registerMaintanceRecord);
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

        public ResponseBase RegisterInstallInfo(RequestBase request)
        {
            RegisterInstallInfoRequset registerRequest = (RegisterInstallInfoRequset)request;
            return Service<RegisterInstallInfoResponse>(() => {
                RegisterInstallInfoResponse response = new RegisterInstallInfoResponse();
                if (registerRequest==null) throw new Exception("null input!");
                using (IUnitOfWork unitOfwork = RepositoryFactory.GetUnitOfWork())
                {
                    IInstallInfoRepository installRepository = (IInstallInfoRepository)RepositoryFactory
                        .Get(typeof(IInstallInfoRepository), unitOfwork);
                    InstallInfo register = new InstallInfo() {
                        CNumber=registerRequest.CNumber,
                        InstallMethod=registerRequest.InstallMethod,
                        MaintancePeriod=registerRequest.MaintancePeriod,
                        Principal=registerRequest.Principal,
                        ProductId=registerRequest.ProductId,
                        Site=registerRequest.Site,
                        StartTime=registerRequest.StartTime
                    };
                    register.Register();
                    installRepository.Add(register);
                    unitOfwork.Commit();
                    response.IsSucess = true;
                    response.InstallInfo = register.Map<InstallInfoView, InstallInfo>();
                }
                return response;
            });
        }

        public DelProductInfoResponse DelProductInfo(DelProductInfoRequest request)
        {
            return (DelProductInfoResponse)Service<DelProductInfoResponse>(() => {
                if (request == null) throw new Exception("null input!");
                using (IUnitOfWork unitOfWork = RepositoryFactory.GetUnitOfWork())
                {
                    IProductInfoRepository productInfoRepository = (IProductInfoRepository)RepositoryFactory
                        .Get(typeof(IProductInfoRepository), unitOfWork);
                    ProductInfo delProductInfo = productInfoRepository.GetAll().Include("InstallInfoes").Include("MaintanceRecords").Include("ProductAddtionalInfoes")
                        .Where(it=>it.Id==request.ProductId).FirstOrDefault();
                    if (delProductInfo == null) throw new Exception("不存在或已删除的产品信息！");
                    //if (delProductInfo.InstallInfoes != null)
                    //{
                    //    IInstallInfoRepository installInfoRepository = (IInstallInfoRepository)RepositoryFactory
                    //        .Get(typeof(IInstallInfoRepository), unitOfWork);
                    //}
                    //if (delProductInfo.MaintanceRecords != null)
                    //{ }
                    //if (delProductInfo.ProductAddtionalInfoes != null)
                    //{ }
                    productInfoRepository.Del(delProductInfo);
                    unitOfWork.Commit();
                    return new DelProductInfoResponse() { IsSucess = true, Message = "" };
                }
            });
        }

        public ResponseBase DelMaintanceRecord(DelMaintanceRecordRequest request)
        {
            return (DelMaintanceRecordResponse)Service<DelMaintanceRecordResponse>(() =>
            {
                if (request == null) throw new Exception("null Input!");
                using (IUnitOfWork unitOfWork = RepositoryFactory.GetUnitOfWork())
                {
                    IMaintanceRecordRepository maintanceRecordRepository = (IMaintanceRecordRepository)RepositoryFactory
                        .Get(typeof(IMaintanceRecordRepository), unitOfWork);
                    MaintanceRecord delMaintanceRecord = maintanceRecordRepository.GetByKey(System.Guid.Parse(request.MaintanceRecordId));
                    if (delMaintanceRecord == null) throw new Exception("不存在或已删除的维护信息");
                    maintanceRecordRepository.Del(delMaintanceRecord);
                    unitOfWork.Commit();
                    return new DelMaintanceRecordResponse() { IsSucess = true, Message = "" };
                }
            });
        }

        public DelProductAddtionInfoResponse DelProductAddtionInfo(DelProductAddtionInfoRequest request)
        {
            DelProductAddtionInfoResponse response = new DelProductAddtionInfoResponse();
            return response;
        }
    }
}
