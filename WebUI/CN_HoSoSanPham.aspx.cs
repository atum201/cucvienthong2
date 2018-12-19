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

public partial class WebUI_CN_HoSoSanPham : PageBase
{
    string HoSoId = string.Empty;
    string Action = string.Empty;
    string Direct = string.Empty;
    int TrangThaiId = 0;
    int KetLuanId = 0;
    string mSanPhamId = string.Empty;
    //Xoa thong bao phi
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();

    //Gui thong bao phi
    List<string> sendSuccess = new List<string>();
    List<string> sendFail = new List<string>();

    private string jsHideButtonView = @"$(function()
                                         {
                                            ShowAction(false);
                                         });";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["direct"] != null)
            Direct = Request["direct"].ToString().ToLower();
        if (Request["action"] != null)
        {
            Action = Request["action"].ToString().ToLower();
            if (Action == "view")
            {
                // Ẩn các link chức năng
                trChucNang.Visible = false;
                gvPhi.Columns[8].Visible = false;

                if (mUserInfo.IsThanhTra)
                {
                    gvPhi.Columns[6].Visible = false;
                    gvPhi.Columns[7].Visible = false;
                }
                StringBuilder sb = new StringBuilder();
                if (Direct == "cn_trahoso")
                    sb.Append(@"<a href='../WebUI/CN_TraCuuThongTinHoSo.aspx'> TRA CỨU HỒ SƠ CHỨNG NHẬN</a>");
                else if (Direct == "cn_hosodi")
                    sb.Append(@"<a href='../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDi'> DANH SÁCH HỒ SƠ CHỨNG NHẬN ĐÃ GỬI</a>");
                lblPath.Text = sb.ToString();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowAction", jsHideButtonView, true);
                if (Request["HosoId"] != null)
                {
                    HoSoId = Request["HosoId"].ToString();
                    if (!IsPostBack)
                    {
                        Bind_HoSo(HoSoId);
                        BindThongBaoPhiGrid();
                    }
                }
            }
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<a href='../WebUI/CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen'> DANH SÁCH HỒ SƠ MỚI</a>");
            lblPath.Text = sb.ToString();
        }

        if (Request["HosoId"] != null && Request["TrangThaiId"] != null)
        {
            try
            {
                HoSoId = Request["HosoId"].ToString();
                TrangThaiId = Convert.ToInt16(Request["TrangThaiId"].ToString());
                if (TrangThaiId != (int)EnTrangThaiHoSoList.HO_SO_MOI)
                    return;
                if (!IsPostBack)
                {
                    Bind_HoSo(HoSoId);
                    BindThongBaoPhiGrid();
                }
                else
                {
                    if (this.Request["__EVENTTARGET"] == "AddNewCommit")
                    {
                        string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                        Response.Redirect("CN_HoSoSanPham.aspx?HoSoId=" + passedArgument + "&TrangThaiId=1");
                    }
                }
            }
            catch (Exception ex) { }
        }

        IBThemMoi.OnClientClick = "popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','ThongBaoNopTienTaoMoi', 950,450); return false;";
        LBThemMoi.OnClientClick = "popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?Action=Add&HoSoID=" + HoSoId + "','ThongBaoNopTienTaoMoi', 950,450); return false;";

        ImgBtnThemmoi.Attributes.Add("onclick", "javascript:window.location.href='CN_TiepNhanHoSo_TaoMoi.aspx?direct=CN_HoSoSanPham&Action=add&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'; return false;");
        lnkThemMoi.Attributes.Add("onclick", "javascript:window.location.href='CN_TiepNhanHoSo_TaoMoi.aspx?direct=CN_HoSoSanPham&Action=add&HoSoId=" + HoSoId + "&TrangThaiId=" + TrangThaiId + "'; return false;");
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
        dt.Columns.Add("TrangThaiId");
        dt.Columns.Add("KetLuanId");

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
                row["TrangThaiId"] = string.Empty;
                row["KetLuanId"] = "0";
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
                    row["TrangThaiId"] = sp.TrangThaiId.ToString();
                }
                if (sp.KetLuanId != null)
                    row["KetLuanId"] = sp.KetLuanId.ToString();

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
    /// 
    /// </summary>
    /// <param name="HoSoId"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    private void Bind_HoSo(string HoSoId)
    {
        try
        {
            HoSo hsInform = ProviderFactory.HoSoProvider.GetById(HoSoId);
            if (hsInform != null)
            {
                if (hsInform.LoaiHoSo == (int)CucQLCL.Common.LoaiHoSo.ChungNhanHopQuy)
                    trThongBaoNopTien.Style.Add("Display", "none");
                if (hsInform.TrangThaiId != (int)EnTrangThaiHoSoList.HO_SO_MOI && Action != "view")
                    return;
                lblSohoso.Text = hsInform.SoHoSo;

                int? ob = hsInform.TrangThaiId;
                if (ob != null)
                {
                    int ttid = Convert.ToInt16(ob);
                    EnTrangThaiHoSo TrangThai = ProviderFactory.EnTrangThaiHoSoProvider.GetById(ttid);
                    lblTrangThai.Text = TrangThai.MoTa;
                }

                //lbSoCongVan.Text = Server.HtmlEncode(hsInform.SoCongVanDonVi);
                lblSoCongVanDen.Text = hsInform.SoCongVanDen;
                lblNguoiNop.Text = hsInform.NguoiNopHoSo;
                DateTime? d = hsInform.NgayTiepNhan;
                if (d != null)
                {
                    DateTime da = Convert.ToDateTime(d);
                    lblNgayNhan.Text = da.ToShortDateString();
                }

                //lblFax.Text = Server.HtmlEncode(hsInform.Fax);
                lblDienthoai.Text = hsInform.DienThoai;
                lblEmail.Text = hsInform.Email;
                ob = hsInform.NhanHoSoTuId;
                if (ob != null)
                {
                    int ntid = Convert.ToInt16(ob);
                    EnNhanHoSoTu nt = ProviderFactory.EnNhanHoSoTuProvider.GetById(ntid);
                    if (nt != null)
                        lblLoaiHinh.Text = nt.MoTa;
                }
                SysUser user = ProviderFactory.SysUserProvider.GetById(hsInform.NguoiTiepNhanId);
                if (user != null)
                    lblNguoiNhan.Text = user.FullName;
                DmDonVi dmDonVi = ProviderFactory.DmDonViProvider.GetById(hsInform.DonViId);
                //LongHH
                if (dmDonVi != null)
                {
                    lblDonVi.Text = dmDonVi.TenTiengViet;
                    lblTenTiengAnh.Text = dmDonVi.TenTiengAnh;
                    lblDiaChi.Text = dmDonVi.DiaChi;
                    lblMaSoThue.Text = dmDonVi.MaSoThue;
                }
                //LongHH
                    
                ob = hsInform.NguonGocId;
                if (ob != null)
                {
                    int ngid = Convert.ToInt16(ob);
                    EnNguonGoc enNguonGoc = ProviderFactory.EnNguonGocProvider.GetById(ngid);
                    if (enNguonGoc != null)
                        lblNguonGoc.Text = enNguonGoc.MoTa;
                }

                if (hsInform.LoaiHoSo == 1)
                    lblLoaiHinhChungNhan.Text = "Chứng nhận hợp quy";
                else
                    lblLoaiHinhChungNhan.Text = "Chứng nhận hợp chuẩn";
                GetSanPhamOFHoSo(HoSoId);
            }
            else
            {
                string thongbao = "Hồ sơ này không tồn tại hoặc đã bị xóa trong CSDL hệ thống!";
                if (Action == "view")
                    Thong_bao(this.Page, thongbao, "CN_TraCuuThongTinHoSo.aspx");

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
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void gvSanPham_DataBound(object sender, EventArgs e)
    {
        int TotalChkBx = 0;
        if (gvSanPham.HeaderRow != null)
        {
            // Lấy tham chiếu đến checkbox header
            CheckBox cbHeader = (CheckBox)gvSanPham.HeaderRow.FindControl("chkCheckAll");
            // Bắt sự kiện onclick của checkbox toàn bộ
            cbHeader.Attributes["onclick"] = string.Format("HeaderClick('{0}');", gvSanPham.ID);

            // Duyệt trên gridview
            foreach (GridViewRow gvr in gvSanPham.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkCheck");
                cb.Attributes["onclick"] = string.Format("ChildClick('{0}');", gvSanPham.ID);
                // Đếm số lượng check box con
                TotalChkBx++;
            }
        }
        // Gán giá trị của biến
        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, 0), true);
        if (Action == "view")
            gvSanPham.Columns[7].Visible = false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        CheckBox chk;
        string IdSanPham = string.Empty;
        List<string> tensanpham = new List<string>();
        TransactionManager transaction = ProviderFactory.Transaction;
        try
        {
            foreach (GridViewRow rowItem in gvSanPham.Rows)
            {
                chk = (CheckBox)(rowItem.Cells[6].FindControl("chkCheck"));
                if (chk.Checked)
                {
                    IdSanPham = gvSanPham.DataKeys[rowItem.RowIndex][0].ToString();

                    TList<SanPhamTieuChuanApDung> listspap = ProviderFactory.SanPhamTieuChuanApDungProvider.GetBySanPhamId(transaction, IdSanPham);
                    //xóa các tiêu chuẩn áp dụng cho sản phẩm 
                    if (listspap != null && listspap.Count > 0)
                    {
                        ProviderFactory.SanPhamTieuChuanApDungProvider.Delete(transaction, listspap);
                        //transaction.Commit();
                    }
                    //transaction.Dispose();

                    //xóa các tài liệu đính kèm cùng sản phẩm
                    //transaction = ProviderFactory.Transaction;
                    TList<TaiLieuDinhKem> listTaiLieu = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(transaction, IdSanPham);
                    if (listTaiLieu != null && listTaiLieu.Count > 0)
                    {
                        //Xóa các file tai liệu lưu trữ tại Server
                        foreach (TaiLieuDinhKem tl in listTaiLieu)
                        {
                            string FileName = tl.TenFile;
                            DeleteFile(FileName);
                        }
                        ProviderFactory.TaiLieuDinhKemProvider.Delete(transaction, listTaiLieu);
                        //transaction.Commit();
                    }

                    //transaction.Dispose();

                    //xóa các thông tin về quát trình xử lý sản phẩm
                    transaction = ProviderFactory.Transaction;
                    TList<QuaTrinhXuLy> listxuly = ProviderFactory.QuaTrinhXuLyProvider.GetBySanPhamId(transaction, IdSanPham);
                    if (listxuly != null && listxuly.Count > 0)
                    {
                        ProviderFactory.QuaTrinhXuLyProvider.Delete(transaction, listxuly);
                        //transaction.Commit();
                    }

                    //transaction.Dispose();

                    //xóa sản phẩm
                    //transaction = ProviderFactory.Transaction;
                    SanPham sp = ProviderFactory.SanPhamProvider.GetById(transaction, IdSanPham);
                    if (sp != null)
                    {
                        ProviderFactory.SanPhamProvider.Delete(transaction, sp);
                        tensanpham.Add(gvSanPham.DataKeys[rowItem.RowIndex][1].ToString());
                        //transaction.Commit();
                    }
                }
            }
            if (tensanpham.Count > 0)
            {
                transaction.Commit();
                string strlog = string.Join(", ", tensanpham.ToArray());
                Thong_bao(string.Format(Resources.Resource.msgXoaSanPhamThanhCong, strlog));
                //GetSanPhamOFHoSo(HoSoId);
            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
        GetSanPhamOFHoSo(HoSoId);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void lnkGui_Click(object sender, EventArgs e)
    {
        HoSo hs = ProviderFactory.HoSoProvider.GetById(HoSoId);
        if (hs != null)
        {
            hs.TrangThaiId = (int)EnTrangThaiHoSoList.CHO_PHAN_CONG;
            TransactionManager transaction = ProviderFactory.Transaction;
            try
            {
                ProviderFactory.HoSoProvider.Update(hs);
                transaction.Commit();
                string ThongBao = Resources.Resource.msgGuiHoSoThanhCong;
                Thong_bao(this.Page, ThongBao, "CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void gvSanPham_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            mSanPhamId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            TrangThaiId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiId"));
            KetLuanId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "KetLuanId"));
            string TrangThaiSp = DataBinder.Eval(e.Row.DataItem, "TrangThai").ToString();
            if (Action != "view")
            {
                LinkButton lnk = new LinkButton();
                lnk.OnClientClick = string.Format("window.location.href='CN_TiepNhanHoSo_TaoMoi.aspx?direct=CN_HoSoSanPham&Action=Edit&HoSoId={0}&SanPhamID={1}&TrangThaiID={2}';return false;", HoSoId, mSanPhamId, TrangThaiId.ToString());
                lnk.Text = DataBinder.Eval(e.Row.DataItem, "TenSanPham").ToString();
                e.Row.Cells[0].Controls.Add(lnk);

            }
            else
            {
                LinkButton lnk = new LinkButton();
                if (Direct == "cn_trahoso")
                    lnk.OnClientClick = string.Format("window.location.href='CN_TiepNhanHoSo_TaoMoi.aspx?direct=CN_TraCuuHoSo_HoSoSanPham&Action=view&HoSoId={0}&SanPhamID={1}';return false;", HoSoId, mSanPhamId);
                else
                    lnk.OnClientClick = string.Format("window.location.href='CN_TiepNhanHoSo_TaoMoi.aspx?direct=CN_HoSoSanPham&Action=view&HoSoId={0}&SanPhamID={1}';return false;", HoSoId, mSanPhamId);
                lnk.Text = DataBinder.Eval(e.Row.DataItem, "TenSanPham").ToString();
                e.Row.Cells[0].Controls.Add(lnk);

                // Thiết lập giá trị cho cột trạng thái sản phẩm
                LinkButton lnkTrangThai = new LinkButton();

                // Kiểm tra trạng thái, lấy công văn trả lời nếu có
                bool CoCongVanTraLoi = false;
                if (KetLuanId != (int)EnKetLuanList.CAP_GCN && KetLuanId != 0)
                {
                    //  lấy danh sách tài liệu
                    TList<TaiLieuDinhKem> lstTaiLieuDinhKem = ProviderFactory.TaiLieuDinhKemProvider.GetBySanPhamId(mSanPhamId);

                    foreach (TaiLieuDinhKem objTaiLieu in lstTaiLieuDinhKem)
                    {
                        string FilePath = objTaiLieu.TenFile.Trim();
                        if (objTaiLieu.LoaiTaiLieuId == (int)EnLoaiTaiLieuList.CONG_VAN)
                        {
                            StringBuilder sbCongVan = new StringBuilder();
                            string TrangThai_KetLuan = TrangThaiSp;
                            if (TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                                TrangThai_KetLuan = "Đang xử lý";
                            else if (TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                                TrangThai_KetLuan = "Huỷ";
                            else
                            {
                                if (TrangThaiSp.Contains("Chờ"))
                                {
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_GCN)
                                        TrangThai_KetLuan = TrangThaiSp + " Không cấp GCN";
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CN)
                                        TrangThai_KetLuan = TrangThaiSp + " Không phải CN";
                                    if (KetLuanId == (int)EnKetLuanList.HUY)
                                        TrangThai_KetLuan = TrangThaiSp + " Huỷ";
                                }
                                else
                                {
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_GCN)
                                        TrangThai_KetLuan = "Không cấp GCN, " + TrangThaiSp;
                                    if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CN)
                                        TrangThai_KetLuan = "Không phải CN, " + TrangThaiSp;
                                    if (KetLuanId == (int)EnKetLuanList.HUY)
                                        TrangThai_KetLuan = "Huỷ, " + TrangThaiSp;
                                }
                            }

                            sbCongVan.Append("<a href='../" + FilePath + "' title='Xem công văn trả lời'>" + TrangThai_KetLuan + "</a>");
                            lnkTrangThai.Text = sbCongVan.ToString();
                            CoCongVanTraLoi = true;
                        }

                    }
                }

                if (KetLuanId == (int)EnKetLuanList.CAP_GCN)
                {
                    if (TrangThaiId == (int)EnTrangThaiSanPhamList.DA_CAP_GCN
                        || TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET
                        || TrangThaiId == (int)EnTrangThaiSanPhamList.HUY_GCN)
                    {
                        lnkTrangThai.Text = DataBinder.Eval(e.Row.DataItem, "TrangThai").ToString();
                        if (TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET)
                            lnkTrangThai.Text = "Giám đốc phê duyệt cấp GCN";
                        lnkTrangThai.ToolTip = "Xem Giấy chứng nhận";
                        lnkTrangThai.OnClientClick = "return popCenter('../ReportForm/HienBaoCao.aspx?LoaiBaoCao=GiayChungNhan&SanphamID=" + mSanPhamId + "','Tra_cuu_giay_chung_nhan',800,600);";
                        e.Row.Cells[6].Controls.Add(lnkTrangThai);
                    }
                    else
                    {
                        if (TrangThaiId < 2)
                            e.Row.Cells[6].Text = "Đang xử lý";
                        else
                        {
                            if (TrangThaiSp.Contains("Chờ"))
                                e.Row.Cells[6].Text = "Cấp GCN, " + TrangThaiSp;
                            else
                                e.Row.Cells[6].Text = TrangThaiSp + " Cấp GCN";
                        }
                    }
                }
                else
                {
                    if (CoCongVanTraLoi)
                        e.Row.Cells[6].Controls.Add(lnkTrangThai);
                    else
                    {
                        string TrangThai_KetLuan = TrangThaiSp;
                        if (TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                            TrangThai_KetLuan = "Đang xử lý";
                        else if (TrangThaiId == (int)EnTrangThaiSanPhamList.HUY)
                            TrangThai_KetLuan = "Huỷ";
                        else
                        {
                            if (TrangThaiSp.Contains("Chờ"))
                            {
                                if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_GCN)
                                    TrangThai_KetLuan = TrangThaiSp + " Không cấp GCN";
                                if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CN)
                                    TrangThai_KetLuan = TrangThaiSp + " Không phải CN";
                                if (KetLuanId == (int)EnKetLuanList.HUY)
                                    TrangThai_KetLuan = TrangThaiSp + " Huỷ";
                            }
                            else
                            {
                                if (KetLuanId == (int)EnKetLuanList.KHONG_CAP_GCN)
                                    TrangThai_KetLuan = "Không cấp GCN, " + TrangThaiSp;
                                if (KetLuanId == (int)EnKetLuanList.KHONG_PHAI_CN)
                                    TrangThai_KetLuan = "Không phải CN, " + TrangThaiSp;
                                if (KetLuanId == (int)EnKetLuanList.HUY)
                                    TrangThai_KetLuan = "Huỷ, " + TrangThaiSp;
                            }
                        }

                        e.Row.Cells[6].Text = TrangThai_KetLuan;
                    }
                }
            }
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    public Boolean DeleteFile(string filename)
    {
        try
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void ImgBtnXoa_Click(object sender, ImageClickEventArgs e)
    {
        lnkXoa_Click(null, null);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void ImgBtnGui_Click(object sender, ImageClickEventArgs e)
    {
        lnkGui_Click(null, null);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modifield>
    /// Người tạo                   ngày tạo            chú thích
    /// Nguyễn Trung Tuyến          9/05/2009           Tạo mới
    /// </Modifield>
    protected void gvSanPham_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSanPham.PageIndex = e.NewPageIndex;
        GetSanPhamOFHoSo(HoSoId);
    }
    /// <summary>
    /// Danh sách thông báo nộp tiền của hồ sơ nếu có
    /// </summary>
    /// <Modified>
    /// Author      Date            Comments    
    /// Quannm      03/03/2010      Create new
    ///</Modified>
    private void BindThongBaoPhiGrid()
    {
        DataTable dtThongBaoPhi = new DataTable();
        dtThongBaoPhi = ProviderFactory.ThongBaoLePhiProvider.ThongBaoNopTienTheoHoSo(HoSoId);
        gvPhi.DataSource = dtThongBaoPhi;
        gvPhi.DataBind();

        if (dtThongBaoPhi.Rows.Count > 0)
        {
            IBXoa.Enabled = true;
            LBXoa.Enabled = true;
        }
    }
    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     Quannm      03/03/2010      Create new
    /// </Modified>
    protected void gvPhi_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPhi.PageIndex = e.NewPageIndex;
        BindThongBaoPhiGrid();
    }

    protected void IBThemMoi_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void LBThemMoi_Click(object sender, EventArgs e)
    {

    }
    protected void IBGui_Click(object sender, ImageClickEventArgs e)
    {
        SendThongBaoPhi();
    }
    protected void IBXoa_Click(object sender, ImageClickEventArgs e)
    {
        DeleteThongBaoPhi();
    }
    protected void LBXoa_Click(object sender, EventArgs e)
    {
        DeleteThongBaoPhi();
    }
    private void SendThongBaoPhi()
    {
        CheckBox chk;
        // Thực hiện gui
        foreach (GridViewRow row in gvPhi.Rows)
        {
            chk = (CheckBox)(row.Cells[7].FindControl("chkCheck"));
            if (chk.Checked)
            {
                string thongbaophiId = gvPhi.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvPhi.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvPhi.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        //chuyen trang thai thong bao phi ve cho phe duyet
                        ThongBaoLePhi objTBLP = ProviderFactory.ThongBaoLePhiProvider.GetById(thongbaophiId);
                        objTBLP.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET;
                        ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objTBLP);

                        transaction.Commit();
                        sendSuccess.Add(sothongbaophi);
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    sendFail.Add(sothongbaophi);
                                    break;
                                default:
                                    sendFail.Add(sothongbaophi);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        sendFail.Add(sothongbaophi);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                else
                {
                    sendFail.Add(sothongbaophi);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (sendSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgGuiThongBaoPhiThanhCong, string.Join(",", sendSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CN_THU_LE_PHI, thongbao);
        }
        if (sendFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgGuiThongBaoPhiThatBai, string.Join(",", sendFail.ToArray()));
        }
        //Response.Redirect("CN_HoSoSanPham.aspx?direct=CN_HoSoMoi&HoSoID=" + HoSoId + "&UserControl=CN_HoSoDen&TrangThaiId=" + TrangThaiId, false);
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSoSanPham.aspx?HoSoId=" + HoSoId + "&UserControl=CN_HoSoDen&TrangThaiId=" + TrangThaiId + "&direct=CN_HoSoMoi';</script>");
    }
    private void DeleteThongBaoPhi()
    {
        CheckBox chk;
        // Thực hiện xoá
        foreach (GridViewRow row in gvPhi.Rows)
        {
            chk = (CheckBox)(row.Cells[7].FindControl("chkCheck"));
            if (chk.Checked)
            {
                //ma thong bao phi
                string thongbaophiId = gvPhi.DataKeys[row.RowIndex][0].ToString();
                //kiem tra trang thai cua thong bao phi, chỉ có thể xóa thong bao phi mới
                int trangThaiId = Convert.ToInt32(gvPhi.DataKeys[row.RowIndex][1].ToString());
                //so giay thong bao phi
                string sothongbaophi = gvPhi.DataKeys[row.RowIndex][2].ToString();
                if (trangThaiId == (int)EnTrangThaiThongBaoPhiList.MOI_TAO)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
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
                else
                {
                    deleteFail.Add(sothongbaophi);
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

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSoSanPham.aspx?HoSoId=" + HoSoId + "&UserControl=CN_HoSoDen&TrangThaiId=" + TrangThaiId + "&direct=CN_HoSoMoi';</script>");
    }

    protected void LBGui_Click(object sender, EventArgs e)
    {
        SendThongBaoPhi();
    }

    /// <summary>
    /// Phân quyền chức năng
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPhi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            // Phân quyền theo chuyên viên và thanh tra, người dùng của cục            
            HtmlAnchor lnkSoTBLP = (HtmlAnchor)e.Row.FindControl("lnkThongBaoLePhi");
            if (lnkSoTBLP != null)
            {
                if (mUserInfo.IsThanhTra)
                {
                    // Bỏ chức năng in giấy báo lệ phí
                    lnkSoTBLP.Attributes.Clear();
                }
                else
                {
                    string ID = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                    string trangThaiID = DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString();
                    string hoSoID = string.Empty;
                    if (Request["HoSoID"] != null)
                        hoSoID = Request["HoSoID"].ToString();
                    if (trangThaiID == "1")
                        lnkSoTBLP.Attributes.Add("onclick", "return popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?TrangThaiID=" + trangThaiID + "&action=edit&HoSoID=" + hoSoID + "&ThongBaoLePhiID=" + ID + "','CN_ThongBaoPhi_TaoMoi',800,600);");
                    else
                        lnkSoTBLP.Attributes.Add("onclick", "return popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?TrangThaiID=" + trangThaiID + "&action=view&HoSoID=" + hoSoID + "&ThongBaoLePhiID=" + ID + "','CN_ThongBaoPhi_TaoMoi',800,600);");

                    //<a href="#" onclick="if(<%# Eval("TrangThaiID") %> == 1) return popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=edit&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);

                    //                                     else if (<%# Eval("TrangThaiID") %> != 1) return popCenter('CN_ThongBaoNopTien_TaoMoi.aspx?TrangThaiID=<%# Eval("TrangThaiID") %>&action=view&HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Eval("ID")%>','CN_ThongBaoPhi_TaoMoi',800,600);
                    //                                     else return popCenter('TestBaoCao.aspx?HoSoID=<%# Request["HoSoID"].ToString() %>&ThongBaoLePhiID=<%# Server.UrlEncode(Eval("ID").ToString()) %>','CN_ThuPhi',800,600);" />
                    //                            <%# Eval("SoGiayThongBaoLePhi")%>
                }
            }
        }
    }
}
