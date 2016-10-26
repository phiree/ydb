using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.Finance.DomainModel;
using NHibernate;
using Ydb.Finance.DomainModel.Enums;
using AutoMapper;
namespace Ydb.Finance.Application
{
   public class UserTypeSharePointService:IUserTypeSharePointService
    {
        IRepositoryUserTypeSharePoint repositoryUserTypeSharePoint;
        public UserTypeSharePointService()
        {
            Bootstrap.Boot();
            repositoryUserTypeSharePoint = Bootstrap.Container.Resolve<IRepositoryUserTypeSharePoint>();
        }

        /// <summary>
        /// 新增用户类型分配比例信息
        /// </summary>
        /// <param name="userType" type="string"></param>
        /// <param name="point" type="decimal"></param>
        [Ydb.Common.Repository.UnitOfWork]
        public void Add(string userType,decimal point) {
            UserType enumUserType;
            bool isUserType = Enum.TryParse<UserType>(userType, out enumUserType);
            if (!isUserType)
            {
                throw new ArgumentException("传入的UserType不是有效值");
            }
            string errMsg;
            decimal existed = GetSharePoint(userType, out errMsg);
            if (string.IsNullOrEmpty(errMsg))
            {
                throw new Exception("该用户类型已经设置了分成比");
            }
            repositoryUserTypeSharePoint.Add(enumUserType, point);
        }

        /// <summary>
        /// 根据用户类型获取用户类型分配比例信息
        /// </summary>
        /// <param name="userType" type="string">用户类型</param>
        /// <returns type="UserTypeSharePoint">用户类型分配比例信息</returns>
        public decimal GetSharePoint(string userType,out string errMsg) {

            errMsg = string.Empty;
            UserType enumUserType;
           bool isUserType= Enum.TryParse<UserType>(userType, out enumUserType);
            if (!isUserType)
            {
                throw new ArgumentException("传入的UserType不是有效值");
            }
            try
            {
                var point = repositoryUserTypeSharePoint.GetSharePoint(enumUserType);

                return point.Point;
            }
            catch (ArgumentNullException)
            {
                errMsg = "该用户类型尚未设置分成比:" + userType;
                throw;

            }
            catch (InvalidOperationException)
            {
                errMsg = "该用户类型设置了多个分成比:" + userType;
                throw;
            }

        }

        /// <summary>
        /// 获取所有用户类型分配比例信息
        /// </summary>
        /// <returns type="IList<UserTypeSharePointDto>">用户类型分配比例信息列表</returns>
        public IList<UserTypeSharePointDto> GetAll() {
            return Mapper.Map<IList<UserTypeSharePoint>, IList<UserTypeSharePointDto>>(repositoryUserTypeSharePoint.Find(x => true));
        }
         
    }
}
