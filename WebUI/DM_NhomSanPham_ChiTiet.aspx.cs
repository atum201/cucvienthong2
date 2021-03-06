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
using System.Data.SqlClient;
using Resources;

/// <summary>
/// Danh mục nhóm sản phẩm chi tiết
/// </summary>
/// <Modified>
///	Name		Date		Comments
/// TruongTV	6/5/2009	Thêm mới
/// </Modified>>
public partial class WebUI_DM_NhomSanPham_ChiTiet : PageBase
{
    private string strDirect;
    /// <summary>
    /// Hiên thị chi tiết nhóm sản phẩm
    /// </summary>
    /// <Modified>
    ///	Name		Date		Comments
    /// TruongTV	6/5/2009	Thêm mới
    /// </Modified>>
    protected void Page_Load(object sender, EventArgs e)
    {
        strDirect = Request["direct"];

        if (!IsPostBack)
        {
            CheckPermission();
            strDirect = Request["direct"];
            if (strDirect == "add")
            {
                //Thêm mới nhóm sản phẩm  	
                this.LoadComboThoiHan();
                rdCo.Checked = true;

            }
            else
            {
                string ID = Request["Id"].ToString();
                //Cập nhật nhóm sản phẩm 
                DmNhomSanPham objNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(ID);


                txtMaNhomSP.Text = objNhomSanPham.MaNhom;
                txtTenNhomSP.Text = objNhomSanPham.TenNhom;
                txtMucLePhi.Text = FormatCurency(objNhomSanPham.MucLePhi);
                //Hiển thị Combo
                this.LoadComboThoiHan();
                ddlThoiHan.SelectedValue = Convert.ToString(objNhomSanPham.ThoiHanGcn);
                ddlLoaiHinhChungNhan.SelectedValue = Convert.ToString(objNhomSanPham.LoaiHinhChungNhan);
                if (objNhomSanPham.LienQuanTanSo)
                {
                    //Có liên quan đến tần số
                    rdCo.Checked = true;
                    rdKhong.Checked = false;
                }
                else
                {
                    //Không liên quan đến tần số
                    rdCo.Checked = false;
                    rdKhong.Checked = true;
                }

            }
        }
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
    /// Load giá trị vào combobox thời hạn
    /// </summary>
    private void LoadComboThoiHan()
    {

        DataTable dtThoiHan = new DataTable();
        dtThoiHan.Columns.Add("Text");
        dtThoiHan.Columns.Add("Value");
        dtThoiHan.Rows.Add("2 năm", (int)EnThoiHanList.HAI_NAM);
        dtThoiHan.Rows.Add("3 năm", (int)EnThoiHanList.BA_NAM);
        dtThoiHan.Rows.Add("Sử dụng 1 lần", (int)EnThoiHanList.MOT_LAN_SU_DUNG);

        ddlThoiHan.DataSource = dtThoiHan;
        ddlThoiHan.DataValueField = "Value";
        ddlThoiHan.DataTextField = "Text";
        ddlThoiHan.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                   "<script>alert('Cập nhật thành công!'); location.href='DM_NhomSanPham_ChiTiet.aspx'; </script>");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
    /// <summary>
    /// Kiểm tra trùng mã
    /// </summary>
    public bool CheckTrungMa(string Ma)
    {
        //Neu la sua
        if (Request["id"] != null)
        {
            string strMaCu = ((DmNhomSanPham)ProviderFactory.DmNhomSanPhamProvider.GetById(Request["ID"].ToString())).MaNhom.ToString();
            if (ProviderFactory.DmNhomSanPhamProvider.CheckExist(Ma, strMaCu))
            {
                Thong_bao(Resource.msgTrungMa);
                return false;
            }
        }
        else
        {
            if (ProviderFactory.DmNhomSanPhamProvider.CheckExist(Ma, string.Empty))
            {
                Thong_bao(Resource.msgTrungMa);
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Cập nhật nhóm sản phẩm
    /// </summary>
    ///<Modified>
    /// Name		Date		Comments
    /// TruongTv	7/5/2009	Thêm mới
    ///</Modified>>
    protected void CapNhat_Click(object sender, EventArgs e)
    {
        if (UnFormatCurency(txtMucLePhi.Text.Trim()) == 0)
        {
            CustomValidator1.IsValid = false;
            Thong_bao("Nhập sai định dạng số.");
            return;
        }
        if (CheckTrungMa(txtMaNhomSP.Text.Trim()))
        {
            string ThongBao = "";
            strDirect = Request["direct"];
            if (strDirect != "add")
            {
                //Sửa nhóm sản phẩm
                Cuc_QLCL.Entities.DmNhomSanPham objNhomSanPhamEntity = ProviderFactory.DmNhomSanPhamProvider.GetById(Request["id"].ToString());

                objNhomSanPhamEntity.Id = Request["id"].ToString();
                objNhomSanPhamEntity.MaNhom = txtMaNhomSP.Text;
                objNhomSanPhamEntity.TenNhom = txtTenNhomSP.Text;
                objNhomSanPhamEntity.MucLePhi = UnFormatCurency(txtMucLePhi.Text.Trim());
                objNhomSanPhamEntity.ThoiHanGcn = int.Parse(ddlThoiHan.SelectedValue);
                objNhomSanPhamEntity.LienQuanTanSo = false;
                objNhomSanPhamEntity.LoaiHinhChungNhan = Convert.ToInt32(ddlLoaiHinhChungNhan.SelectedValue);
                if (rdCo.Checked)
                {
                    objNhomSanPhamEntity.LienQuanTanSo = true;
                }
                try
                {
                    try
                    {
                        ProviderFactory.DmNhomSanPhamProvider.Update(objNhomSanPhamEntity);
                        ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_NHOM_SAN_PHAM_SUA, "Sửa nhóm sản phẩm");
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 2601: // Primary key violation (Duplicate row)
                                    Thong_bao("Nhóm sản phẩm đã tồn tại");
                                    break;
                            }
                        }
                    }

                    ThongBao = Resources.Resource.msgCapNhatNhomSanPhamThanhCong;
                }
                catch
                {
                    Thong_bao(this, Resources.Resource.msgCapNhatNhomSanPhamThatBai);
                }
            }
            else
            {
                //Thêm mới nhóm sản phẩm.

                DmNhomSanPham objNhomSanPhamEntity = new DmNhomSanPham();
                objNhomSanPhamEntity.MaNhom = txtMaNhomSP.Text;
                objNhomSanPhamEntity.Id = "1";
                objNhomSanPhamEntity.TenNhom = txtTenNhomSP.Text;
                objNhomSanPhamEntity.MucLePhi = int.Parse(txtMucLePhi.Text.Trim());
                objNhomSanPhamEntity.ThoiHanGcn = int.Parse(ddlThoiHan.SelectedValue);
                objNhomSanPhamEntity.LienQuanTanSo = false;
                objNhomSanPhamEntity.LoaiHinhChungNhan = Convert.ToInt32( ddlLoaiHinhChungNhan.SelectedValue);
                if (rdCo.Checked)
                {
                    objNhomSanPhamEntity.LienQuanTanSo = true;
                }

                if (objNhomSanPhamEntity.IsValid)
                {
                    try
                    {
                        ProviderFactory.DmNhomSanPhamProvider.Save(objNhomSanPhamEntity);

                        ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.DM_NHOM_SAN_PHAM_THEM_MOI, "Thêm mới nhóm sản phẩm");

                    }
                    catch (SqlException ex)
                    {
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 2601: // Primary key violation (Duplicate row)
                                    Thong_bao("Nhóm sản phẩm đã tồn tại");
                                    break;
                            }
                        }
                    }
                    ThongBao = Resources.Resource.msgTaoMoiNhomSanPhamThanhCong;
                }
                else
                {
                    Thong_bao(this, Resources.Resource.msgTaoMoiNhomSanPhamThatBai);
                }
            }
            ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                       "<script>alert('" + ThongBao + "'); window.opener.location.href='DM_NhomSanPham.aspx';window.close();</script>");

        }

    }
}
