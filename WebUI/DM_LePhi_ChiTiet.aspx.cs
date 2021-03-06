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
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;
using Resources;

public partial class WebUI_DM_LePhi_ChiTiet : PageBase
{
    private string MaLePhi = "";
    DmLePhi objLP = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        MaLePhi = Server.UrlDecode(Request.QueryString["id"]);
        EditLePhi(MaLePhi);
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
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_NHOMSP))
        {
            btnCapNhat.Visible = false;
        }

    }
    /// <summary>
    /// Cập nhật lệ phí
    /// </summary>
    /// <param name="MaLePhi"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void EditLePhi(string MaLePhi)
    {
        objLP = new DmLePhi();
        if ((MaLePhi != "") && (MaLePhi != null))
        {
            objLP = ProviderFactory.DmLePhiProvider.GetById(MaLePhi);
            if (!IsPostBack)
            {
                txtGiaTriLoHang.Text = objLP.GiaTriLoHang;
                txtMucLePhi.Text = FormatCurency(objLP.LePhi);
            }
        }
    }
    /// <summary>
    /// Kiểm tra trùng tên
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Tuannd                5/5/2009              
    /// </Modified>
    public bool CheckTrungTen(string Ten)
    {
        //Neu la sua
        if (Request["id"] != null)
        {
            string strTenCu = ((DmLePhi)ProviderFactory.DmLePhiProvider.GetById(Request["id"].ToString())).GiaTriLoHang.ToString();
            if (ProviderFactory.DmLePhiProvider.CheckExist(Ten, strTenCu))
            {
                Thong_bao("Giá trị lô hàng đã tồn tại, nhập giá trị khác");
                return false;
            }
        }
        else
        {
            if (ProviderFactory.DmLePhiProvider.CheckExist(Ten, string.Empty))
            {
                Thong_bao("Giá trị lô hàng đã tồn tại, nhập giá trị khác");
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Tạo mới hoặc cập nhật danh mục lệ phí
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (UnFormatCurency(txtMucLePhi.Text.Trim()) == 0)
        {
            CustomValidator1.IsValid = false;
            Thong_bao("Nhập sai định dạng số.");
            return;
        }
        if (CheckTrungTen(txtGiaTriLoHang.Text.Trim()))
        {
            string ThongBao = "";
            if ((MaLePhi != "") && (MaLePhi != null))
            {
                ThongBao = Resource.msgCapNhatLePhi;
            }
            else
            {
                ThongBao = Resource.msgTaoMoiLePhi;
            }

            objLP.GiaTriLoHang = txtGiaTriLoHang.Text.Trim();
            objLP.LePhi = UnFormatCurency(txtMucLePhi.Text.Trim());

            try
            {
                ProviderFactory.DmLePhiProvider.Save(objLP);
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                       "<script>alert('" + ThongBao + "'); window.opener.location.href='DM_LePhi.aspx';window.close();</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    /// <summary>
    /// Đóng trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
}
