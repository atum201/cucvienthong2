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
using Cuc_QLCL.Data;
using System.IO;

public partial class WebUI_MauDauHopQuy : PageBase
{
    String DonViId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["DonViId"] != null)
        {
            DonViId = Request["DonViId"];
            DmDonVi dv = ProviderFactory.DmDonViProvider.GetById(DonViId);
            txtDonVi.Text = dv.TenTiengViet;
            Bind_MauDau();
        }
    }
    public void Bind_MauDau()
    {
        TList<MauDauHopQuy> listMauDau = ProviderFactory.MauDauHopQuyProvider.GetByDonViId(DonViId);
        dtlstMauDau.DataSource = listMauDau;        
        dtlstMauDau.DataBind();
    }
  
    
}
