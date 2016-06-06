using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.DAL;

namespace Dianzhu.BLL.Client
{
    public class BLLRefreshToken:IBLLRefreshToken
    {
        DALRefreshToken dalrefreshtoken = new DALRefreshToken();
        /// <summary>
        /// 添加新的RefreshToken
        /// </summary>
        /// <param name="token"></param>
        public void AddRefreshToken(Model.RefreshToken token)
        {
            dalrefreshtoken.AddRefreshToken(token);
        }

        /// <summary>
        /// 根据Id删除RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        public void RemoveRefreshToken(string refreshTokenId)
        {
            dalrefreshtoken.RemoveRefreshToken(refreshTokenId);
        }

        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="refreshtoken"></param>
        public void RemoveRefreshToken(Model.RefreshToken refreshtoken)
        {
            dalrefreshtoken.RemoveRefreshToken(refreshtoken);
        }

        /// <summary>
        /// 根据Id获取RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public Model.RefreshToken FindRefreshToken(string refreshTokenId)
        {
            return dalrefreshtoken.FindRefreshToken(refreshTokenId);
        }

        /// <summary>
        /// 获取所有的RefreshToken
        /// </summary>
        /// <returns></returns>
        public IList<Model.RefreshToken> GetAllRefreshTokens()
        {
            return dalrefreshtoken.GetAllRefreshTokens();
        }
    }
}
