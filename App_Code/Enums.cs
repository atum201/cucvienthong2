using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using System.Text;
namespace CucQLCL.Common
{
    /// <summary>
    /// Danh mục kỳ sao lưu: ngày/tuần/tháng
    /// Dùng chung với đồng bộ
    /// </summary>
    public enum LoaiSaoLuu
    {
        Ngay = 4,
        Tuan = 8,
        Thang = 16
    }
    /// <summary>
    /// Danh mục các ngày trong tuần
    /// </summary>
    public enum NgayTrongTuan
    {
        ChuNhat = 1,
        ThuHai = 2,
        ThuBa = 4,
        ThuTu = 8,
        ThuNam = 16,
        ThuSau = 32,
        ThuBay = 64
    }
    /// <summary>
    /// enum loại hồ sơ
    /// </summary>
    /// <Modified>
    /// Name         Date        Comment
    /// Quannm       9/05/2009   Thêm mới
    /// </Modified>
    public enum LoaiHoSo
    {
        ChungNhanHopQuy = 1,
        CongBoHopQuy = 2,
        ChungNhanHopChuan = 3
    }

    /// <summary>
    /// enum loại giay thu phi
    /// </summary>
    /// <Modified>
    /// Name         Date        Comment
    /// TuanVM       9/05/2009   Thêm mới
    /// </Modified>
    public enum LoaiGiayThuPhi
    {
        LePhiChungNhanHopQuy = 1,
        LePhiChungNhanHopChuan = 2,
        PhiLayMauDanhGiaQuyTrinh = 3,
        PhiTiepNhanCongBoHopQuy = 4
    }
    /// <summary>
    /// enum trạng thái sản phẩm
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv       25/04/2009  Thêm mới
    /// </Modified>
    public enum TrangThaiSanPham
    {
        ChoXuLy = 0, ChoThamDinh = 1, ThamDinhDongY = 2, ThamDinhKhongDongY = 3, ChoPheDuyet = 4, KhongPheDuyet = 5, DaPheDuyet = 6, HoSoMoi = 7, ChoPhanCong = 8, ChoLuuTru = 9
    }
    /// <summary>
    /// enum trạng thái thong bao le phi
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv       25/04/2009  Thêm mới
    /// </Modified>
    public enum TrangThaiThongBaoLePhi
    {
        MoiTao = 0
    }
    /// <summary>
    /// Giá trị xác định người dùng thuộc vụ thẩm định hay vụ chuyên ngành
    /// </summary>
    /// <Modified>
    /// Name        Date            Comment
    /// TuanVM      08/02/2009     Tạo mới
    /// </Modified>
    public class SpecialDepartmentIdOfDIAS
    {
        public static string DIAS = "0";
    }

    /// <summary>
    /// Enum loại hành động trong một chức năng
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv    20/01/2009  Thêm mới
    /// </Modified>
    public enum Action
    {
        Insert = 1, Update = 2, Delete = 3, Null = 4
    }

    /// <summary>
    /// Enum về các loại thông báo
    /// </summary>
    public enum MessageType
    {
        Successful = 0, Warning = 1, Error = 2, Question = 3
    }

     /// <summary>
    /// Danh sách quyền trong CSDL
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Phuongnx     30/12/2008  Thêm mới
    /// TuanVM       02/02/2009     Sửa danh sách quyền
    /// </Modified>
    public class LoaiTieuChuanApDung
    {
        public static string TieuChuanBatBuocApDung = "A";
        public static string TieuChuanKhongBatBuocApDung = "B";
        public static string TieuChuanTuDangKy = "C";
    }
    /// <summary>
    /// Danh sách quyền trong CSDL
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Phuongnx     30/12/2008  Thêm mới
    /// TuanVM       02/02/2009     Sửa danh sách quyền
    /// </Modified>
    public class ConfigInfor
    {
        public static string DEFAULT_LANGUAGE = "DEFAULT_LANGUAGE";

        // Ldap server
        public static string LDAP_SERVER = "LDAP_SERVER";
        public static string LDAP_DOMAIN = "LDAP_DOMAIN";
        public static string LDAP_USER = "LDAP_USER";
        public static string LDAP_PWD = "LDAP_PWD";

        //Config file server
        public static string FILE_SERVER = "FILE_SERVER";
        public static string FILE_SERVER_USER = "FILE_SERVER_USER";
        public static string FILE_SERVER_PWD = "FILE_SERVER_PWD";
        public static string FILE_SERVER_FOLDER = "FILE_SERVER_FOLDER";
        //Mail server

        public static string SMTP_HOST = "SMTP_HOST";
        public static string SMTP_PORT = "SMTP_PORT";
        public static string MAIL_SENDER = "MAIL_SENDER";
        public static string SENDER_NAME = "SENDER_NAME";
        public static string SENDER_PWD = "SENDER_PWD";

