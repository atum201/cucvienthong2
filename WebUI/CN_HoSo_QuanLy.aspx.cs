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
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
/// <summary>
/// Quản lý trạng thái của Hồ sơ
/// </summary>
/// <Modified>
/// Người tạo              Ngày tạo                Chú thích
/// QuanNM                 14/5/2009              
/// </Modified>
public partial class WebUI_CN_HoSo_QuanLy : PageBase
{
    private UserControls_uc_CN_HoSoMoiNhan uc_CN_HoSoMoiNhan = null;
    private UserControls_uc_CN_HoSoDaGui uc_CN_HoSoDaGui = null;

    private UserControls_uc_CB_HoSoMoiNhan uc_CB_HoSoMoiNhan = null;
    private UserControls_uc_CB_HoSoDaGui uc_CB_HoSoDaGui = null;

    int SoBanGhi = 0;
    int SoBanGhiDaGui = 0;
    int CB_SoBanGhi = 0;
    int CB_SoBanGhiDaGui = 0;

    string strDriectionPage = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadUserControls();
        //dem so ban ghi cua tat ca cac control
        DemSoBanGhiCuaCacUserControl();
        //gan gia tri default cho viec hien thi usercontrol
        if (tvDanhMucHoSo.SelectedValue != null && tvDanhMucHoSo.SelectedValue != "")
        {
            strDriectionPage = tvDanhMucHoSo.SelectedNode.Value.ToString();
        }
        else
        {
            //hien thi usercontrol cho tung loai ho so
            if (Request["UserControl"] != "" && Request["UserControl"] != null)
                strDriectionPage = Request["UserControl"];
        }
        DirectionPage(strDriectionPage);
        //chuyen trang cho tung usercontrol
        SwitchLink(Request["UserControl"], Request["HoSoId"], Request["TrangThaiId"]);
        //bind treenode
        AddTreeNode();
    }
    /// <summary>
    /// Đếm số bản ghi của các usercontrol
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 14/5/2009              
    /// </Modified>
    private void DemSoBanGhiCuaCacUserControl()
    {
        DataTable dtHoSoMoiNhan = ProviderFactory.HoSoProvider.LayTatCaHoSoTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("01"), ((PageBase)this.Page).mUserInfo.UserID);
        SoBanGhi = dtHoSoMoiNhan.Rows.Count;
        DataTable dtHoSoDaGui = ProviderFactory.HoSoProvider.LayTatCaHoSoDaGuiTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("01"), ((PageBase)this.Page).mUserInfo.UserID);
        SoBanGhiDaGui = dtHoSoDaGui.Rows.Count;

        DataTable dtHoSoCBMoiNhan = ProviderFactory.HoSoProvider.LayTatCaHoSoCongBoTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("02"), ((PageBase)this.Page).mUserInfo.UserID);
        CB_SoBanGhi = dtHoSoCBMoiNhan.Rows.Count;
        DataTable dtHoSoCBDaGui = ProviderFactory.HoSoProvider.LayTatCaHoSoCongBoDaGuiTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("02"), ((PageBase)this.Page).mUserInfo.UserID);
        CB_SoBanGhiDaGui = dtHoSoCBDaGui.Rows.Count;
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
        if (mUserInfo.IsInRole("01"))
        {
            TreeNode ChungNhanHopQuy = new TreeNode("Chứng Nhận", "CN_HoSoDen");
            TreeNode nHoSoCNDen = new TreeNode("Hồ sơ đến (" + SoBanGhi.ToString() + ")", "CN_HoSoDen");
            TreeNode nHoSoCNDi = new TreeNode("Hồ sơ đã gửi (" + SoBanGhiDaGui.ToString() + ")", "CN_HoSoDi");
            ChungNhanHopQuy.ChildNodes.Add(nHoSoCNDen);
            ChungNhanHopQuy.ChildNodes.Add(nHoSoCNDi);
            tvDanhMucHoSo.Nodes.Add(ChungNhanHopQuy);
        }
        if (mUserInfo.IsInRole("02"))
        {
            TreeNode CongBoHopQuy = new TreeNode("Công Bố", "CB_HoSoDen");
            TreeNode nHoSoCBDen = new TreeNode("Hồ sơ đến (" + CB_SoBanGhi.ToString() + ")", "CB_HoSoDen");
            TreeNode nHoSoCBDi = new TreeNode("Hồ sơ đã gửi (" + CB_SoBanGhiDaGui.ToString() + ")", "CB_HoSoDi");
            CongBoHopQuy.ChildNodes.Add(nHoSoCBDen);
            CongBoHopQuy.ChildNodes.Add(nHoSoCBDi);
            tvDanhMucHoSo.Nodes.Add(CongBoHopQuy);
        }
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
        uc_CN_HoSoMoiNhan = (UserControls_uc_CN_HoSoMoiNhan)this.LoadControl(@"~\UserControls\uc_CN_HoSoMoiNhan.ascx");
        uc_CN_HoSoDaGui = (UserControls_uc_CN_HoSoDaGui)this.LoadControl(@"~\UserControls\uc_CN_HoSoDaGui.ascx");

        uc_CB_HoSoMoiNhan = (UserControls_uc_CB_HoSoMoiNhan)this.LoadControl(@"~\UserControls\uc_CB_HoSoMoiNhan.ascx");
        uc_CB_HoSoDaGui = (UserControls_uc_CB_HoSoDaGui)this.LoadControl(@"~\UserControls\uc_CB_HoSoDaGui.ascx");

        MyPlaceHolder.Controls.Add(uc_CN_HoSoMoiNhan);
        MyPlaceHolder.Controls.Add(uc_CN_HoSoDaGui);
        MyPlaceHolder.Controls.Add(uc_CB_HoSoMoiNhan);
        MyPlaceHolder.Controls.Add(uc_CB_HoSoDaGui);
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
        Response.Redirect("CN_HoSo_QuanLy.aspx?UserControl=" + strDriectionPage, false);
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
                MyPlaceHolder.Controls[2].Visible = false;
                MyPlaceHolder.Controls[3].Visible = false;
                break;
            case "CN_HoSoDi":
                MyPlaceHolder.Controls[0].Visible = false;
                MyPlaceHolder.Controls[1].Visible = true;
                MyPlaceHolder.Controls[2].Visible = false;
                MyPlaceHolder.Controls[3].Visible = false;
                break;
            case "CB_HoSoDen":
                MyPlaceHolder.Controls[0].Visible = false;
                MyPlaceHolder.Controls[1].Visible = false;
                MyPlaceHolder.Controls[2].Visible = true;
                MyPlaceHolder.Controls[3].Visible = false;
                break;
            case "CB_HoSoDi":
                MyPlaceHolder.Controls[0].Visible = false;
                MyPlaceHolder.Controls[1].Visible = false;
                MyPlaceHolder.Controls[2].Visible = false;
                MyPlaceHolder.Controls[3].Visible = true;
                break;
            default:
                MyPlaceHolder.Controls[0].Visible = true;
                MyPlaceHolder.Controls[1].Visible = false;
                MyPlaceHolder.Controls[2].Visible = false;
                MyPlaceHolder.Controls[3].Visible = false;
                break;
        }
    }
    /// <summary>
    /// Chuyen trang thai cho tat ca ho so chung nhan va cong bo
    /// </summary>
    /// <param name="UserControl"></param>
    /// <param name="HoSoID"></param>
    /// <param name="TrangThaiID"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 14/5/2009              
    /// </Modified>
    private void SwitchLink(string UserControl, string HoSoID, string TrangThaiID)
    {
        if (UserControl != "" && UserControl != null)
        {
            //bien su dung chung giua cac usercontrol
            int TrangThai = 0;
            string link = "";
            switch (UserControl)
            {
                #region "CB_HoSoDen"
                case "CB_HoSoDen":
                    if ((TrangThaiID != string.Empty) && (TrangThaiID != null))
                    {
                        //Chuyển trạng thái chưa đọc sang đã đọc của hồ sơ
                        ProviderFactory.HoSoProvider.ChangeStatusDaDoc(((PageBase)this.Page).mUserInfo.UserID, HoSoID, "Add");
                        //Chuyển trang
                        TrangThai = Convert.ToInt32(TrangThaiID);
                       
                        switch (TrangThai)
                        {
                            case (int)EnTrangThaiHoSoList.HO_SO_MOI:
                                link = "CB_HoSoSanPham.aspx?direct=CB_HoSoDen&HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "&UserControl=CB_HoSoDen";
                                break;
                            case (int)EnTrangThaiHoSoList.CHO_PHAN_CONG:
                                link = "CB_PhanCongXuLyThamDinh.aspx?direct=CB_HoSoDen&HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "&UserControl=CB_HoSoDen";
                                break;
                            case (int)EnTrangThaiHoSoList.DANG_XU_LY:
                                link = "CB_HoSoSanPham_QuanLy.aspx?HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "&UserControl=CB_HoSoDen&UserControlHS=CB_HoSoDen";
                                break;
                            case (int)EnTrangThaiHoSoList.CHO_LUU_TRU:
                                if (mUserInfo.IsPermission(EnPermission.CN_LUUTRU))
                                {
                                    link = "CN_HoSo_LuuTru.aspx?HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "";
                                }
                                else
                                {
                                    link = "CN_HoSo_LuuTru.aspx?HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "&action=view";
                                }
                                break;
                        }
                        Response.Redirect(link, false);
                    }
                    break;
                #endregion "CB_HoSoDen"
                #region "CN_HoSoDen"
                case "CN_HoSoDen":
                    if ((TrangThaiID != string.Empty) && (TrangThaiID != null))
                    {
                        //Chuyển trạng thái chưa đọc sang đã đọc của hồ sơ
                        ProviderFactory.HoSoProvider.ChangeStatusDaDoc(((PageBase)this.Page).mUserInfo.UserID, HoSoID, "Add");
                        //Chuyển trang
                        TrangThai = Convert.ToInt32(TrangThaiID);
                        switch (TrangThai)
                        {
                            case (int)EnTrangThaiHoSoList.HO_SO_MOI:
                                link = "CN_HoSoSanPham.aspx?direct=CN_HoSoMoi&HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "&UserControl=CN_HoSoDen";
                                break;
                            case (int)EnTrangThaiHoSoList.CHO_PHAN_CONG:
                                link = "CN_PhanCongXuLyThamDinh.aspx?direct=CN_HoSoMoi&HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "";
                                break;
                            case (int)EnTrangThaiHoSoList.DANG_XU_LY:
                                link = "CN_HoSoSanPham_QuanLy.aspx?HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "&UserControl=CN_HoSoDen&UserControlHS=CN_HoSoDen";
                                break;
                            case (int)EnTrangThaiHoSoList.CHO_LUU_TRU:
                                if (mUserInfo.IsPermission(EnPermission.CN_LUUTRU))
                                {
                                    link = "CN_HoSo_LuuTru.aspx?HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "";
                                }
                                else
                                {
                                    link = "CN_HoSo_LuuTru.aspx?HoSoID=" + HoSoID + "&TrangThaiId=" + TrangThaiID + "&action=view";
                                }
                                break;
                        }
                        Response.Redirect(link, false);
                    }
                    break;
                #endregion "CN_HoSoDen"
            }
        }
    }
}
