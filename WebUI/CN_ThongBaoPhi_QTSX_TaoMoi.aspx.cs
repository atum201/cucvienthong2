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

public partial class WebUI_CN_ThongBaoPhi_QTSX_TaoMoi : PageBase
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
        
        lblTitle.Text = "GIẤY BÁO LỆ PHÍ ĐÁNH GIÁ QUÁ TRÌNH SẢN XUẤT";
        

        DonViID = objHoSo.DonViId;
        Session["DonViID"] = DonViID;
        CachTinhPhi = objHoSo.NguonGocId == null ? 0 : (int)objHoSo.NguonGocId;

        if (!IsPostBack)
        {

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
                    ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
                ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
                txtSoTBP.Text = objThongBaoLePhi.SoGiayThongBaoLePhi;
                txtTongPhi.Text = objThongBaoLePhi.TongPhi.ToString();
            }
            if (mAction.ToLower() == "view")
            {
                if (Request["ThongBaoLePhiID"] != null)
                    ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
                ThongBaoLePhi objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(ThongBaoLePhiID);
                txtSoTBP.Text = objThongBaoLePhi.SoGiayThongBaoLePhi;
                txtTongPhi.Text = objThongBaoLePhi.TongPhi.ToString();
                txtSoTBP.ReadOnly = true;
                btnCapNhat.Visible = false;
            }
        }
    }

    protected void btnCapNhat_Click(object sender, EventArgs e)
    {
        ThongBaoLePhi objThongBaoLePhi;
        HoSo objHoso = ProviderFactory.HoSoProvider.GetById(ID);
        if (Request["ThongBaoLePhiID"] != null)
        {
            objThongBaoLePhi = ProviderFactory.ThongBaoLePhiProvider.GetById(Request["ThongBaoLePhiID"].ToString());
            objThongBaoLePhi.DonViId = Session["DonViID"].ToString();
            objThongBaoLePhi.TongPhi = int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000;
        }
        else
        {
            objThongBaoLePhi = new ThongBaoLePhi();
            objThongBaoLePhi.DonViId = objHoso.DonViId;
            objThongBaoLePhi.TongPhi = int.Parse(txtTongPhi.Text.Trim().Replace(".", "")) / 1000;
            objThongBaoLePhi.TrangThaiId = (int?)EnTrangThaiThongBaoPhiList.MOI_TAO;
            objThongBaoLePhi.HoSoId = objHoso.Id;
            objThongBaoLePhi.LoaiPhiId = 10;
            //objThongBaoLePhi.NguoiPheDuyetId = ddlNguoiKy.SelectedValue;
            //objThongBaoLePhi.TenNguoiKyDuyet = ddlNguoiKy.SelectedItem.Text;
            //objThongBaoLePhi.SoGiayThongBaoLePhi = sogiaycn;
        }
        objThongBaoLePhi.LoaiPhiId = 10;

        TransactionManager transaction = ProviderFactory.Transaction;
        try
        {
            string CurrentState = objThongBaoLePhi.EntityState.ToString();
            ProviderFactory.ThongBaoLePhiProvider.Save(transaction, objThongBaoLePhi);
            transaction.Commit();
            transaction.Dispose();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }
    }

    protected void btnBoQua_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "closePopUp",
                "<script>self.close();</script>");
    }
}