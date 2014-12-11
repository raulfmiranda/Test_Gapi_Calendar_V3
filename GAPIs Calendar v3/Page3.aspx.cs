using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GAPIs_Calendar_v3.Add_Code.GApisCalV3Integrator;
using Google.Apis.Calendar.v3.Data;
using System.Web.UI.HtmlControls;

namespace GAPIs_Calendar_v3
{
    public partial class Page3 : System.Web.UI.Page
    {
        string AccountName;
        GooCalIntegrator Integrator;
        CalendarListEntry CalendarEntry;

        List<Event> CalendarEvents;

        int EventsTableIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            AccountName = Session["GooCalAccountName"] as string;
            Integrator = Session["GooCalIntegrator"] as GooCalIntegrator;
            var selectedCalendarId = Session["SelectedCalendarId"] as string;

            // проверка
            if (Integrator == null || string.IsNullOrEmpty(AccountName) || string.IsNullOrEmpty(selectedCalendarId))
            {
                Response.Redirect("Page2.aspx", true);
                return;
            }

            // пробую получить календарь
            var result = Integrator.GetCalendar(selectedCalendarId);
            if (result is Exception)
                RenderException(result as Exception);

            CalendarEntry = result as CalendarListEntry;

            result = Integrator.GetEvents(selectedCalendarId);
            if (result is Exception)
                RenderException(result as Exception);

            CalendarEvents = result as List<Event>;

            AccountNameLabel.InnerText = "Пользователь: " + AccountName;
            CalendarDescriptionLabel.InnerText = string.Format("Календарь: {0} ({1} событий)", CalendarEntry.Summary, CalendarEvents.Count);

            EventsTableIndex = 0;
            EventsTableRepeater.DataSource = CalendarEvents;
        }

        protected override void OnPreRender(EventArgs e)
        {
            DataBind();
        }

        protected void RenderException(Exception ex)
        {
            while (ex != null)
            {
                ErrorPan.Controls.Add(new HtmlGenericControl() { InnerText = "• " + ex.Message });
                ex = ex.InnerException;
            }
        }

        protected int IncIndex()
        {
            return ++EventsTableIndex;
        }

        protected string GetAttendees(object attendees)
        {
            if (attendees == null)
                return string.Empty;

            var list = (attendees as List<EventAttendee>);

            List<string> items = (from a in list
                                  select string.Format("{0} ({1})", a.DisplayName, a.Email)).ToList();
            return string.Join("<br/>", items);
        }

        protected string GetCreator(object creator)
        {
            if (creator == null)
                return string.Empty;
            else
                return string.Format("{0} ({1})", (creator as Event.CreatorData).DisplayName, (creator as Event.CreatorData).Email);
        }

        protected string GetExtendedProperties(object ep)
        {
            if (ep == null)
                return string.Empty;

            var data = (ep as Event.ExtendedPropertiesData);

            var lines = new List<string>();

            if (data.Private != null)
            {
                lines.Add("Private:");

                (from pair in data.Private
                 select pair).ToList().ForEach(pair => lines.Add(pair.Value));
            }

            if (data.Shared != null)
            {
                lines.Add("Shared:");

                (from pair in data.Shared
                 select pair).ToList().ForEach(pair => lines.Add(pair.Value));
            }

            return string.Join("<br/>", lines);
        }

        protected string GadgetData(object gadget)
        {
            if (gadget == null)
                return string.Empty;

            var data = gadget as Event.GadgetData;

            var lines = new List<string>();

            lines.Add(string.Format("Display={0}, Height={1}, IconLink={2}, Link={3}, Title={4}, Type={5}, Width={6}", data.Display, data.Height, data.IconLink, data.Link, data.Title, data.Type, data.Width));

            if (data.Preferences != null)
            {
                lines.Add("Preferences:");

                (from pair in data.Preferences
                 select pair).ToList().ForEach(pair => lines.Add(pair.Key + "=" + pair.Value));
            }

            return string.Join("<br/>", lines);
        }

        protected string GetOrganizerData(object organizer)
        {
            if (organizer == null)
                return string.Empty;

            var data = (organizer as Event.OrganizerData);

            return string.Format("{0} {1} {2} {3}", data.Id, data.DisplayName, data.Email, data.Self);
        }

        protected string GetRemindersData(object reminders)
        {
            if (reminders == null)
                return string.Empty;

            var data = (reminders as Event.RemindersData);

            var lines = new List<string>();

            lines.Add(string.Format("UseDefault: {0}", data.UseDefault));

            if (data.Overrides != null)
            {
                lines.Add("Overrides:");

                foreach (var rem in data.Overrides)
                {
                    lines.Add(string.Format("ETag={0}, Method={1}, Minutes={2}", rem.ETag, rem.Method, rem.Minutes));
                }
            }

            return string.Join("<br/>", lines);
        }

        protected string GetRecurrence(object recurrence)
        {
            if (recurrence == null)
                return string.Empty;

            var data = recurrence as List<string>;

            return string.Join("<br/>", data);
        }
    }
}