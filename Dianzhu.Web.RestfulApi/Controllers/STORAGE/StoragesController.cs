using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dianzhu.ApplicationService;

namespace Dianzhu.Web.RestfulApi.Controllers.STORAGE
{
    [HMACAuthentication]
    public class StoragesController : ApiController
    {
        private ApplicationService.Storage.IStorageService istorage = null;
        public StoragesController()
        {
            istorage = Bootstrap.Container.Resolve<ApplicationService.Storage.IStorageService>();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileBase4"></param>
        /// <returns></returns>
        [Route("api/v1/Storages/Images/")]
        public IHttpActionResult PostImages(FileBase64 fileBase4)
        {
            try
            {
                if (fileBase4 == null)
                {
                    fileBase4 = new FileBase64();
                }
                return Json(istorage.PostImages(fileBase4, GetRequestHeader.GetTraitHeaders("post/storages/images")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 上传头像图片
        /// </summary>
        /// <param name="fileBase4"></param>
        /// <returns></returns>
        [Route("api/v1/Storages/AvatarImages/")]
        public IHttpActionResult PostAvatarImages(FileBase64 fileBase4)
        {
            try
            {
                if (fileBase4 == null)
                {
                    fileBase4 = new FileBase64();
                }
                return Json(istorage.PostImages(fileBase4, GetRequestHeader.GetTraitHeaders("post/storages/avatarImages")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }

        /// <summary>
        /// 上传语音
        /// </summary>
        /// <param name="fileBase4"></param>
        /// <returns></returns>
        [Route("api/v1/Storages/Audios/")]
        public IHttpActionResult PostAudios(FileBase64 fileBase4)
        {
            try
            {
                if (fileBase4 == null)
                {
                    fileBase4 = new FileBase64();
                }
                return Json(istorage.PostAudios(fileBase4, GetRequestHeader.GetTraitHeaders("post/storages/audios")));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, utils.SetRes_Error(ex));
            }
        }
    }
}
