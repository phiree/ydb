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
        ISession session;
        IRepositoryUserTypeSharePoint repositoryUserTypeSharePoint;
        internal UserTypeSharePointService(ISession session, IRepositoryUserTypeSharePoint repositoryUserTypeSharePoint)
        {
            this.session = session;
            this.repositoryUserTypeSharePoint = repositoryUserTypeSharePoint;
        }
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
        public IList<UserTypeSharePointDto> GetAll() {
            return Mapper.Map<IList<UserTypeSharePoint>, IList<UserTypeSharePointDto>>(repositoryUserTypeSharePoint.Find(x => true));
        }
         
    }
}
