using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;

public partial class WebUI_CN_HoSo_Delete_SanPham : PageBase
{
    private UserControls_uc_CN_HoSoSanPham_DeleteSP uc_CN_HoSoSanPham_DeleteSP = null;
    int SoBanGhi = 0;
    int SoBanGhiDaGui = 0;
    string HoSoId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        HoSoId = Request["HoSoId"];
        DemSoBanGhiCuaCacUserControl();
        LoadUserControls();

        if (!IsPostBack)
        {
        }
        else
        {
            if (this.Request["__EVENTTARGET"] == "AddNewCommit")
            {
                string passedArgument = Request.Params.Get("__EVENTARGUMENT");
                Response.Redirect("CN_HoSo_Delete_SanPham.aspx?HoSoId=" + passedArgument);
            }
        }

        string strDriectionPage = "";
        if (tvDanhMucHoSo.SelectedValue != null && tvDanhMucHoSo.SelectedValue != "")
        {
            strDriectionPage = tvDanhMucHoSo.SelectedNode.Value.ToString();
        }
        else
        {
            if (Request["UserControl"] != "" && Request["UserControl"] != null)
                strDriectionPage = Request["UserControl"];
        }
        AddTreeNode();
    }

    private void DemSoBanGhiCuaCacUserControl()
    {
        DataTable dtSanPhamDen = ProviderFactory.SanPhamProvider.LayTatCaSanPhamTheoQuyenDangNhap(HoSoId, mUserInfo.UserID, mUserInfo.GetPermissionList("01"));// ProviderFactory.SanPhamProvider.LayTatCaSanPhamTheoQuyenDangNhap(HoSoId, mUserInfo.UserID, mUserInfo.GetPermissionList("01"));
        SoBanGhi = dtSanPhamDen.Rows.Count;
    }

    private void AddTreeNode()
    {
        tvDanhMucHoSo.Nodes.Clear();
        TreeNode ChungNhanHopQuy = new TreeNode("Chứng Nhận", "");
        TreeNode nHoSoCNDen = new TreeNode("Sản phẩm đến (" + SoBanGhi.ToString() + ")", "CN_HoSoDen");
        ChungNhanHopQuy.ChildNodes.Add(nHoSoCNDen);
        tvDanhMucHoSo.Nodes.Add(ChungNhanHopQuy);
        tvDanhMucHoSo.ExpandAll();
    }
    
    private void LoadUserControls()
    {
        uc_CN_HoSoSanPham_DeleteSP = (UserControls_uc_CN_HoSoSanPham_DeleteSP)this.LoadControl(@"~\UserControls\uc_CN_HoSoSanPham_DeleteSP.ascx");
        MyPlaceHolder.Controls.Add(uc_CN_HoSoSanPham_DeleteSP);
    }
    
    protected void tvDanhMucHoSo_SelectedNodeChanged(object sender, EventArgs e)
    {
        string strPageSelect = tvDanhMucHoSo.SelectedNode.Value.ToString();
        //DirectionPage(strPageSelect);
    }
}