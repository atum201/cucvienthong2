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
using System.Data.SqlClient;
using Cuc_QLCL.Data;
using System.Drawing;

/// <summary>
/// Quản lý hồ sơ mới
/// </summary>
/// <Modified>
/// Người tạo              Ngày tạo                Chú thích
/// QuanNM                 9/5/2009              
/// </Modified>
public partial class UserControls_uc_CN_HoSoMoiNhan : System.Web.UI.UserControl
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
            LayDanhSachHoSo(TrangThaiID);
            //có quyền quản lý hồ sơ hay không?
            if (!((PageBase)this.Page).mUserInfo.IsPermission("0101"))
            {
                lnkThemMoi.Visible = false;
                imgButtonAdd.Visible = false;
                lnkXoa.Visible = false;
                ImgBtnXoa.Visible = false;
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
    private void LayDanhSachHoSo(string TrangThaiID)
    {
        DataTable dt = ProviderFactory.HoSoProvider.LayTatCaHoSoTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("01"), ((PageBase)this.Page).mUserInfo.UserID);
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

        gvHoSo.DataSource = dv;
        gvHoSo.DataBind();
    }

    /// <summary>
    /// Bind du lieu vao combo loc ho so theo trang thai
    /// </summary>
    private void BindFilterTrangThai()
    {
        DataTable dt = ProviderFactory.HoSoProvider.LayTatCaHoSoTheoQuyen(((PageBase)this.Page).mUserInfo.GetPermissionList("01"), ((PageBase)this.Page).mUserInfo.UserID);
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
        Response.Redirect("CN_HoSo_ChiTiet.aspx");
    }
    protected void lnkThemMoiVB_Click(object sender, EventArgs e)
    {
        Response.Redirect("CN_HoSo_ChiTiet.aspx?vb=1");
    }
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
            //chi cho phep sua ho so o 2 trang thai la hosomoi va dangxuly(trang thai nay` chi cho phep cap nhat hosodaydu va khongdaydu)
            if ((Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.HO_SO_MOI) || (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TrangThaiID")) == (int)EnTrangThaiHoSoList.DANG_XU_LY))
            {
                Control div = e.Row.FindControl("View");
                div.Visible = true;
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

            e.Row.Cells[6].Text = TenTrangThai;
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
        LayDanhSachHoSo(TrangThaiID);
    }
    private void DeleteHoSo()
    {
        // Thực hiện xoá
        foreach (GridViewRow row in gvHoSo.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkCheck");
            if (chk.Checked)
            {
                string hoSoId = gvHoSo.DataKeys[row.RowIndex][0].ToString();
                //Kiểm tra trạng thái và số hồ sơ, chỉ cho phép xóa hồ sơ mới và có số hồ sơ là số mới nhất
                int trangThaiId = Convert.ToInt32(gvHoSo.DataKeys[row.RowIndex][1].ToString());
                string soHoSo = gvHoSo.DataKeys[row.RowIndex][2].ToString();

                // Lấy số hồ sơ mới nhất
                int soHoSoMoiNhat = ProviderFactory.HoSoProvider.getSoLonNhat(hoSoId);
                int soHienTai = Convert.ToInt32(soHoSo.Substring(0, 4));
                if (trangThaiId == (int)EnTrangThaiHoSoList.HO_SO_MOI && soHienTai == soHoSoMoiNhat)
                {
                    TransactionManager transaction = ProviderFactory.Transaction;
                    try
                    {
                        ProviderFactory.SanPhamProvider.XoaSanPhamTheoMaHoSo(hoSoId, transaction);
                        ProviderFactory.HoSoProvider.Delete(transaction, hoSoId);

                        transaction.Commit();
                        deleteSuccess.Add(soHoSo);
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
                    finally
                    {
                        transaction.Dispose();
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
            ProviderFactory.SysLogProvier.Write(((PageBase)this.Page).mUserInfo, SysEventList.CN_HO_SO_XOA, thongbao);
        }
        if (deleteFail.Count > 0)
        {
            thongbao += String.Format(Resources.Resource.msgXoaHoSoThatBai, string.Join(",", deleteFail.ToArray()));
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo", "<script>alert('" + thongbao + "');location.href='CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen';</script>");
        //LayDanhSachHoSo(TrangThaiID);
        //Response.Redirect("CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen");
    }
    protected void ImgBtnXoa_Click(object sender, ImageClickEventArgs e)
    {
        DeleteHoSo();
    }
    protected void imgButtonAdd_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CN_HoSo_ChiTiet.aspx");
    }
    protected void imgButtonAddVB_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CN_HoSo_ChiTiet.aspx?vb=1");
    }
    protected void ddlFilterTrangThai_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("CN_HoSo_QuanLy.aspx?UserControl=CN_HoSoDen&TrangThaiFilter=" + ddlFilterTrangThai.SelectedValue.ToString());
    }

    //private void KiemTraTrangThaiSanPham(string HoSoId)
    //{
    //    // Kiểm tra danh sách các sản phẩm trong hồ sơ, nếu tất cả đã được phê duyệt thì chuyển hồ sơ sang trạng thái chờ lưu trữ
    //    TList<SanPham> lstSanPham = ProviderFactory.SanPhamProvider.GetByHoSoId(HoSoId);
    //    int count = 0;
    //    foreach (SanPham objSP in lstSanPham)
    //    {
    //        if (objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.GD_PHE_DUYET || objSP.TrangThaiId == (int)EnTrangThaiSanPhamList.KET_THUC)
    //            count++;
    //    }
    //    if (count == lstSanPham.Count)
    //    {
    //        imgGuiTBP.Visible = true;
    //        lnkGuiTBP.Visible = true;
    //        // Chuyển hồ sơ sang trạng thái chờ lưu trữ
    //        //HoSo objHoSo = ProviderFactory.HoSoProvider.GetById(trans, HoSoId);
    //        //objHoSo.TrangThaiId = (int)EnTrangThaiHoSoList.CHO_LUU_TRU;
    //        //ProviderFactory.HoSoProvider.Save(trans, objHoSo);
    //    }
    //}
}
