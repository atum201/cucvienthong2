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
using CucQLCL.Common;
using Cuc_QLCL.Data;

public partial class WebUI_NhapLieu_CN_ThongBaoPhi : PageBase
{
    string ID = "";
    string DonViID = "";
    int CachTinhPhi = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        string ThongBaoLePhiID = "";
        string mAction = "";
        if (Request["action"] != null)
            mAction = Request["action"].ToString();
        if (Request["HoSoID"] != null)
        {
            ID = Request["HoSoID"].ToString();
        }

        Session["HoSoId"] = ID;
        // Load thông tin đơn vị
        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(ID);
        if (objHoSo.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopChuan)
            lblTitle.Text = "GIẤY BÁO LỆ PHÍ CHỨNG NHẬN HỢP CHUẨN";
        else
            lblTitle.Text = "GIẤY BÁO LỆ PHÍ CHỨNG NHẬN HỢP QUY";

        DonViID = objHoSo.DonViId;
        Session["DonViID"] = DonViID;
        CachTinhPhi = objHoSo.NguonGocId == null ? 0 : (int)objHoSo.NguonGocId;

        if (!IsPostBack)
        {
            DmDonVi objDonVi = ProviderFactory.DmDonViProvider.GetById(DonViID);
            txtDonVi.Text = objDonVi.TenTiengViet;
            txtDiaChi.Text = objDonVi.DiaChi;
            txtDienThoai.Text = objDonVi.DienThoai;
            txtFax.Text = objDonVi.Fax;

            if (mAction.ToLower() == "add")
            {
                this.Bind_gvPhi(ID);
            }
            if (mAction.ToLower() == "edit")
            {
                if (Request["ThongBaoLePhiID"] != null)
                    ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
                ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
                txtSoTBP.Text = objThongBaoLePhi.SoGiayThongBaoLePhi;
                txtNgayKy.Text = objThongBaoLePhi.NgayPheDuyet.Value.ToShortDateString();
                txtNgayThuTien.Text = objThongBaoLePhi.NgayThuTien != null ? objThongBaoLePhi.NgayThuTien.Value.ToShortDateString() : string.Empty;
                txtSoHoaDon.Text = objThongBaoLePhi.SoHoaDon;

                //txtTongPhi.Text = objThongBaoLePhi.TongPhi.ToString();
                DataTable dtSanPham = ProviderFactory.SanPhamProvider.GetSanPhamDaThamDinh(ID);
                DataTable dtSanPhamCuaTBP = ProviderFactory.ThongBaoLePhiProvider.GetSanPham(ThongBaoLePhiID);
                dtSanPham.Merge(dtSanPhamCuaTBP);
                gvPhi.DataSource = dtSanPham;
                gvPhi.DataBind();
                //txtTongPhi.Text = objThongBaoLePhi.TongPhi.ToString();
            }
        }
    }

    private void Bind_gvPhi(string HoSoID)
    {

        DataTable dtSanPham = ProviderFactory.SanPhamProvider.GetSanPhamDuocCapGCN(HoSoID);
        int intTongPhi = 0;
        foreach (DataRow row in dtSanPham.Rows)
        {
            int intLePhi = 0;
            int.TryParse(row["LePhi"].ToString(), out intLePhi);
            intTongPhi += intLePhi;
        }
        //txtTongPhi.Text = intTongPhi.ToString();
        gvPhi.AutoGenerateColumns = false;
        gvPhi.DataSource = dtSanPham;
        gvPhi.DataBind();
        //Tính toán và hiển thị tổng phí

    }
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        TransactionManager transaction = ProviderFactory.Transaction;
        try
        {
            //Thêm mới 1 thông báo lệ phí và lấy ra ID vừa thêm
            //Với mỗi sản phẩm trên grid thì thêm mới 1 bản ghi thongbaolephisanpham
            ThongBaoLePhi objThongBaoLePhi;
            HoSo objHoso = ProviderFactory.HoSoProvider.GetById(transaction, ID);
            if (Request["ThongBaoLePhiID"] != null)
            {
                objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(transaction, Request["ThongBaoLePhiID"].ToString());

                SinhSoThongBaoLePhi(objThongBaoLePhi, objHoso);

                objThongBaoLePhi.DonViId = Session["DonViID"].ToString();
                objThongBaoLePhi.TongPhi = int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000;
            }
            else
            {
                objThongBaoLePhi = new ThongBaoLePhi();
                SinhSoThongBaoLePhi(objThongBaoLePhi, objHoso);

                objThongBaoLePhi.DonViId = Session["DonViID"].ToString();
                objThongBaoLePhi.TongPhi = int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000;

                objThongBaoLePhi.HoSoId = ID;
            }
            if (objHoso.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopChuan)
                objThongBaoLePhi.LoaiPhiId = (int)EnLoaiPhiList.PHI_CHUNG_NHAN_HC;
            else
                objThongBaoLePhi.LoaiPhiId = (int)EnLoaiPhiList.PHI_CHUNG_NHAN_HQ;

            objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.DA_THU_PHI;
            objThongBaoLePhi.NguoiPheDuyetId = mUserInfo.GiamDoc.Id;
            objThongBaoLePhi.TenNguoiKyDuyet = mUserInfo.GiamDoc.FullName;
            objThongBaoLePhi.ChucVu = (int)EnChucVuList.GDTT;
            objThongBaoLePhi.NgayPheDuyet = Convert.ToDateTime(txtNgayKy.Text);
            objThongBaoLePhi.NgayThuTien = Convert.ToDateTime(txtNgayThuTien.Text);
            objThongBaoLePhi.SoHoaDon = txtSoHoaDon.Text;

            string CurrentState = objThongBaoLePhi.EntityState.ToString();
            ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objThongBaoLePhi);
            objThongBaoLePhi.TrangThaiId = (int)EnTrangThaiThongBaoPhiList.DA_THU_PHI;
            SinhSoThongBaoLePhi(objThongBaoLePhi, objHoso);
            ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objThongBaoLePhi);

            if (Request["ThongBaoLePhiID"] != null)
            {
                DataTable dtSanPham = ProviderFactory.ThongBaoLePhiProvider.GetSanPham(transaction, Request["ThongBaoLePhiID"].ToString());
                foreach (DataRow row in dtSanPham.Rows)
                    ProviderFactory.ThongBaoLePhiSanPhamProvider.DeleteLePhiSanPhamBySanPhamID_Extend(row["SanPhamID"].ToString(), transaction);
            }
            ////lap cac dong cua grid
            int SoLuongSanPham = 0;

            foreach (GridViewRow gvr in gvPhi.Rows)
            {
                HtmlInputCheckBox cb = (HtmlInputCheckBox)gvr.FindControl("chkCheck");
                if (cb.Checked)
                {
                    SoLuongSanPham++;
                    //Save thongbaolephisanpham
                    ThongBaoLePhiSanPham objThongBaoLePhiSanPham = new ThongBaoLePhiSanPham();
                    objThongBaoLePhiSanPham.SanPhamId = gvPhi.DataKeys[gvr.RowIndex][0].ToString();
                    objThongBaoLePhiSanPham.LePhi = int.Parse(gvr.Cells[3].Text.Replace(".", "")) / 1000;
                    objThongBaoLePhiSanPham.ThongBaoLePhiId = objThongBaoLePhi.Id;
                    objThongBaoLePhiSanPham.TenSanPham = Server.HtmlDecode(gvr.Cells[2].Text);
                    objThongBaoLePhiSanPham.CachTinhPhi = CachTinhPhi;
                    ProviderFactory.ThongBaoLePhiSanPhamProvider.Save(transaction, objThongBaoLePhiSanPham);
                }
            }
            // Xóa thống báo lệ phí chi tiết trước khi thêm mới
            ProviderFactory.ThongBaoLePhiChiTietProvider.DeleteByThongBaoLePhiId(objThongBaoLePhi.Id, transaction);

            // Insert thông tin vào bảng thông báo lệ phí chi tiết
            ThongBaoLePhiChiTiet objChiTiet = new ThongBaoLePhiChiTiet();
            objChiTiet.ThongBaoLePhiId = objThongBaoLePhi.Id;
            objChiTiet.LoaiPhiId = objThongBaoLePhi.LoaiPhiId;
            objChiTiet.SoLuong = SoLuongSanPham;
            objChiTiet.MucPhi = 1;
            objChiTiet.TenNhomSanPham = string.Empty;
            ProviderFactory.ThongBaoLePhiChiTietProvider.Save(transaction, objChiTiet);

            //Ghi log
            //if (objThongBaoLePhi.EntityState == EntityState.Added)
            //    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CN_THU_LE_PHI, "Thêm mới thông báo phí số: " + objThongBaoLePhi.SoGiayThongBaoLePhi);
            //if (objThongBaoLePhi.EntityState == EntityState.Changed)
            //    ProviderFactory.SysLogProvier.Write(mUserInfo, SysEventList.CN_THU_LE_PHI, "Cập nhật thông báo phí số: " + objThongBaoLePhi.SoGiayThongBaoLePhi);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                "<script>alert('" + Resource.msgCapNhatThanhCong + "');opener.__doPostBack('AddNewCommit','" + Session["HoSoId"].ToString() + "');self.close() ;</script>");

            txtSoTBP.Text = DataRepository.ThongBaoLePhiProvider.GetById(transaction, objThongBaoLePhi.Id).SoGiayThongBaoLePhi;
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    /// <summary>
    /// Sinh so thong bao le phi
    /// </summary>
    /// <param name="objThongBaoLePhi"></param>
    /// <param name="objHoso"></param>
    private void SinhSoThongBaoLePhi(ThongBaoLePhi objThongBaoLePhi, HoSo objHoso)
    {
        string Stt = txtSoTBP.Text;
        string SoThongBaoLP = txtSoTBP.Text;
        string LoaiPhi = string.Empty;
        if (!SoThongBaoLP.Contains("/"))
        {
            if (Stt.Length == 1)
                SoThongBaoLP = "000" + Stt;
            else if (Stt.Length == 2)
                SoThongBaoLP = "00" + Stt;
            else if (Stt.Length == 3)
                SoThongBaoLP = "0" + Stt;
            else if (Stt.Length >= 4)
                SoThongBaoLP = Stt.Substring(0, 4);

            if (objHoso.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopChuan)
                LoaiPhi = "CPCNHC";
            else if (objHoso.LoaiHoSo == (int)LoaiHoSo.ChungNhanHopQuy)
                LoaiPhi = "LPCNHQ";
            else
                LoaiPhi = "LPCBHQ";
            objThongBaoLePhi.SoGiayThongBaoLePhi = SoThongBaoLP + "/" + LoaiPhi + "-" + mUserInfo.MaTrungTam;
        }
        else
            objThongBaoLePhi.SoGiayThongBaoLePhi = txtSoTBP.Text;
    }

    protected void gvPhi_DataBound(object sender, EventArgs e)
    {

        int TotalChkBx = 0;
        if (gvPhi.HeaderRow != null)
        {
            //lay tham chieu den checkbox header
            HtmlInputCheckBox cbHeader = (HtmlInputCheckBox)gvPhi.HeaderRow.FindControl("chkCheckAll");
            cbHeader.Attributes["onclick"] = "HeaderClick('gvPhi');TinhTongPhi()";

            //Bat su kien onclick cua check toan bo
            //cbHeader.Attributes["onclick"] = string.Format("HeaderClick(this,'{0}');CalculateFeeTotal(this,'{1}',{2});", gvPhi.ClientID, txtTongPhi.ClientID, txtTongPhi.Text);
            DataTable dtSanPhamCuaTBP = new DataTable();
            if (Request["ThongBaoLePhiID"] != null)
            {
                dtSanPhamCuaTBP = ProviderFactory.ThongBaoLePhiProvider.GetSanPham(Request["ThongBaoLePhiID"].ToString());
            }
            //lap cac dong cua grid
            foreach (GridViewRow gvr in gvPhi.Rows)
            {
                //string strCalculate = string.Format("CalculateFee(this,'" + txtTongPhi.ClientID + "',{0});",gvr.Cells[3].Text);
                HtmlInputCheckBox cb = (HtmlInputCheckBox)gvr.FindControl("chkCheck");
                //cb.Attributes["onclick"] = string.Format("ChildClick(this,'{0}');" + strCalculate, cbHeader.ClientID);
                //cb.Attributes["onChange"] = strCalculate;
                //dem so luong checkbox con
                //Kiem tra SanPhamID,neu trung voi  
                cb.Value = gvr.Cells[3].Text;
                if (dtSanPhamCuaTBP == null || dtSanPhamCuaTBP.Rows.Count < 1)
                    cb.Checked = true;
                else
                {
                    string sanphamid = gvPhi.DataKeys[gvr.RowIndex][0].ToString();
                    cb.Checked = CheckExist(sanphamid, dtSanPhamCuaTBP);
                }
                cb.Attributes["onclick"] = "ChildClick('gvPhi');TinhTongPhi()";
                TotalChkBx++;
            }
            //cbHeader.Checked = true;
        }
        //gan lai gia tri cua bien

        //this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TotalChkBx", string.Format("TotalChkBx={0};Counter={1};", TotalChkBx, TotalChkBx), true);
    }
    public bool CheckExist(string ID, DataTable dt)
    {
        foreach (DataRow row in dt.Rows)
        {
            if (ID.Equals(row["SanPhamID"].ToString()))
            {
                return true;
            }
        }
        return false;
    }
    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                "<script>self.close();</script>");
    }
}

