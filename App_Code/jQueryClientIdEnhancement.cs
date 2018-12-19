using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for jQueryClientIdEnhancement
/// </summary>
public class jQueryClientIdEnhancement
{

    public static void RegisterExtScript(Page page)
    {

        Dictionary<string, string> dct = new Dictionary<string, string>();

        //explore all containers to find all visible webcontrols

        searchContentPlaceHolder(page.Form, dct);

        StringBuilder sb = new StringBuilder();

        foreach (string key in dct.Keys)
        {

            if (sb.Length > 0) sb.Append(",");

            //key = ClientId, value = Id

            sb.AppendFormat("{0}:\"{1}\"", key, dct[key]);

        }

        //build the hashtable 
        string script = @"window.aspNetWebControls = {" + sb.ToString() + "};\n";

        //assign the assistant class name to each web control's HTML element

        script += @"

        if (typeof(jQuery) == 'function' && window.aspNetWebControls) {

            var c = window.aspNetWebControls;

            for (var clientId in c) 

                $('#' + clientId).addClass('_' + c[clientId]);

            var pattern = /##(\w+)/g;

            window.$$ = function( selector, context ) {

                selector = selector.replace(pattern, '._$1');

                return jQuery(selector, context);                        

            }                        

        }            

        ";

        //put the script at the end of form to make sure every webcontrol

        //element is declared

        page.ClientScript.RegisterStartupScript(page.GetType(),

            "jQueryClientIdEnhancement",

            script, true);

    }

    private static void searchContentPlaceHolder(Control ctrl,

        Dictionary<string, string> dct)
    {

        if (ctrl.HasControls())

            foreach (Control c in ctrl.Controls)

                searchContentPlaceHolder(c, dct);

        if (ctrl.Visible)

            dct.Add(ctrl.ClientID, ctrl.ID);

    }

}        
