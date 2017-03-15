using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Ydb.BusinessResource.Application;
using Ydb.BusinessResource.DomainModel;
using Ydb.Common;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;
//编辑 和 新增,合二为一.
public partial class Staff_Edit : BasePage
{
    private Guid StaffId = Guid.Empty;
 
    private bool IsNew { get { return StaffId == Guid.Empty; } }//新增. 或者 编辑
    Staff s = new Staff();
    public string StaffAvatarUrl = "/images/components/inputFile/input_head_default_128_128.png";

    IStaffService staffService = Bootstrap.Container.Resolve<IStaffService>();
    ServiceType ServiceType = new ServiceType();
    IServiceTypeService typeService = Bootstrap.Container.Resolve<IServiceTypeService>();
    IDZMembershipService memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
    protected void Page_Load(object sender, EventArgs e)
    {

        string paramId = Request.Params["id"]; //如果参数带有ID值, 则是编辑该职员.
        if (!string.IsNullOrEmpty(paramId))
        {
            StaffId = new Guid(paramId);
            s = staffService.GetOne(StaffId); //获取该职员信息
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
        empheadimg.Attributes["data-preview"] = StaffAvatarUrl;
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
        string imagePath = staffService.Save(StaffId, empheadimg.PostedFile, enum_imagetype);
        

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        UpdateForm();

        //创建登录用户
        MemberDto dzms = memberService.GetUserByName(s.Phone);
        if (dzms == null)
        {
            System.Web.Security.MembershipCreateStatus mc = new System.Web.Security.MembershipCreateStatus();
            RegisterResult registerResult = memberService.RegisterStaff(s.Phone, "123456", "123456",
                System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority);
            dzms = registerResult.ResultObject;
        }
        else
        {
            if (dzms.UserType != enum_UserType.staff.ToString())
            {
                PHSuit.Notification.Alert(this, "该用户名已经存在其他类型的用户！");
                //throw new Exception("该用户名已经存在其他类型的用户！");
            }
        }
        s.LoginName = dzms.UserName;
        s.UserID = dzms.Id.ToString();
        if (IsNew)
        {
            staffService.Save(s);
        }
        else
        {
            staffService.Update(s);
        }
         
        StaffId = s.Id;
        UploadImage();
        Response.Redirect("/staff/default.aspx?businessid="+Request["businessId"]);
       

    }


}