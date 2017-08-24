using GAPIs_Calendar_v3.Add_Code.GApisCalV3Integrator;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAPIs_Calendar_v3
{
    public partial class Page4InserirEvento : System.Web.UI.Page
    {
        string AccountName;
        GooCalIntegrator Integrator;
        string selectedCalendarId;

        protected void Page_Load(object sender, EventArgs e)
        {
            AccountName = Session["GapiUserId"] as string;
            Integrator = Session["GooCalIntegrator"] as GooCalIntegrator;
            this.selectedCalendarId = Session["SelectedCalendarId"] as string;

            if (Integrator == null || string.IsNullOrEmpty(AccountName))
            {
                Response.Redirect("Page2.aspx", true);
            }
            AccountNameLabel.InnerText = "Account Name: " + AccountName;
        }

        protected void EventTitleInput_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btInserirEvento_Click(object sender, EventArgs e)
        {
            DateTime dataInicial = DateTime.Now + (new TimeSpan(5, 0, 0, 0));
            DateTime dataFinal = DateTime.Now + (new TimeSpan(6, 2, 0, 0));
            Event ev = Integrator.InserirEvento(EventTitleInput.Text, dataInicial, dataFinal, this.selectedCalendarId);
            string mensagem = "Evento criado: " + ev.HtmlLink;
            this.lblMensagem.Text = mensagem;
        }
    }
}