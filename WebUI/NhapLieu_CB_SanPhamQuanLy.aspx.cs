using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

public partial class WebUI_NhapLieu_CB_SanPhamQuanLy : PageBase
{
    private string HoSoId = string.Empty;
    private int LoaiHoSo = 0;
    private string strUserControl = string.Empty;
    private string strUserControlHS = string.Empty;
    //Xoa thong bao phi
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    //Xoa san pham
    List<string> deleteSPSuccess = new List<string>();
    List<string> deleteSPFail = new List<string>();
    string Action = string.Empty;
    /// <summary>
    /// Hiển thị thông tin
    /// </summary>
    ///<Modified>
    /// Author      Data        Comments    
    /// Truongtv    12/05/2009  TrườngTV
    /// Quannm      18/05/2009  Edited
    ///</Modified>
    protected void Page_Load(object sender, EventArgs e)
    {
        HoSoId = Request["HosoID"];
        LoaiHoSo = DataRepository.HoSoProvider.GetById(HoSoId).LoaiHoSo;
        strUserControl = Request["UserControl"];
        strUserControlHS = Request["UserControlHS"];

        imgThemMoiTBP.OnClientClick = "popCenter('NhapLieu_CB_ThongBaoPhi.aspx?Action=Add&HoSoID=" + HoSoId + "','CB_ThongBaoPhi_TaoMoi', 950,450); return false;";
        lnkThemMoiTBP.OnClientClick = "popCenter('NhapLieu_CB_ThongBaoPhi.aspx?Action=Add&HoSoID=" + HoSoId + "','CB_ThongBaoPhi_TaoMoi', 950,450); return false;";

        if (!IsPostBack)
        {
            BindChiTietHoSo();
            BindThongBaoPhiGrid();
            GetSanPhamOFHoSo(HoSoId);
        }
        if (this.Request["__EVENTTARGET"] == "AddNewCommit")
        {
            BindThongBaoPhiGrid();
        }

    }
    /// <summary>
    /// Hiển thị chi tiết hồ sơ
    /// </summary>
    ///<Modified>
    /// Author      Data            Comments    
    /// Truongtv    12/05/2009      Create new
    ///</Modified>
    private void BindChiTietHoSo()
    {
        try
        {
            HoSo hsInform = ProviderFactory.HoSoProvider.GetById(HoSoId);
            if (hsInform != null)
            {
                lblSoHoSo.Text = hsInform.SoHoSo;

                lblNguoiNop.Text = hsInform.NguoiNopHoSo;
                DateTime? d = hsInform.NgayTiepNhan;
                if (d != null)
                {
                    DateTime da = Convert.ToDateTime(d);
                    lblNgayTiepNhan.Text = da.ToShortDateString();
                }

                //lblFax.Text = Server.HtmlEncode(hsInform.Fax);
                lblDienthoai.Text = hsInform.DienThoai;
                lblEmail.Text = hsInform.Email;
                int? ob = hsInform.NhanHoSoTuId;

                SysUser user = ProviderFactory.SysUserProvider.GetById(hsInform.NguoiTiepNhanId);
                if (user != null)
                    lblNguoiNhan.Text = user.FullName;
                DmDonVi dmDonVi = ProviderFactory.DmDonViProvider.GetById(hsInform.DonViId);
                if (dmDonVi != null)
                    lblDonVi.Text = dmDonVi.TenTiengViet;
                if (hsInform.NguonGocId == (int)EnNguonGocList.NHAP_KHAU)
                    lblNguonGoc.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.SX_TRONG_NUOC);
                else
                    lblNguonGoc.Text = EntityHelper.GetEnumTextValue(EnNguonGocList.SX_TRONG_NUOC);
            }
            else
            {
                string thongbao = "Hồ sơ này không tồn tại hoặc đã bị xóa trong CSDL hệ thống!";

                return;
            }
        }
        catch (SqlException Sqlex)
        {
            Response.Write(Sqlex.Message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Lấy danh sách hồ sơ sản phẩm mới nhận
    /// </summary>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    private void GetSanPhamOFHoSo(string HoSoId)
    {
        TList<SanPham> listSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
        DataTable dt = new DataTable();
        dt.Columns.Add("TenSanPham");
        dt.Columns.Add("MaSanPham");
        dt.Columns.Add("KyHieu");
        dt.Columns.Add("MaNhom");
        dt.Columns.Add("TrangThai");
        dt.Columns.Add("TieuChuan");
        dt.Columns.Add("NoiDungXuLy");
        dt.Columns.Add("GhiChu");
        dt.Columns.Add("Id");

        if (listSanPham != null)
        {
            foreach (SanPham sp in listSanPham)
            {
                DataRow row = dt.NewRow();
                row["Id"] = sp.Id;
                row["TenSanPham"] = string.Empty;
                row["MaSanPham"] = string.Empty;
                row["KyHieu"] = string.Empty;
                row["MaNhom"] = string.Empty;
                row["TrangThai"] = string.Empty;
                row["TieuChuan"] = string.Empty;
                row["GhiChu"] = string.Empty;
                row["NoiDungXuLy"] = string.Empty;
                DmSanPham dmSanPham = ProviderFactory.DmSanPhamProvider.GetById(sp.SanPhamId);
                if (dmSanPham != null)
                {
                    row["TenSanPham"] = dmSanPham.TenTiengViet;
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
                    row["TieuChuan"] = strTieuChuan;
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
                if (sp.TrangThaiId != null)
                {
                    string TenTrangThai = ProviderFactory.EnTrangThaiSanPhamProvider.GetById(sp.TrangThaiId).MoTa;
                    row["TrangThai"] = TenTrangThai;
                }
                dt.Rows.Add(row);
            }
        }
        gvSanPham.DataSource = dt;
        gvSanPham.DataBind();
        if (gvSanPham.Rows.Count > 0)
        {
            ImgBtnXoa.Enabled = true;
            lnkXoa.Enabled = true;

            ImgBtnXoa.Attributes.Add("onclick", "if(GridIsChecked('gvSanPham')) { return confirm('Bạn có chắc chắn muốn xóa sản phẩm đã chọn không?');} else {alert('Bạn chưa chọn sản phẩm cần xóa.'); return false;}");
            lnkXoa.Attributes.Add("onclick", "if(GridIsChecked('gvSanPham')) { return confirm('Bạn có chắc chắn muốn xóa sản phẩm đã chọn không?');} else {alert('Bạn chưa chọn sản phẩm cần xóa.'); return false;}");

        }

        else
        {
            ImgBtnXoa.Enabled = false;
            lnkXoa.Enabled = false;
            ImgBtnXoa.Attributes.Remove("onclick");
            lnkXoa.Attributes.Remove("onclick");
        }



    }
    /// <summary>
    /// Danh sách thông báo phí của hồ sơ nếu có
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      16/05/2009      Create new
    ///</Modified>
    private void BindThongBaoPhiGrid()
    {
        DataTable dtThongBaoPhi = new DataTable();
        dtThongBaoPhi = ProviderFactory.ThongBaoLePhiProvider.GetAllThongBaoLePhi(HoSoId);
        gvPhi.DataSource = dtThongBaoPhi;
        gvPhi.DataBind();
    }

    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     QuanNM      18/05/2009    Tạo mới
    /// </Modified>
    protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSanPham.PageIndex = e.NewPageIndex;
        GetSanPhamOFHoSo(HoSoId);
    }

    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     QuanNM      18/05/2009    Tạo mới
    /// </Modified>
    protected void gvPhi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPhi.PageIndex = e.NewPageIndex;
        BindThongBaoPhiGrid();
    }

    #region "Hàm sử dụng chung cho các sự kiện trên ảnh và link"
    private void DeleteSanPham()
    {
        // Thực hiện xoá
        foreach (GridViewRow row in gvSanPham.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                string sanPhamId = gvSanPham.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                TransactionManager transaction = ProviderFactory.Transaction;
                try
                {
                    //Xoa tai lieu dinh kem truoc khi xoa san pham
                    ProviderFactory.TaiLieuDinhKemProvider.XoaTaiLieuDinhKemBySanPhamID(sanPhamId, transaction);
                    //Xoa tieu chuan ap dung truoc khi xoa san pham
                    ProviderFactory.SanPhamTieuChuanApDungProvider.XoaTieuChuanApDungBySanPhamID(sanPhamId, transaction);
                    //Xoa qua trinh xu ly
                    ProviderFactory.QuaTrinhXuLyProvider.XoaQuaTrinhXuLyBySanPhamID(sanPhamId, transaction);
                    //Xoa san pham
                    ProviderFactory.SanPhamProvider.Delete(transaction, sanPhamId);
                    transaction.Commit();
                    deleteSPSuccess.Add(sanPhamId);
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                    {
                        switch (ex.Errors[0].Number)
                        {
                            case 547: // Foreign Key violation
                                deleteSPFail.Add(sanPhamId);
                                break;
                            default:
                                deleteSPFail.Add(sanPhamId);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    deleteSPFail.Add(sanPhamId);
                }
                finally
                {
                    transaction.Dispose();
                }

            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteSPSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgXoaSanPhamThanhCong, string.Join(",", deleteSPSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_SAN_PHAM_XOA, thongbao);
        }
        if (deleteSPFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaSanPhamThatBai, string.Join(",", deleteSPFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='NhapLieu_CB_SanPhamQuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CB_HoSoDen';</script>");
    }
    private void AddSanPham()
    {
        Response.Redirect("NhapLieu_CB_SanphamChiTiet.aspx?Direct=NhapLieu_CB_SanPhamQuanLy.aspx&Action=add&HoSoID=" + HoSoId + "&UserControl=CB_HoSoDen");
    }

    private void DeleteThongBaoPhi()
    {
        // Thực hiện xoá
        foreach (GridViewRow row in gvPhi.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                //ma thong bao phi
                string thongbaophiId = gvPhi.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvPhi.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvPhi.DataKeys[row.RowIndex][2].ToString();

                TransactionManager transaction = ProviderFactory.Transaction;
                try
                {
                    ProviderFactory.ThongBaoLePhiSanPhamProvider.XoaThongBaoLePhiSanPhamTheoThongBaoLePhiID(thongbaophiId, transaction);
                    ProviderFactory.ThongBaoLePhiChiTietProvider.DeleteByThongBaoLePhiId(thongbaophiId, transaction);
                    ProviderFactory.ThongBaoLePhiProvider.Delete(transaction, thongbaophiId);
                    transaction.Commit();
                    deleteSuccess.Add(sothongbaophi);
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                    {
                        switch (ex.Errors[0].Number)
                        {
                            case 547: // Foreign Key violation
                                deleteFail.Add(sothongbaophi);
                                break;
                            default:
                                deleteFail.Add(sothongbaophi);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    deleteFail.Add(sothongbaophi);
                }
                finally
                {
                    transaction.Dispose();
                }

            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgXoaThongBaoPhi, string.Join(",", deleteSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_HO_SO_XOA, thongbao);
        }
        if (deleteFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaThongBaoPhiThatBai, string.Join(",", deleteFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='NhapLieu_CB_SanPhamQuanLy.aspx?HoSoId=" + HoSoId + "&UserControl=CB_HoSoDen';</script>");
    }
    #endregion "Hàm sử dụng chung cho các sự kiện trên ảnh và link"

    #region "Thêm mới, xóa sản phẩm. Gửi, xóa thông báo phí"
    /// <summary>
    /// Thêm mới sản phẩm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    /// <summary>
    /// Xóa thông báo phí, chỉ áp dụng cho role xử lý và trạng thái là mới tạo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    ///<Modified>
    /// Author      Data        Comments
    /// Quannm      18/05/2009  Edited
    ///</Modified>
    protected void lnkXoaTBP_Click(object sender, EventArgs e)
    {
        DeleteThongBaoPhi();
    }

    protected void imgXoaTBP_Click(object sender, ImageClickEventArgs e)
    {
        DeleteThongBaoPhi();
    }
    #endregion "Thêm mới, xóa sản phẩm. Gửi, xóa thông báo phí"


    protected void CompleteHoSo()
    {
        //Chuyển hồ sơ sang trạng thái chờ lưu trữ
        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(HoSoId);
        objHoSo.TrangThaiId = (int)EnTrangThaiHoSoList.CHO_LUU_TRU;

        //thêm thông tin hồ sơ+sản phẩm(có tên người ký và chức vụ)+sản phẩm tiêu chuẩn áp dụng
        string strDmDonViId = objHoSo.DonViId;
        string strDmTrungTamId = objHoSo.TrungTamId;
        DmDonVi objDonVi = ProviderFactory.DmDonViProvider.GetById(strDmDonViId);
        DmTrungTam objTrungTam = ProviderFactory.DmTrungTamProvider.GetById(strDmTrungTamId);
        objHoSo.DmDonViDiaChi = objDonVi.DiaChi;
        objHoSo.DmDonViDienThoai = objDonVi.DienThoai;
        objHoSo.DmDonViEmail = objDonVi.Email;
        objHoSo.DmDonViFax = objDonVi.Fax;
        objHoSo.DmDonViMaDonVi = objDonVi.MaDonVi;
        objHoSo.DmDonViTenTiengAnh = objDonVi.TenTiengAnh;
        objHoSo.DmDonViTenTiengViet = objDonVi.TenTiengViet;
        objHoSo.DmDonViTenVietTat = objDonVi.TenVietTat;
        objHoSo.DmDonViTinhThanhId = objDonVi.TinhThanhId;

        objHoSo.DmTrungTamDiaChi = objTrungTam.DiaChi;
        objHoSo.DmTrungTamDiaChiCoQuanThuHuong = objTrungTam.DiaChiCoQuanThuHuong;
        objHoSo.DmTrungTamDiaChiCoQuanThuHuongCuc = objTrungTam.DiaChiCoQuanThuHuongCuc;
        objHoSo.DmTrungTamDienThoai = objTrungTam.DienThoai;
        objHoSo.DmTrungTamEmail = objTrungTam.Email;
        objHoSo.DmTrungTamFax = objTrungTam.Fax;
        objHoSo.DmTrungTamGiamDocId = objTrungTam.GiamDocId;
        objHoSo.DmTrungTamMaTrongGcn = objTrungTam.MaTrongGcn;
        objHoSo.DmTrungTamMaTrungTam = objTrungTam.MaTrungTam;
        objHoSo.DmTrungTamSoTaiKhoan = objTrungTam.SoTaiKhoan;
        objHoSo.DmTrungTamSoTaiKhoanCuc = objTrungTam.SoTaiKhoanCuc;
        objHoSo.DmTrungTamTenCoQuanThuHuong = objTrungTam.TenCoQuanThuHuong;
        objHoSo.DmTrungTamTenCoQuanThuHuongCuc = objTrungTam.TenCoQuanThuHuongCuc;
        objHoSo.DmTrungTamTenKhoBac = objTrungTam.TenKhoBac;
        objHoSo.DmTrungTamTenKhoBacCuc = objTrungTam.TenKhoBacCuc;
        objHoSo.DmTrungTamTenTiengAnh = objTrungTam.TenTiengAnh;
        objHoSo.DmTrungTamTenTrungTam = objTrungTam.TenTrungTam;
        objHoSo.DmTrungTamTinhThanhId = objTrungTam.TinhThanhId;

        TList<SanPham> listSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(objHoSo.Id);
        foreach (SanPham sp in listSanPham)
        {
            DmSanPham objSanPham = ProviderFactory.DmSanPhamProvider.GetById(sp.SanPhamId);
            DmNhomSanPham objNhomSanPham = ProviderFactory.DmNhomSanPhamProvider.GetById(sp.NhomSanPhamId);
            DmCoQuanDoKiem objCoQuanDoKiem = ProviderFactory.DmCoQuanDoKiemProvider.GetById(sp.CoQuanDoKiemId);
            DmHangSanXuat objHangSanXuat = ProviderFactory.DmHangSanXuatProvider.GetById(sp.HangSanXuatId);
            SysUser objUser = ProviderFactory.SysUserProvider.GetById(sp.NguoiKyDuyetId);

            sp.TenNguoiKyDuyet = objUser.FullName;
            if (objUser.Position != null)
                sp.ChucVu = Convert.ToInt32(objUser.Position);
            DmTrungTam trungtam = ProviderFactory.DmTrungTamProvider.GetById(objUser.OrganizationId);
            sp.TenTrungTam = trungtam.TenTrungTam;
            DmPhongBan phongban = ProviderFactory.DmPhongBanProvider.GetById(objUser.DepartmentId);
            sp.PhongBan = phongban.TenPhongBan;

            sp.DmCqdkDiaChi = objCoQuanDoKiem.DiaChi;
            sp.DmCqdkTenCoQuanDoKiem = objCoQuanDoKiem.TenCoQuanDoKiem;
            sp.DmCqdkTenTiengAnh = objCoQuanDoKiem.TenTiengAnh;
            sp.DmHsxTenHangSanXuat = objHangSanXuat.TenHangSanXuat;
            sp.DmHsxTenTiengAnh = objHangSanXuat.TenTiengAnh;
            sp.DmNhomSpLienQuanTanSo = objNhomSanPham.LienQuanTanSo;
            sp.DmNhomSpLoaiHinhChungNhan = objNhomSanPham.LoaiHinhChungNhan;
            sp.DmNhomSpMaNhom = objNhomSanPham.MaNhom;
            sp.DmNhomSpMucLePhi = objNhomSanPham.MucLePhi;
            sp.DmNhomSpTenNhom = objNhomSanPham.TenNhom;
            sp.DmNhomSpThoiHanGcn = objNhomSanPham.ThoiHanGcn;
            sp.DmSanPhamLoaiSanPham = objSanPham.LoaiSanPham;
            sp.DmSanPhamLoaiTieuChuanApDung = objSanPham.LoaiTieuChuanApDung;
            sp.DmSanPhamMaSanPham = objSanPham.MaSanPham;
            sp.DmSanPhamNhomSanPhamId = objSanPham.NhomSanPhamId;
            sp.DmSanPhamTenTiengAnh = objSanPham.TenTiengAnh;
            sp.DmSanPhamTenTiengViet = objSanPham.TenTiengViet;

            sp.TenNguoiKyDuyet = objUser.FullName;
            if (objUser.Position != null)
                sp.ChucVu = Convert.ToInt32(objUser.Position);

            TList<SanPhamTieuChuanApDung> listSanPhamTieuChuanApDung = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(sp.Id);
            foreach (SanPhamTieuChuanApDung spad in listSanPhamTieuChuanApDung)
            {
                DmTieuChuan objTieuChuan = ProviderFactory.DmTieuChuanProvider.GetById(spad.TieuChuanApDungId);
                spad.DmTieuChuanMaTieuChuan = objTieuChuan.MaTieuChuan;
                spad.DmTieuChuanTenTieuChuan = objTieuChuan.TenTieuChuan;
                ProviderFactory.SanPhamTieuChuanApDungProvider.Save(spad);
            }
            ProviderFactory.SanPhamProvider.Save(sp);
        }
        ProviderFactory.HoSoProvider.Save(objHoSo);
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('Gửi lưu trữ thành công');location.href='CB_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen';</script>");
    }

    protected void IBThemMoi_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void LBThemMoi_Click(object sender, EventArgs e)
    {

    }

    #region Them/xoa san pham
    /// <summary>
    /// Xoa san pham
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgBtnXoa_Click(object sender, ImageClickEventArgs e)
    {
        DeleteSanPham();
    }

    /// <summary>
    /// Xoa san pham
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        DeleteSanPham();
    }

    /// <summary>
    /// Them moi san pham
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkThemMoi_Click(object sender, EventArgs e)
    {
        AddSanPham();
    }

    /// <summary>
    /// Them moi san pham
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgBtnThemmoi_Click(object sender, ImageClickEventArgs e)
    {
        AddSanPham();
    }
    #endregion

}

