using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Profile;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Security.Cryptography;
using Cuc_QLCL.AdapterData;
/// <summary>
/// Lớp Base dùng chung khai báo các phương thức, thuộc tình
/// </summary>
/// <Modified>
/// Người tạo              Ngày tạo                Chú thích
/// GiangUM                 7/5/2009                Tạo mới
/// </Modified>
public class PageBase : Page
{
    /// <summary>
    /// Thông tin về người dùng đăng nhập hệ thống
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// GiangUM                08/05/2009                Tạo mới
    /// </Modified>
    public UserInfo mUserInfo
    {
        get
        {
            UserInfo _mUserInfo = Session["User"] as UserInfo;
            if (_mUserInfo == null)
                _mUserInfo = new UserInfo();
            return _mUserInfo;
        }
    }
    /// <summary>
    /// Khởi tọa các tham số event của trang
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// GiangUM                08/05/2009                Tạo mới
    /// </Modified>
    public PageBase()
    {
        UICulture = "vi-VN";
        Culture = "vi-VN";
        Page.ClientScript.RegisterHiddenField("hidIdPrefix", ClientID + "_");
        //this.PreLoad += new EventHandler(PageBase_PreLoad);
        //this.PreRender += new EventHandler(PageBase_PreRender);
        this.Error += new EventHandler(this.PageBase_Error);

    }



    void PageBase_PreRender(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Phương thức trước khi Load của hệ thống check khởi tạo thông tin User
    /// </summary>
    /// <Modified>
    /// Người tạo              Ngày tạo                Chú thích
    /// GiangUM                08/05/2009                Tạo mới
    /// </Modified>
    void PageBase_PreLoad(object sender, EventArgs e)
    {

    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!mUserInfo.IsAuthenticated)
        {
            Response.Redirect("~/WebUI/HT_DangNhap.aspx", true);
        }
        //gán thông tin user vào context user
        Context.User = mUserInfo;
    }
    protected void PageBase_Error(object sender, EventArgs e)
    {
        Exception currentError = new Exception();
        currentError = Server.GetLastError();
        LogEntry logEntry = new LogEntry();
        logEntry.Message = "Page: " + this.Page.Title + Environment.NewLine + currentError.Message.ToString();
        logEntry.Categories.Clear();
        Logger.Write(logEntry);
        ShowError(currentError);
        Server.ClearError();
    }

    public static void ShowError(Exception currentError)
    {
        HttpContext context = HttpContext.Current;

        context.Response.Write("<link rel=\"stylesheet\" href=\"/Archive/Styles.css\">" +
            "<h2>Lỗi</h2><hr/>" +
            "Đã có lỗi xảy ra trên trang này." +
            "Chương trình đã thông báo tới quản trị hệ thống của bạn.<br/>" +
            "<br/><b>Lỗi đã xảy ra ở:</b>" +
            "<pre>" + context.Request.Url.ToString() + "</pre>" +
            "<br/><b>Thông báo lỗi:</b>" +
            "<pre>" + currentError.Message.ToString() + "</pre>" +
            "<br/><b>Các lỗi:</b>" +
            "<pre>" + currentError.ToString() + "</pre>");
    }

    /// <summary>
    /// Mã hóa password
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv       6/5/2009  Thêm mới
    /// </Modified>
    protected string EncryptPassword(string Password)
    {
        System.Text.UnicodeEncoding encoding = new System.Text.UnicodeEncoding();
        byte[] hashBytes = encoding.GetBytes(Password);

        //Compute the SHA-1 hash
        SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

        byte[] cryptPassword = sha1.ComputeHash(hashBytes);

        return BitConverter.ToString(cryptPassword);
    }
    /// <summary>
    /// Chuyển từ định dạng số sang chuỗi dạng curency
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv       6/5/2009  Thêm mới
    /// </Modified>
    protected string FormatCurency(long num)
    {
        return String.Format("{0:0,0}", num);
    }
    /// <summary>
    /// Chuyển từ chuỗi dạng curency sang định dạng số 
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv     6/5/2009     Thêm mới
    /// </Modified>
    protected int UnFormatCurency(string num)
    {
        string strReturn=num.Replace(".","");
        try
        {
            return int.Parse(strReturn);
        }
        catch
        {
            return 0;
        }
    }
    #region Thông báo
    /// <summary>
    /// Thông báo
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv       6/5/2009  Thêm mới
    /// </Modified>
    protected void Thong_bao(object Page, string message)
    {
        message = message.Replace("'", @"\'");
        string strMessage = String.Format(@"<script language = 'javascript'>alert('" + message + "');</script>");
        ((System.Web.UI.Page)Page).RegisterClientScriptBlock("Validate", strMessage);
    }
    /// <summary>
    /// Thông báo
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv       6/5/2009  Thêm mới
    /// </Modified>
    protected void Thong_bao(object Page, string message, string urlPageBack)
    {
        message = message.Replace("'", @"\'");
        urlPageBack = '\"' + urlPageBack + '\"'; 
        string strMessage = String.Format(@"<script language = 'javascript'>alert('" + message + "');"
                + "document.location.href ="+urlPageBack+";"
                + "</script>");

        ((System.Web.UI.Page)Page).RegisterClientScriptBlock("Validate",
                strMessage);
    }
    /// <summary>
    /// Thông báo
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv       6/5/2009  Thêm mới
    /// </Modified>
    protected void Thong_bao(object Page, string message, bool blCondition)
    {
        if (blCondition)
        {
            message = message.Replace("'", @"\'");
            string strMessage = String.Format(@"<script language = 'javascript'>alert('" + message + "');"
                    + "self.close();"
                + "</script>");

            ((System.Web.UI.Page)Page).RegisterClientScriptBlock("Validate",
                strMessage);
        }
    }
    /// <summary>
    /// Thông báo
    /// </summary>
    /// <Modified>
    /// Name        Date        Comment
    /// Hùngnv       6/5/2009  Thêm mới
    /// </Modified>
    protected void Thong_bao(string message)
    {
        message = message.Replace("'", @"\'");
        string strMessage = String.Format(@"<script language = 'javascript'>alert('" + message + "');</script>");
        ((System.Web.UI.Page)this.Page).RegisterClientScriptBlock("Validate", strMessage);
    }
    protected void Thong_bao_khang_dinh(object Page, string message, string urlPageBack)
    {
        message = message.Replace("'", @"\'");
        urlPageBack = '\"' + urlPageBack + '\"';
        string strMessage = String.Format(@"<script language = 'javascript'>if (confirm('" + message + "'))"
                + "document.location.href('" + urlPageBack + "');"
                + "</script>");

        ((System.Web.UI.Page)Page).RegisterClientScriptBlock("Validate", strMessage);
    }
    #endregion

    /// <summary>
    /// Thay đổi trạng thái textbox thành readonly, chuyển sang style readonly
    /// </summary>
    /// <param name="TextBox">Text box cần chuyển</param>
    /// <Modified>
    /// Name        Date        Comment
    /// LamDS       16/01/2009  Thêm mới
    /// </Modified>
    protected void SetReadOnly(TextBox TextBox)
    {
        TextBox.ReadOnly = true;
        TextBox.CssClass = "input_readonly";
    }
}
