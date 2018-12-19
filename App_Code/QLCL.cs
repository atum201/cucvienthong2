using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Security;
using System.IO;
using System.ComponentModel;
using System.Web.Configuration;
using Cuc_QLCL.AdapterData.Provider;
using System.Collections.Generic;
/// <summary>
/// Summary description for DTQGKV
/// </summary>
///<Modified>
/// Author      Date        Comments
/// Truongtv    15/09/2008  Create 
///</Modified>>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class QLCL : System.Web.Services.WebService
{

    #region  Các biến
    DongBo objDongBo = new DongBo();

    private wVDC.VDC objVDC = new wVDC.VDC();
    //Members
    private string UploadPath;
    private DTQG_KV.KV_Server oServerRegister;
    //Cái này để Upload dữ liệu
    private AppSrvLib.FileTransferUpload fileUpload;
    //Biến cấu hình lại webconfig của Cục dự trữ quốc gia gọi đến 18 khu vự khác.
    private Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

    #endregion  Các biến

    public QLCL()
    {
        UploadPath = Server.MapPath(ConfigurationManager.AppSettings["UploadFolder"].ToString());


    }


    /// <summary>
    /// Config địa chỉ web của VDC
    /// </summary>
    /// <param name="Http">http://</param>
    ///<Modified>
    /// Author      Date        Commets
    /// Truongtv    18/08/2008  created
    ///</Modified>>
    private void ConfigDiaChiVDC(string Http)
    {
        //truyền địa chỉ web service của khu vực
        config.AppSettings.Settings["wVDC.VDC"].Value = Http;
        //Lưu lại thông tin config đây.
        config.Save(ConfigurationSaveMode.Modified);
    }
    #region Cac phuong thuc upload
    /// <summary>
    /// Get the number of bytes in a file in the Upload folder on the server.
    /// The client needs to know this to know when to stop downloading
    /// </summary>
    /// <Modified>
    ///	Name		         Date		     Comment 
    ///	TrườngTV	      27/06/2008	         Thêm mới
    ///	</Modified>      
    [WebMethod]
    public long GetFileSize(string FileName)
    {
        string FilePath = UploadPath + "\\" + FileName;

        // check that requested file exists
        if (!File.Exists(FilePath))
            CustomSoapException("File not found", "The file " + FilePath + " does not exist");

        return new FileInfo(FilePath).Length;
    }
    /// <summary>
    /// The winforms client needs to know what is the max size of chunk that the server 
    /// will accept.  this is defined by MaxRequestLength, which can be overridden in
    /// web.config.
    /// </summary>
    /// <Modified>
    ///	Name		         Date		     Comment 
    ///	TrườngTV	      27/06/2008	     Modifier   
    ///	</Modified>       
    [WebMethod]
    public long GetMaxRequestLength()
    {
        return (ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection).MaxRequestLength;
    }

    /// <summary>
    /// Throws a soap exception.  It is formatted in a way that is more readable to the client, after being put through the xml serialisation process
    /// Typed exceptions don't work well across web services, so these exceptions are sent in such a way that the client
    /// can determine the 'name' or type of the exception thrown, and any message that went with it, appended after a : character.
    /// </summary>
    /// <param name="exceptionName"></param>
    /// <param name="message"></param>
    /// <Modified>
    ///	Name		         Date		     Comment 
    ///	TrườngTV	      27/06/2008	         Thêm mới
    ///	</Modified>        
    public static void CustomSoapException(string exceptionName, string message)
    {
        throw new System.Web.Services.Protocols.SoapException(exceptionName + ": " + message, new System.Xml.XmlQualifiedName("BufferedUpload"));
    }
    private byte[] AnalyzeUpload(byte[] package)
    {

        oServerRegister = new DTQG_KV.KV_Server(package);
        this.oServerRegister.GetPackageSettoServer(package);

        byte[] buffer = this.oServerRegister.GetDataMesage();
        byte[] byteHash;

        byte[] byteHashValue;
        byteHash = this.oServerRegister.GetDivisionHash();
        //byteHash[0] = 0;
        byteHashValue = this.HashBuffer(buffer);

        string FileName = this.oServerRegister.GetFileName();
        long Offset = long.Parse(this.oServerRegister.GetStringDivisionOffset());
        // so sánh xem dữ liệu có đúng không ?
        if (System.Text.ASCIIEncoding.ASCII.GetString(byteHash) == System.Text.ASCIIEncoding.ASCII.GetString(byteHashValue))
        {

            if (FileName.Substring(0, 7) == "NenFile")
            {
                // UploadPath = Server.MapPath(ConfigurationManager.AppSettings["FileUpload"].ToString());
                //  UploadPath = Server.MapPath(ConfigurationManager.AppSettings["DocFile"].ToString()) + "\\.." + "\\.." + "\\FileUpload";
                UploadPath = Server.MapPath(ConfigurationManager.AppSettings["UploadFolder"].ToString());

            }
            else
            {
                UploadPath = Server.MapPath(ConfigurationManager.AppSettings["UploadFolder"].ToString());
            }

            string FilePath = Path.Combine(UploadPath, FileName);
            if (Offset == 0)	// new file, create an empty file
                File.Create(FilePath).Close();

            // open a file stream and write the buffer.  Don't open with FileMode.Append because the transfer may wish to start a different point
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
            {
                fs.Seek(Offset, SeekOrigin.Begin);
                fs.Write(buffer, 0, buffer.Length);

            }
            //Đoạn này kiểm tra xem đã phải là cuối cùng chưa, nếu đúng thì bắt đầu đồng bộ
            if (oServerRegister.GetStringDivisionRC() == "STOP")
            {
                //Gọi hàm này để đồng bộ dữ liệu lên Cục
                //Truyền đường dẫn để đọc dữ liệu   
                //File.Create(FilePath).Close();


                MakeZip.Unzip(FilePath, UploadPath);
                if (FileName.Substring(0, 7) == "NenFile")
                {
                    string MaTrungTam = FilePath.Substring(FilePath.LastIndexOf("\\")).Replace("\\NenFile__", "").Replace(".zip", "");
                    MaTrungTam = MaTrungTam.Replace("@", "");
                    //this.DongBoGuiFileTheoTrungTam(MaTrungTam);

                }
                else
                {
                    string MaTrungTam = FilePath.Substring(FilePath.LastIndexOf("\\")).Replace("\\LayDuLieu", "").Replace(".zip", "");
                    // this.DongBoQLCL(MaTrungTam);

                }
            }
            this.oServerRegister.SetDivisionRC("GOOD");
            return this.oServerRegister.SetPackagetoClient();
        }
        else
        {
            this.oServerRegister.SetDivisionRC("FALSE");
            return this.oServerRegister.SetPackagetoClient();
        }
    }
    /// <summary>
    /// Append a chunk of bytes to a file.
    /// The client should ensure that all messages are sent in sequence. 
    /// This method always overwrites any existing file with the same name
    /// </summary>
    /// <param name="FileName">The name of the file that this chunk belongs to, e.g. Vista.ISO</param>
    /// <param name="buffer">The byte array, i.e. the chunk being transferred</param>
    /// <param name="Offset">The offset at which to write the buffer to</param>
    /// <Modified>
    ///	Name		         Date		     Comment 
    ///	TrườngTV	      27/06/2008	     Modifier   
    ///	</Modified>       
    [WebMethod]
    public byte[] AppendUpload(byte[] package)
    {
        try
        {
            return AnalyzeUpload(package);
        }
        catch (Exception ex)
        {
            return System.Text.ASCIIEncoding.ASCII.GetBytes(ex.Message.ToCharArray());
        }
    }
    #region file hashing
    /// <summary>
    /// Check File 
    /// </summary>
    /// <param name="FileName">File Name</param>
    /// <returns>string</returns
    /// <Modified>
    ///	Name		         Date		     Comment 
    ///	TrườngTV	      27/06/2008	     Modifier   
    ///	</Modified>       
    [WebMethod]
    public string CheckFileHash(string FileName)
    {
        string FilePath = UploadPath + "\\" + FileName;
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] hash;
        using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
            hash = md5.ComputeHash(fs);
        return BitConverter.ToString(hash);
    }
    [WebMethod]
    public byte[] HashBuffer(byte[] buffer)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] hash;
        hash = md5.ComputeHash(buffer);
        return (hash);
    }
    #endregion
    #endregion Cacs phuong thuc upload

    #region Gửi dữ liệu từ các trung tâm lên VDC
    /// <summary>
    /// Gửi dữ liệu len VDC
    /// </summary>
    ///<Modified>
    /// Author      Date        Commets
    /// Truongtv    18/08/2008  created
    ///</Modified>>    
    public void SendData(string MaTrungTam)
    {
        if (MaTrungTam.Length == 3)
            MaTrungTam = MaTrungTam + "@";
        fileUpload = new AppSrvLib.FileTransferUpload();
        string streAppSet = "";
        streAppSet = Server.MapPath(ConfigurationManager.AppSettings["GhiFile"].ToString()) + "\\" + "LayDuLieu" + MaTrungTam + ".zip";
        //streAppSet = Server.MapPath(ConfigurationManager.AppSettings["FileUpload"].ToString()) + "\\" + "LayDuLieu" + MaTrungTam + ".zip";

        //Không được sử dụng thì dùng OK  
        //string strURLUploadEApplicationSet = System.Configuration.ConfigurationManager.
        //                                                                       AppSettings["EFilingGatewayServer"];
        if (this.fileUpload.IsBusy)
        {
            System.Threading.Thread.Sleep(1000);	// give it a chance to cancel if it is cancelling
            if (this.fileUpload.IsBusy)
                return;
        }
        // fileUpload = new AppSrvLib.FileTransferUpload();
        //fileUpload.ProgressChanged += new ProgressChangedEventHandler(fileUpload_ProgressChanged);
        fileUpload.LocalFilePath = streAppSet;
        fileUpload.AutoSetChunkSize = false;
        fileUpload.ChunkSize = 16 * 1024;
        fileUpload.RunWorkerAsync(0);
        // fileUpload.RunWorkerCompleted += new RunWorkerCompletedEventHandler(fileUpload_RunWorkerCompleted);


    }

    /// <summary>
    /// Gửi dữ liệu xuống khu vực
    /// </summary>
    ///<Modified>
    /// Author      Date        Commets
    /// Truongtv    18/08/2008  created
    ///</Modified>>      
    public void SendDataFileUpload(string MaTrungTam)
    {
        fileUpload = new AppSrvLib.FileTransferUpload();
        string streAppSet = "";
        streAppSet = Server.MapPath(ConfigurationManager.AppSettings["UploadFolder"].ToString()) + "NenFile__" + MaTrungTam + ".zip";

        if (this.fileUpload.IsBusy)
        {
            System.Threading.Thread.Sleep(1000);	// give it a chance to cancel if it is cancelling
            if (this.fileUpload.IsBusy)
                return;
        }
        // fileUpload = new AppSrvLib.FileTransferUpload();
        //fileUpload.ProgressChanged += new ProgressChangedEventHandler(fileUpload_ProgressChanged);
        fileUpload.LocalFilePath = streAppSet;
        fileUpload.AutoSetChunkSize = false;
        fileUpload.ChunkSize = 16 * 1024;
        fileUpload.RunWorkerAsync(0);
        // fileUpload.RunWorkerCompleted += new RunWorkerCompletedEventHandler(fileUpload_RunWorkerCompleted);

    }
    [WebMethod]
    public void DongBoQLCL(string MaTrungTam)
    {
        DataSet dtsDuLieu = new DataSet();
        string streAppSet = Server.MapPath(ConfigurationManager.AppSettings["DocFile"].ToString()) + "\\" + "LayDuLieu" + MaTrungTam + ".xml";
        dtsDuLieu.ReadXml(streAppSet);
        MaTrungTam = MaTrungTam.Replace("@", "");
        if (objDongBo.DongBoDuLieu(dtsDuLieu) == 1)
        {
            objVDC.XoaLichSu_CapNhatNgayDongBoSauCung_VDC(MaTrungTam);
        }
    }

    #endregion Gửi dữ liệu từ các trung tâm lên VDC

    #region Lấy dữ liệu từ VDC -nén thành file zip
    /// <summary>
    /// Đông bộ theo trung tâm 
    /// </summary>
    ///<Modified>
    /// Author      Date        Commets
    /// Truongtv    18/08/2008  created
    ///</Modified>>    
    [WebMethod]
    public bool DongBoGuiDuLieuTheoTrungTam(string MaTrungTam)
    {
        if (objVDC.YeuCauLayDuLieuDongBo(MaTrungTam))
        {

            objDongBo.DongBo_DElETE_DB_LichSuXoa(MaTrungTam);

            this.DongBoGuiFileTheoTrungTam(MaTrungTam);
            System.Threading.Thread.Sleep(1000);
            System.Threading.Thread.Sleep(1000);
            // Lay du lieu VDC 
            if (objVDC.DongBoGuiDuLieuTuVDC(MaTrungTam))
            {
                objDongBo.DongBo_CapNhatNgayDongBoSauCung(MaTrungTam);
                return true;
            }
            else return false;         
            
        }
        else {
            return false;
        
        
        }
    }


    [WebMethod]
    public bool YeuCauLayDuLieuDongBo(string MaTrungTam)
    {
        DataSet dtsDuLieu = objVDC.VDCGuiDuLieu(MaTrungTam);
        if (objDongBo.DongBoDuLieu(dtsDuLieu) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [WebMethod]
    public DataSet QLCLGuiDuLieu(string MaTrungTam)
    {
        DataSet dts = objDongBo.LayDuLieuDongBo_TatCa(MaTrungTam);
        return dts;
    }


    /// <summary>
    /// Đông bộ theo trung tâm 
    /// </summary>
    ///<Modified>
    /// Author      Date        Commets
    /// Truongtv    18/08/2008  created
    ///</Modified>>    
    [WebMethod]
    public void DongBoGuiFileTheoTrungTam(string MaTrungTam)
    {
        if (MaTrungTam.Length == 3)
            MaTrungTam = MaTrungTam + "@";
        // DataSet dtsDuLieu = objDongBo.LayDuLieuDongBo_TaiLieuDinhKem();
        //string strGhiDuLieu = Server.MapPath(ConfigurationManager.AppSettings["FileUpload"].ToString()) + "\\" + "LayDuLieu" + MaTrungTam + ".xml";
        string strGhiDuLieu = Server.MapPath(ConfigurationManager.AppSettings["UploadFolder"].ToString());


        //dtsDuLieu.WriteXml(strGhiDuLieu, XmlWriteMode.WriteSchema);

        MakeZip objZip = new MakeZip();

        objZip.FileZipName = strGhiDuLieu + "NenFile__" + MaTrungTam + ".zip";
        DataTable dtbTaialieuDinhKem = objDongBo.LayDuLieuDongBo_TaiLieuDinhKem(MaTrungTam);
        int count = dtbTaialieuDinhKem.Rows.Count;
        string[] str = new string[count];
        int iCount = 0;
        for (int i = 0; i < count; i++)
        {

            string strTemp = dtbTaialieuDinhKem.Rows[i]["TenFile"].ToString();
            if (strTemp.LastIndexOf("/") > 0)
            {
                strTemp = strTemp.Substring(strTemp.LastIndexOf("/") + 1);
            }

            if (System.IO.File.Exists((strGhiDuLieu + "\\" + strTemp)))
            {

                str[iCount] = strGhiDuLieu + "\\" + strTemp;
                iCount++;
            }
        }
        string[] str1 = new string[iCount];
        for (int i = 0; i < iCount; i++)
        {
            str1[i] = str[i];
        }

        objZip.arrZipFilesName = str1;
        objZip.MakeZip1();
        //objZip.ZipFilesName = strGhiDuLieu.Replace(".xml", "") + ".zip";      
        //this.ConfigDiaChiKhuVuc("truyen duong dan vao ");         
        this.SendDataFileUpload(MaTrungTam);
        //fileUpload.RunWorkerCompleted += new RunWorkerCompletedEventHandler(fileUpload_RunWorkerCompleted);         


    }
    
    [WebMethod]


    public void XoaLichSu_CapNhatNgayDongBoSauCung_QLCL(string MaTrungTam)
    {
        try
        {
            objDongBo.DongBo_DElETE_DB_LichSuXoa(MaTrungTam);

            this.DongBoGuiFileTheoTrungTam(MaTrungTam);
            objDongBo.DongBo_CapNhatNgayDongBoSauCung(MaTrungTam);
            objDongBo.BaoCao_DongBo_ThemMoi(MaTrungTam, true);
        }
        catch (Exception ex)
        {
            objDongBo.BaoCao_DongBo_ThemMoi(MaTrungTam, false);
            throw ex;
        }

    }


    #endregion Lấy dữ liệu từ VDC



}

