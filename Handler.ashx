<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using Cuc_QLCL.AdapterData;
using Cuc_QLCL.Entities;
using Cuc_QLCL.Data;
public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        String MauDauId = context.Request["MauDauId"];
        MauDauHopQuy MauDau = ProviderFactory.MauDauHopQuyProvider.GetById(MauDauId);
        context.Response.ContentType = "image";
        //context.Response.ContentType = "text/plain";
        byte[] img = (byte[])MauDau.Dau;
        //context.Response.Write(MauDauId);
        context.Response.OutputStream.Write(img, 0, img.Length);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}