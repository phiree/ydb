﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.ApplicationService.Store
{
    public interface IStoreService
    {
        /// <summary>
        /// 新建店铺
        /// </summary>
        /// <param name="storeobj"></param>
        /// <returns></returns>
        storeObj PostStore(storeObj storeobj);

        /// <summary>
        /// 条件读取店铺
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="storefilter"></param>
        /// <returns></returns>
        IList<storeObj> GetStores(common_Trait_Filtering filter, common_Trait_StoreFiltering storefilter);

        /// <summary>
        /// 统计店铺数量
        /// </summary>
        /// <param name="storefilter"></param>
        /// <returns></returns>
        countObj GetStoresCount(common_Trait_StoreFiltering storefilter);

        /// <summary>
        /// 读取店铺 根据ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        storeObj GetStore(string storeID);



        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        object DeleteStore(string storeID);

        /// <summary>
        /// 更新店铺信息
        /// </summary>
        /// <param name="storeID"></param>
        /// <param name="storeobj"></param>
        /// <returns></returns>
        storeObj PatchStore(string storeID, storeObj storeobj);
    }
}
