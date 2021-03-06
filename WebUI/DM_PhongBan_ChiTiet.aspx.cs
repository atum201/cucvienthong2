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
using Resources;

/// <summary>
/// Xem chi tiết, tạo mới, cập nhật phòng ban
/// </summary>
/// <Modified>
/// Người tạo              Ngày tạo                Chú thích
/// QuanNM                 7/5/2009              
/// </Modified>
public partial class WebUI_DM_PhongBan_ChiTiet : PageBase
{
    private string MaPhongBan = "";
    DmPhongBan objPB = new DmPhongBan();

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckPermission();
        txtMoTa.Attributes.Add("onkeyup", " if (!checkLength('" + txtMoTa.ClientID + "', '255')) return false;");
        MaPhongBan = Server.UrlDecode(Request.QueryString["id"]);
        if (!IsPostBack)
        {
            Bind_ddlTruongPhong();
        }
        //Hiển thị dữ liệu nếu là sửa phòng ban
        BindDataEdit(MaPhongBan);

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
        if (!mUserInfo.IsPermission(EnPermission.QUANLY_DM_PHONGBAN))
        {
            btnCapNhat.Visible = false;
        }

    }
    /// <summary>
    /// Lấy tất cả danh sách nguời dùng hệ thống
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void Bind_ddlTruongPhong()
    {
        TList<SysUser> list = ProviderFactory.SysUserProvider.GetAll();
        list.Filter = "OrganizationId = " + mUserInfo.TrungTam.Id;
        ddlTruongPhong.DataSource = list;
        ddlTruongPhong.DataValueField = "ID";
        ddlTruongPhong.DataTextField = "FullName";
        ddlTruongPhong.DataBind();
    }
    /// <summary>
    /// Lấy thông tin phòng ban
    /// </summary>
    /// <param name="MaPhongBan"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void BindDataEdit(string MaPhongBan)
    {
        //gán dữ liệu vào dropdownlist trưởng phòng        
        if ((MaPhongBan != "") && (MaPhongBan != null))
        {
            objPB = ProviderFactory.DmPhongBanProvider.GetById(MaPhongBan);
            //chỉ nạp dữ liệu khi load trong đầu tiên
            if (!IsPostBack)
            {
                txtTenPB.Text = objPB.TenPhongBan;
                txtMoTa.Text = objPB.MoTa;
                foreach (ListItem item in ddlTruongPhong.Items)
                {
                    if (item.Value == objPB.TruongPhongId)
                    {
                        ddlTruongPhong.SelectedValue = objPB.TruongPhongId;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Kiểm tra trùng mã
    /// </summary>
    /// <param name="MaDonVi"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// Tuannd                5/5/2009              
    /// </Modified>
    public bool CheckTrungTen(string Ten)
    {
        //Neu la sua
        if (Request["id"] != null)
        {
            string strTenCu = ((DmPhongBan)ProviderFactory.DmPhongBanProvider.GetById(Request["id"].ToString())).TenPhongBan.ToString();
            if (ProviderFactory.DmPhongBanProvider.CheckExist(Ten, strTenCu))
            {
                Thong_bao(Resource.msgTrungTen);
                return false;
            }
        }
        else
        {
            if (ProviderFactory.DmPhongBanProvider.CheckExist(Ten, string.Empty))
            {
                Thong_bao(Resource.msgTrungTen);
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Tạo mới hoặc cập nhật danh mục phòng ban
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        if (CheckTrungTen(txtTenPB.Text.Trim()))
        {
            //chuỗi chứa thông báo
            string ThongBao = "";
            if ((MaPhongBan != "") && (MaPhongBan != null))
            {
                ThongBao = Resource.msgCapNhatPhongBan;
            }
            else
            {
                ThongBao = Resource.msgTaoMoiPhongBan;
            }
            objPB.TenPhongBan = txtTenPB.Text.Trim();
            objPB.TruongPhongId = ddlTruongPhong.SelectedValue;
            objPB.MoTa = txtMoTa.Text.Trim();
            //Lấy mã trung tâm
            //objPB.TrungTamId = ProviderFactory.DmPhongBanProvider.Config_GetValueByName("MA_TRUNG_TAM");
            try
            {
                ProviderFactory.DmPhongBanProvider.Save(objPB);
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                       "<script>alert('" + ThongBao + "'); window.opener.location.href='DM_PhongBan.aspx';window.close();</script>");
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
