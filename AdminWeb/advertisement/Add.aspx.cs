﻿

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ydb.Push.DomainModel;
using Ydb.Push.Application;
using MediaServer;
 

public partial class advertisement_Add : BasePage
{
    IAdvertisementService advService = Bootstrap.Container.Resolve<IAdvertisementService>();

    Guid id = Guid.Empty;
    string imgUrlOld = string.Empty;
    bool IsNew
    {
        get
        {
            return id == Guid.Empty;
        }
    }
   public Advertisement adObj = new Advertisement();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string idStr = Request["Id"];
        Guid.TryParse(idStr, out id);
        if (!IsNew)
        {
            adObj = advService.GetByUid(id);
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
        imgAdv.ImageUrl = Dianzhu.Config.Config.GetAppSetting("MediaGetUrl") + adObj.ImgUrl+"_150X150";        
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
        if (adObj.PushType == "customer")
        {
            rdCustomer.Checked = true;
        }
        else if (adObj.PushType == "business")
        {
            rdBusiness.Checked = true;
        }
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
        string pushType = string.Empty;
        if (rdCustomer.Checked)
        {
            pushType = "customer";
        }
        else if (rdBusiness.Checked)
        {
            pushType = "business";
        }
        string targetStr = txtTarget.Text.Trim();

        adObj.ImgUrl = imgUrl;
        adObj.Url = url;
        adObj.Num = num;
        adObj.StartTime = startTime;
        adObj.EndTime = endTime;
        adObj.PushTarget = targetStr;
        adObj.IsUseful = isUseful;
        adObj.PushType = pushType;
        adObj.SaveTime = DateTime.Now;
        //adObj.SaveController=
        //adObj.UpdateController=
        adObj.LastUpdateTime = DateTime.Now;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UpdateForm();
        if (IsNew)
        { advService.Save(adObj); }
        else
        {
            advService.Update(adObj);
        }
       
        if (IsNew)
        { lblSaveSuccess.Text = "创建成功"; }
        else
        { lblSaveSuccess.Text = "保存成功"; }
        lblSaveSuccess.Visible = true;
    }
}