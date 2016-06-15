﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Dianzhu.DAL;
using Dianzhu.IDAL;
namespace Dianzhu.BLL
{
    public class BLLArea
    {


        //暴露 数据库实现,用于单元测试mock
        public IDALArea  repoArea;
        private IUnitOfWork iuw;
      
        public BLLArea(IDALArea repoArea) {
            this.repoArea = repoArea;
           // this.iuw = iuw;
        }
 
        /// <summary>
        /// 获取省份下的市,
        /// </summary>
        /// <param name="areaid">6位数中的前两位,如浙江的33</param>
        /// <returns>330000,330100,330200------331100</returns>
       
        public IList<Model.Area> GetArea(int areaid) 
        {
         // iuw.BeginTransaction();
            Expression<Func<Model.Area, bool>> where = i => i.Id == areaid;
        //    iuw.Commit();
            return repoArea.Find(where).ToList();
        }
        /// <summary>
        /// 获取直接下级地区.
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
        public IList<Model.Area> GetSubArea(string areacode)
        {
            string sql = "";
            //开始2位编号
            string bCode = areacode.Substring(0, 2);
            //中间2位编号
            string mCode = areacode.Substring(2, 2);
            //最后2位编号
            string lCode = areacode.Substring(4, 2);
 
            //城市
            Expression<Func<Model.Area, bool>> where = i=>true ;
            if (mCode == "00")
            {
                //查找市级区域单位
                where = i => i.Code.StartsWith(bCode) && i.Code.Substring(2, 2) != "00" && i.Code.EndsWith("00");


            }
            else if (lCode == "00")
            {
                //查找市内区、县级区域单位(并排除市和辖区)
                where = i => i.Code.StartsWith(bCode + mCode) && !i.Code.EndsWith("00");

            }
            else
            {
                Func<Model.Area, bool> where2 = i => i.Code == "" && i.Code == "dd";
            }
         //  iuw.BeginTransaction();
            var result= repoArea.Find(where).ToList();
       //   iuw.Commit();
            return result;
        }


        /// <summary>
        /// 根据areaname获得area
        /// </summary>
        /// <param name="areaname">area名称</param>
        /// <returns>area实体</returns>
        public Model.Area GetAreaByAreaname(string areaname)
        {
            if (string.IsNullOrEmpty(areaname))
            {
                return null;
            }
            Expression<Func<Model.Area, bool>> where = i => i.Name == areaname;
         //   iuw.BeginTransaction();
           
            var list= repoArea.FindOne(where);
        //     iuw.Commit();
            return list;
            
        }



        /// <summary>
        /// 获取所有的省份
        /// </summary>
        /// <returns></returns>
        public IList<Model.Area> GetAreaProvince()
        {
         //  iuw.BeginTransaction();
            Expression<Func<Model.Area, bool>> where = i => i.Code.EndsWith("0000");
            var result= repoArea.Find(where).ToList();
         //   iuw.Commit();
            return result;
        }

        public Model.Area GetOne(int areaId)
        {
           //  iuw.BeginTransaction();
            var area = repoArea.FindById(areaId);
          //  iuw.Commit();
            return area;
        }
    }
}
