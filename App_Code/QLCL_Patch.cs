using Cuc_QLCL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

/// <summary>
/// Summary description for QLCL_Patch
/// </summary>
public static class QLCL_Patch
{
    public  enum LoaiPatch { FileAttach_CoQuanDoKiem, FileAttach_DonVi_Nop_HoSo, LePhi_HoSoChungNhan, NguoiKy_GiayBaoPhi, IDLink };
    public static string connectString = ConfigurationManager.ConnectionStrings["Cuc_QLCL.Data.ConnectionString"].ConnectionString;
    public enum LePhi { DonGiaQuyChuan1 = 950, DonGiaQuyChuan2 = 600 , DonGiaTiepNhan = 300, PhiXemXet = 1350, PhiDonGia = 750};
    public static bool InsertToSql(String query)
    {
        try{
            SqlConnection conn = new SqlConnection(connectString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteScalar();
            conn.Close();
        }catch{
            return false;
        }
        return true;
    }

    public static bool SetFileAttach_DonVi_Nop_HoSo(string idDvNopHoSo,string file)
    {
        return InsertToSql("INSERT INTO [dbo].[QLCL_Patch](ID,Type,FileAttach) VALUES ('"+idDvNopHoSo+"',"+(int)LoaiPatch.FileAttach_DonVi_Nop_HoSo+",'"+file+"')");
    }
    public static bool SetFileAttach_CoQuanDoKiem(string idCoQuanDoKiem, string file)
    {
        return InsertToSql("INSERT INTO [dbo].[QLCL_Patch](ID,Type,FileAttach) VALUES ('" + idCoQuanDoKiem + "'," + (int)LoaiPatch.FileAttach_CoQuanDoKiem + ",'" + file + "')");
    }

    public static bool SetLePhi_HoSoChungNhan(string idHoSo, int LePhi)
    {
        return InsertToSql("INSERT INTO [dbo].[QLCL_Patch](ID,Type,LePhi) VALUES ('" + idHoSo + "'," + (int)LoaiPatch.LePhi_HoSoChungNhan + "," + LePhi + "))");
    }

    public static bool Delete_SanPham(string SanPhamID) {
        return InsertToSql("delete from ThongBaoLePhi_SanPham where SanPhamID='" + SanPhamID + "';" +
                           "delete from QuaTrinhXuLy where SanPhamID='" + SanPhamID + "';" +
                           "delete from SanPham_TieuChuanApDung where SanPhamID='"+SanPhamID+"';" +
                           "delete from TaiLieuDinhKem where SanPhamID='" + SanPhamID + "';" +
                           "delete from SanPham where ID='" + SanPhamID + "';");
    }

    public static bool Delete_HoSo(String HoSoID) {
        return InsertToSql("delete from CB_QuaTrinhXuLy where HoSoID='" + HoSoID + "';" +
                            "delete from TaiLieuDinhKemHoSo where HoSoID='" + HoSoID + "';" +
                            "delete from PhanCong where HoSoID='" + HoSoID + "';" +
                            "delete from ThongBaoLePhi_SanPham where SanPhamID in (select ID from SanPham where HoSoID='" + HoSoID + "');" +
                            "delete from QuaTrinhXuLy where SanPhamID in (select ID from SanPham where HoSoID='" + HoSoID + "');" +
                            "delete from SanPham_TieuChuanApDung where SanPhamID in (select ID from SanPham where HoSoID='" + HoSoID + "');" +
                            "delete from ThongBaoLePhiChiTiet where ThongBaoLePhiID in (select ID from ThongBaoLePhi where HoSoId='" + HoSoID + "');" +
                            "delete from SanPham where HoSoID='" + HoSoID + "';" +
                            "delete from ThongBaoLePhi where HoSoId='" + HoSoID + "';" +
                            "delete from HoSo where ID='" + HoSoID + "';");
    }

    public static bool SetNguoiKy_GiayBaoPhi(string idHoSoChungNhan, string nguoiKy,int SLTiepNhan, int SLXemXet)
    {
        string query = 
        "begin tran if exists (select * from QLCL_Patch where ID='" + idHoSoChungNhan + "' and [type]=" + (int)LoaiPatch.NguoiKy_GiayBaoPhi + ") " +
        "begin update QLCL_Patch set NguoiKy='" + nguoiKy + "', SLTiepNhan="+SLTiepNhan+" , SLXemXet="+SLXemXet+" where ID='" + idHoSoChungNhan + "' and [Type]=" + (int)LoaiPatch.NguoiKy_GiayBaoPhi +
        " end else begin " +
            "insert into QLCL_Patch (ID, [Type],NguoiKy,SLTiepNhan,SLXemXet) " +
            "values ('" + idHoSoChungNhan + "', " + (int)LoaiPatch.NguoiKy_GiayBaoPhi + ",'" + nguoiKy + "'," + SLTiepNhan + ","+SLXemXet+") end commit tran";
        return InsertToSql(query);
    }

    public static bool SetIDLink(string id, string idLink)
    {
        return InsertToSql("INSERT INTO [dbo].[QLCL_Patch](ID,Type,IDLink) VALUES ('" + id + "'," + (int)LoaiPatch.IDLink + ",'" + idLink + "')");
    }

    public static List<String> GetFileAttach_DonVi_Nop_HoSo(string idDvNopHoSo)
    {
        List<String> result = new List<string>();
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT FileAttach FROM QLCL_Patch WHERE Type=" + (int)LoaiPatch.FileAttach_DonVi_Nop_HoSo + " AND ID='" + idDvNopHoSo + "'"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow row = dt.Rows[i];

                            if (row[0] != DBNull.Value)
                            {
                                result.Add(row[0].ToString());
                            }
                            
                        }
                        return result;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return result;
    }

