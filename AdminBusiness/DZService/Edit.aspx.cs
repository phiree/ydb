using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dianzhu.Model;
using Dianzhu.BLL;
using PHSuit;
using FluentValidation.Results;
/// <summary>
/// 编辑/新增 服务信息.
/// </summary>
public partial class DZService_Edit : BasePage
{
    private Guid ServiceId = Guid.Empty;
    BLLDZService bllService = new BLLDZService();
    BLLServiceType bllServiceType = new BLLServiceType();
    BLLServiceProperty bllServiceProperty = new BLLServiceProperty();
    BLLServicePropertyValue bllServicePropertyValue = new BLLServicePropertyValue();
    public IList<ServiceProperty>  TypeProperties = new List<ServiceProperty>();
    private bool IsNew { get { return ServiceId == Guid.Empty; } }
    
   public DZService CurrentService = new DZService();//当前的服务 对象.
   ServiceType _servicetype;
   public ServiceType ServiceType {
       get
       {
           if (_servicetype == null)
           {
               Guid typeId = new Guid(hiTypeId.Value);
               _servicetype= bllServiceType.GetOne(typeId);
           }
           return _servicetype;
       }
       set { _servicetype = value; }
   }

 
    protected void Page_Load(object sender, EventArgs e)
    {
        string paramId = Request.Params["id"];
        if (!string.IsNullOrEmpty(paramId))
        {
            ServiceId = new Guid(paramId);
            CurrentService = bllService.GetOne(ServiceId);
            ServiceType = CurrentService.ServiceType;
        }
        else //新建服务一定要传入typeid
        {
            //string paramTypeId = hiTypeId.Value;// Request.Params["typeId"];
            //if (string.IsNullOrEmpty(paramTypeId))
            //{
            //    Notification.Show(Page, "抱歉", "页面参数有误,请从正常入口访问本页面", "/");
            //}
            //Guid typeId = new Guid(paramTypeId);
            //ServiceType = bllServiceType.GetOne(typeId);
            //TypeProperties = ServiceType.Properties;


        }
        if (!IsPostBack)
        {
            LoadInit();
            LoadForm();
        }
    }
    public void LoadInit()
    { 
        //加载服务属性
        //rptProperties.DataSource = ServiceType.Properties;
        //rptProperties.ItemDataBound += new RepeaterItemEventHandler(rptProperties_ItemDataBound);
        //rptProperties.DataBind();
    }

    void rptProperties_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ServiceProperty p=e.Item.DataItem as ServiceProperty;
            RadioButtonList rbl = e.Item.FindControl("rblValues") as RadioButtonList;
            rbl.DataSource = p.Values;
            rbl.DataTextField = "PropertyValue";
            rbl.DataValueField = "Id";
            rbl.DataBind();
            ServicePropertyValue selectedValue=CurrentService.PropertyValues.SingleOrDefault(x => x.ServiceProperty == p);
            if(selectedValue!=null)
            {
                rbl.SelectedValue = selectedValue.Id.ToString();
            }
        }
    }
    public void LoadForm()
    {
        tbxDescription.Text = CurrentService.Description;
        tbxName.Text = CurrentService.Name;
    }
    public void UpdateForm()
    {
        CurrentService.Name = tbxName.Text;
        CurrentService.Description = tbxName.Text;
        CurrentService.Business = CurrentBusiness;
        CurrentService.ServiceType = ServiceType;
        IList<ServicePropertyValue> values = new List<ServicePropertyValue>();
        foreach (RepeaterItem item in rptProperties.Items)
        {
            RadioButtonList rblPv = item.FindControl("rblValues") as RadioButtonList;
            foreach(ListItem rb in rblPv.Items)
            {
                if (rb.Selected)
                {
                    ServicePropertyValue v = bllServicePropertyValue.GetOne(new Guid(rb.Value));
                    values.Add(v);
                }
            }
        }
        CurrentService.PropertyValues = values;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateForm();
        ValidationResult result;
        bllService.SaveOrUpdate(CurrentService,out result);
        if (result.IsValid)
        { PHSuit.Notification.Show(Page, "", "保存成功", Request.RawUrl); }
        else
        {
            string err = string.Empty;
            foreach (ValidationFailure f in result.Errors)
            {
                err += f.ErrorMessage + "<br/>";
            }
            PHSuit.Notification.Show(Page, "保存失败", err, Request.RawUrl);
        }
       
    }
}