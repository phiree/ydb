using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
//编辑 和 新增,合二为一.
public partial class Staff_Edit : BasePage
{
    private Guid StaffId = Guid.Empty;
 
    private bool IsNew { get { return StaffId == Guid.Empty; } }//新增. 或者 编辑
    Staff s = new Staff();
    BLLStaff bllStaff = new BLLStaff();
    ServiceType ServiceType = new ServiceType();
    BLLServiceType bllServiceType = new BLLServiceType();
    public DZService CurrentService = new DZService();//当前的服务 对象.
   
    protected void Page_Load(object sender, EventArgs e)
    {

        string paramId = Request.Params["id"]; //如果参数带有ID值, 则是编辑该职员.
        if (!string.IsNullOrEmpty(paramId))
        {
            StaffId = new Guid(paramId);
            s = bllStaff.GetOne(StaffId); //获取该职员信息
        }
        
        if (!IsPostBack)
        {
            LoadInit(); 
            if (!IsNew)
            {
                LoadForm();
            }
        }
    }
    /// <summary>
    /// 加载对象关联的初始值
    /// </summary>
    private void LoadInit()
    {
        
    }
    /// <summary>
    /// 加载对象初始值
    /// </summary>
    private void LoadForm()
    {
        Code.Text = s.Code;
        Name.Text = s.Name;
        NickName.Text = s.NickName;
        Phone.Text = s.Phone;
       
        hiGender.Value = s.Gender;
        
    }
    private void UpdateForm()
    {
        Business b = CurrentBusiness;
        s.Belongto = b;
        s.Code = Code.Text;
        s.Name = Name.Text;
        s.NickName = NickName.Text;
        s.Gender = hiGender.Value;
        s.Phone = Phone.Text;
        
         
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        UpdateForm();
        bllStaff.SaveOrUpdate(s);
        PHSuit.Notification.Show(Page, "", "保存成功", Request.Url.OriginalString);
       

    }


}