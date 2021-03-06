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
using Resources;

public partial class WebUI_CN_HosoDaPhanCong : PageBase
{
    HoSo objHoSo = null;
    QuaTrinhXuLy objQuaTrinhXuLy = null;
    EnNguonGocList objNguonGoc = new EnNguonGocList();
    SanPham objSanPham = null;
    TList<SysUser> lstUser = null;
    SysUser lstUserDetail = null;
    DmDonVi objDonVi = null;
    PhanCong objPhanCong = null;
    TList<PhanCong> objPhanCong_old = null;
    string CvThamDinhID = null;
    string CvXuLyID = null;
    string strHoSoID = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["HoSoID"] != null)
            {
                strHoSoID = Request["HoSoID"].ToString();
                HienThiThongTin(Request["HoSoID"].ToString());
                LayDanhSachSanPham(Request["HoSoID"].ToString());
                LayDanhSachChuyenVien(strHoSoID);
                
                ThongTinXuLy(strHoSoID);
                objPhanCong_old = ProviderFactory.PhanCongProvider.GetByHoSoId(Request["HoSoID"].ToString());
                if (objPhanCong_old.Count > 0)
                {
                    foreach (PhanCong obj in objPhanCong_old)
                    {

                        txtYKien.Text = obj.YkienCuaLanhDao.ToString();

                    }
                }
            }


            SetReadOnly(txtSanPham);
            SetReadOnly(txtXuLy);
        }
    }
    /// <summary>
    ///Lưu thông tin phân công
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 8/5/2009              
    /// </Modified>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        objPhanCong = new PhanCong();
        objHoSo = new HoSo();

        string ThongBao = "";
        ThongBao = Resource.msgPhanCongThanhCong;

        if (Request["HoSoID"] != null)
        {
            objPhanCong_old = ProviderFactory.PhanCongProvider.GetByHoSoId(Request["HoSoID"].ToString());
            if (objPhanCong_old.Count > 0)
            {
                foreach (PhanCong obj in objPhanCong_old)
                {

                    objPhanCong = ProviderFactory.PhanCongProvider.GetById(obj.Id);

                }
            }

            CvThamDinhID = cboChuyenVienThamDinh.SelectedValue.ToString();
            CvXuLyID = cboChuyenVienXuLy.SelectedValue.ToString();
            objPhanCong.NguoiXuLy = CvXuLyID;
            objPhanCong.NguoiThamDinh = CvThamDinhID;
            objPhanCong.NgayCapNhatSauCung = DateTime.Now;
            objPhanCong.NgayPhanCong = DateTime.Now;
            objPhanCong.YkienCuaLanhDao = txtYKien.Text.ToString();

            //////// cập nhật thông tin hồ sơ
            objHoSo = ProviderFactory.HoSoProvider.GetById(Request["HoSoID"].ToString());
            objHoSo.TrangThaiId = (int)EnTrangThaiHoSoList.DANG_XU_LY;
            try
            {
                ProviderFactory.PhanCongProvider.Save(objPhanCong);
                /////// ghi log
                ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CN_PHAN_CONG_XU_LY, Resources.Resource.msgPhanCongThanhCong);
                ProviderFactory.HoSoProvider.Save(objHoSo);
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                  "<script>alert('" + ThongBao + "'); window.location.href='CN_HoSo_QuanLy.aspx';</script>");
                // Response.Redirect("CN_HoSo_QuanLy.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        Response.Redirect("CN_HoSo_QuanLy.aspx?direct=CN_HoSoDaGui&SoHoSo=");
    }


    /// <summary>
    ///Hien thi thông tin về hồ sơ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 8/5/2009              
    /// </Modified>
    private void HienThiThongTin(string strHoSoID)
    {

        if ((strHoSoID != "") && (strHoSoID != null))
        {
            objHoSo = new HoSo();
            objNguonGoc = new EnNguonGocList();
            objHoSo = ProviderFactory.HoSoProvider.GetById(strHoSoID);


            if (!IsPostBack)
            {
                lblSoHoSo.Text = objHoSo.SoHoSo;
                objDonVi = ProviderFactory.DmDonViProvider.GetById(objHoSo.DonViId);
                lblDonVi.Text = objDonVi.TenTiengViet;
                lblDienThoai.Text = objHoSo.DienThoai;
                lblFax.Text = objHoSo.Fax;
                lblNguoiNopHoSo.Text = objHoSo.NguoiNopHoSo;

                lblNgayNhan.Text = objHoSo.NgayTiepNhan.ToString();
                lblEmail.Text = objHoSo.Email;
                lblSoCVden.Text = objHoSo.SoCongVanDen;
                lblNhanhstu.Text = objHoSo.SoCongVanDen;
                lstUserDetail = ProviderFactory.SysUserProvider.GetById(objHoSo.NguoiTiepNhanId);
                lblNguoiTiepNhan.Text = lstUserDetail.FullName;
                lblSoCVDonVi.Text = objHoSo.SoCongVanDonVi;
                lblNguonGoc.Text = EntityHelper.GetEnumTextValue((EnNguonGocList) objHoSo.NguonGocId);
                lblNhanhstu.Text = EntityHelper.GetEnumTextValue((EnNhanHoSoTuList)objHoSo.NhanHoSoTuId);
                lblTrangThai.Text = EntityHelper.GetEnumTextValue((EnTrangThaiHoSoList)objHoSo.TrangThaiId);
            }
        }

    }
    /// <summary>
    ///Hien thi thông tin xu ly cua ho so
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 9/5/2009              
    /// </Modified>
    private void ThongTinXuLy(string strHoSoID)
    {

        if ((strHoSoID != "") && (strHoSoID != null))
        {
            objHoSo = new HoSo();
            objQuaTrinhXuLy = new QuaTrinhXuLy();
            objHoSo = ProviderFactory.HoSoProvider.GetById(strHoSoID);
            // objQuaTrinhXuLy = ProviderFactory.QuaTrinhXuLyProvider.get

            if (!IsPostBack)
            {
                txtSanPham.Text = objHoSo.DanhSachSanPham;
                txtXuLy.Text = objHoSo.Luuy;


            }
        }

    }
    /// <summary>
    ///Hien thi san pham cua hồ sơ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 8/5/2009              
    /// </Modified>
    public void LayDanhSachSanPham(string strSoHoSo)
    {

        //  objSanPham = ProviderFactory.SanPhamProvider.GetSanPhamByHoSoID(strSoHoSo);
        gvSanPham.DataSource = ProviderFactory.SanPhamProvider.GetSanPhamByHoSoID_Extend(strSoHoSo);
        gvSanPham.DataBind();

    }
    /// <summary>
    ///lấy danh sách chuyên viên
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// DucLv                 8/5/2009              
    /// </Modified>
    public void LayDanhSachChuyenVien(string strHoSoID)
    {
        if(!IsPostBack)
        {
        
        // lấy danh sách chuyên viên xử lý
        lstUser = ProviderFactory.SysUserProvider.GetAll();
        cboChuyenVienXuLy.DataSource = lstUser;
        cboChuyenVienXuLy.DataTextField = "FullName";
        cboChuyenVienXuLy.DataValueField = "ID";
        cboChuyenVienXuLy.DataBind();
        /// lấy danh sách chuyên viên thẩm định
        cboChuyenVienThamDinh.DataSource = lstUser;
        cboChuyenVienThamDinh.DataTextField = "FullName";
        cboChuyenVienThamDinh.DataValueField = "ID";
        cboChuyenVienThamDinh.DataBind();
        objPhanCong_old = ProviderFactory.PhanCongProvider.GetByHoSoId(strHoSoID);
        if (objPhanCong_old.Count > 0)
        {
            foreach (PhanCong obj in objPhanCong_old)
            {
                foreach (SysUser objUser in lstUser)
                {
                    if(obj.NguoiXuLy==objUser.Id)
                    cboChuyenVienXuLy.SelectedValue = obj.NguoiXuLy;
                    if (obj.NguoiThamDinh == objUser.Id)
                        cboChuyenVienThamDinh.SelectedValue = obj.NguoiThamDinh;

                }
                

            }
        }
        }

    }
}
