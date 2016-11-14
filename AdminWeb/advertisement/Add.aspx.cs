using Dianzhu.BLL;
using Dianzhu.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MediaServer;
public partial class advertisement_Add : BasePage
{
    BLLAdvertisement bllAd = Bootstrap.Container.Resolve<BLLAdvertisement>();

    Guid id = Guid.Empty;
    string imgUrlOld = string.Empty;
    bool IsNew
    {
        get
        {
            return id == Guid.Empty;
        }
    }
    Advertisement adObj = new Advertisement();
    protected void Page_Load(object sender, EventArgs e)
    {
        string idStr = Request["Id"];
        Guid.TryParse(idStr, out id);
        if (!IsNew)
        {
            adObj = bllAd.GetByUid(id);
            imgUrlOld = adObj.ImgUrl;
        }
        if (!IsPostBack)
        {
            LoadForm();
        }

    }
    //填充控件的值
    protected void LoadForm()
    {
        txtNum.Text = adObj.Num.ToString();
        imgAdv.ImageUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + adObj.ImgUrl;        
        txtUrl.Text = adObj.Url;
        txtStartTime.Text = adObj.StartTime.ToString();
        txtEndTime.Text = adObj.EndTime.ToString();

        if (adObj.IsUseful)
        {
            rdYes.Checked = true;
        }
        else
        {
            rdNo.Checked = true;
        }
        if (adObj.ViewType == "customer")
        { rdCustomer.Checked = true; }
        else
        { rdBusiness.Checked = true; }
        txtTarget.Text = adObj.PushTarget;
    }
    //通过控件的值更新对象
    private void UpdateForm()
    {
        int num = Int32.Parse(txtNum.Text.Trim());
        string imgUrl = imgUrlOld;
        if (flupImg.PostedFile.InputStream.Length > 0)
        {
            var reader = new BinaryReader(flupImg.PostedFile.InputStream);
            var data = new byte[flupImg.PostedFile.InputStream.Length];
            reader.Read(data, 0, data.Length);
            var strBase64 = Convert.ToBase64String(data, 0, data.Length);
            string originalName = flupImg.PostedFile.FileName;
            string fileType = "image";
            string domainType = "Advertisement";
            string localSavePath = Server.MapPath("/media/");


            string savedName = FileUploader.Upload(strBase64, originalName,
                 localSavePath, domainType,
                 (FileType)Enum.Parse(typeof(FileType), fileType));
            var fileGetter = new FileGetter(savedName, localSavePath);
            FileType emFileType;
            string relativeFileName = fileGetter.GetRelativeFileName(out emFileType);

            imgUrl = savedName;
        }

        string url = txtUrl.Text.Trim();
        DateTime startTime = DateTime.Parse(txtStartTime.Text.Trim());
        DateTime endTime = DateTime.Parse(txtEndTime.Text.Trim());
        bool isUseful = false;
        if (rdYes.Checked)
        {
            isUseful = true;
        }
        string targetStr = txtTarget.Text.Trim();

        adObj.ImgUrl = imgUrl;
        adObj.Url = url;
        adObj.Num = num;
        adObj.StartTime = startTime;
        adObj.EndTime = endTime;
        adObj.PushTarget = targetStr;
        adObj.IsUseful = isUseful;

        adObj.SaveTime = DateTime.Now;
        //adObj.SaveController=
        //adObj.UpdateController=
        adObj.LastUpdateTime = DateTime.Now;

        adObj.ViewType = rdCustomer.Checked ? "customer" : "business";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateForm();
        if (IsNew)
        { bllAd.Save(adObj); }
        else
        {
            bllAd.Update(adObj);
        }
        NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
        if (IsNew)
        { lblSaveSuccess.Text = "创建成功"; }
        else
        { lblSaveSuccess.Text = "保存成功"; }
        lblSaveSuccess.Visible = true;
        System.Threading.Thread.Sleep(1500);
        Response.Redirect("default.aspx");
    }
}