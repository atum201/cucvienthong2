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

public partial class WebUI_DB_BaoCaoDongBo1 : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DataTable dtDongBo;
		
        dtDongBo = new Cuc_QLCL.AdapterData.Provider.DongBo().LayDuLieuBaoCao_DongBo();
		gvDongBo.DataSource = dtDongBo;
		gvDongBo.DataBind();
    }
    protected void gvDongBo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDongBo.PageIndex = e.NewPageIndex;
    }
    protected void gvDongBo_Sorting(object sender, GridViewSortEventArgs e)
    {
    }
}
