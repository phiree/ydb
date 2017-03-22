using Castle.Facilities.NHibernateIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Reception;
using NHibernate;
using Ydb.InstantMessage.DomainModel.Enums;
using Ydb.Common.Repository;
using Ydb.InstantMessage.Application.Dto;

using Ydb.InstantMessage.Infrastructure;
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
        IReceptionSession receptionSession;





        public ReceptionService(IInstantMessage im, IRepositoryReception receptionRepository, IReceptionAssigner receptionAssigner, IReceptionSession receptionSession)

        {
            this.im = im;
            this.receptionRepository = receptionRepository;

            this.receptionAssigner = receptionAssigner;
            this.receptionSession = receptionSession;
        }

        string DianDianId
        {
            get
            {
                return Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            }
        }

        [UnitOfWork]
        public void UpdateOrderId(Guid Id, string newOrderId)
        {
            ReceptionStatus reception = receptionRepository.FindById(Id);
            reception.OrderId = newOrderId;
            reception.LastUpdateTime = DateTime.Now;
            receptionRepository.Update(reception);
        }

        [UnitOfWork]
        public void UpdateOrderId(string customerId, string csId, string newOrderId)
        {
            ReceptionStatus reception = receptionRepository.FindOne(x => x.CustomerId == customerId && x.CustomerServiceId == csId);
            reception.OrderId = newOrderId;
            reception.LastUpdateTime = DateTime.Now;
            receptionRepository.Update(reception);
        }

        [UnitOfWork]
        public void DeleteReception(string customerId)
        {
            IList<ReceptionStatus> rsList = receptionRepository.FindByCustomerId(customerId);
            if (rsList.Count > 1)
            {
                log.Error("返回分配数量大于1，数量为：" + rsList.Count);
                for (int i = 0; i < rsList.Count; i++)
                {
                    log.Error(i + ":csId:" + rsList[i].CustomerServiceId);
                }
            }
            if (rsList.Count > 0)
            {
                for (int j = 0; j < rsList.Count; j++)
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

        [UnitOfWork]
        public ReceptionStatusDto AssignCustomerLogin(string customerId,string areaId, out string errorMessage,IList<MemberArea> onlineCsList)
        {
            ReceptionStatusDto rsDto = new ReceptionStatusDto();
            string assignCS = string.Empty;
            errorMessage = string.Empty;

            MemberArea customerToAssign = new MemberArea(customerId, areaId);

            IList<ReceptionStatus> existReceptions = receptionRepository.FindByCustomerId(customerId);

            try
            {
                assignCS = receptionAssigner.AssignCustomerLogin(existReceptions, customerToAssign, onlineCsList);
            }
            catch (Exception ee)
            {
                PHSuit.ExceptionLoger.ExceptionLog(log, ee);
                errorMessage = "分配失败";
                return rsDto;
            }

            if (existReceptions.Count > 0)
            {
                ReceptionStatus assignStatus;
                if (existReceptions.Where(x => x.CustomerServiceId == assignCS).ToList().Count > 0)
                {
                    assignStatus = existReceptions.Where(x => x.CustomerServiceId == assignCS).ToList()[0];
                }
                else
                {
                    assignStatus = new ReceptionStatus(customerId, assignCS,areaId, string.Empty);
                    receptionRepository.Add(assignStatus);
                }

                rsDto.Id = assignStatus.Id;
                rsDto.CustomerId = assignStatus.CustomerId;
                rsDto.CustomerServiceId = assignStatus.CustomerServiceId;
                rsDto.OrderId = assignStatus.OrderId;

                //删除冗余数据
                for (int i = 0; i < existReceptions.Count; i++)
                {
                    if (assignStatus.Id != existReceptions[i].Id)
                    {
                        log.Debug("删除分配关系:rsid:" + existReceptions[i].Id + ",cusomterId:" + existReceptions[i].CustomerId
                            + ",csId:" + existReceptions[i].CustomerServiceId + ",orderId:" + existReceptions[i].OrderId);
                        receptionRepository.Delete(existReceptions[i]);
                    }
                }
            }
            else
            {
                ReceptionStatus status = new ReceptionStatus(customerId, assignCS,areaId, string.Empty);
                receptionRepository.Add(status);

                rsDto.Id = status.Id;
                rsDto.CustomerId = status.CustomerId;
                rsDto.CustomerServiceId = status.CustomerServiceId;
                rsDto.OrderId = status.OrderId;
            }

            return rsDto;
        }

        public void SendCSLoginMessageToDD()
        {
            string ddId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            im.SendCSLoginMessage(Guid.NewGuid(), "客服上线", ddId, "YDBan_DianDian", string.Empty);
        }

        public void SendCSLogoffMessageToDD()
        {
            string ddId = Dianzhu.Config.Config.GetAppSetting("DiandianLoginId");
            im.SendCSLogoffMessage(Guid.NewGuid(), "客服下线", ddId, "YDBan_DianDian", string.Empty);
        }

        [UnitOfWork]
        public IList<ReceptionStatusDto> AssignCSLogin(string csId,string areaId ,int amount)
        {
            MemberArea cs = new MemberArea(csId, areaId);
            IList<ReceptionStatusDto> assignList = new List<ReceptionStatusDto>();

            IList<ReceptionStatus> existReceptions = receptionRepository.FindByDiandian(DianDianId, amount);

            Dictionary<string, string> assignReception = receptionAssigner.AssignCSLogin(existReceptions, cs);

            foreach (var item in existReceptions)
            {
                if (!assignReception.ContainsKey(item.CustomerId))
                { continue; }

                item.ChangeCS(assignReception[item.CustomerId]);
                receptionRepository.Update(item);

                assignList.Add(item.ToDto());

                im.SendReAssignToCustomer(assignReception[item.CustomerId], string.Empty, string.Empty,
                    Guid.NewGuid(), item.CustomerId, XmppResource.YDBan_User.ToString(), item.OrderId);
            }

            return assignList;
        }

        [UnitOfWork]
        public void AssignCSLogoff(string csId,IList<MemberArea> onlineCsList)
        {
            IList<ReceptionStatus> existReceptions = receptionRepository.FindByCustomerServiceId(csId);

            if (existReceptions.Count > 0)
            {
                var assignReception = receptionAssigner.AssignCSLogoff(existReceptions, onlineCsList);

                foreach (var item in existReceptions)
                {
                    item.ChangeCS(assignReception[item.CustomerId]);
                    receptionRepository.Update(item);

                    im.SendReAssignToCustomer(assignReception[item.CustomerId], string.Empty, string.Empty,
                        Guid.NewGuid(), item.CustomerId, XmppResource.YDBan_User.ToString(), item.OrderId);
                }
            }
        }

        public IList<string> GetOnlineUserList(string resouceName)
        {
            string errMsg = "";
            XmppResource resouce;
            bool isValid = Enum.TryParse<XmppResource>(resouceName, out resouce);
            if (!isValid)
            {
                 errMsg = "传入资源名有误";
                log.Error(errMsg);
                throw new Exception(errMsg);

            }
            var onlineSessions = receptionSession.GetOnlineSessionUser(resouce);
            if (onlineSessions == null)
            {
                return new List<string>();
            }
            return onlineSessions.Select(x => x.username).ToList();

        }
    }
}
