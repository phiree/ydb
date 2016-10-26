using Castle.Facilities.NHibernateIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using NHibernate;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.InstantMessage.DomainModel.Chat;

namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 分配关系
    /// </summary>
    public class ReceptionService : IReceptionService
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.InstantMessage.Application.ReceptionService");

        DomainModel.Reception.IRepositoryReception receptionRepository;
        IInstantMessage im;
        IReceptionAssigner receptionAssigner;

 
       
       
 
        public ReceptionService(IInstantMessage im, IRepositoryReception receptionRepository,  IReceptionAssigner receptionAssigner)
 
        {
            this.im = im;
            this.receptionRepository = receptionRepository;
           
            this.receptionAssigner = receptionAssigner;
        }

        string DianDianId
        {
            get
            {
                return Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            }
        }

        public void DeleteReception(string customerId)
        {
            throw new NotImplementedException();
        }

        [Ydb.Common.Repository.UnitOfWork]
        public string AssignCustomerLogin(string customerId,out string errorMessage)
        {
            
                string assignCS = string.Empty;
                errorMessage = string.Empty;

                IList<ReceptionStatus> existReceptions = receptionRepository.FindByCustomerId(customerId);


                try
                {
                    assignCS = receptionAssigner.AssignCustomerLogin(existReceptions, customerId);
                }
                catch (Exception ee)
                {
                    PHSuit.ExceptionLoger.ExceptionLog(log, ee);
                    errorMessage = "分配失败";
                    return assignCS;
                }

                for (int i = 0; i < existReceptions.Count; i++)
                {
                    receptionRepository.Delete(existReceptions[i]);
                }

                ReceptionStatus status = new ReceptionStatus(customerId, assignCS, string.Empty);
                receptionRepository.Add(status);

             
                return assignCS;
            
        }

        [Ydb.Common.Repository.UnitOfWork]
        public IList<string> AssignCSLogin(string csId, int amount)
        {
 
                im.SendCSLoginMessage();

 
                IList<string> assignList = new List<string>();

                IList<ReceptionStatus> existReceptions = receptionRepository.FindByDiandian(DianDianId, amount);

                Dictionary<string, string> assignReception = receptionAssigner.AssignCSLogin(existReceptions, csId);

                foreach (var item in existReceptions)
                {
                    item.ChangeCS(assignReception[item.CustomerId]);
                    receptionRepository.Update(item);

                    assignList.Add(item.CustomerId);

                    im.SendReAssignToCustomer(assignReception[item.CustomerId], string.Empty, string.Empty,
                        Guid.NewGuid(), item.CustomerId, XmppResource.YDBan_User.ToString(), item.OrderId);
                }

               
                return assignList;
           
        }
        [Ydb.Common.Repository.UnitOfWork]
        public void AssignCSLogoff(string csId)
        {
 
                im.SendCSLogoffMessage();
 
                IList<ReceptionStatus> existReceptions = receptionRepository.FindByCustomerServiceId(csId);

                if (existReceptions.Count > 0)
                {
                    var assignReception = receptionAssigner.AssignCSLogoff(existReceptions);

                    foreach (var item in existReceptions)
                    {
                        item.ChangeCS(assignReception[item.CustomerId]);
                        receptionRepository.Update(item);

                        im.SendReAssignToCustomer(assignReception[item.CustomerId], string.Empty, string.Empty,
                            Guid.NewGuid(), item.CustomerId, XmppResource.YDBan_User.ToString(), item.OrderId);
                    }
                }
 
        } 
    }
}
