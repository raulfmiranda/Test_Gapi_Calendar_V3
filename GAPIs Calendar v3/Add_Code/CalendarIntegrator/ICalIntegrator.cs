using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAPIs_Calendar_v3.Add_Code.CalendarIntegrator
{
    public interface ICalIntegrator
    {
        object Authorize(HttpRequest Request, string accountName);
    }
}