    public static List<String> GetFileAttach_CoQuanDoKiem_Nop_HoSo(string idCoQuanDoKiem)
    {
        List<String> result = new List<string>();
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT FileAttach FROM QLCL_Patch WHERE Type=" + (int)LoaiPatch.FileAttach_CoQuanDoKiem + " AND ID='" + idCoQuanDoKiem + "'"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow row = dt.Rows[i];

                            if (row[0] != DBNull.Value)
                            {
                                result.Add(row[0].ToString());
                            }

                        }
                        return result;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return result;
    }

    public static int GetLePhi_HoSo(string idpHoSo)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT LePhi FROM QLCL_Patch WHERE Type=" + (int)LoaiPatch.LePhi_HoSoChungNhan + " AND ID='" + idpHoSo + "'"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                            return int.Parse(dt.Rows[0][0].ToString());
                        return 0;
                    }
                }
            }
        }
        catch
        {
            return 0;
        }
    }

    public static string GetNguoiKyGiayBaoPhi(string idHoSo)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT NguoiKy FROM QLCL_Patch WHERE Type=" + (int)LoaiPatch.NguoiKy_GiayBaoPhi + " AND ID='" + idHoSo + "'"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                            return dt.Rows[0][0].ToString();
                        return string.Empty;
                    }
                }
            }
        }
        catch
        {
            return string.Empty;
        }
    }

    public static DataTable GetTTGiayBaoPhi(string idHoSo)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM QLCL_Patch WHERE Type=" + (int)LoaiPatch.NguoiKy_GiayBaoPhi + " AND ID='" + idHoSo + "'"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        catch
        {
            return null;
        }
    }

    public static DataRow GetTBPhiTiepNhan(string idHoSo)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from ThongBaoLePhi where HoSoId='" + idHoSo + "' and LoaiPhiID=9"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if(dt!=null && dt.Rows.Count > 0)
                            return dt.Rows[0];
                    }
                }
            }
        }
        catch
        {
            return null;
        }
        return null;
    }

    public static DataTable GetAllSanPhamByHoSo(string idHoso) { 
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT 
		                                                     SP.ID 
		                                                    ,DM_SP.TenTiengViet
		                                                    ,DM_SP.TenTiengAnh
		                                                    ,SP.KyHieu
		                                                    ,DM_SP.MaSanPham
		                                                    ,DM_NHOM_SP.MaNhom AS MaNhomSanPham
		                                                    ,SP.TrangThaiID
		                                                    ,DM_HSX.TenHangSanXuat
		                                                    ,TTSP.MoTa
	                                                    FROM  dbo.SanPham AS SP
		                                                    INNER JOIN DM_SANPHAM AS DM_SP ON DM_SP.ID = SP.SanPhamID
		                                                    INNER JOIN dbo.DM_NhomSanPham AS DM_NHOM_SP ON DM_NHOM_SP.ID = SP.NhomSanPhamID
		                                                    INNER JOIN dm_hangsanxuat AS DM_HSX ON DM_HSX.ID = SP.HangSanXuatID
		                                                    INNER JOIN dbo.EN_TrangThaiSanPham AS TTSP ON TTSP.ID = SP.TrangThaiID
	                                                    WHERE 
		                                                    SP.HoSoID = '" + idHoso + "'"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        catch
        {
            return null;
        }
        return null;
    }

    public static bool CheckSoGiayThongBaoPhiExist(string sogiaythongbao)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select * from ThongBaoLePhi where SoGiayThongBaoLePhi='" + sogiaythongbao + "'"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                            return true;
                    }
                }
            }
        }
        catch
        {
            return false;
        }
        return false;
    }

    public static DataTable GetTBPhiTiepNhans(string trangthai)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select t.*, d.TenTiengViet, d.DiaChi, d.DienThoai, d.Fax from ThongBaoLePhi t join DM_DonVi d on t.DonViID=d.ID  where t.LoaiPhiId=9"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                            return dt;
                    }
                }
            }
        }
        catch
        {
            return null;
        }
        return null;
    }
    public static bool UpdateThongBaoPhiTiepNhan(String IDHoSo,String IDNguoiPheDuyet,int TongPhi, String TenNguoiPheDuyet)
    {
        string query ="UPDATE ThongBaoLePhi "+
                      "SET TongPhi = "+TongPhi+", NguoiPheDuyetID='"+IDNguoiPheDuyet+"', TenNguoiKyDuyet='"+TenNguoiPheDuyet+"' "+
                      "WHERE HoSoId='"+IDHoSo+"' and LoaiPhiID=9";
        return InsertToSql(query);
    }
    public static string GetIDLink(string idHoSo)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT IDLink FROM QLCL_Patch WHERE Type=" + (int)LoaiPatch.IDLink + " AND ID='" + idHoSo + "'"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                            return dt.Rows[0][0].ToString();
                        return string.Empty;
                    }
                }
            }
        }
        catch
        {
            return string.Empty;
        }
    }
    public static Boolean DeleteFile(string filename)
    {
        try
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {

        }
        return false;
    }

    public static DataTable GetDSNguoiKyGiayBaoPhi()
    {
        try
        {
            // roleID: 'TT2_150','TT2_155','TT2_153': Giám đốc, Phó Giám Đốc, Trưởng phòng chứng nhận
            using (SqlConnection con = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand("select u.ID,u.FullName,u.Position,r.RoleName from Sys_User u join Sys_User_Role ur on u.ID=ur.UserID join Sys_Role r on ur.RoleID=r.ID where  r.ID in ('TT2_150','TT2_155','TT2_153') order by u.Position"))
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
        catch
        {
            return null;
        }
    }

    public static string GetChucVuKy(String ChucVu)
    {
        switch (ChucVu)
        {
            case "1":
                ChucVu = "GIÁM ĐỐC";
                break;
            case "2":
                ChucVu = "KT. GIÁM ĐỐC" + "\n" + "PHÓ GIÁM ĐỐC";
                break;
            case "3":
                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "TRƯỞNG PHÒNG CHỨNG NHẬN";
                break;
            case "4":
                ChucVu = "TUQ. GIÁM ĐỐC" + "\n" + "PHÓ PHÒNG CHỨNG NHẬN";
                break;
            case "5":
                ChucVu = "CHUYÊN VIÊN";
                break;
        }
        return ChucVu;
    }
    public enum EnTrangThaiThongBaoPhiList
    {
        [EnumTextValue("Mới tạo")]
        MOI_TAO = 1,
        [EnumTextValue("Chờ phê duyệt")]
        CHO_PHE_DUYET = 2,
        [EnumTextValue("Chờ thu phí")]
        CHO_THU_PHI = 3,
        [EnumTextValue("Đã thu phí")]
        DA_THU_PHI = 4,
        [EnumTextValue("Huỷ")]
        HUY = 5,
        [EnumTextValue("Chờ giám đốc phê duyệt huỷ ")]
        CHO_DUYET_HUY = 6,
        [EnumTextValue("Giám đốc không phê duyệt việc huỷ giấy báo lệ phí")]
        GD_KHONG_DUYET = 7,
        [EnumTextValue("Giám đốc phê duyệt việc huỷ giấy báo lệ phí")]
        GD_DUYET_HUY = 8,
        [EnumTextValue("Giám đốc không phê duyệt")]
        GD_KHONG_PHE_DUYET = 9,
    }
    public static string GetTrangThaiThongBaoPhi(int trangthai)
    {
        string result = string.Empty;
        switch (trangthai)
        {
            case (int)EnTrangThaiThongBaoPhiList.MOI_TAO:
                result = "Mới tạo";
                break;
            case (int)EnTrangThaiThongBaoPhiList.CHO_PHE_DUYET:
                result = "Chờ phê duyệt";
                break;
            case (int)EnTrangThaiThongBaoPhiList.CHO_THU_PHI:
                result = "Chờ thu phí";
                break;
            case (int)EnTrangThaiThongBaoPhiList.DA_THU_PHI:
                result = "Đã thu phí";
                break;
            case (int)EnTrangThaiThongBaoPhiList.HUY:
                result = "Hủy";
                break;
            case (int)EnTrangThaiThongBaoPhiList.CHO_DUYET_HUY:
                result = "Chờ giám đốc phê duyệt huỷ";
                break;
            case (int)EnTrangThaiThongBaoPhiList.GD_KHONG_DUYET:
                result = "Giám đốc không phê duyệt việc huỷ giấy báo lệ phí";
                break;
            case (int)EnTrangThaiThongBaoPhiList.GD_KHONG_PHE_DUYET:
                result = "Giám đốc phê duyệt việc huỷ giấy báo lệ phí";
                break;
            case (int)EnTrangThaiThongBaoPhiList.GD_DUYET_HUY:
                result = "Giám đốc không phê duyệt";
                break;
            default:
                break;
        }
        return result;
    }
    public static string GetTrangThaiHoSo(int trangthai)
    {
        string result = string.Empty;
        switch (trangthai){ 
            case (int) EnTrangThaiHoSoList.CHO_LUU_TRU:
                result = "Chờ lưu trữ";
                break;
            case (int)EnTrangThaiHoSoList.CHO_PHAN_CONG:
                result = "Chờ phân công";
                break;
            case (int)EnTrangThaiHoSoList.CHO_PHE_DUYET:
                result = "Chờ phê duyệt";
                break;
            case (int)EnTrangThaiHoSoList.CHO_THU_PHI:
                result = "Chờ thu phí";
                break;
            case (int) EnTrangThaiHoSoList.CHO_THAM_DINH:
                result = "Chờ thẩm định";
                break;
            case (int)EnTrangThaiHoSoList.CHO_XU_LY:
                result = "Chờ xử lý";
                break;
            case (int)EnTrangThaiHoSoList.DA_CAP_BTN:
                result = "Đã cấp bản tiếp nhận công bố hợp quy";
                break;
            case (int)EnTrangThaiHoSoList.DA_DONG:
                result = "Đã lưu trữ";
                break;
            case (int)EnTrangThaiHoSoList.DANG_XU_LY:
                result = "Đang xử lý";
                break;
            case (int)EnTrangThaiHoSoList.GD_KHONG_DUYET:
                result = "Giám đốc không phê duyệt";
                break;
            case (int)EnTrangThaiHoSoList.HO_SO_MOI:
                result = "Hồ sơ mới";
                break;
            case (int)EnTrangThaiHoSoList.HUY:
                result = "Hủy hồ sơ";
                break;
            case (int)EnTrangThaiHoSoList.THAM_DINH_DONG_Y:
                result = "Thẩm định đồng ý";
                break;
            case (int)EnTrangThaiHoSoList.THAM_DINH_KHONG_DONG_Y:
                result = "Thẩm định không đồng ý";
                break;
            default:
                break;
        }
        return result;
    }

}