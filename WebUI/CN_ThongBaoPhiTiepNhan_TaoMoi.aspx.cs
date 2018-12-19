using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Data;
using Cuc_QLCL.Entities;
using CucQLCL.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_CN_ThongBaoPhiTiepNhan_TaoMoi : System.Web.UI.Page
{
    public bool edit = false;
    public string ThongBaoLePhiID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
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
        
        hfDonGiaTiepNhan.Value = ((int)QLCL_Patch.LePhi.DonGiaTiepNhan).ToString();
        hfDonGiaXemXet.Value = ((int)QLCL_Patch.LePhi.PhiXemXet).ToString();
        if (!IsPostBack)
        {
            DataTable dsnguoiky = QLCL_Patch.GetDSNguoiKyGiayBaoPhi();
            if (dsnguoiky.Rows.Count > 0)
            {
                for (int i = 0; i < dsnguoiky.Rows.Count; i++)
                {
                    ddlNguoiKy.Items.Add(new ListItem(dsnguoiky.Rows[i]["FullName"].ToString(), dsnguoiky.Rows[i]["ID"].ToString()));
                    ddlThamQuyen.Items.Add(new ListItem(QLCL_Patch.GetChucVuKy(dsnguoiky.Rows[i]["Position"].ToString()), dsnguoiky.Rows[i]["ID"].ToString()));
                }
            }
            DataTable TTGiayBaoPhi = QLCL_Patch.GetTTGiayBaoPhi(ID);
            if (TTGiayBaoPhi.Rows.Count > 0)
            {
                ddlNguoiKy.SelectedIndex = ddlNguoiKy.Items.IndexOf(ddlNguoiKy.Items.FindByValue(TTGiayBaoPhi.Rows[0]["NguoiKy"].ToString()));
                ddlThamQuyen.SelectedIndex = ddlThamQuyen.Items.IndexOf(ddlThamQuyen.Items.FindByValue(TTGiayBaoPhi.Rows[0]["NguoiKy"].ToString()));
                txtSLTiepNhan.Text = TTGiayBaoPhi.Rows[0]["SLTiepNhan"].ToString();
            }
            if (mAction.ToLower() == "edit")// Load lại thông báo
            {
                DataRow rtb = QLCL_Patch.GetTBPhiTiepNhan(objHoSo.Id);
                if (rtb != null)
                {
                    ThongBaoLePhiID = rtb["ID"].ToString();
                    ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
                    txtTongPhi.Text = objThongBaoLePhi.TongPhi.ToString();
                    edit = true;
                    lblTitle.Text = "Update";
                }
                
            }
            
            UserInfo _mUserInfo = Session["User"] as UserInfo;
            if (_mUserInfo == null || !_mUserInfo.IsAuthenticated)
            {
                Response.Redirect("~/WebUI/HT_DangNhap.aspx", true);
            }
            DataTable dtbSanPham = ProviderFactory.SanPhamProvider.LayTatCaSanPhamTheoQuyenDangNhap(objHoSo.Id, _mUserInfo.UserID, _mUserInfo.GetPermissionList("01"));
            txtSLTiepNhan.Text = dtbSanPham.Rows.Count.ToString();
        }
    }

    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        
        HoSo objHoso = ProviderFactory.HoSoProvider.GetById(ID);
        if (edit)
        {
            //objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
            //objThongBaoLePhi.TongPhi = int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000;
            //objThongBaoLePhi.DonViId = objHoso.DonViId;
            //objThongBaoLePhi.LoaiPhiId = 9;
            //objThongBaoLePhi.NguoiPheDuyetId = ddlNguoiKy.SelectedValue;
            //objThongBaoLePhi.TenNguoiKyDuyet = ddlNguoiKy.SelectedItem.Text;
            //objThongBaoLePhi.SoGiayThongBaoLePhi = objHoso.SoHoSo.Replace("CNHQ", "PNHS");
            QLCL_Patch.UpdateThongBaoPhiTiepNhan(objHoso.Id, ddlNguoiKy.SelectedValue, int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000, ddlNguoiKy.SelectedItem.Text);
            int sl = 1, xx = 1;
            try
            {
                sl = int.Parse(txtSLTiepNhan.Text);
            }
            catch { }
            QLCL_Patch.SetNguoiKy_GiayBaoPhi(objHoso.Id, ddlNguoiKy.SelectedValue, sl, xx);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                "<script>alert('Đã cập nhật thông báo lệ phí tiếp nhận.');opener.__doPostBack('AddNewCommit','" + Session["HoSoId"].ToString() + "');self.close() ;</script>");
        }
        else
        {
            ThongBaoLePhi objThongBaoLePhi = new ThongBaoLePhi();
            objThongBaoLePhi.DonViId = objHoso.DonViId;
            objThongBaoLePhi.TongPhi = int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000;
            objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.MOI_TAO;
            objThongBaoLePhi.HoSoId = objHoso.Id;
            objThongBaoLePhi.LoaiPhiId = 9;
            objThongBaoLePhi.NguoiPheDuyetId = ddlNguoiKy.SelectedValue;
            objThongBaoLePhi.TenNguoiKyDuyet = ddlNguoiKy.SelectedItem.Text;
            objThongBaoLePhi.SoGiayThongBaoLePhi = objHoso.SoHoSo.Replace("CNHQ", "PNHS");
            TransactionManager transaction = ProviderFactory.Transaction;
            try
            {
                ProviderFactory.ThongBaoLePhiProvider.Insert(transaction, objThongBaoLePhi);

                int sl = 1, xx = 1;
                try
                {
                    sl = int.Parse(txtSLTiepNhan.Text);
                }
                catch { }
                QLCL_Patch.SetNguoiKy_GiayBaoPhi(objHoso.Id, ddlNguoiKy.SelectedValue, sl, xx);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                    "<script>alert('Đã tạo thông báo lệ phí tiếp nhận.');opener.__doPostBack('AddNewCommit','" + Session["HoSoId"].ToString() + "');self.close() ;</script>");
                transaction.Commit();
                transaction.Dispose();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        
        
        
    }

    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                "<script>self.close();</script>");
    }
}