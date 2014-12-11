using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GAPIs_Calendar_v3.Add_Code.GApisCalV3Integrator;
using System.Web.UI.HtmlControls;
using Google.Apis.Auth.OAuth2;
using System.Web.Configuration;
using Google.Apis.Calendar.v3;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using GAPIs_Calendar_v3.Add_Code.CalendarIntegrator;

namespace GAPIs_Calendar_v3
{
    public partial class Page1 : System.Web.UI.Page
    {
        protected void Page_Init(object s, EventArgs e)
        {
            var clear = Request["clear"];
            if (clear != null)
            {
                Session["GooCalAccountName"] = null;
                Session["GooCalIntegrator"] = null;
                Response.Redirect(Request.Url.LocalPath, true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Session["GooCalAccountName"] = AccountNameInput.Text;
            }

            string accountName = Session["GooCalAccountName"] as string;

            bool accDef = !string.IsNullOrEmpty(accountName);

            InputAccountNamePanel.Visible = !accDef;
            WaitingPanel.Visible = accDef;

            if (accDef)
            {
                GooCalIntegrator integra = new GooCalV3Integrator();
                var result = integra.Authorize(Request, accountName);

                if (result == null)
                {
                    // Alright
                    Session["GooCalIntegrator"] = integra;
                    Response.Redirect("Page2.aspx");
                }
                    else if (result is string)
                {
                    // вернулась строка, это значит нужно выполнить переход на это url
                    Response.Redirect(result as string);
                }
                else if (result is UserRejectException)
                {
                    // пользователь отвергнул приглашение
                    RenderException(new Exception("Пользователь отвергнул приглашение", result as UserRejectException));
                }
                else if (result is Exception)
                {
                    // произошла другая ошибка при обращении к сервису Google Api
                    RenderException(result as Exception);
                }
                else
                {
                    // случилось непредвиденное
                    RenderException(new Exception("Случилось непредвиденное", new Exception("GooCalIntegrator.Authorize вернул другой тип результата")));
                }
            }

            // в случае ошибки, оставляю только ошибку
            if (ErrorPan.Controls.Count > 0)
            {
                InputAccountNamePanel.Visible = false;
                WaitingPanel.Visible = false;

                var panel = new Panel();
                panel.Controls.Add(new HyperLink() { NavigateUrl = "Page1.aspx", Text = "вернуться" });
                ErrorPan.Controls.Add(panel);
            }
        }

        protected void RenderException(Exception ex)
        {
            while (ex != null)
            {
                ErrorPan.Controls.Add(new HtmlGenericControl() { InnerText = "• " + ex.Message });
                ex = ex.InnerException;
            }
        }
    }
}