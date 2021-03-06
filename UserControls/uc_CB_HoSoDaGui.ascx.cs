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

public partial class UserControls_uc_CB_HoSoDaGui : System.Web.UI.UserControl
{
    List<string> deleteSuccess = new List<string>();
    List<string> deleteFail = new List<string>();
    string TrangThaiID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        TrangThaiID = Request["TrangThaiFilter"];
        if (!IsPostBack)
        {
            BindFilterTrangThai();
            LayDanhSachHoSo();
        }
    }
    /// <summary>
    /// Lấy danh sách hồ sơ sản phẩm đã gửi
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    private void LayDanhSachHoSo()
    {
        DataTable dt = ProviderFactory.HoSoProvider.LayTatCaHoSoCongBoDaGuiTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("02"), ((PageBase)this.Page).mUserInfo.UserID);
        string strExpr;
        DataView dv = new DataView(dt);

        if (TrangThaiID != "" && TrangThaiID != null)
        {
            strExpr = "TrangThaiID = " + TrangThaiID;
            dv.RowFilter = strExpr;
        }
        gvHoSo.DataSource = dv;
        gvHoSo.DataBind();
    }
    private void BindFilterTrangThai()
    {
        DataTable dt = ProviderFactory.HoSoProvider.LayTatCaHoSoCongBoDaGuiTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("02"), ((PageBase)this.Page).mUserInfo.UserID);
        DataTable dtTrangThai = new DataTable();
        dtTrangThai.Columns.Add("TrangThaiID");
        dtTrangThai.Columns.Add("TenTrangThai");
        dtTrangThai.PrimaryKey = new DataColumn[1] { dtTrangThai.Columns[0] };
        DataRow dataRow = dtTrangThai.NewRow();
        dataRow["TrangThaiID"] = "";
        dataRow["TenTrangThai"] = "--Chọn trạng thái hồ sơ--";
        // add new data row to the data table.
        dtTrangThai.Rows.Add(dataRow);

        foreach (DataRow dr in dt.Rows)
        {
            DataRow row = dtTrangThai.Rows.Find(dr["TrangThaiID"].ToString());
            if (row == null)
            {
                row = dtTrangThai.NewRow();
                row["TrangThaiID"] = dr["TrangThaiID"].ToString();
                row["TenTrangThai"] = dr["TenTrangThai"].ToString();
                dtTrangThai.Rows.Add(row);
            }
        }
        ddlFilterTrangThai.DataSource = dtTrangThai;
        ddlFilterTrangThai.DataValueField = "TrangThaiID";
        ddlFilterTrangThai.DataTextField = "TenTrangThai";
        ddlFilterTrangThai.DataBind();
        if (TrangThaiID != "" && TrangThaiID != null)
        {
            ddlFilterTrangThai.SelectedValue = TrangThaiID;
        }
    }
    /// <summary>
    /// Đổi màu bản ghi nếu DaDoc field = false
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 13/5/2009              
    /// </Modified>
    protected void gvHoSo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //đổi màu hồ sơ chưa đọc
            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "CheckDaDoc")) == false)
            {
                //e.Row.BackColor = System.Drawing.Color.Gray;
                e.Row.CssClass = "unread rowitem";
            }
            //hien thi ho so qua han, sap het han duoi dang mau do
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "XuLyHoSoDungHan")) == 1)
            {
                e.Row.CssClass = "nearexpired rowitem";
            }
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "XuLyHoSoDungHan")) == 2)
            {
                e.Row.CssClass = "expired rowitem";
            }
            //nếu là người tiếp nhận thì thay đổi label thành đã gửi lãnh đạo
            //if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
            //{
            //    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.CHO_PHAN_CONG)
            //    {
            //        ((Label)e.Row.FindControl("lblTrangThai")).Text = "Đã gửi lãnh đạo";
            //    }
            //}
            //thêm link cho tất cả các quyền của user
            #region "Trạng thái Chờ phân công"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.CHO_PHAN_CONG)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Tiếp nhân hồ sơ";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).NavigateUrl = "../WebUI/CB_HoSoSanPham.aspx?direct=CB_HoSoDi&HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&action=view&UserControl=CB_HoSoDen";
                }
            }
            #endregion "Trạng thái Chờ phân công"

            #region "Trạng thái Chờ phân công"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.CHO_PHAN_CONG)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Tiếp nhận hồ sơ";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).NavigateUrl = "../WebUI/CB_HoSoSanPham.aspx?direct=CB_HoSoDi&HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&UserControl=CB_HoSoDi&action=view";
                }
            }
            #endregion "Trạng thái Chờ phân công"

            #region "Trạng thái Đang xử lý"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.DANG_XU_LY)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Tiếp nhận hồ sơ";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).NavigateUrl = "../WebUI/CB_HoSoSanPham.aspx?direct=CB_HoSoDi&HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&UserControl=CB_HoSoDi&action=view";
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                {
                    //voi vai tro xu ly, phai kiem tra, it nhat co 1 san pham cho tham dinh hoac cho phe duyet
                    if (ProviderFactory.SanPhamProvider.LayMaSanPhamTheoTrangThaiVaHoSoId("2,5,6,7", DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                    {
                        //hien thi hyperlink control
                        ((Panel)e.Row.FindControl("Panel2")).Visible = true;
                        ((HyperLink)e.Row.FindControl("HyperLink2")).Text = "- Xử lý hồ sơ";
                        ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "../WebUI/CB_HoSoSanPham_QuanLy.aspx?HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&UserControl=CB_HoSoDi";
                    }
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                {
                    if (ProviderFactory.SanPhamProvider.LayMaSanPhamTheoTrangThaiVaHoSoId("3,4,6,7", DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                    {
                        ((Panel)e.Row.FindControl("Panel3")).Visible = true;
                        ((HyperLink)e.Row.FindControl("HyperLink3")).Text = "- Thẩm định hồ sơ";
                        ((HyperLink)e.Row.FindControl("HyperLink3")).NavigateUrl = "../WebUI/CB_HoSoSanPham_QuanLy.aspx?HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&UserControl=CB_HoSoDi";
                    }
                }
                if (((PageBase)this.Page).mUserInfo.IsPermission(EnPermission.PHANCONG_HOSO_CHUNGNHAN))
                {
                    ((Panel)e.Row.FindControl("Panel4")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink4")).Text = "- Phân công hồ sơ";
                    ((HyperLink)e.Row.FindControl("HyperLink4")).NavigateUrl = "../WebUI/CB_PhanCongXuLyThamDinh.aspx?HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "";
                }
                if (((PageBase)this.Page).mUserInfo.IsPermission(EnPermission.CB_PHE_DUYET))
                {
                    if (ProviderFactory.SanPhamProvider.LayMaSanPhamTheoTrangThaiVaHoSoId("6,7", DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                    {
                        ((Panel)e.Row.FindControl("Panel5")).Visible = true;
                        ((HyperLink)e.Row.FindControl("HyperLink5")).Text = "- Phê duyệt hồ sơ";
                        ((HyperLink)e.Row.FindControl("HyperLink5")).NavigateUrl = "../WebUI/CB_HoSoSanPham_QuanLy.aspx?HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&UserControl=CB_HoSoDi";
                    }
                }
            }
            #endregion "Trạng thái Đang xử lý"

            #region "Trạng thái Chờ lưu trữ"
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.CHO_LUU_TRU)
            {
                if (((PageBase)this.Page).mUserInfo.IsNguoiTiepNhan(DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                {
                    ((Panel)e.Row.FindControl("Panel1")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink1")).Text = "- Tiếp nhận hồ sơ";
                    ((HyperLink)e.Row.FindControl("HyperLink1")).NavigateUrl = "../WebUI/CB_HoSoSanPham.aspx?direct=CB_HoSoDi&HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&UserControl=CB_HoSoDi&action=view";
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                {
                    //voi vai tro xu ly, phai kiem tra, it nhat co 1 san pham cho tham dinh hoac cho phe duyet
                    if (ProviderFactory.SanPhamProvider.LayMaSanPhamTheoTrangThaiVaHoSoId("2,5,6,7", DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                    {
                        ((Panel)e.Row.FindControl("Panel2")).Visible = true;
                        ((HyperLink)e.Row.FindControl("HyperLink2")).Text = "- Xử lý hồ sơ";
                        ((HyperLink)e.Row.FindControl("HyperLink2")).NavigateUrl = "../WebUI/CB_HoSoSanPham_QuanLy.aspx?HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&UserControl=CB_HoSoDi";
                    }
                }
                if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                {
                    if (ProviderFactory.SanPhamProvider.LayMaSanPhamTheoTrangThaiVaHoSoId("3,4,6,7", DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                    {
                        ((Panel)e.Row.FindControl("Panel3")).Visible = true;
                        ((HyperLink)e.Row.FindControl("HyperLink3")).Text = "- Thẩm định hồ sơ";
                        ((HyperLink)e.Row.FindControl("HyperLink3")).NavigateUrl = "../WebUI/CB_HoSoSanPham_QuanLy.aspx?HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&UserControl=CB_HoSoDi";
                    }
                }
                if (((PageBase)this.Page).mUserInfo.IsPermission(EnPermission.PHANCONG_HOSO_CHUNGNHAN))
                {
                    ((Panel)e.Row.FindControl("Panel4")).Visible = true;
                    ((HyperLink)e.Row.FindControl("HyperLink4")).Text = "- Phân công hồ sơ";
                    ((HyperLink)e.Row.FindControl("HyperLink4")).NavigateUrl = "../WebUI/CB_PhanCongXuLyThamDinh.aspx?HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "";
                }
                if (((PageBase)this.Page).mUserInfo.IsPermission(EnPermission.CB_PHE_DUYET))
                {
                    if (ProviderFactory.SanPhamProvider.LayMaSanPhamTheoTrangThaiVaHoSoId("6,7", DataBinder.Eval(e.Row.DataItem, "ID").ToString()))
                    {
                        ((Panel)e.Row.FindControl("Panel5")).Visible = true;
                        ((HyperLink)e.Row.FindControl("HyperLink5")).Text = "- Phê duyệt hồ sơ";
                        ((HyperLink)e.Row.FindControl("HyperLink5")).NavigateUrl = "../WebUI/CB_HoSoSanPham_QuanLy.aspx?HoSoID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&TrangThaiId=" + DataBinder.Eval(e.Row.DataItem, "TrangThaiID").ToString() + "&UserControl=CB_HoSoDi";
                    }
                }
            }
            #endregion "Trạng thái Chờ lưu trữ"
        }
    }
    /// <summary>
    /// Phân trang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    ///     Author      Date          Comments
    ///     QuanNM      18/05/2009    Tạo mới
    /// </Modified>
    protected void gvHoSo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHoSo.PageIndex = e.NewPageIndex;
        LayDanhSachHoSo();
    }
    protected void ddlFilterTrangThai_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDi&TrangThaiFilter=" + ddlFilterTrangThai.SelectedValue.ToString());
    }
}
