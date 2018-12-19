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
using System.IO;
using System.Web.Configuration;
using ICSharpCode.SharpZipLib.Zip;
using Cuc_QLCL.Entities;
using Cuc_QLCL.AdapterData;


public partial class WebUI_DB_DongBoTuFile : PageBase
{
    /// <summary>
    /// Load trang
    /// </summary>
    /// <param name="MaTrungTam"></param>
    /// <modified>
    /// Author      Date        comment
    /// TrườngTV    ???         Tạo mới
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DataTable dtbDuongDan = new Cuc_QLCL.AdapterData.Provider.DongBo().LayDuongDanDongBo_TatCa();
            ddlTrungTamChungNhan.DataValueField = "MaTrungTam";
            ddlTrungTamChungNhan.DataTextField = "TenTrungTam";
            ddlTrungTamChungNhan.DataSource = dtbDuongDan;
            lblKetQuaDongBo.Visible = false;
            ddlTrungTamChungNhan.DataBind();
        }
    }
    protected void btnChonFile_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Xuất file dữ liệu đồng bộ theo trung tâm
    /// </summary>
    /// <param name="MaTrungTam"></param>
    /// <modified>
    /// Author      Date        comment
    /// TrườngTV    ???         Tạo mới
    /// TuấnVM      17/11/2009  Sửa: Thay đổi cách thức nén file
    /// </modified>
    public void DongBoGuiDuLieuTheoTrungTam(string MaTrungTam)
    {
        string TenThuMucLuuFile = ConfigurationManager.AppSettings["DuLieuDongBoFolder"].ToString();
        string DuongDan = Server.MapPath(string.Empty).Replace("WebUI", TenThuMucLuuFile);

        // Lấy thư mục chứa file dữ liệu xuất ra
        string tempDirectory = DuongDan + "\\Output";
        Cuc_QLCL.AdapterData.Provider.DongBo objDongBo = new Cuc_QLCL.AdapterData.Provider.DongBo();
        DataSet dtsDuLieu = objDongBo.LayDuLieuDongBo_TatCa(MaTrungTam);

        //string strGhiDuLieu =  ddlODia.Text  + "LayDuLieu" + MaTrungTam + ".xml";
        string streAppSet = tempDirectory + "\\" + "LayDuLieu" + MaTrungTam + ".xml";

        // Ghi dữ liệu ra file trong thư mục tạm
        dtsDuLieu.WriteXml(streAppSet, XmlWriteMode.WriteSchema);

        string TenFile = DuongDan + "\\LayDuLieu" + MaTrungTam + ".qlcl";
        // Nén file dữ liệu đồng bộ vào 1 file "TenFile"
        DongGoi(tempDirectory, TenFile);

        
        // Đẩy file đã nén ra trình duyệt cho người dùng download or open
        Response.Clear();
        Response.AppendHeader("Content-Type", "application/vnd.ms-excel");
        Response.AppendHeader("Content-disposition", "attachment; filename=" + "LayDuLieu" + MaTrungTam + ".qlcl");
        Response.WriteFile(TenFile);
        Response.Flush();

        // Xoá file dữ liệu ban đầu
        FileInfo xmlFile = new FileInfo(streAppSet);
        xmlFile.Attributes = FileAttributes.Normal;
        xmlFile.Delete();

        // Xoá file nén
        FileInfo zipFile = new FileInfo(TenFile);
        zipFile.Attributes = FileAttributes.Normal;
        zipFile.Delete();

        Response.End();
    }

    /// <summary>
    /// Xuất dữ liệu đồng bộ ra file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Author      Date        comment
    /// TruongTV    ????        Tạo mới
    /// </modified>
    protected void btnXuatFile_Click(object sender, EventArgs e)
    {
        DongBoGuiDuLieuTheoTrungTam(ddlTrungTamChungNhan.SelectedValue.ToString());
    }

    /// <summary>
    /// Đồng bộ dữ liệu từ file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <modified>
    /// Author      Date        comment
    /// TruongTV    ????        Tạo mới
    /// TuấnVM      17/11/2009  Sửa: Fix lỗi upload file, giải nén
    /// </modified>
    protected void btnDongBo_Click(object sender, EventArgs e)
    {
        if (!CheckFileExtension(fileUploadDuLieuDongBo))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Thông báo"
                  , "<script>alert('Chỉ cho phép file có định dạng *.qlcl')</script>");
            return;
        }
        string MaTrungTam = ddlTrungTamChungNhan.SelectedValue.ToString();

        string TenThuMucLuuFile = ConfigurationManager.AppSettings["DuLieuDongBoFolder"].ToString();
        string DuongDan = Server.MapPath(string.Empty).Replace("WebUI", TenThuMucLuuFile);

        // Lấy thư mục chứa file dữ liệu đồng bộ
        string tempDirectory = DuongDan + "\\Input";

        DataSet dtsDuLieu = new DataSet();

        string strName = System.IO.Path.GetFileName(fileUploadDuLieuDongBo.PostedFile.FileName);
        string FilePath = DuongDan + "\\" + strName;

        fileUploadDuLieuDongBo.SaveAs(FilePath);

        // Thực hiện giải nén
        GiaiNen(FilePath, tempDirectory);

        DirectoryInfo dirInfo = new DirectoryInfo(tempDirectory);
        string TenFileDuLieu = string.Empty;
        foreach(FileInfo file in  dirInfo.GetFiles())
        {
            TenFileDuLieu = file.FullName;
        }
        dtsDuLieu.ReadXml(TenFileDuLieu);
        Cuc_QLCL.AdapterData.Provider.DongBo objDongBo = new Cuc_QLCL.AdapterData.Provider.DongBo();

        DbBaoCaoDongBo objBaoCao = new DbBaoCaoDongBo();
        try
        {

            objDongBo.DongBoDuLieu(dtsDuLieu);
            lblKetQuaDongBo.Visible = true;
            lblKetQuaDongBo.Text = "Đồng bộ thành công ";
            objBaoCao.TrangThai = 1;   
        }
        catch (Exception ex)
        {
            lblKetQuaDongBo.Visible = true;
            lblKetQuaDongBo.Text = "Đồng bộ không thành công";
            objBaoCao.TrangThai = 0;   
        }
        objBaoCao.MaTrungTam = ddlTrungTamChungNhan.SelectedValue;
        ProviderFactory.DbBaoCaoDongBoProvider.Save(objBaoCao);
        // Xoá file dữ nén
        FileInfo zipFile = new FileInfo(FilePath);
        zipFile.Attributes = FileAttributes.Normal;
        zipFile.Delete();

        // Xoá file dữ liệu đồng bộ
        FileInfo xmlFile = new FileInfo(TenFileDuLieu);
        xmlFile.Attributes = FileAttributes.Normal;
        xmlFile.Delete();

    }

    #region Các phương thức nén, giải nén thư mục
    /// <summary>
    /// Nén các file dữ liệu đồng bộ thành 1 file
    /// </summary>
    /// <param name="strThuMucDongGoi"></param>
    /// <param name="strDuongDanFileXuat"></param>
    /// <modified>
    /// Author          Date        comment
    /// TuấnVM          02/11/2009  Tạo mới    
    /// </modified> 
    private void DongGoi(string strThuMucDongGoi, string strDuongDanFileXuat)
    {
        // Kiểm tra thư mục đóng gói chứa dữ liệu
        DirectoryInfo dirInfo = new DirectoryInfo(strThuMucDongGoi);
        if (!dirInfo.Exists)
            throw new Exception("Thư mục chứa dữ liệu cần đóng gói không tồn tại");

        // Nếu có một file có từ trước trùng tên thi xóa file đó đi
        if (File.Exists(strDuongDanFileXuat))
        {
            FileInfo fileInfo = new FileInfo(strDuongDanFileXuat);
            fileInfo.Attributes = FileAttributes.Normal;
            fileInfo.Delete();
        }

        // Nén thư mục cần đóng gói và tạo file xuất
        ICSharpCode.SharpZipLib.Zip.FastZip zipLib = new ICSharpCode.SharpZipLib.Zip.FastZip();
        zipLib.CreateZip(strDuongDanFileXuat, strThuMucDongGoi, true, null);
        //XoaThuMuc(strThuMucDongGoi);

    }

    /// <summary>
    /// Xóa thư mục
    /// </summary>
    /// <param name="strThuMuc">Đường dẫn thư mục cần xóa</param>
    private void XoaThuMuc(string strThuMuc)
    {
        DirectoryInfo dirInfoThuMuc = new DirectoryInfo(strThuMuc);
        
        if (!dirInfoThuMuc.Exists)
            throw new Exception("Thư mục cần xóa không tồn tại.");
        DatThuocTinhNormal(strThuMuc);
        dirInfoThuMuc.Delete(true);

    }

    /// <summary>
    /// Đặt thuộc tính normal cho tất cả các file trong thư mục
    /// </summary>
    /// <param name="strThuMuc"></param>
    private void DatThuocTinhNormal(string strThuMuc)
    {
        if (!Directory.Exists(strThuMuc)) return;
        DirectoryInfo dirThuMucGoc = new DirectoryInfo(strThuMuc);

        // Đặt thuộc tính normal cho tất cả các file trong thư mục gốc
        foreach (FileInfo fileInfo in dirThuMucGoc.GetFiles())
        {
            fileInfo.Attributes = FileAttributes.Normal;
        }

        // Đệ quy để đặt thuộc tính normal cho tất cả các file trong thư mục con
        foreach (DirectoryInfo dirThuMucCon in dirThuMucGoc.GetDirectories())
        {
            DatThuocTinhNormal(dirThuMucCon.FullName);
        }
    }

    /// <summary>
    /// Giải nén file dữ liệu nhập vào ra một thư mục tạm, trả về tên thư mục chứa các file đã giải nén
    /// </summary>
    /// <param name="strDuongDanFileNen">Đường dẫn file bị nén</param>
    /// <returns>Đường dẫn thư mục giải nén</returns>
    private void GiaiNen(string strDuongDanFileNen, string strThuMucGiaiNen)
    {
        // Lấy tên file dữ liệu bị nén
        string strTenFile = Path.GetFileNameWithoutExtension(strDuongDanFileNen);

        // Nếu thư mục giải nén chưa tồn tại thì tạo mới thư mục này
        if (!Directory.Exists(strThuMucGiaiNen))
            Directory.CreateDirectory(strThuMucGiaiNen);

        // Giải nén
        try
        {
            ICSharpCode.SharpZipLib.Zip.FastZip zipLib = new ICSharpCode.SharpZipLib.Zip.FastZip();
            zipLib.ExtractZip(strDuongDanFileNen, strThuMucGiaiNen, string.Empty);
        }
        catch
        {
            throw new Exception("Định dạng file dữ liệu nhập vào không hợp lệ.");
        }
    }

    /// <summary>
    /// kiểm tra loại file
    /// </summary>
    /// <param name="fileUP"></param>
    /// <param name="strFileName"></param>
    /// <returns></returns>
    private bool CheckFileExtension(FileUpload fileUP)
    {
        if (fileUP.HasFile)
        {
            String fileExtension =
                System.IO.Path.GetExtension(fileUP.FileName).ToLower();
            String[] allowedExtensions ={ ".qlcl" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    return true;
                }
            }
        }
        return false;
    }
    #endregion
}
