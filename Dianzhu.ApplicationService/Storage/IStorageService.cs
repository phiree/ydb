using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Storage
{
    public  interface IStorageService
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileBase4"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        imageObj PostImages(FileBase64 fileBase4, Customer customer);

        /// <summary>
        /// /上传语音
        /// </summary>
        /// <param name="fileBase4"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        audioObj PostAudios(FileBase64 fileBase4, Customer customer);


    }
}
