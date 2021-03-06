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
using System.Collections.Generic;

public partial class WebUI_Default : PageBase
{

    /// <summary>
    /// Hiển thị thông báo nhắc việc
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Author      Date            comment
    /// ĐứcHH       ????            Tạo mới
    /// TuấnVM      20/06           Sửa lỗi New DateTime(temp.Year, temp.Month, temp.Day - KhoangThoiGianDelta); lỗi khi temp.Day < KhoangThoiGianDelta
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["notaccess"] == "1")
            ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "ThongBao", "alert('Bạn không có quyền truy cập trang này');if (window.opener!=null) window.close();", true);
        try
        {
            int SoHoSoSapHetHanCN = 0;
            int SoHoSoHetHanCN = 0;
            int SoHoSoSapHetHanCB = 0;
            int SoHoSoHetHanCB = 0;
            int SoThongBaoNopTien = 0;

            DateTime NgayHienThoi = DateTime.Now;
            int KhoangThoiGianDelta = ConfigurationManager.AppSettings["KhoangThoiGianDeltaHetHan"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["KhoangThoiGianDeltaHetHan"]) : 3;
            int SoNgayXuLyHoSoChungNhan = ConfigurationManager.AppSettings["SoNgayXuLyHoSoChungNhan"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["SoNgayXuLyHoSo"]) : 30;
            int SoNgayXuLyHoSoCongBo = ConfigurationManager.AppSettings["SoNgayXuLyHoSoCongBo"] != null ? Convert.ToInt32(ConfigurationManager.AppSettings["SoNgayXuLyHoSo"]) : 30;

            List<string> listQuyenChungNhan = ((PageBase)this.Page).mUserInfo.GetPermissionList("01");
            List<string> listQuyenCongBo = ((PageBase)this.Page).mUserInfo.GetPermissionList("02");
            List<string> listQuyenThuPhi = ((PageBase)this.Page).mUserInfo.GetPermissionList("0109");
            List<string> listQuyenPheDuyet = ((PageBase)this.Page).mUserInfo.GetPermissionList("0106");
            DateTime currentDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (listQuyenChungNhan != null && listQuyenChungNhan.Count > 0)
            {
                DataTable dtHoSoMoiNhan = ProviderFactory.HoSoProvider.LayTatCaHoSoTheoQuyen(listQuyenChungNhan, ((PageBase)this.Page).mUserInfo.UserID);
                int SoBanGhi = dtHoSoMoiNhan.Rows.Count;

                if (SoBanGhi > 0)
                {
                    lbSoHoSoCNMoi.Text = String.Format(Resources.Resource.msgSoHoSoCNMoi, SoBanGhi.ToString());
                }

                lbSoHoSoCNMoi.Visible = SoBanGhi > 0? true:false;
                ImageSoHoSoCNMoi.Visible = SoBanGhi > 0 ? true : false;

                foreach (DataRow row in dtHoSoMoiNhan.Rows)
                {
                    if (row["NgayHetHanXuLy"] != null && row["NgayHetHanXuLy"] != System.DBNull.Value)
                    {
                        DateTime temp = Convert.ToDateTime(row["NgayHetHanXuLy"]);
                        DateTime newDay = new DateTime(temp.Year, temp.Month, temp.Day).AddDays(0 - KhoangThoiGianDelta);
                        if (temp < currentDay)
                        {
                            SoHoSoHetHanCN = SoHoSoHetHanCN + 1;
                            HoSo hoso = ProviderFactory.HoSoProvider.GetById(row["ID"].ToString());
                            if (hoso != null)
                            {
                                hoso.HoSoXuLyDungHan = 0;
                                ProviderFactory.HoSoProvider.Save(hoso);
                            }
                        }
                        else if (newDay < currentDay)
                        {
                            SoHoSoSapHetHanCN = SoHoSoSapHetHanCN + 1;
                        }
                    }
                }
                if (SoHoSoSapHetHanCN > 0)
                {
                    lbSoHoSoSapHetHanCN.Text = String.Format(Resources.Resource.msgSoHoSoCNSapHetHan, SoHoSoSapHetHanCN.ToString());
                }
                lbSoHoSoSapHetHanCN.Visible = SoHoSoSapHetHanCN > 0 ? true : false;
                ImageSoHoSoSapHetHanCN.Visible = SoHoSoSapHetHanCN > 0 ? true : false;

                if (SoHoSoHetHanCN > 0)
                {
                    lbSoHoSoHetHanCN.Text = String.Format(Resources.Resource.msgSoHoSoCNHetHan, SoHoSoHetHanCN.ToString());
                }
                lbSoHoSoHetHanCN.Visible = SoHoSoHetHanCN > 0 ? true : false;
                ImageSoHoSoHetHanCN.Visible = SoHoSoHetHanCN > 0 ? true : false;
            }
            else
            {
                lbSoHoSoCNMoi.Visible = false;
                lbSoHoSoSapHetHanCN.Visible = false;
                lbSoHoSoHetHanCN.Visible = false;

                ImageSoHoSoCNMoi.Visible = false;
                ImageSoHoSoSapHetHanCN.Visible = false;
                ImageSoHoSoHetHanCN.Visible = false;
            }

            if (listQuyenCongBo != null && listQuyenCongBo.Count > 0)
            {
                DataTable dtHoSoCBMoiNhan = ProviderFactory.HoSoProvider.LayTatCaHoSoCongBoTheoQuyen(listQuyenCongBo, ((PageBase)this.Page).mUserInfo.UserID);
                int CB_SoBanGhi = dtHoSoCBMoiNhan.Rows.Count;

                if (CB_SoBanGhi > 0)
                {
                    lbCB_SoBanGhi.Text = String.Format(Resources.Resource.msgSoHoSoCBMoi, CB_SoBanGhi.ToString());
                }
                lbCB_SoBanGhi.Visible = CB_SoBanGhi > 0 ? true : false;
                ImageCB_SoBanGhi.Visible = CB_SoBanGhi > 0 ? true : false;

                foreach (DataRow row in dtHoSoCBMoiNhan.Rows)
                {
                    if (row["NgayHetHanXuLy"] != null && row["NgayHetHanXuLy"] != System.DBNull.Value)
                    {
                        DateTime temp = Convert.ToDateTime(row["NgayHetHanXuLy"]);
                        DateTime newDay = new DateTime(temp.Year, temp.Month, temp.Day).AddDays(0 - KhoangThoiGianDelta);

                        if (temp < currentDay)
                        {
                            SoHoSoHetHanCB = SoHoSoHetHanCB + 1;
                            HoSo hoso = ProviderFactory.HoSoProvider.GetById(row["ID"].ToString());
                            if (hoso != null)
                            {
                                hoso.HoSoXuLyDungHan = 0;
                                ProviderFactory.HoSoProvider.Save(hoso);
                            }
                        }
                        else if (newDay < currentDay)
                        {
                            SoHoSoSapHetHanCB = SoHoSoSapHetHanCB + 1;
                        }
                    }
                }
                if (SoHoSoSapHetHanCB > 0)
                {
                    lbSoHoSoSapHetHanCB.Text = String.Format(Resources.Resource.msgSoHoSoCBSapHetHan, SoHoSoSapHetHanCB.ToString());
                }
                lbSoHoSoSapHetHanCB.Visible = SoHoSoSapHetHanCB > 0 ? true : false;
                ImageSoHoSoSapHetHanCB.Visible = SoHoSoSapHetHanCB > 0 ? true : false;

                if (SoHoSoHetHanCB > 0)
                {
                    lbSoHoSoHetHanCB.Text = String.Format(Resources.Resource.msgSoHoSoCBHetHan, SoHoSoHetHanCB.ToString());
                }
                lbSoHoSoHetHanCB.Visible = SoHoSoHetHanCB > 0 ? true : false;
                ImageSoHoSoHetHanCB.Visible = SoHoSoHetHanCB > 0 ? true : false;
            }
            else
            {
                lbCB_SoBanGhi.Visible = false;
                lbSoHoSoSapHetHanCB.Visible = false;
                lbSoHoSoHetHanCB.Visible = false;

                ImageCB_SoBanGhi.Visible = false;
                ImageSoHoSoSapHetHanCB.Visible = false;
                ImageSoHoSoHetHanCB.Visible = false;
            }

            // Thông báo số lượng Giấy Thông báo lệ phí chờ thu phí
            int SoThongBaoPhiChoThuPhi = 0;
            int SoThongBaoPhiChoHuy = 0;
            if (listQuyenThuPhi != null && listQuyenThuPhi.Count > 0)
            {
                DataTable dt = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoPhiChoThuPhi(listQuyenThuPhi, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.TrungTam.Id);
                dt.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoNopTienChoThuPhi(listQuyenThuPhi, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.TrungTam.Id));
                
                DataTable dtChoHuy = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoPhiChoHuy(listQuyenThuPhi, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.TrungTam.Id);
                dtChoHuy.Merge(ProviderFactory.ThongBaoLePhiProvider.GetThongBaoNopTienChoHuy(listQuyenThuPhi, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.TrungTam.Id));
                if ((dt != null && dt.Rows.Count > 0) || (dtChoHuy != null && dtChoHuy.Rows.Count > 0))
                {
                    SoThongBaoPhiChoThuPhi = dt.Rows.Count;
                    SoThongBaoPhiChoHuy = dtChoHuy.Rows.Count;
                }
                if (SoThongBaoPhiChoThuPhi > 0)
                {
                    lbThongBaoPhi.Text = String.Format(Resources.Resource.msgSoThongBaoLePhiChoThuPhi, SoThongBaoPhiChoThuPhi.ToString(), SoThongBaoPhiChoHuy.ToString());
                }
                lbThongBaoPhi.Visible = SoThongBaoPhiChoThuPhi > 0 ? true : false;
                ImageThongBaoPhi.Visible = SoThongBaoPhiChoThuPhi > 0 ? true : false;

                lbThongBaoPhi.Attributes.Add("href", "CN_ThongBaoPhi.aspx?trangthaiid=3");
            }

            // Thông báo số lượng Thông báo nộp tiền lấy mẫu sp và đánh giá qtrình sx cần duyệt
            int SoThongNopTien = 0;
            if (listQuyenPheDuyet != null && listQuyenPheDuyet.Count > 0)
            {
                DataTable dtThongBao = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoNopTienChoPheDuyet(listQuyenPheDuyet, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.TrungTam.Id);

                if (dtThongBao != null && dtThongBao.Rows.Count > 0)
                {
                    SoThongBaoNopTien = dtThongBao.Rows.Count;
                }
                if(SoThongBaoNopTien > 0)
                    lbThongBaoNopTien.Text = "Có " + dtThongBao.Rows.Count + " Thông báo nộp tiền lấy mẫu sản phẩm và đánh giá quy trình sản xuất chờ phê duyệt";
                lbThongBaoNopTien.Visible = SoThongBaoNopTien > 0 ? true : false;
                ImageThongBaoNopTien.Visible = SoThongBaoNopTien > 0 ? true : false;

                lbThongBaoNopTien.Attributes.Add("href", "CN_ThongBaoPhi.aspx?trangthaiid=2");
            }

            // Thông báo số Giấy thông báo lệ phí chờ duyệt huỷ
            int SoThongBaoPhiChoDuyetHuy = 0;
            if (listQuyenPheDuyet != null && listQuyenPheDuyet.Count > 0)
            {
                DataTable dt = ProviderFactory.ThongBaoLePhiProvider.GetThongBaoPhiChoDuyetHuy(listQuyenPheDuyet, ((PageBase)this.Page).mUserInfo.UserID, ((PageBase)this.Page).mUserInfo.TrungTam.Id);

                if (dt != null && dt.Rows.Count > 0)
                {
                    SoThongBaoPhiChoDuyetHuy = dt.Rows.Count;
                }
                if (SoThongBaoPhiChoDuyetHuy > 0)
                {
                    lbThongBaoPhi.Text += " " + String.Format(Resources.Resource.msgSoThongBaoLePhiChoDuyetHuy, SoThongBaoPhiChoDuyetHuy.ToString());
                }

                lbThongBaoPhi.Attributes.Add("href", "CN_ThongBaoPhi.aspx?trangthaiid=6");
            }

            if ((listQuyenThuPhi != null && listQuyenThuPhi.Count > 0) || (listQuyenPheDuyet != null && listQuyenPheDuyet.Count > 0))
            {
                if (SoThongBaoPhiChoDuyetHuy > 0 || SoThongBaoPhiChoHuy > 0 || SoThongBaoPhiChoThuPhi > 0)
                {
                    lbThongBaoPhi.Visible = true;
                    ImageThongBaoPhi.Visible = true;
                }
                else
                {
                    lbThongBaoPhi.Visible = false;
                    ImageThongBaoPhi.Visible = false;
                }
            }
            else
            {
                lbThongBaoPhi.Visible = false;
                ImageThongBaoPhi.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
