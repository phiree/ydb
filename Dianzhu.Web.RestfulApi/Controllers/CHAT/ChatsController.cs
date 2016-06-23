﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.CHAT
{
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
        [Route("api/orders/{orderID}/Chats")]
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
                return Json(ichat.GetChats(orderID, filter, chatfilter));
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
        [Route("api/orders/{orderID}/Chats/count")]
        public IHttpActionResult GetChatsCount(string orderID,  [FromUri]common_Trait_ChatFiltering chatfilter)
        {
            try
            {
                if (chatfilter == null)
                {
                    chatfilter = new common_Trait_ChatFiltering();
                }
                return Json(ichat.GetChatsCount(orderID, chatfilter));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
