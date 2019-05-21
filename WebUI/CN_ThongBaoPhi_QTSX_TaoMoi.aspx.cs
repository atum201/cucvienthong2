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
using System.Collections.Generic;

public partial class WebUI_CN_ThongBaoPhi_QTSX_TaoMoi : PageBase
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
            if (mAction.ToLower() == "add")
            {
                if (objHoSo.HoSoMoi == false)
                {
                    txtSoTBP.Text = string.Empty;
                    txtSoTBP.Attributes.Add("style", "background-color:#FFFFFF");
                    txtSoTBP.ReadOnly = false;
                }
                else
                {
                    txtSoTBP.Text = "Số sinh tự động";
                }
            }
            if (mAction.ToLower() == "edit")
            {
                if (Request["ThongBaoLePhiID"] != null)
                {
                    ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
                }
                ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
                txtSoTBP.Text = objThongBaoLePhi.SoGiayThongBaoLePhi;


                lstObjectThongBaoLePhiChiTiet = ProviderFactory.ThongBaoLePhiChiTietProvider.GetByThongBaoLePhiId(objThongBaoLePhi.Id);
            }

            if (mAction.ToLower() == "view")
            {
                if (Request["ThongBaoLePhiID"] != null)
                {
                    ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
                }
                ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
                txtSoTBP.Text = objThongBaoLePhi.SoGiayThongBaoLePhi;
                btnCapNhat.Visible = false;

                lstObjectThongBaoLePhiChiTiet = ProviderFactory.ThongBaoLePhiChiTietProvider.GetByThongBaoLePhiId(objThongBaoLePhi.Id);
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
            txtPhiLayMau.Text = ((int)QLCL_Patch.LePhi.PhiLayMauSanPham*1000).ToString();

            txtPhiDanhGiaQuyTrinh.Text = ((int)QLCL_Patch.LePhi.PhiDanhGiaQuaTrinhSanXuat*1000).ToString();

            txtTongPhi.Text = (Convert.ToInt32(txtPhiLayMau.Text) + Convert.ToInt32(txtPhiDanhGiaQuyTrinh.Text)).ToString();
        }
        if (mAction.ToLower() == "edit")
        {
            txtPhiLayMau.Text = ((int)QLCL_Patch.LePhi.PhiLayMauSanPham * 1000).ToString();
            txtPhiDanhGiaQuyTrinh.Text = ((int)QLCL_Patch.LePhi.PhiDanhGiaQuaTrinhSanXuat * 1000).ToString();

            foreach (ThongBaoLePhiChiTiet objObj in lstObjectThongBaoLePhiChiTiet)
            {
                if (objObj.LoaiPhiId == (int)QLCL_Patch.LoaiPhi.DanhGiaQTSX)
                {
                    txtSoQuyTrinh.Text = objObj.SoLuong.ToString();
                }
                else if (objObj.LoaiPhiId == (int)QLCL_Patch.LoaiPhi.LayMauQTSX)
                {
                    txtSoLayMau.Text = objObj.SoLuong.ToString();
                }
                else
                {
                    
                }
            }

            txtTongPhi.Text = ((Convert.ToInt32(txtPhiLayMau.Text) * Convert.ToInt32(txtSoLayMau.Text)) + (Convert.ToInt32(txtPhiDanhGiaQuyTrinh.Text) * Convert.ToInt32(txtSoQuyTrinh.Text))).ToString();
        }
        if (mAction.ToLower() == "view")
        {
            txtPhiLayMau.Text = ((int)QLCL_Patch.LePhi.PhiLayMauSanPham * 1000).ToString();
            txtPhiDanhGiaQuyTrinh.Text = ((int)QLCL_Patch.LePhi.PhiDanhGiaQuaTrinhSanXuat * 1000).ToString();

            foreach (ThongBaoLePhiChiTiet objObj in lstObjectThongBaoLePhiChiTiet)
            {
                if (objObj.LoaiPhiId == (int)QLCL_Patch.LoaiPhi.DanhGiaQTSX)
                {
                    txtSoQuyTrinh.Text = "1";
                }
                else
                {
                    txtSoQuyTrinh.Text = objObj.SoLuong.ToString();
                }
                if (objObj.LoaiPhiId == (int)QLCL_Patch.LoaiPhi.LayMauQTSX)
                {
                    txtSoLayMau.Text = "1";
                }
                else
                {
                    txtSoLayMau.Text = objObj.SoLuong.ToString();
                }
            }

            txtTongPhi.Text = ((Convert.ToInt32(txtPhiLayMau.Text) * Convert.ToInt32(txtSoLayMau.Text)) + (Convert.ToInt32(txtPhiDanhGiaQuyTrinh.Text) * Convert.ToInt32(txtSoQuyTrinh.Text))).ToString();
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
            objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(Request["ThongBaoLePhiID"].ToString());
            SinhSoThongBaoNopTien(objThongBaoLePhi);
            objThongBaoLePhi.DonViId = Session["DonViID"].ToString();
            if (!string.IsNullOrEmpty(hdnTongPhi.Value))
            {
                objThongBaoLePhi.TongPhi = int.Parse(hdnTongPhi.Value.Replace(".", "")) / 1000;
            }
            else
            {
                objThongBaoLePhi.TongPhi = int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000;
            }
            ThongBao = "Cập nhật thành công";
        }
        else
        {
            //tao moi thong bao le phi
            objThongBaoLePhi = new ThongBaoLePhi();
            SinhSoThongBaoNopTien(objThongBaoLePhi);
            objThongBaoLePhi.DonViId = Session["DonViID"].ToString();

            if (!string.IsNullOrEmpty(hdnTongPhi.Value))
            {
                objThongBaoLePhi.TongPhi = int.Parse(hdnTongPhi.Value.Replace(".", "")) / 1000;
            }
            else
            {
                objThongBaoLePhi.TongPhi = int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000;
            }
            objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.MOI_TAO;
            objThongBaoLePhi.HoSoId = ID;
            objThongBaoLePhi.LoaiPhiId = (int)QLCL_Patch.LoaiPhi.DanhGiaQTSX;
            ThongBao = "Thêm mới thành công";

        }

        TransactionManager transaction = ProviderFactory.Transaction;
        try
        {
            string CurrentState = objThongBaoLePhi.EntityState.ToString();
            ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objThongBaoLePhi);

            // Xóa chi tiết thông báo lệ phí cũ
            ProviderFactory.ThongBaoLePhiChiTietProvider.DeleteByThongBaoLePhiId(objThongBaoLePhi.Id, transaction);

            //tao moi thong bao le phi chi tiet
            ThongBaoLePhiChiTiet objThongBaoLePhiChiTiet = new ThongBaoLePhiChiTiet();
            objThongBaoLePhiChiTiet.LoaiPhiId = (int)QLCL_Patch.LoaiPhi.LayMauQTSX;
            objThongBaoLePhiChiTiet.MucPhi = Convert.ToInt32(txtPhiLayMau.Text.Trim());
            objThongBaoLePhiChiTiet.SoLuong = Convert.ToInt32(txtSoLayMau.Text.Trim());
            objThongBaoLePhiChiTiet.ThongBaoLePhiId = objThongBaoLePhi.Id;
            lstObjThongBaoLePhiChiTiet.Add(objThongBaoLePhiChiTiet);

            ThongBaoLePhiChiTiet objThongBaoLePhiChiTiet2 = new ThongBaoLePhiChiTiet();
            objThongBaoLePhiChiTiet2.LoaiPhiId = (int)QLCL_Patch.LoaiPhi.DanhGiaQTSX;
            objThongBaoLePhiChiTiet2.MucPhi = Convert.ToInt32(txtPhiDanhGiaQuyTrinh.Text);
            objThongBaoLePhiChiTiet2.SoLuong = Convert.ToInt32(txtSoQuyTrinh.Text.Trim());
            objThongBaoLePhiChiTiet2.ThongBaoLePhiId = objThongBaoLePhi.Id;
            lstObjThongBaoLePhiChiTiet.Add(objThongBaoLePhiChiTiet2);

            foreach (ThongBaoLePhiChiTiet objObject in lstObjThongBaoLePhiChiTiet)
            {
                ProviderFactory.ThongBaoLePhiChiTietProvider.Save(transaction, objObject);
            }

            ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
               "<script>alert('" + Resource.msgCapNhatThanhCong + "');opener.__doPostBack('AddNewCommit','" + Session["HoSoId"].ToString() + "');self.close() ;</script>");

            //ClientScript.RegisterClientScriptBlock(typeof(Page), "Thông báo",
            //           "<script>alert('" + ThongBao + "'); window.opener.location.href='CN_HoSoSanPham.aspx?HoSoId=" + Session["HoSoId"].ToString() + "&TrangThaiId=1';window.close();</script>");
            txtSoTBP.Text = DataRepository.ThongBaoLePhiProvider.GetById(transaction, objThongBaoLePhi.Id).SoGiayThongBaoLePhi;
            transaction.Commit();
            transaction.Dispose();
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

            objThongBaoLePhi.SoGiayThongBaoLePhi = SoThongBaoLP + "/PĐGQT-" + mUserInfo.MaTrungTam;
        }
        else
            objThongBaoLePhi.SoGiayThongBaoLePhi = txtSoTBP.Text;
    }
}