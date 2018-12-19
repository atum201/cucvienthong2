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

/// <summary>
/// Quản lý mẫu dấu hợp quy
/// </summary>
/// <modifided>
/// Name        Date        comment
/// TuấnVM      20/05/2010  tạo mới
/// </modifided>
public partial class WebUI_QuanLyMauDau : PageBase
{
    /// <summary>
    /// Load trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modifided>
    /// Name        Date        comment
    /// TuấnVM      20/05/2010  tạo mới
    /// </modifided>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_DonVi();
        }
        else
        {
            string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
            //nếu postback từ trang thêm mới sản phẩm
            if (eventTarget == "MauDauPostBack")
                Bind_MauDau();
        }

        // Nếu chưa chọn đơn vị thì chưa cho thêm mới/xoá mẫu dấu
        if (ddlDonVi.SelectedValue == "-1")
        {
            tdAction.Visible = false;
        }
        else
        {
            tdAction.Visible = true;
        }
    }

    /// <summary>
    /// Load danh sách mẫu dấu theo đơn vị
    /// </summary>
    /// <param name="DonViID"></param>
    /// <modifided>
    /// Name        Date        comment
    /// TuấnVM      20/05/2010  tạo mới
    /// </modifided>
    public void Bind_MauDau()
    {
        string DonViID = ddlDonVi.SelectedValue;
        TList<MauDauHopQuy> listMauDau = ProviderFactory.MauDauHopQuyProvider.GetByDonViId(DonViID);
        dtlstMauDau.DataSource = listMauDau;
        dtlstMauDau.DataBind();
        if (listMauDau.Count == 0 && ddlDonVi.SelectedValue != "-1")
        {
            lblThongbao.Text = "Đơn vị hiện chưa có mẫu dấu nào!";
            lblThongbao.Visible = true;
            lnkXoa.Visible = false;
        }
        else if (ddlDonVi.SelectedValue == "-1")
        {
            lblThongbao.Text = "Lựa chọn đơn vị để xem mẫu dấu!";
            lblThongbao.Visible = true;
        }
        else
        {
            lblThongbao.Visible = false;
            lnkXoa.Visible = true;
        }
        hdMauDauId.Value = string.Empty;
    }

    /// <summary>
    /// Load danh sách đơn vị
    /// </summary>
    /// <modifided>
    /// Name        Date        comment
    /// TuấnVM      20/05/2010  tạo mới
    /// </modifided>
    public void Bind_DonVi()
    {
        DmDonVi objNull = new DmDonVi();
        objNull.TenTiengViet = "Chọn ...";
        objNull.Id = "-1";
        TList<DmDonVi> listdv = ProviderFactory.DmDonViProvider.GetAll();
        listdv.Add(objNull);
        ddlDonVi.DataSource = listdv;
        ddlDonVi.DataTextField = "Tentiengviet";
        ddlDonVi.DataValueField = "id";
        ddlDonVi.DataBind();
        ddlDonVi.SelectedValue = "-1";
    }

    /// <summary>
    /// Load lại danh sách mẫu dấu theo đơn vị được chọn
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modifided>
    /// Name        Date        comment
    /// TuấnVM      20/05/2010  tạo mới
    /// </modifided>
    protected void ddlDonVi_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_MauDau();
    }

    /// <summary>
    /// Xoá mẫu dấu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modifided>
    /// Name        Date        comment
    /// TuấnVM      20/05/2010  tạo mới
    /// </modifided>
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdMauDauId.Value))
        {
            string MauDauId = hdMauDauId.Value.Substring(hdMauDauId.Value.IndexOf('=') + 1);
            ProviderFactory.MauDauHopQuyProvider.Delete(MauDauId);
            Bind_MauDau();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script> alert('Xoá mẫu dấu thành công');</script>");
        }
    }

    /// <summary>
    /// Thêm mới mẫu dấu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modifided>
    /// Name        Date        comment
    /// TuấnVM      20/05/2010  tạo mới
    /// </modifided>
    protected void llbThemMoi_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Themmaudau", "<script>popCenter('MauDauHopQuy_ChiTiet.aspx?openner=ql&DonViId=" + ddlDonVi.SelectedValue + "','ThemMoi',600,300);</script>");
    }

    /// <summary>
    /// Thêm mới mẫu dấu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modifided>
    /// Name        Date        comment
    /// TuấnVM      20/05/2010  tạo mới
    /// </modifided>
    protected void imgThemMoi_Click(object sender, ImageClickEventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Them mau dau", "<script> popCenter('MauDauHopQuy_ChiTiet.aspx?openner=ql&DonViId=" + ddlDonVi.SelectedValue + "','ThemMoi',600,300);</script>");
    }
}

