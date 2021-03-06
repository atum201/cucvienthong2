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
using Cuc_QLCL.Entities;

public partial class WebUI_DieuKienInBaoCao : PageBase
{
    public String strLoaiBaoCao = "";
    private string strHoSoID = string.Empty;
    private string strTenBaoCao = string.Empty;
    private string strSanPhamId = string.Empty;
    public void Page_Load(object sender, EventArgs e)
    {
        ChonDinhDangBaoCao.Attributes["onchange"] = string.Format("ChonLoaiBaoCao()");
        if (Request["LoaiBaoCao"] != null)
        {
            strLoaiBaoCao = Request["LoaiBaoCao"].ToString();
        }
        if (Request["SanPhamId"] != null)
        {
            strSanPhamId = Request["SanPhamId"].ToString();
        }
        if (Request["HoSoID"] != null)
            strHoSoID = Request["HoSoID"].ToString();
        if (!Page.IsPostBack)
        {
            DinhDangBaoCao();
            AnHienControl();
        }
    }
    #region Định dạng báo cáo
    private void DinhDangBaoCao()
    {
        ChonDinhDangBaoCao.Items.Clear();
        ChonDinhDangBaoCao.Items.Add(new ListItem("Word", "Word"));
        ChonDinhDangBaoCao.Items.Add(new ListItem("PDF", "PDF"));
        if (strLoaiBaoCao.Contains("BaoCao"))
            ChonDinhDangBaoCao.Items.Add(new ListItem("Excel", "Excel"));
        ChonDinhDangBaoCao.Items.Add(new ListItem("Crystal report", "CR"));
    }

    public String LayDinhDangXuatBaoCao()
    {
        return this.ChonDinhDangBaoCao.SelectedValue;
    }
    #endregion

    #region Ẩn hiện control
    private void AnHienControl()
    {
        if (strLoaiBaoCao == "PhieuNhanHoSo" || strLoaiBaoCao == "CBPhieuNhanHoSo" || strLoaiBaoCao == "CBPhieuNhanHoSo"+QLCL_Patch.duoiCBNK)
        {
            NhanInThongTinMatKhau.Visible = true;
            InThongTinMatKhau.Visible = true;
        }
        else
        {
            NhanInThongTinMatKhau.Visible = false;
            InThongTinMatKhau.Visible = false;
        }
    }
    #endregion

