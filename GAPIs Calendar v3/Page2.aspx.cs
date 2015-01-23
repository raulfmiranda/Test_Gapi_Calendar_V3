using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GAPIs_Calendar_v3.Add_Code.GApisCalV3Integrator;
using GAPIs_Calendar_v3.Add_Code.CalendarIntegrator;
using System.Threading;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using System.Web.UI.HtmlControls;

namespace GAPIs_Calendar_v3
{
    public partial class Page2 : System.Web.UI.Page
    {
        string AccountName;
        GooCalIntegrator Integrator;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountName = Session["GapiUserId"] as string;
            Integrator = Session["GooCalIntegrator"] as GooCalIntegrator;

            if (Integrator == null || string.IsNullOrEmpty(AccountName))
            {
                Response.Redirect("Page1.aspx", true);
            }

            AccountNameLabel.InnerText = "Account Name: " + AccountName;
        }

        protected override void OnPreRender(EventArgs e)
        {
            DataBind();
        }

        protected void AccountCalendarsList_OnDataBound(object sender, EventArgs events)
        {
            var result = Integrator.GetCalendarList();
            if (result is Exception)
                RenderException(result as Exception);
            else
            {
                AccountCalendarsList.Items.Clear();

                AccountCalendarsList.Items.Add(new ListItem("Choose calendar"));

                var calList = result as CalendarList;

                calList.Items.ToList().ForEach(cal =>
                {
                    AccountCalendarsList.Items.Add(new ListItem(cal.Summary, cal.Id));
                });

            }

        }

        protected void AccountCalendarsList_OnSelectedIndexChanged(object sender, EventArgs events)
        {
            Session["SelectedCalendarId"] = AccountCalendarsList.SelectedValue;

            Response.Redirect("Page3.aspx", true);
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