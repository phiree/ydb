using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
//编辑 和 新增,合二为一.
public partial class Staff_Edit : BasePage
{
    private Guid StaffId = Guid.Empty;
 
    private bool IsNew { get { return StaffId == Guid.Empty; } }//新增. 或者 编辑
    Staff s = new Staff();
    public string StaffAvatarUrl = "/images/components/inputFile/input_head_default_128_128.png";
    BLLStaff bllStaff = new BLLStaff();
    ServiceType ServiceType = new ServiceType();
    BLLServiceType bllServiceType = new BLLServiceType();
    
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
//        NickName.Text = s.NickName;
        Phone.Text = s.Phone;
        var avatarList = s.StaffAvatar.Where(x => x.IsCurrent == true).ToList();
        if(avatarList.Count>0)
        {
            StaffAvatarUrl = "/ImageHandler.ashx?imagename=" + HttpUtility.UrlEncode(avatarList[0].ImageName) + "&width=90&height=90&tt=3";
      
        }
       hiGender.Value = s.Gender == "男" ? "0" : "1";
        
    }
    private void UpdateForm()
    { 
        Business b = CurrentBusiness;
        s.Belongto = b;
        s.Code = Code.Text;
        s.Name = Name.Text;
//        s.NickName = NickName.Text;
        s.Gender = hiGender.Value == "0" ? "男" : "女";
        s.Phone = Phone.Text;            
         
    }
    private void UploadImage()
    {
        enum_ImageType enum_imagetype = enum_ImageType.Staff_Avatar;
        if (empheadimg.PostedFile.ContentLength > 2 * 1024 * 1024)
        {
            PHSuit.Notification.Alert(this, "上传的图片超过2M，请重试!");
        }
        string imagePath = bllStaff.Save(StaffId, empheadimg.PostedFile, enum_imagetype);
        

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        UpdateForm();
        bllStaff.SaveOrUpdate(s);
        StaffId = s.Id;
        UploadImage();
        Response.Redirect("/staff/default.aspx?businessid="+Request["businessId"]);
       

    }


}