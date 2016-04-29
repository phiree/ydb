using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 

namespace Dianzhu.DAL
{
    public interface IDALArea  
    {


            IList<Model.Area> GetArea(int areaid);
          Model.Area GetAreaByAreaid(int areaid);

          Model.Area GetAreaByAreaname(string areaname);

        Model.Area GetAreaByAreanamelike(string areaname);

          Model.Area GetAreaBySeoName(string seoName);

        /// <summary>
        /// todo:需要将逻辑部分移至bll层
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
          IList<Model.Area> GetSubArea(string areacode);
        /// <summary>
        /// todo:需要移至bll层
        /// </summary>
        /// <param name="areacode"></param>
        /// <returns></returns>
          string GetSubAreaIds(string areacode);

          IList<Model.Area> GetAreaProvince();

          Model.Area GetAreaByCode(string code);
          Model.Area GetOne(int code);
    }
}
