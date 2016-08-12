using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.CHAT
{
    [HMACAuthentication]
    public class ChatsController : ApiController
    {
        private ApplicationService.Chat.IChatService ichat = null;
        public ChatsController()
        {
            ichat = Bootstrap.Container.Resolve<ApplicationService.Chat.IChatService>();
        }

        /// <summary>
        /// 条件读取聊天记录
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="filter"></param>
        /// <param name="chatfilter"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/Chats")]
        public IHttpActionResult GetChats(string orderID, [FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_ChatFiltering chatfilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (chatfilter == null)
                {
                    chatfilter = new common_Trait_ChatFiltering();
                }
                return Json(ichat.GetChats(orderID, filter, chatfilter, GetRequestHeader.GetTraitHeaders("get/orders/{orderID}/chats")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计聊天信息的数量
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="chatfilter"></param>
        /// <returns></returns>
        [Route("api/v1/orders/{orderID}/Chats/count")]
        public IHttpActionResult GetChatsCount(string orderID,  [FromUri]common_Trait_ChatFiltering chatfilter)
        {
            try
            {
                if (chatfilter == null)
                {
                    chatfilter = new common_Trait_ChatFiltering();
                }
                return Json(ichat.GetChatsCount(orderID, chatfilter, GetRequestHeader.GetTraitHeaders("get/orders/{orderID}/chats/count")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 条件读取所有聊天记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="chatfilter"></param>
        /// <returns></returns>
        [Route("api/v1/allChats")]
        public IHttpActionResult GetAllChats( [FromUri]common_Trait_Filtering filter, [FromUri]common_Trait_ChatFiltering chatfilter)
        {
            try
            {
                if (filter == null)
                {
                    filter = new common_Trait_Filtering();
                }
                if (chatfilter == null)
                {
                    chatfilter = new common_Trait_ChatFiltering();
                }
                return Json(ichat.GetAllChats(filter, chatfilter, GetRequestHeader.GetTraitHeaders("get/allChats")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 统计所有聊天信息的数量
        /// </summary>
        /// <param name="chatfilter"></param>
        /// <returns></returns>
        [Route("api/v1/allChats/count")]
        public IHttpActionResult GetAllChatsCount([FromUri]common_Trait_ChatFiltering chatfilter)
        {
            try
            {
                if (chatfilter == null)
                {
                    chatfilter = new common_Trait_ChatFiltering();
                }
                return Json(ichat.GetAllChatsCount( chatfilter, GetRequestHeader.GetTraitHeaders("get/allChats/count")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
