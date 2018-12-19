using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Data;
using Cuc_QLCL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Delete_HoSo : System.Web.UI.Page
{
    public string MaHoSo = "";
    public HoSo objHS = null;
    public DmDonVi objDonVi = null;
    public String Message = "DLHS:";
    protected void Page_Load(object sender, EventArgs e)
    {
        MaHoSo = Server.UrlDecode(Request.QueryString["id"]);
        //kiem tra trang thai ho so
        if ((MaHoSo != "") && (MaHoSo != null))
        {
            objHS = ProviderFactory.HoSoProvider.GetById(MaHoSo);
            if (objHS != null && !string.IsNullOrEmpty(objHS.DonViId))
                objDonVi = ProviderFactory.DmDonViProvider.GetById(objHS.DonViId);
        }
    }
    private void DeleteHoSo()
    {
        string m = "DeleteHoSo:";
        bool b = false;
        TransactionManager transaction = ProviderFactory.Transaction;
        try
        {
            m+="delete start";
            int a = ProviderFactory.SanPhamProvider.XoaSanPhamTheoMaHoSo(objHS.Id, transaction);
            m += "Delete:" + a;
            b = ProviderFactory.HoSoProvider.Delete(transaction, objHS.Id);
            m += b ? "delete success" : "delete faile";
            transaction.Commit();
            m += "delete commit";
        }
        catch (SqlException ex)
        {
            m += "delete sql errror" + ex.Message;
            transaction.Rollback();
        }
        catch (Exception ex)
        {
            m += "delete errror" + ex.Message;
            transaction.Rollback();
        }
        finally
        {
            m += "finally";
            transaction.Dispose();
        }
        Message = m;
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + m + "');</script>");
    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        if(objHS != null && !String.IsNullOrEmpty(objHS.Id))
            QLCL_Patch.Delete_HoSo(objHS.Id);
    }
}