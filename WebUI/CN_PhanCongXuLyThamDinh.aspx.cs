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
using CucQLCL.Common;
public partial class WebUI_CN_PhanCongXulyThamDinh : PageBase
{
    HoSo objHoSo = null;
    QuaTrinhXuLy objQuaTrinhXuLy = null;
    EnNguonGocList objNguonGoc = new EnNguonGocList();
    SanPham objSanPham = null;
    DataTable lstUser_TD, lstUser_XL = null;
    SysUser lstUserDetail = null;
    DmDonVi objDonVi = null;
    PhanCong objPhanCong = null;
    TList<PhanCong> objPhanCong_old = null;
    string CvThamDinhID = null;
    string CvXuLyID = null;
    string strHoSoID = null;
    TList<SanPham> lstSanPham = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["HoSoID"] != null)
            {
                strHoSoID = Request["HoSoID"].ToString();
                HienThiThongTin(Request["HoSoID"].ToString());
                // LayDanhSachSanPham(Request["HoSoID"].ToString());
                GetSanPhamOFHoSo(strHoSoID);
                LayDanhSachChuyenVien(strHoSoID);
                ThongTinXuLy(strHoSoID);
            }


            SetReadOnly(txtSanPham);
            SetReadOnly(txtKyHieu);
            SetReadOnly(txtXuLy);
        }
        if (!string.IsNullOrEmpty(strHoSoID))
        {
            if (ProviderFactory.HoSoProvider.GetById(strHoSoID).TrangThaiId == (int)EnTrangThaiHoSoList.CHO_LUU_TRU)
            {
                cboChuyenVienThamDinh.Enabled = false;
                cboChuyenVienXuLy.Enabled = false;
                txtYKien.Enabled = false;
                btnCapNhat.Visible = false;
            }

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
        objQuaTrinhXuLy = new QuaTrinhXuLy();
        string UserControl = "CN_HoSoDen";
        string ThongBao = "";
        ThongBao = Resource.msgPhanCongThanhCong;

        if (Request["HoSoID"] != null)
        {
            objPhanCong_old = ProviderFactory.PhanCongProvider.GetByHoSoId(Request["HoSoID"].ToString());
            objHoSo = ProviderFactory.HoSoProvider.GetById(Request["HoSoID"].ToString());
            if (objPhanCong_old.Count > 0)
            {
                foreach (PhanCong obj in objPhanCong_old)
                {
                    UserControl = "CN_HoSoDi";
                    objPhanCong = ProviderFactory.PhanCongProvider.GetById(obj.Id);

                }
            }
            else
            {
                objHoSo.TrangThaiId = (int)EnTrangThaiHoSoList.DANG_XU_LY;
            }

            CvThamDinhID = cboChuyenVienThamDinh.SelectedValue.ToString();
            CvXuLyID = cboChuyenVienXuLy.SelectedValue.ToString();
            objPhanCong.HoSoId = Request["HoSoID"].ToString();
            objPhanCong.NguoiXuLy = CvXuLyID;
            objPhanCong.NguoiThamDinh = CvThamDinhID;
            objPhanCong.NgayCapNhatSauCung = DateTime.Now;
            objPhanCong.NgayPhanCong = DateTime.Now;
            objPhanCong.NguoiPhanCong = mUserInfo.UserID;
            objPhanCong.YkienCuaLanhDao = txtYKien.Text.ToString();

            //////// cập nhật thông tin hồ sơ


            lstSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(Request["HoSoID"].ToString());
            // foreach(SanPham objSP in lstSanPham)
            // {
            //                 ////// luu qua trinh xu ly ////

            // //objQuaTrinhXuLy.LoaiXuLyId = (int)EnLoaiXuLyList.PHAN_CONG_XU_LY;
            // //objQuaTrinhXuLy.NoiDungXuLy = txtYKien.Text.ToString();
            // //objQuaTrinhXuLy.NguoiXuLyId = mUserInfo.UserID;
            // //objQuaTrinhXuLy.NgayXuLy = DateTime.Now;
            // //objQuaTrinhXuLy.SanPhamId = objSP.SanPhamId;
            //ProviderFactory.QuaTrinhXuLyProvider.Save(objQuaTrinhXuLy);
            // }

            try
            {
                ProviderFactory.PhanCongProvider.Save(objPhanCong);
                /////// ghi log
                ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CN_PHAN_CONG_XU_LY, Resources.Resource.msgPhanCongThanhCong);
                ProviderFactory.HoSoProvider.Save(objHoSo);
                ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
                  "<script>alert('" + ThongBao + "'); window.location.href='CN_HoSo_QuanLy.aspx?UserControl=" + UserControl + "';</script>");
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
        Response.Redirect("CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen");
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

                lblNguoiNopHoSo.Text = objHoSo.NguoiNopHoSo;
                lblNgayNhan.Text = ((DateTime)objHoSo.NgayTiepNhan).ToShortDateString();

                lblEmail.Text = objHoSo.Email;
                lblSoCVden.Text = objHoSo.SoCongVanDen;
                lblLoaiHinhChungNhan.Text = objHoSo.SoCongVanDen;
                lstUserDetail = ProviderFactory.SysUserProvider.GetById(objHoSo.NguoiTiepNhanId);
                lblNguoiTiepNhan.Text = lstUserDetail.FullName;
                lblNguonGoc.Text = EntityHelper.GetEnumTextValue((EnNguonGocList)objHoSo.NguonGocId);
                lblHinhThucTiepNhan.Text = EntityHelper.GetEnumTextValue((EnNhanHoSoTuList)objHoSo.NhanHoSoTuId);
                lblTrangThai.Text = EntityHelper.GetEnumTextValue((EnTrangThaiHoSoList)objHoSo.TrangThaiId);
                if (objHoSo.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopChuan)
                    lblLoaiHinhChungNhan.Text = "Chứng nhận hợp chuẩn";
                else
                    lblLoaiHinhChungNhan.Text = "Chứng nhận hợp quy";

                lblSanPham.Text = objHoSo.DanhSachSanPham;
                lblKyHieu.Text = objHoSo.DanhSachKyHieuSanPham;
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
                txtKyHieu.Text = objHoSo.DanhSachKyHieuSanPham;
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
    private void GetSanPhamOFHoSo(string HoSoId)
    {
        TList<SanPham> listSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
        DataTable dt = new DataTable();
        dt.Columns.Add("TenTiengViet");
        dt.Columns.Add("MaSanPham");
        dt.Columns.Add("KyHieu");
        dt.Columns.Add("MaNhom");
        dt.Columns.Add("TenTieuChuan");
        dt.Columns.Add("NoiDungXuLy");
        dt.Columns.Add("GhiChu");
        dt.Columns.Add("SanPhamID");

        if (listSanPham != null)
        {
            foreach (SanPham sp in listSanPham)
            {
                DataRow row = dt.NewRow();
                row["SanPhamID"] = sp.Id;
                row["TenTiengViet"] = string.Empty;
                row["MaSanPham"] = string.Empty;
                row["KyHieu"] = string.Empty;
                row["MaNhom"] = string.Empty;
                row["TenTieuChuan"] = string.Empty;
                row["GhiChu"] = string.Empty;
                row["NoiDungXuLy"] = string.Empty;
                DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(sp.SanPhamId);
                if (dmSanPham != null)
                {
                    row["TenTiengViet"] = dmSanPham.TenTiengViet;
                    row["MaSanPham"] = dmSanPham.MaSanPham;

                }
                row["KyHieu"] = sp.KyHieu;
                DmNhomSanPham dmNhomSP = ProviderFactory.DmNhomSanPhamProvider.GetById(sp.NhomSanPhamId);
                if (dmNhomSP != null)
                    row["MaNhom"] = dmNhomSP.MaNhom;

                TList<SanPhamTieuChuanApDung> listSPTieuChuan = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sp.Id);
                if (listSPTieuChuan != null)
                {
                    string strTieuChuan = string.Empty;
                    foreach (SanPhamTieuChuanApDung SPTieuChuan in listSPTieuChuan)
                    {
                        DmTieuChuan dmTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(SPTieuChuan.TieuChuanApDungId);
                        if (dmTieuChuan != null)
                            strTieuChuan += dmTieuChuan.MaTieuChuan + ",";
                    }
                    if (strTieuChuan.Length > 0)
                        strTieuChuan = strTieuChuan.Remove(strTieuChuan.Length - 1, 1);
                    row["TenTieuChuan"] = strTieuChuan;
                }

                TList<QuaTrinhXuLy> listQTXL = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(sp.Id);
                if (listQTXL != null)
                {
                    foreach (QuaTrinhXuLy qtxl in listQTXL)
                    {
                        if (qtxl.LoaiXuLyId == 1)
                        {
                            row["GhiChu"] = qtxl.GhiChu;
                            row["NoiDungXuLy"] = qtxl.NoiDungXuLy;
                            break;
                        }
                    }
                }

                dt.Rows.Add(row);
            }
        }
        gvSanPham.DataSource = dt;
        gvSanPham.DataBind();




    }
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
        // lấy danh sách chuyên viên xử lý
        lstUser_XL = ProviderFactory.SysUserProvider.GetUserByPermissionID("0104", mUserInfo.TrungTam.Id);// lay user co quyen xu ly chung nhan
        lstUser_TD = ProviderFactory.SysUserProvider.GetUserByPermissionID("0105", mUserInfo.TrungTam.Id);// lay user co quyen tham dinh chung nhan
        cboChuyenVienXuLy.DataSource = lstUser_XL;
        cboChuyenVienXuLy.DataTextField = "FullName";
        cboChuyenVienXuLy.DataValueField = "ID";
        cboChuyenVienXuLy.DataBind();
        /// lấy danh sách chuyên viên thẩm định
        cboChuyenVienThamDinh.DataSource = lstUser_TD;
        cboChuyenVienThamDinh.DataTextField = "FullName";
        cboChuyenVienThamDinh.DataValueField = "ID";
        cboChuyenVienThamDinh.DataBind();
        objPhanCong_old = ProviderFactory.PhanCongProvider.GetByHoSoId(Request["HoSoID"].ToString());
        if (objPhanCong_old != null)
        {
            foreach (PhanCong obj in objPhanCong_old)
            {
                //foreach (SysUser objUser in lstUser)
                foreach (DataRow dr_xl in lstUser_XL.Rows)
                {

                    //if (obj.NguoiXuLy == objUser.Id)
                    if (obj.NguoiXuLy == dr_xl["ID"].ToString())
                        cboChuyenVienXuLy.SelectedValue = obj.NguoiXuLy;

                }

                foreach (DataRow dr_td in lstUser_TD.Rows)
                {

                    if (obj.NguoiThamDinh == dr_td["ID"].ToString())
                        cboChuyenVienThamDinh.SelectedValue = obj.NguoiThamDinh;

                }
                txtYKien.Text = obj.YkienCuaLanhDao;
                //btnCapNhat.Enabled = false;
            }

        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "DM_SanPham_ChiTiet.aspx",
                       "<script>popCenter('DM_SanPham_ChiTiet.aspx','DM_SanPham_ChiTiet',720,520);</script>");
    }

    protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSanPham.PageIndex = e.NewPageIndex;
        strHoSoID = Request["HoSoID"].ToString();
        LayDanhSachSanPham(strHoSoID);
    }
}
