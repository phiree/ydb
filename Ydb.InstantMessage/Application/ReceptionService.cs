using Castle.Facilities.NHibernateIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using NHibernate;
namespace Ydb.InstantMessage.Application
{
    /// <summary>
    /// 分配关系
    /// </summary>
    public class ReceptionService : IReceptionService
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.InstantMessage.Application.ReceptionService");

        DomainModel.Reception.IRepositoryReception receptionRepository;
        DomainModel.Reception.IReceptionSession receptionSession;
        
        IReceptionAssigner receptionAssigner;
       
        ISession session;
        public ReceptionService(IRepositoryReception receptionRepository,ISession session,IReceptionAssigner receptionAssigner)
        {
            this.receptionRepository = receptionRepository;
            this.session = session;
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

        public string AssignCustomerLogin(string customerId,out string errorMessage)
        {
            using(var t = session.BeginTransaction())
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


                t.Commit();
                return assignCS;
            }
        }

        public void AssignCSLogin(string csId)
        {
            using(var t = session.BeginTransaction())
            {
                IList<ReceptionStatus> existReceptions = receptionRepository.FindByDiandian(DianDianId);

                Dictionary<string,string> assignReception = receptionAssigner.AssignCSLogin(existReceptions, csId);

                foreach(var item in existReceptions)
                {
                    item.ChangeCS(assignReception[item.CustomerId]);
                    receptionRepository.Update(item);
                }

                t.Commit();
            }
        }

        public void AssignCSLogoff(string csId)
        {
            using(var t = session.BeginTransaction())
            {
                IList<ReceptionStatus> existReceptions = receptionRepository.FindByCustomerServiceId(csId);

                if (existReceptions.Count > 0)
                {
                    var assignReception = receptionAssigner.AssignCSLogoff(existReceptions);

                    foreach(var item in existReceptions)
                    {
                        item.ChangeCS(assignReception[item.CustomerId]);
                        receptionRepository.Update(item);
                    }
                }

                t.Commit();
            }
        }
    }
}
