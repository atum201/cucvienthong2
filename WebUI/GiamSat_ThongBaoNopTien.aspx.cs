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
using Cuc_QLCL.Data;
using Resources;
using System.Collections.Generic;
using Cuc_QLCL.AdapterData;
using CucQLCL.Common;

public partial class WebUI_GiamSat_ThongBaoNopTien : PageBase
{
    string ID = "";
    string DonViID = "";
    int CachTinhPhi = 1;
    string Direct = string.Empty;
    TList<ThongBaoLePhiChiTiet> lstObjectThongBaoLePhiChiTiet = new TList<ThongBaoLePhiChiTiet>();

    string ThongBaoLePhiID = "";
    string mAction = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["action"] != null)
            mAction = Request["action"].ToString();
        if (Request["HoSoID"] != null)
        {
            ID = Request["HoSoID"].ToString();
        }

        Session["HoSoId"] = ID;
        // Load thông tin đơn vị
        HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(ID);
        DonViID = objHoSo.DonViId;
        Session["DonViID"] = DonViID;

        ////neu la loai ho so la chung nhan hop chuan thi xuat hien row nop tien danh gia quy trinh
        //if (objHoSo.NguonGocId == (int)EnNguonGocList.SX_TRONG_NUOC_KHONG_CO_ISO)
        //{
        //    soObjectThongBaoLePhiChiThiet = 2;
        //    trPhiDanhGia.Visible = true;
        //    trSoQuyTrinh.Visible = true;
        //}

        if (!IsPostBack)
        {

            DmDonVi objDonVi = ProviderFactory.DmDonViProvider.GetById(DonViID);
            txtDonVi.Text = objDonVi.TenTiengViet;
            txtDiaChi.Text = objDonVi.DiaChi;
            txtDienThoai.Text = objDonVi.DienThoai;
            txtFax.Text = objDonVi.Fax;
            if (mAction.ToLower() == "edit")
            {
                if (Request["ThongBaoLePhiID"] != null)
                {
                    ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
                }
                ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
                txtSoTBP.Text = objThongBaoLePhi.SoGiayThongBaoLePhi;
                txtNgayKy.Text = objThongBaoLePhi.NgayPheDuyet.Value.ToShortDateString();
                txtNgayThuTien.Text = objThongBaoLePhi.NgayThuTien != null ? objThongBaoLePhi.NgayThuTien.Value.ToShortDateString() : string.Empty;
                txtSoHoaDon.Text = objThongBaoLePhi.SoHoaDon;

                lstObjectThongBaoLePhiChiTiet = ProviderFactory.ThongBaoLePhiChiTietProvider.GetByThongBaoLePhiId(objThongBaoLePhi.Id);
                DataTable dtSanPham = ProviderFactory.SanPhamProvider.GetSanPhamDaThamDinh(ID);
                DataTable dtSanPhamCuaTBP = ProviderFactory.ThongBaoLePhiProvider.GetSanPhamGS(ThongBaoLePhiID);

                dtSanPham.Merge(dtSanPhamCuaTBP);
                gvPhi.DataSource = dtSanPham;
                gvPhi.DataBind();
            }
            LayThongTinCacMucPhiNopTien();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private void LayThongTinCacMucPhiNopTien()
    {
        if (mAction.ToLower() == "add")
        {
            txtPhiLayMau.Text = EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM_DGL);
            txtPhiDanhGiaQuyTrinh.Text = EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_DANH_GIA_LAI_QUY_TRINH_SX);
            this.Bind_gvPhi(ID);
            //txtTongPhi.Text = (Convert.ToInt32(txtPhiLayMau.Text) + Convert.ToInt32(txtPhiDanhGiaQuyTrinh.Text)).ToString();
        }
        if (mAction.ToLower() == "edit")
        {
            txtPhiLayMau.Text = EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM_DGL);
            txtPhiDanhGiaQuyTrinh.Text = EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_DANH_GIA_LAI_QUY_TRINH_SX);

            foreach (ThongBaoLePhiChiTiet objObj in lstObjectThongBaoLePhiChiTiet)
            {
                if (objObj.LoaiPhiId == (int)EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM_DGL)
                {
                    txtSoQuyTrinh.Text = "1";
                }
                else
                {
                    txtSoQuyTrinh.Text = objObj.SoLuong.ToString();
                }
            }

            // txtTongPhi.Text = (Convert.ToInt32(txtPhiLayMau.Text) + (Convert.ToInt32(txtPhiDanhGiaQuyTrinh.Text) * Convert.ToInt32(txtSoQuyTrinh.Text))).ToString();
        }
        if (mAction.ToLower() == "view")
        {
            txtPhiLayMau.Text = EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM_DGL);
            txtPhiDanhGiaQuyTrinh.Text = EntityHelper.GetEnumTextValue(EnLoaiPhiList.PHI_DANH_GIA_LAI_QUY_TRINH_SX);

            foreach (ThongBaoLePhiChiTiet objObj in lstObjectThongBaoLePhiChiTiet)
            {
                if (objObj.LoaiPhiId == (int)EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM_DGL)
                {
                    txtSoQuyTrinh.Text = "1";
                }
                else
                {
                    txtSoQuyTrinh.Text = objObj.SoLuong.ToString();
                }
            }

            // txtTongPhi.Text = (Convert.ToInt32(txtPhiLayMau.Text) + (Convert.ToInt32(txtPhiDanhGiaQuyTrinh.Text) * Convert.ToInt32(txtSoQuyTrinh.Text))).ToString();
            txtSoQuyTrinh.Enabled = false;
            btnCapNhat.Enabled = false;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        //Thêm mới 1 thông báo lệ phí và lấy ra ID vừa thêm
        ThongBaoLePhi objThongBaoLePhi;
        string ThongBao = string.Empty;
        List<ThongBaoLePhiChiTiet> lstObjThongBaoLePhiChiTiet = new List<ThongBaoLePhiChiTiet>();
        if (Request["ThongBaoLePhiID"] != null)
        {
            //edit
            //  string tongphi = txtTongPhi.Text.Trim().Replace(".", string.Empty);
            objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(Request["ThongBaoLePhiID"].ToString());
            SinhSoThongBaoNopTien(objThongBaoLePhi);
            objThongBaoLePhi.DonViId = Session["DonViID"].ToString();
            ThongBao = "Cập nhật thành công";
        }
        else
        {
            //tao moi thong bao le phi
            objThongBaoLePhi = new ThongBaoLePhi();
            SinhSoThongBaoNopTien(objThongBaoLePhi);
            objThongBaoLePhi.DonViId = Session["DonViID"].ToString();
            objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.MOI_TAO;
            objThongBaoLePhi.HoSoId = ID;
            ThongBao = "Thêm mới thành công";

        }
        if (hdnTongPhi.Value != null)
        {
            objThongBaoLePhi.TongPhi = int.Parse(hdnTongPhi.Value) / 1000;
        }
        else
        {
            objThongBaoLePhi.TongPhi = int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000;
        }
        objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.DA_THU_PHI;
        objThongBaoLePhi.NguoiPheDuyetId = mUserInfo.GiamDoc.Id;
        objThongBaoLePhi.TenNguoiKyDuyet = mUserInfo.GiamDoc.FullName;
        objThongBaoLePhi.ChucVu = (int)EnChucVuList.GDTT;
        objThongBaoLePhi.NgayPheDuyet = Convert.ToDateTime(txtNgayKy.Text);
        if (!string.IsNullOrEmpty(txtNgayThuTien.Text))
            objThongBaoLePhi.NgayThuTien = Convert.ToDateTime(txtNgayThuTien.Text);

        objThongBaoLePhi.SoHoaDon = txtSoHoaDon.Text;
        objThongBaoLePhi.LoaiPhiId = (int)EnLoaiPhiList.PHI_DANH_GIA_LAI;
        TransactionManager transaction = ProviderFactory.Transaction;
        try
        {
            string CurrentState = objThongBaoLePhi.EntityState.ToString();
            ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objThongBaoLePhi);
            if (Request["ThongBaoLePhiID"] != null)
            {
                ProviderFactory.ThongBaoLePhiSanPhamProvider.XoaThongBaoLePhiSanPhamTheoThongBaoLePhiID(Request["ThongBaoLePhiID"].ToString(), transaction);
            }
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
            //tao moi thong bao le phi chi tiet
            if (Request["ThongBaoLePhiID"] != null)
            {

                TList<ThongBaoLePhiChiTiet> objThongBaoLePhiChiTiet = ProviderFactory.ThongBaoLePhiChiTietProvider.GetByThongBaoLePhiId(Request["ThongBaoLePhiID"].ToString());
                foreach (ThongBaoLePhiChiTiet objThongBao in objThongBaoLePhiChiTiet)
                {
                    if (objThongBao.LoaiPhiId.Value == (int)EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM_DGL)
                    {
                        objThongBao.MucPhi = Convert.ToInt32(txtPhiLayMau.Text);
                        objThongBao.SoLuong = SoLuongSanPham;
                        //objThongBao.ThongBaoLePhiId = objThongBaoLePhi.Id;
                    }
                    else if (objThongBao.LoaiPhiId == (int)EnLoaiPhiList.PHI_DANH_GIA_LAI_QUY_TRINH_SX)
                    {
                        objThongBao.MucPhi = Convert.ToInt32(txtPhiDanhGiaQuyTrinh.Text);
                        objThongBao.SoLuong = Convert.ToInt32(txtSoQuyTrinh.Text);
                        // objThongBao.ThongBaoLePhiId = objThongBaoLePhi.Id;
                    }
                    ProviderFactory.ThongBaoLePhiChiTietProvider.Save(transaction, objThongBao);
                }
            }
            else
            {
                ThongBaoLePhiChiTiet objThongBaoLePhiChiTiet = new ThongBaoLePhiChiTiet();
                objThongBaoLePhiChiTiet.LoaiPhiId = (int?)EnLoaiPhiList.PHI_LAY_MAU_SAN_PHAM_DGL;
                objThongBaoLePhiChiTiet.MucPhi = Convert.ToInt32(txtPhiLayMau.Text);
                objThongBaoLePhiChiTiet.SoLuong = SoLuongSanPham;
                objThongBaoLePhiChiTiet.ThongBaoLePhiId = objThongBaoLePhi.Id;
                lstObjThongBaoLePhiChiTiet.Add(objThongBaoLePhiChiTiet);

                ThongBaoLePhiChiTiet objThongBaoLePhiChiTiet2 = new ThongBaoLePhiChiTiet();
                objThongBaoLePhiChiTiet2.LoaiPhiId = (int?)EnLoaiPhiList.PHI_DANH_GIA_LAI_QUY_TRINH_SX;
                objThongBaoLePhiChiTiet2.MucPhi = Convert.ToInt32(txtPhiDanhGiaQuyTrinh.Text);
                objThongBaoLePhiChiTiet2.SoLuong = Convert.ToInt32(txtSoQuyTrinh.Text);
                objThongBaoLePhiChiTiet2.ThongBaoLePhiId = objThongBaoLePhi.Id;
                lstObjThongBaoLePhiChiTiet.Add(objThongBaoLePhiChiTiet2);

                foreach (ThongBaoLePhiChiTiet objObject in lstObjThongBaoLePhiChiTiet)
                {
                    ProviderFactory.ThongBaoLePhiChiTietProvider.Save(transaction, objObject);
                }


            }
            transaction.Commit();
            transaction.Dispose();

            ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
               "<script>alert('" + Resource.msgCapNhatThanhCong + "');opener.__doPostBack('AddNewCommit','" + Session["HoSoId"].ToString() + "');self.close() ;</script>");

            //ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
            //           "<script>alert('" + ThongBao + "'); window.opener.location.href='CN_HoSoSanPham.aspx?HoSoId=" + Session["HoSoId"].ToString() + "&TrangThaiId=1';window.close();</script>");
            txtSoTBP.Text = DataRepository.ThongBaoLePhiProvider.GetById(objThongBaoLePhi.Id).SoGiayThongBaoLePhi;

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            if (ex.ToString().Contains("Cannot insert duplicate key row in object"))
            {
                Thong_bao("Số thông báo nộp tiền vừa nhập đã tồn tại, đề nghị nhập số khác!");
            }
            else
                throw ex;
            throw ex;
        }
    }

    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                "<script>self.close();</script>");
    }

    /// <summary>
    /// Sinh so thong bao nop tien
    /// </summary>
    /// <param name="objThongBaoLePhi"></param>
    /// <param name="objHoso"></param>
    private void SinhSoThongBaoNopTien(ThongBaoLePhi objThongBaoLePhi)
    {
        string Stt = txtSoTBP.Text;
        string SoThongBaoLP = txtSoTBP.Text;
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

            objThongBaoLePhi.SoGiayThongBaoLePhi = SoThongBaoLP + "/CPGSHC-" + mUserInfo.MaTrungTam;
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
    private void Bind_gvPhi(string HoSoID)
    {

        DataTable dtSanPham = ProviderFactory.SanPhamProvider.GetSanPhamGSDuocCapGCN(HoSoID);
        if (dtSanPham.Rows.Count <= 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "thongbao", "<script>alert('Không có sản phẩm nào.');self.close();</script>");
            return;
        }
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
}