        // Backup config
        public static string BACKUP_TYPE = "BACKUP_TYPE";
        public static string BACKUP_TIME = "BACKUP_TIME";
        public static string BACKUP_DAY = "BACKUP_DAY";
        public static string BACKUP_PATH = "BACKUP_PATH";
        public static string BACKUP_NO = "BACKUP_NO";
        // Dồng bộ config
        public static string SYNC_TYPE = "SYNC_TYPE";
        public static string SYNC_TIME = "SYNC_TIME";
        public static string SYNC_DAY = "SYNC_DAY";
        
        // Admin user
        public static string ADMIN_USER = "ADMIN_USER";

        // Extension file
        public static string FILTER_EXTENSION = "FILTER_EXTENSION";
        public static string CMR_WEBSERVICE = "CMR_WEBSERVICE";
    }

    /// <summary>
    /// Danh sách quyền trong CSDL
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Phuongnx     30/12/2008  Thêm mới
    /// TuanVM       02/02/2009     Sửa danh sách quyền
    /// </Modified>
    public class PermissionList
    {
        public static string System = "01";

        public static string SystemUserManagenment = "0101";
        public static string User_Insert = "010101";
        public static string User_Update = "010102";
        public static string User_Delete = "010103";
        public static string SystemRoleManagement = "0102";
        public static string Role_Insert = "010201";
        public static string Role_Update = "010202";
        public static string Role_Delete = "010203";
        public static string SystemLogManagement = "0103";
        public static string Log_Delete = "010301";
        public static string Log_Update = "010302";
        public static string SynchronousConfig = "0104";
        public static string Backup = "0105";
        public static string Restore = "0106";

        public static string CategoriesManagerment = "02";

        public static string IndicatorsManagement = "0201";
        public static string MeasureManagement = "0202";
        public static string UnitManagement = "0203";
        public static string PeriodManagement = "0204";
        public static string OrganizationManagement = "0205";
        public static string ProvincesManagement = "0206";
        public static string LineAgencyManagement = "0207";
        public static string LineAgencyTypeManagement = "0208";
        public static string DocTypeManagement = "0209";
        public static string ProjectProblemsManagement = "0210";
        public static string FundManagement = "0211";
        public static string SectorsManagements = "0212";
        public static string SpecializedDepartmentManagement = "0213";
        public static string CategoriesExport = "0214";

        public static string Template = "03";
        public static string TemplateManagement = "0301";
        public static string Template_Insert = "030101";
        public static string Template_Update = "030102";
        public static string Template_Delete = "030103";
        public static string Template_ExportToExcel = "030104";
        public static string SendTemplate_ProjectManagerUnit = "030105";
        public static string SendTemplate_Lines_Agencies = "030106";

        public static string Process1_Management = "04";
        public static string Process1_ProjectsManagement = "0401";
        public static string Process1_Projects_Insert = "040101";
        public static string Process1_Projects_Update = "040102";
        public static string Process1_Projects_Delete = "040103";
        public static string Process1_Projects_Print = "040104";
        public static string Process1_Projects_ExportToExcel = "040105";
        public static string Process1_Projects_ImportFromFile = "040106";

        public static string Process1_SpecializedDepartmentReportManagement = "0402";
        public static string Process1_SpecializedDepartmentReport_Insert = "040201";
        public static string Process1_SpecializedDepartmentReport_Update = "040202";
        public static string Process1_SpecializedDepartmentReport_Delete = "040203";
        public static string Process1_SpecializedDepartmentReport_Search = "040204";
        public static string Process1_DataOutput = "0403";
        public static string Process1_DataOutput_Insert = "040301";
        public static string Process1_DataOutput_Update = "040302";
        public static string Process1_DataOutput_Output = "040303";
        public static string Process1_DataOutput_ExportToExcel = "040304";
        public static string Process1_DataOutput_Delete = "040305";
        public static string Process1_ReportConfig = "0404";
        public static string Process1_ReportSearch = "0405";
        public static string Process1_FixReport = "0406";
        public static string Process1_SendReport = "040601";
        public static string Process1_ProjectProblems = "040602";

        public static string Process2_Management = "05";
        public static string GeneralReportManagement = "0501";
        public static string GeneralReport_Insert = "050101";
        public static string GeneralReport_Update = "050102";
        public static string GeneralReport_Delete = "050103";
        public static string GeneralReport_InsertData = "050104";
        public static string GeneralReport_ExportToExcel = "050105";
        public static string Process2_DataOutput = "0502";
        public static string Process2_DataOutput_Insert = "050201";
        public static string Process2_DataOutput_Update = "050202";
        public static string Process2_DataOutput_Output = "050203";
        public static string Process2_DataOutput_ExportToExcel = "050204";
        public static string Process2_DataOutput_Delete = "050205";
        public static string Process2_ReportConfig = "0503";
        public static string Process2_FixReport = "0504";
        public static string Process2_SendReport = "050401";
    }

}
