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

public partial class WebUI_CN_HoSoSanPham_QuanLy : PageBase
{
    private UserControls_uc_CN_HoSoSanPhamMoiNhan uc_CN_HoSoSanPhamMoiNhan = null;
    private UserControls_uc_CN_HoSoSanPhamDaGui uc_CN_HoSoSanPhamDaGui = null;
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
                Response.Redirect("CN_HoSoSanPham_QuanLy.aspx?HoSoId=" + passedArgument);
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
        DirectionPage(strDriectionPage);
        AddTreeNode();
    }
    /// <summary>
    /// Đếm số bản ghi của các usercontrol và add vào treeview
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 14/5/2009              
    /// </Modified>
    private void DemSoBanGhiCuaCacUserControl()
    {
        DataTable dtSanPhamDen = ProviderFactory.SanPhamProvider.LayTatCaSanPhamTheoQuyenDangNhap(HoSoId, mUserInfo.UserID, mUserInfo.GetPermissionList("01"));
        SoBanGhi = dtSanPhamDen.Rows.Count;
        DataTable dtSanPhamDi = ProviderFactory.SanPhamProvider.LayTatCaSanPhamDaGuiTheoQuyenDangNhap(HoSoId, mUserInfo.UserID, mUserInfo.GetPermissionList("01"));
        SoBanGhiDaGui = dtSanPhamDi.Rows.Count;
    }
    /// <summary>
    /// Add tree nodes
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 14/5/2009              
    /// </Modified>
    private void AddTreeNode()
    {
        tvDanhMucHoSo.Nodes.Clear();
        TreeNode ChungNhanHopQuy = new TreeNode("Chứng Nhận", "");
        TreeNode nHoSoCNDen = new TreeNode("Sản phẩm đến (" + SoBanGhi.ToString() + ")", "CN_HoSoDen");
        TreeNode nHoSoCNDi = new TreeNode("Sản phẩm đã gửi (" + SoBanGhiDaGui.ToString() + ")","CN_HoSoDi");
        ChungNhanHopQuy.ChildNodes.Add(nHoSoCNDen);
        ChungNhanHopQuy.ChildNodes.Add(nHoSoCNDi);
        tvDanhMucHoSo.Nodes.Add(ChungNhanHopQuy);
        tvDanhMucHoSo.ExpandAll();
    }
    /// <summary>
    /// Load các usercontrol
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 14/5/2009              
    /// </Modified>
    private void LoadUserControls()
    {
        uc_CN_HoSoSanPhamMoiNhan = (UserControls_uc_CN_HoSoSanPhamMoiNhan)this.LoadControl(@"~\UserControls\uc_CN_HoSoSanPhamMoiNhan.ascx");
        uc_CN_HoSoSanPhamDaGui = (UserControls_uc_CN_HoSoSanPhamDaGui)this.LoadControl(@"~\UserControls\uc_CN_HoSoSanPhamDaGui.ascx");
        MyPlaceHolder.Controls.Add(uc_CN_HoSoSanPhamMoiNhan);
        MyPlaceHolder.Controls.Add(uc_CN_HoSoSanPhamDaGui);
    }
    /// <summary>
    /// Event chọn vào TreeView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 14/5/2009              
    /// </Modified>
    protected void tvDanhMucHoSo_SelectedNodeChanged(object sender, EventArgs e)
    {
        string strPageSelect = tvDanhMucHoSo.SelectedNode.Value.ToString();
        DirectionPage(strPageSelect);
    }
    /// <summary>
    /// Show, hide các usercontrol
    /// </summary>
    /// <param name="strPageSelect"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 14/5/2009              
    /// </Modified>
    private void DirectionPage(string strPageSelect)
    {
        switch (strPageSelect)
        {
            case "CN_HoSoDen":
                MyPlaceHolder.Controls[0].Visible = true;
                MyPlaceHolder.Controls[1].Visible = false;
                break;
            case "CN_HoSoDi":
                MyPlaceHolder.Controls[0].Visible = false;
                MyPlaceHolder.Controls[1].Visible = true;
                break;
            default:
                MyPlaceHolder.Controls[0].Visible = true;
                MyPlaceHolder.Controls[1].Visible = false;
                break;
        }
    }
}
