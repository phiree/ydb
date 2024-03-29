﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.BLL.Client
{
    public interface IBLLRefreshToken
    {
        /// <summary>
        /// 添加新的RefreshToken
        /// </summary>
        /// <param name="token"></param>
        bool AddRefreshToken(Model.RefreshToken token);

        /// <summary>
        /// 根据Id删除RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        void RemoveRefreshToken(string refreshTokenId);

        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="refreshtoken"></param>
        void RemoveRefreshToken(Model.RefreshToken refreshtoken);

        /// <summary>
        /// 根据Id获取RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        Model.RefreshToken FindRefreshToken(string refreshTokenId);

        /// <summary>
        /// 获取所有的RefreshToken
        /// </summary>
        /// <returns></returns>
        IList<Model.RefreshToken> GetAllRefreshTokens();
    }
}
