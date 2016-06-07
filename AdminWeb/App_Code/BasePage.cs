using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage:System.Web.UI.Page
{
	public BasePage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    unitofwork uf;
    public unitofwork UnitOfWork {
        get {
            return uf=uf ??new unitofwork();
        }
    }
}