    #region In báo cáo
    private void OpenNewWindow(string url)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "NoticeBox", String.Format("<script>window.open('{0}','BaoCao','height=600,width=720,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes');</script>", url));
    }

    public void btnPrint_Click(object sender, EventArgs e)
    {
        String strFormat = LayDinhDangXuatBaoCao();       
        if (strLoaiBaoCao == "GiayChungNhan")
        {           
            strTenBaoCao = "Giay_Chung_Nhan";
            Response.Redirect("./HienBaoCao.aspx?HoSoID=" + strHoSoID + "&SanPhamId=" + strSanPhamId + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat);            
        }
            // LongHH
        else if (strLoaiBaoCao == "LePhiTiepNhanCNHQ")
        {
            strTenBaoCao = "Le_Phi_Tiep_NhanCNHQ";
            Response.Redirect("./HienBaoCao.aspx?HoSoID=" + strHoSoID + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=Excel" );
        }
            // LongHH
        else if (strLoaiBaoCao == "PhieuDanhGia")
        {           
            strTenBaoCao = "Phieu_Danh_Gia";
            Response.Redirect("./HienBaoCao.aspx?HoSoID=" + strHoSoID + "&SanPhamId=" + strSanPhamId + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat);
        }
        else if (strLoaiBaoCao == "BanTiepNhan")
        {            
            strTenBaoCao = "Ban_Tiep_Nhan";
            string SanPhamId = Request["SanPhamId"] != null ? Request["SanPhamId"].ToString() : string.Empty;
            Response.Redirect("./HienBaoCao.aspx?HoSoID=" + strHoSoID + "&SanPhamId=" + SanPhamId + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat);
        }
        else if (strLoaiBaoCao == "PhieuNhanHoSo")
        {         
            strTenBaoCao = "Phieu_Tiep_Nhan";
            Response.Redirect("./HienBaoCao.aspx?HoSoID=" + strHoSoID + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat + "&InThongTinTruyCap=" + (InThongTinMatKhau.Checked == true ? "1" : "0"));
        }
        else if (strLoaiBaoCao == "CBPhieuNhanHoSo")
        {           
            strTenBaoCao = "Phieu_Nhan_Ho_So";
            Response.Redirect("./HienBaoCao.aspx?HoSoID=" + strHoSoID + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat + "&InThongTinTruyCap=" + (InThongTinMatKhau.Checked == true ? "1" : "0"));
        }
        else if (strLoaiBaoCao == "CBPhieuNhanHoSo"+QLCL_Patch.duoiCBNK+"")
        {
            strTenBaoCao = "Phieu_Nhan_Ho_So_"+QLCL_Patch.duoiCBNK;
            Response.Redirect("./HienBaoCao.aspx?HoSoID=" + strHoSoID + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat + "&InThongTinTruyCap=" + (InThongTinMatKhau.Checked == true ? "1" : "0"));
        }
        else if (strLoaiBaoCao == "CBPhieuNhanHoSo" + EntityHelper.GetEnumTextValue(QLCL_Patch.DuoiHoSo.CongBo_MiemKiemTra) + "")
        {
            strTenBaoCao = "Phieu_Nhan_Ho_So_" + EntityHelper.GetEnumTextValue(QLCL_Patch.DuoiHoSo.CongBo_MiemKiemTra);
            Response.Redirect("./HienBaoCao.aspx?HoSoID=" + strHoSoID + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat + "&InThongTinTruyCap=" + (InThongTinMatKhau.Checked == true ? "1" : "0"));
        }
        else if (strLoaiBaoCao == "BaoCaoTuan")
        {           
            strTenBaoCao = "Bao_Cao_Tuan";
            if (CheckValue(Request["type"]) && CheckValue(Request["trungtam"])
                && CheckValue(Request["tu"]) && CheckValue(Request["den"]))
            {
                Response.Redirect("./HienBaoCao.aspx?type=" + Request["type"].ToString() + "&trungtam=" + Request["trungtam"].ToString()
                + "&tu=" + Request["tu"].ToString() + "&den=" + Request["den"].ToString());
            }
        }
        else if (strLoaiBaoCao == "BaoCaoQuy")
        {            
            strTenBaoCao = "BaoCaoQuy";
            if (CheckValue(Request["type"]) && CheckValue(Request["trungtam"])
                && CheckValue(Request["tu"]) && CheckValue(Request["den"]))
            {
                Response.Redirect("./HienBaoCao.aspx?type=" + Request["type"].ToString() + "&trungtam=" + Request["trungtam"].ToString()
                + "&tu=" + Request["tu"].ToString() + "&den=" + Request["den"].ToString());
            }
        }
        else if (strLoaiBaoCao == "BaoCaoThongKe")
        {           
            strTenBaoCao = "BaoCaoThongKe";
            if (CheckValue(Request["chuyenvien"])
                && CheckValue(Request["tu"]) && CheckValue(Request["den"]))
            {
                Response.Redirect("./HienBaoCao.aspx?chuyenvien=" + Request["chuyenvien"].ToString() + "&tu=" + Request["tu"].ToString()
                                        + "&den=" + Request["den"].ToString() + "&TrungTamID=" + Request["trungtam"].ToString()
                                        + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat);
            }
        }
        else if (strLoaiBaoCao == "CBPhieuDanhGia")
        {           
            strTenBaoCao = "CBPhieu_Danh_Gia";
            string SanPhamId = Request["SanPhamId"].ToString();
            Response.Redirect("./HienBaoCao.aspx?HoSoID=" + strHoSoID + "&SanPhamId=" + SanPhamId + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat);
        }        
        else if (strLoaiBaoCao == "ThongBaoLePhi")
        {
            string ThongBaoLePhiID = "";      
            strTenBaoCao = "ThongBaoLePhi";
            if (Request["ThongBaoLePhiID"] != null)
            {
                ThongBaoLePhiID = Request["ThongBaoLePhiID"].ToString();
            }
            string action = "";
            if (Request["action"] != null)
            {
                action = Request["action"].ToString();
            }
            Response.Redirect("../WebUI/TestBaoCao.aspx?ThongBaoLePhiID=" + ThongBaoLePhiID + "&action = " + action);
        }
        else if (strLoaiBaoCao == "ThongBaoNopTien")
        {
            strTenBaoCao = "Thong_Bao_Nop_Tien";
            string strThongBaoLePhiId = Request["ThongBaoLePhiID"].ToString();
            Response.Redirect("./HienBaoCao.aspx?ThongBaoLePhiID=" + strThongBaoLePhiId + "&LoaiBaoCao=" + strLoaiBaoCao + "&TenBaoCao=" + strTenBaoCao + "&format=" + strFormat);
        }
    }

    /// <summary>
    /// Kiểm tra giá trị truyền vào để in báo cáo
    /// LinhNM
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private bool CheckValue(object obj)
    {
        if (obj == null)
            return false;
        if (obj.ToString() == "")
            return false;
        return true;
    }
    #endregion
}
