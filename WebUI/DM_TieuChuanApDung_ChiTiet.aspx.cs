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
using System.Web.UI.WebControls;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
using Cuc_QLCL.Data;
using Resources;
public partial class WebUI_DM_TieuChuanApDung_ChiTiet : PageBase
{
    private string MaTieuChuan = "";
    DmTieuChuan objTC = null;
    String Direct = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["direct"] != null)
        {
            Direct = Request["direct"].ToString();
        }
        txtGhiChu.Attributes.Add("onkeyup", " if (!checkLength('" + txtGhiChu.ClientID + "', '255')) return false;");
        objTC = new DmTieuChuan();
        MaTieuChuan = Server.UrlDecode(Request.QueryString["Ma"]);
        EditTieuChuan(MaTieuChuan);
        CheckPermission();
        
    }
    /// <summary>
    /// Kiểm tra người dùng có quyền cập nhật danh mục 
    /// </summary>
    /// <param name="MaDonVi"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// giangum                 5/5/2009              
    /// </Modified>
    void CheckPermission()
    {
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_TIEUCHUAN_APDUNG))
        {
            btnCapNhat.Visible = false;
        }

    }
    /// <summary>
    /// Kiểm tra trùng mã
    /// </summary>
    public bool CheckTrungMa(string Ma)
    {
        //Neu la sua
        if (Request["Ma"] != null)
        {
            string strMaCu = ((DmTieuChuan)ProviderFactory.DmTieuChuanProvider.GetById(Request["Ma"].ToString())).MaTieuChuan.ToString();
            if (ProviderFactory.DmTieuChuanProvider.CheckExist(Ma, strMaCu))
            {
                Thong_bao(Resource.msgTrungMa);
                return false;
            }
        }
        else
        {
            if (ProviderFactory.DmTieuChuanProvider.CheckExist(Ma, string.Empty))
            {
                Thong_bao(Resource.msgTrungMa);
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Tạo mới hoặc cập nhật danh mục tiêu chuẩn
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 7/5/2009              
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (CheckTrungMa(txtMaTC.Text.Trim()))
        {
            string ThongBao = "";

            if (string.IsNullOrEmpty(MaTieuChuan))
            {
                ThongBao = Resource.msgTaoMoiTieuChuan;
            }
            else
            {
                ThongBao = Resource.msgCapNhatTieuChuan;
            }
            objTC.MaTieuChuan = txtMaTC.Text;
            objTC.TenTieuChuan = txtTenTieuChuan.Text;
            objTC.TenTiengAnh = txtTenTiengAnh.Text;
            objTC.GhiChu = txtGhiChu.Text;
            try
            {
                ProviderFactory.DmTieuChuanProvider.Save(objTC);
                ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.DM_TIEU_CHUAN_THEM_MOI, Resources.Resource.msgTaoMoiTieuChuan);
                if (Direct == "sanphamchitiet")
                {
                    Thong_bao(Resources.Resource.msgTaoMoiTieuChuan);
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "window.close();window.opener.__doPostBack('TieuChuanPostBack', '" + objTC.Id + "," + objTC.MaTieuChuan + "');", true);
                }
                else 
                    ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                       "<script>alert('" + ThongBao + "'); window.opener.location.href='DM_TieuChuanApDung.aspx';window.close();</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    /// <summary>
    /// Lấy chi tiết 1 tiêu chuẩn
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 7/5/2009              
    /// </Modified>
    protected void EditTieuChuan(string MaTieuChuan)
    {
       
        if ((MaTieuChuan != "") && (MaTieuChuan != null))
        {
            objTC = ProviderFactory.DmTieuChuanProvider.GetById(MaTieuChuan);
            if (!IsPostBack)
            {
                txtMaTC.Enabled = false;
                txtMaTC.Text = objTC.MaTieuChuan;
                txtTenTieuChuan.Text = objTC.TenTieuChuan;
                txtTenTiengAnh.Text = objTC.TenTiengAnh;
                txtGhiChu.Text = objTC.GhiChu;
                
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
       

            

    }
}
