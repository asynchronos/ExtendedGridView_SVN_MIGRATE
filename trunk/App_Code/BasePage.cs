using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    /// <summary>
    ///     Alert a client-side message
    /// </summary>
    /// <param name="message"></param>
    protected void Alert(string message)
    {
        StringBuilder sbMessage = new StringBuilder();
        sbMessage.Append("function AlertMe(msg)");
        sbMessage.Append("\r\n");
        sbMessage.Append("{");
        sbMessage.Append("\t");
        sbMessage.AppendFormat("alert(\"{0}\");", message);
        sbMessage.Append("\r\n");
        sbMessage.Append("}");

        if (!Page.ClientScript.IsStartupScriptRegistered("AlertScript"))
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "AlertScript", sbMessage.ToString(), true);
        }
    }
}
