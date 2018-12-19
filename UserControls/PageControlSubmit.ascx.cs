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

public partial class UserControls_PageControlSubmit : System.Web.UI.UserControl
{
    public int pPageSize = 10;
    public int pPageRecordCount = 100;
    public int pCurrentPage = 1;
    public int pDisplayAfterPageCount = 5;
    public int pDisplayBeforePageCount = 5;

    public string pPageQueryString = string.Empty;
    public string pImagesPath = string.Empty;
    Random rndNum = new Random(unchecked((int)DateTime.Now.Ticks));

    public event EventHandler ChangPageIndex;
    /// <summary>
    /// sinh mot chuoi ngau nhieu voi tham so la do dai va tien to
    /// </summary>
    /// <param name="preFix"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    private string GenRamdomString(string preFix, int length)
    {
        string sVal = preFix;
        int numI;
        for (int i = 0; i < length; i++)
        {
            numI = rndNum.Next(33, 127);
            while (checkPunc(numI)) { numI = rndNum.Next(33, 127); }

            sVal = sVal + Convert.ToChar(numI);
        }

        return sVal;
    }
    /// <summary>
    /// kiem tra do dai cua chuoi ngau nhien
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private bool checkPunc(int num)
    {
        if ((num >= 33) && (num <= 47)) { return true; }
        if ((num >= 58) && (num <= 64)) { return true; }
        if ((num >= 91) && (num <= 96)) { return true; }
        if ((num >= 123) && (num <= 126)) { return true; }

        return false;
    }
    /// <summary>
    /// build mot link button
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    private LinkButton buildButton(int pageNumber)
    {
        LinkButton lnk = new LinkButton();
        lnk.ID = "lnkbutpagecontrolsubmit_" + pageNumber.ToString() + GenRamdomString("_", 16);
        lnk.Text = pageNumber.ToString();

        lnk.CausesValidation = false;
        
        if (ChangPageIndex != null)
            ChangPageIndex(pageNumber, new EventArgs());
        return lnk;
    }
    /// <summary>
    /// build mot link button
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    private LinkButton buildButton(int pageNumber, string imgSrc)
    {
        HtmlImage img = new HtmlImage();
        img.Border = 0;
        img.Src = imgSrc;

        LinkButton lnk = new LinkButton();
        lnk.ID = "lnkbutpagecontrolsubmit_" + pageNumber.ToString() + GenRamdomString("_", 16);
        lnk.Controls.Add(img);
        lnk.CausesValidation = false;
        //lnk.Command += new CommandEventHandler(
        //ibn.Command += new CommandEventHandler(this.btn_Click);
        return lnk;

    }
    public void build()
    {
        if (pPageQueryString.Equals(string.Empty))
        {
            pPageQueryString = "page";
        }
        int pPageCount = pPageRecordCount / pPageSize + ((pPageRecordCount % pPageSize > 0) ? 1 : 0);
        if (pCurrentPage > pPageCount) pCurrentPage = pPageCount;
        if (pCurrentPage < 1) pCurrentPage = 1;
        int startcount = (((pCurrentPage - pDisplayBeforePageCount) < 1) ? (1) : (pCurrentPage - pDisplayBeforePageCount));
        int endcount = (((pCurrentPage + pDisplayAfterPageCount) > pPageCount) ? (pPageCount) : (pCurrentPage + pDisplayAfterPageCount));
        if (startcount == 1)
        {
            endcount = startcount + pDisplayBeforePageCount + pDisplayAfterPageCount;
            endcount = (endcount > pPageCount) ? pPageCount : endcount;
        }
        if (endcount == pPageCount)
        {
            startcount = endcount - pDisplayBeforePageCount - pDisplayAfterPageCount;
            startcount = (startcount < 1) ? 1 : startcount;
        }
        tdPaging.Controls.Clear();
        HtmlGenericControl span;
        LinkButton lnk;
        for (int i = startcount; i <= endcount; i++)
        {
            lnk = buildButton(i);
            if (i != pCurrentPage)
            {
                lnk.CssClass = "Paging";
            }
            else
            {
                lnk.CssClass = "PagingSelected";
            }
            tdPaging.Controls.Add(lnk);
            if (i < endcount)
            {
                span = new HtmlGenericControl("span");
                span.Attributes.Add("class", "PagingSeperator");
                span.InnerHtml = " | ";
                tdPaging.Controls.Add(span);
            }
        }
        int currentP = (Session["currentpage"] == null ? 1 : Convert.ToInt32(Session["currentpage"]));
        ctlTotalPage.InnerHtml = currentP.ToString() + "/" + pPageCount.ToString();

        if (pCurrentPage == 1)
        {
            tdLeft.Visible = false;
            tdFirst.Visible = false;
        }
        else
        {
            tdLeft.Visible = true;
            tdFirst.Visible = true;
            tdLeft.Controls.Clear();
            tdLeft.Controls.Add(buildButton(pCurrentPage - 1, pImagesPath + "left.gif"));
            tdFirst.Controls.Clear();
            tdFirst.Controls.Add(buildButton(1, pImagesPath + "first.gif"));
        }
        if (pCurrentPage == pPageCount)
        {
            tdRight.Visible = false;
            tdLast.Visible = false;
        }
        else
        {
            tdRight.Visible = true;
            tdLast.Visible = true;
            tdRight.Controls.Clear();
            tdRight.Controls.Add(buildButton(pCurrentPage + 1, pImagesPath + "right.gif"));
            tdLast.Controls.Clear();
            tdLast.Controls.Add(buildButton(pPageCount, pImagesPath + "last.gif"));
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
