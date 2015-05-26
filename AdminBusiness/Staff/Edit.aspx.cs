using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.BLL;
using Dianzhu.Model;

public partial class Staff_Edit : BasePage
{
    private Guid StaffId = Guid.Empty;
    private bool IsNew { get { return StaffId == Guid.Empty; } }
    Staff s = new Staff();
    BLLStaff bllStaff = new BLLStaff();
    ServiceType ServiceType = new ServiceType();
    BLLServiceType bllServiceType = new BLLServiceType();
    public DZService CurrentService = new DZService();//当前的服务 对象.

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rbl_Parent.DataSource = bllServiceType.GetAll();

            rbl_Parent.DataBind();
        }
    }
    private void LoadInit()
    { }
    private void LoadForm()
    { }
    private void UpdateForm()
    {
        Business b = ((BusinessUser)CurrentUser).BelongTo;
        s.Belongto = b;
        s.Code = Code.Text;
        s.Name = Name.Text;
        s.NickName = NickName.Text;
        s.Gender = Gender.Text;
        s.Phone = Phone.Text;
        s.Photo = Photo.Text;
        //s.ServiceTypes = 
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        UpdateForm();
        bllStaff.SaveOrUpdate(s);
        PHSuit.Notification.Show(Page, "", "保存成功", Request.Url.AbsolutePath);
       

    }


}