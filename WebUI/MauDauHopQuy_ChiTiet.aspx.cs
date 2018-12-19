using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
using Cuc_QLCL.Data;
using CucQLCL.Common;
using Resources;

public partial class WebUI_MauDauHopQuy_ChiTiet : PageBase
{
    String DonViId = string.Empty;
    String HoSoId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["DonViId"] != null)
        {
            DonViId = Request["DonViId"];
            DmDonVi dv = ProviderFactory.DmDonViProvider.GetById(DonViId);
            txtDonvi.Text = dv.TenTiengViet;
        }
        else
            btnCapNhat.Enabled = false;
        btnBoQua.Attributes.Add("onclick", "javascript:window.close();");

        if (!IsPostBack)
        {
            // Xử lý khi thực hiện chỉnh sửa mẫu dấu
            if (Request["id"] != null)
            {
                LoadThongTinMauDau(Request["id"].ToString());
                btnCapNhat.Enabled = true;
            }
        }
    }

    /// <summary>
    /// Hiển thị thông tin mẫu dấu khi sửa
    /// </summary>
    /// <param name="mauDauId"></param>
    private void LoadThongTinMauDau(string mauDauId)
    {
        MauDauHopQuy objMauDau = ProviderFactory.MauDauHopQuyProvider.GetById(mauDauId);
        if (objMauDau != null)
        {
            if (objMauDau.DonViId != null)
            {
                DmDonVi objDonVi = ProviderFactory.DmDonViProvider.GetById(objMauDau.DonViId);
                if (objDonVi != null)
                    txtDonvi.Text = objDonVi.TenTiengViet;
            }
            txtTenMauDau.Text = objMauDau.TenMauDau;
            txtMaMauDau.Text = objMauDau.MaMauDau;
            imgMauDau.ImageUrl = "../Handler.ashx?MauDauId=" + mauDauId;
        }
    }

    /// <summary>
    /// Cập nhật thông tin mẫu dấu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        bool b = CheckFileType(ref fileupMauDau);
        if (!CheckFileType(ref fileupMauDau))
            return;
        int Length = System.Convert.ToInt32(fileupMauDau.PostedFile.InputStream.Length);
        byte[] Content = new byte[Length];
        fileupMauDau.PostedFile.InputStream.Read(Content, 0, Length);


        TransactionManager transaction = ProviderFactory.Transaction;
        try
        {
            if (Request["id"] == null)
            {
                MauDauHopQuy MauDau = new MauDauHopQuy();
                MauDau.TenMauDau = txtTenMauDau.Text.Trim();
                MauDau.MaMauDau = txtMaMauDau.Text.Trim();
                MauDau.DonViId = DonViId;
                MauDau.Dau = Content;

                ProviderFactory.MauDauHopQuyProvider.Save(transaction, MauDau);
                transaction.Commit();
                if (Request["openner"] != null)
                    Page.RegisterClientScriptBlock("Onload", "<script>alert('" + Resource.msgTaoMoiMauDau + "');window.opener.__doPostBack('MauDauPostBack','');window.close();</script>");
                else
                    Page.RegisterClientScriptBlock("Onload", "<script>alert('" + Resource.msgTaoMoiMauDau + "');window.opener.SetSignImage('../Handler.ashx?MauDauId=" + MauDau.Id + "');window.close();</script>");
            }
            else
            {
                MauDauHopQuy MauDau = ProviderFactory.MauDauHopQuyProvider.GetById(transaction, Request["id"].ToString());

                MauDau.TenMauDau = txtTenMauDau.Text.Trim();
                MauDau.MaMauDau = txtMaMauDau.Text.Trim();
                if (Content.Length > 0)
                    MauDau.Dau = Content;

                ProviderFactory.MauDauHopQuyProvider.Save(transaction, MauDau);

                transaction.Commit();
                if (Request["openner"] != null)
                    Page.RegisterClientScriptBlock("Onload", "<script>alert('Cập nhật thành công');window.opener.__doPostBack('MauDauPostBack','');window.close();</script>");
                else
                    Page.RegisterClientScriptBlock("Onload", "<script>alert('Cập nhật thành công');window.opener.SetSignImage('../Handler.ashx?MauDauId=" + MauDau.Id + "');window.close();</script>");
            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }

    }

    /// <summary>
    /// Kiểm tra kiểu file ảnh upload
    /// </summary>
    /// <param name="fu"></param>
    /// <returns></returns>
    public bool CheckFileType(ref FileUpload fu)
    {
        if (fu.HasFile)
        {
            String FileName = fu.PostedFile.FileName.ToLower();
            String extraType = FileName.Substring(FileName.LastIndexOf('.'), FileName.Length - FileName.LastIndexOf('.'));
            if (extraType == ".jpeg"
                  || extraType == ".jpg"
                  || extraType == ".bmp"
                  || extraType == ".gif")
                return true;
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo"
                   , "<script>alert('Chỉ cho phép các file có định dạng (*.bmp,*.jpeg,*.jpg,*gif)')</script>");
                fu.Focus();
                return false;
            }
        }
        return true;
    }
}
