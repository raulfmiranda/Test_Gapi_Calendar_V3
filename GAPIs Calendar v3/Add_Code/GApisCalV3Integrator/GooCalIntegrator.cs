using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GAPIs_Calendar_v3.Add_Code.CalendarIntegrator;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using System.Web.Configuration;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;

namespace GAPIs_Calendar_v3.Add_Code.GApisCalV3Integrator
{
    public abstract class GooCalIntegrator : ICalIntegrator
    {
        //
        //  Properties

        protected static string
            CLIENT_ID = WebConfigurationManager.AppSettings["GAPICal Client Id"].ToString(),
            CLIENT_SECRET = WebConfigurationManager.AppSettings["GAPICal Client Secret"].ToString(),
            API_KEY = WebConfigurationManager.AppSettings["GAPICal API Key"].ToString(),
            APPLICATION_NAME = WebConfigurationManager.AppSettings["GAPICal Application Name"].ToString(),
            GAPICAL_STORAGE = HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["GAPICal Storage"].ToString());

        protected static GoogleAuthorizationCodeFlow
            CodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = CLIENT_ID,
                ClientSecret = CLIENT_SECRET
            },
            Scopes = new[] { CalendarService.Scope.Calendar },

            //DataStore = new FileDataStore(GAPICAL_STORAGE)
            DataStore = new MyFileDataStore(GAPICAL_STORAGE),
        });

        protected CalendarService CalendarService;

        //
        //  Ctor & init

        //
        //  Inherit methods from Interface

        public abstract object Authorize(HttpRequest Request, string accountName);

        //
        //  Calendar List methods

        public abstract object GetCalendarList();

        public abstract Event InserirEvento(string titulo, DateTime dataInicial, DateTime dataFinal, string calendarId);

        public abstract object GetCalendar(string calendarId);

        public abstract object GetEvents(string calendarId, DateTime? timeMin = null, DateTime? timeMax = null);
    }
}