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
using CucQLCL.Common;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Data;
using Cuc_QLCL.Entities;
using System.Data.SqlClient;

public partial class UserControls_uc_CB_HoSoMoiNhan : System.Web.UI.UserControl
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
            //có quyền quản lý hồ sơ hay không?
            if (!((PageBase)this.Page).mUserInfo.IsPermission("0201"))
            {
                lnkThemMoi.Visible = false;
                imgButtonAdd.Visible = false;
                lnkXoa.Visible = false;
                imgBtnXoa.Visible = false;
            }
        }
    }
    /// <summary>
    /// Lấy danh sách hồ sơ sản phẩm mới nhận
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    private void LayDanhSachHoSo()
    {
        DataTable dt = ProviderFactory.HoSoProvider.LayTatCaHoSoCongBoTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("02"), ((PageBase)this.Page).mUserInfo.UserID);
        string strExpr;
        DataView dv = new DataView(dt);

        if (TrangThaiID != "" && TrangThaiID != null)
        {
            if (TrangThaiID == "41")
            {
                strExpr = "(";
                foreach (DataRow row in dt.Rows)
                {
                    string tmpId = row["ID"].ToString();
                    if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(tmpId) && row["TrangThaiID"].ToString() == "4")
                    {
                        strExpr += "'" + tmpId + "', ";
                    }
                }
                if (strExpr.Length > 1)
                {
                    strExpr = strExpr.Substring(0, strExpr.Length - 2) + ")";
                    strExpr = "ID in " + strExpr;
                }
                else
                    strExpr = " 1 = 1";
            }
            else if (TrangThaiID == "42")
            {
                strExpr = "(";
                foreach (DataRow row in dt.Rows)
                {
                    string tmpId = row["ID"].ToString();
                    if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(tmpId) && row["TrangThaiID"].ToString() == "4")
                    {
                        strExpr += "'" + tmpId + "', ";
                    }
                }
                if (strExpr.Length > 1)
                {
                    strExpr = strExpr.Substring(0, strExpr.Length - 2) + ")";
                    strExpr = "ID in " + strExpr;
                }
                else
                    strExpr = " 1 = 1";
            }
            else if (TrangThaiID == "43")
            {
                strExpr = "(";
                foreach (DataRow row in dt.Rows)
                {
                    string tmpId = row["ID"].ToString();
                    if (((PageBase)this.Page).mUserInfo.IsPermission("0106") && row["TrangThaiID"].ToString() == "4")
                    {
                        strExpr += "'" + tmpId + "', ";
                    }
                }
                if (strExpr.Length > 1)
                {
                    strExpr = strExpr.Substring(0, strExpr.Length - 2) + ")";
                    strExpr = "ID in " + strExpr;
                }
                else
                    strExpr = " 1 = 1";
            }
            else
            {
                strExpr = "TrangThaiID = " + TrangThaiID;
            }

            // thuc hien loc
            dv.RowFilter = strExpr;
        }
        gvHoSoCB.DataSource = dv;
        gvHoSoCB.DataBind();
    }
    private void BindFilterTrangThai()
    {
        DataTable dt = ProviderFactory.HoSoProvider.LayTatCaHoSoCongBoTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("02"), ((PageBase)this.Page).mUserInfo.UserID);
        DataTable dtTrangThai = new DataTable();
        dtTrangThai.Columns.Add("ID");
        dtTrangThai.Columns.Add("TrangThaiID");
        dtTrangThai.Columns.Add("TenTrangThai");
        dtTrangThai.PrimaryKey = new DataColumn[1] { dtTrangThai.Columns[2] };
        DataRow dataRow = dtTrangThai.NewRow();
        dataRow["ID"] = "0";
        dataRow["TrangThaiID"] = "";
        dataRow["TenTrangThai"] = "--Chọn trạng thái hồ sơ--";
        // add new data row to the data table.
        dtTrangThai.Rows.Add(dataRow);

        foreach (DataRow dr in dt.Rows)
        {
            DataRow row = dtTrangThai.Rows.Find(dr["TenTrangThai"].ToString());
            if (row == null)
            {
                string tmpId = dr["ID"].ToString();

                row = dtTrangThai.NewRow();
                row["ID"] = tmpId;
                row["TrangThaiID"] = dr["TrangThaiID"].ToString();
                row["TenTrangThai"] = dr["TenTrangThai"].ToString();
                if (dr["TrangThaiID"].ToString() == "4")
                {
                    if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(tmpId))
                    {
                        row["TenTrangThai"] = "Đang xử lý";
                        row["TrangThaiID"] = "41";
                    }
                    else if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(tmpId))
                    {
                        row["TenTrangThai"] = "Chờ thẩm định";
                        row["TrangThaiID"] = "42";
                    }
                    else if (((PageBase)this.Page).mUserInfo.IsPermission("0106"))
                    {
                        row["TenTrangThai"] = "Chờ phê duyệt";
                        row["TrangThaiID"] = "43";
                    }
                }
                try
                {
                    dtTrangThai.Rows.Add(row);
                }
                catch { }
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
    /// Thêm mới hồ sơ
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void lnkThemMoi_Click(object sender, EventArgs e)
    {
        Response.Redirect("CB_HoSo_ChiTiet.aspx");
    }
    // LongHH
    /// <summary>
    /// Thêm mới hồ sơ miễm giảm
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void lnkMiemGiam_Click(object sender, EventArgs e)
    {
        Response.Redirect("CB_HoSo_ChiTiet.aspx?NguonGoc=" + (int)QLCL_Patch.DuoiHoSo.CongBo_MiemKiemTra);
    }
    // LongHH
    /// <summary>
    /// Thêm mới hồ sơ nhập khẩu
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void lnkNhapKhau_Click(object sender, EventArgs e)
    {
        Response.Redirect("CB_HoSo_ChiTiet.aspx?NguonGoc=" + (int)EnNguonGocList.NHAP_KHAU);
    }
    // LongHH
    /// <summary>
    /// Thêm mới hồ sơ sản xuất trong nước
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void lnkSanXuatTN_Click(object sender, EventArgs e)
    {
        Response.Redirect("CB_HoSo_ChiTiet.aspx?NguonGoc=" + (int)EnNguonGocList.SX_TRONG_NUOC);
    }
    //LongHH
    /// <summary>
    /// Xoá hồ sơ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// QuanNM                 7/5/2009              
    /// </Modified>
    protected void lnkXoa_Click(object sender, EventArgs e)
    {
        DeleteHoSo();
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
        string TenTrangThai = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "CheckDaDoc")) == false)
            {
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
            //chi cho phep sua ho so o 2 trang thai la hosomoi va dangxuly(trang thai nay` chi cho phep cap nhat hosodaydu va khongdaydu)
            if ((Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.HO_SO_MOI) || (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.DANG_XU_LY))
            {
                Control div = e.Row.FindControl("View");
                div.Visible = true;
            }

            TenTrangThai = Cuc_QLCL.Entities.EntityHelper.GetEnumTextValue((Cuc_QLCL.Entities.EnTrangThaiHoSoList)Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")));
            // Thiết lập lại tên trạng thái
            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.DANG_XU_LY)
            {
                string tmpId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();

                if (((PageBase)this.Page).mUserInfo.IsNguoiXuLy(tmpId))
                {
                    TList<SanPham> lSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(tmpId);
                    int spChoXuLy = 0;
                    int spDaThamDinh = 0;
                    int spDaPheDuyet = 0;
                    foreach (SanPham sp in lSanPham)
                    {
                        if (sp.TrangThaiId == (int)EnTrangThaiSanPhamList.CHO_XU_LY)
                            spChoXuLy++;
                        if (sp.TrangThaiId == (int)EnTrangThaiSanPhamList.THAM_DINH_DONG_Y || sp.TrangThaiId == (int)EnTrangThaiSanPhamList.THAM_DINH_KHONG_DONG_Y)
                            spDaThamDinh++;
                        if (sp.TrangThaiId == (int)EnTrangThaiSanPhamList.GD_KHONG_DUYET || sp.TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET)
                            spDaPheDuyet++;
                    }
                    TenTrangThai = string.Empty;
                    if (spChoXuLy > 0)
                        TenTrangThai = spChoXuLy.ToString() + " Sp chờ X.Lý";
                    if (spDaThamDinh > 0)
                        if (!string.IsNullOrEmpty(TenTrangThai))
                            TenTrangThai += "<br />" + spDaThamDinh.ToString() + " Sp đã T.Định";
                        else
                            TenTrangThai += spDaThamDinh.ToString() + " Sp đã T.Định";
                    if (spDaPheDuyet > 0)
                        if (!string.IsNullOrEmpty(TenTrangThai))
                            TenTrangThai += "<br />" + spDaPheDuyet.ToString() + " Sp đã P.Duyệt";
                        else
                            TenTrangThai += spDaPheDuyet.ToString() + " Sp đã P.Duyệt";
                }
                else if (((PageBase)this.Page).mUserInfo.IsNguoiThamDinh(tmpId))
                    TenTrangThai = "Chờ thẩm định";
                else if (((PageBase)this.Page).mUserInfo.IsPermission("0106"))
                    TenTrangThai = "Chờ phê duyệt";
            }

            e.Row.Cells[5].Text = TenTrangThai;
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
        gvHoSoCB.PageIndex = e.NewPageIndex;
        LayDanhSachHoSo();
    }
    private void DeleteHoSo()
    {
        // Thực hiện xoá
        foreach (GridViewRow row in gvHoSoCB.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                string hoSoId = gvHoSoCB.DataKeys[row.RowIndex][0].ToString();
                string soHoSo = gvHoSoCB.DataKeys[row.RowIndex][2].ToString();
                //kiem tra trang thai cua HS, chỉ có thể xóa HS mới
                int trangThaiId = Convert.ToInt32(gvHoSoCB.DataKeys[row.RowIndex][1].ToString());
                if (trangThaiId == (int)EnTrangThaiHoSoList.HO_SO_MOI)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        ProviderFactory.SanPhamProvider.XoaSanPhamTheoMaHoSo(hoSoId, transaction);
                        ProviderFactory.HoSoProvider.Delete(transaction, hoSoId);
                        deleteSuccess.Add(soHoSo);
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                        {
                            switch (ex.Errors[0].Number)
                            {
                                case 547: // Foreign Key violation
                                    deleteFail.Add(soHoSo);
                                    break;
                                default:
                                    deleteFail.Add(soHoSo);
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        deleteFail.Add(soHoSo);
                        //throw ex;
                    }
                }
                else
                {
                    deleteFail.Add(soHoSo);
                }
            }
        }

        // Đưa ra thông báo thành công và ghi log sự kiện
        string thongbao = "";
        if (deleteSuccess.Count > 0)
        {
            thongbao = String.Format(Resources.Resource.msgXoaHoSo, string.Join(",", deleteSuccess.ToArray()));
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CB_HO_SO_XOA, thongbao);
        }
        if (deleteFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaHoSoThatBai, string.Join(",", deleteFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen';</script>");
        //LayDanhSachHoSo();
    }
    protected void imgButtonAdd_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CB_HoSo_ChiTiet.aspx");
    }
    protected void imgBtnXoa_Click(object sender, ImageClickEventArgs e)
    {
        DeleteHoSo();
    }
    protected void ddlFilterTrangThai_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("CN_HoSo_QuanLy.aspx?UserControl=CB_HoSoDen&TrangThaiFilter=" + ddlFilterTrangThai.SelectedValue.ToString());
    }
}
