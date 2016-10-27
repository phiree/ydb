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
using Ydb.InstantMessage.Application.Dto;

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

        [Ydb.Common.Repository.UnitOfWork]
        public void UpdateOrderId(Guid Id, string newOrderId)
        {
            ReceptionStatus reception = receptionRepository.FindById(Id);
            reception.OrderId = newOrderId;
            reception.LastUpdateTime = DateTime.Now;
            receptionRepository.Update(reception);
        }

        [Ydb.Common.Repository.UnitOfWork]
        public void UpdateOrderId(string customerId, string csId, string newOrderId)
        {
            ReceptionStatus reception = receptionRepository.FindOne(x=>x.CustomerId==customerId&&x.CustomerServiceId==csId);
            reception.OrderId = newOrderId;
            reception.LastUpdateTime = DateTime.Now;
            receptionRepository.Update(reception);
        }

        [Ydb.Common.Repository.UnitOfWork]
        public void DeleteReception(string customerId)
        {
            IList< ReceptionStatus> rsList = receptionRepository.FindByCustomerId(customerId);
            if (rsList.Count > 1)
            {
                log.Error("返回分配数量大于1，数量为：" + rsList.Count);
                for(int i=0;i<rsList.Count;i++)
                {
                    log.Error(i + ":csId:" + rsList[i].CustomerServiceId);
                }
            }
            if (rsList.Count>0)
            {
                for(int j = 0; j < rsList.Count; j++)
                {
                    log.Debug("删除ReceptionStatus中的关系，rs.Id:" + rsList[j].Id 
                                                        + ",csId:" + rsList[j].CustomerServiceId 
                                                        + ",customerId:" + rsList[j].CustomerId 
                                                        + ",orderId" + rsList[j].OrderId);
                    receptionRepository.Delete(rsList[j]);

                    im.SendCSLogoffMessage(Guid.NewGuid(), "客服下线", rsList[j].CustomerId, "YDBan_User", rsList[j].OrderId);
                }
            }
        }

        [Ydb.Common.Repository.UnitOfWork]
        public ReceptionStatusDto AssignCustomerLogin(string customerId, out string errorMessage)
        {
            ReceptionStatusDto rsDto = new ReceptionStatusDto();
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
                return rsDto;
            }

            for (int i = 0; i < existReceptions.Count; i++)
            {
                receptionRepository.Delete(existReceptions[i]);
            }

            ReceptionStatus status = new ReceptionStatus(customerId, assignCS, string.Empty);
            receptionRepository.Add(status);

            rsDto.Id = status.Id;
            rsDto.CustomerId = customerId;
            rsDto.CustomerServiceId = assignCS;
            rsDto.OrderId = status.OrderId;

            return rsDto;
        }

        [Ydb.Common.Repository.UnitOfWork]
        public IList<ReceptionStatusDto> AssignCSLogin(string csId, int amount)
        {
            string ddId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            im.SendCSLoginMessage(Guid.NewGuid(), "客服上线", ddId, "YDBan_DianDian", string.Empty);

            IList<ReceptionStatusDto> assignList = new List<ReceptionStatusDto>();

            IList<ReceptionStatus> existReceptions = receptionRepository.FindByDiandian(DianDianId, amount);

            Dictionary<string, string> assignReception = receptionAssigner.AssignCSLogin(existReceptions, csId);

            foreach (var item in existReceptions)
            {
                item.ChangeCS(assignReception[item.CustomerId]);
                receptionRepository.Update(item);

                assignList.Add(item.ToDto());

                im.SendReAssignToCustomer(assignReception[item.CustomerId], string.Empty, string.Empty,
                    Guid.NewGuid(), item.CustomerId, XmppResource.YDBan_User.ToString(), item.OrderId);
            }

            return assignList;
        }

        [Ydb.Common.Repository.UnitOfWork]
        public void AssignCSLogoff(string csId)
        {
            string ddId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            im.SendCSLogoffMessage(Guid.NewGuid(), "客服下线", ddId, "YDBan_DianDian", string.Empty);

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
