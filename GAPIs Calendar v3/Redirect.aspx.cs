using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GAPIs_Calendar_v3.Add_Code.GApisCalV3Integrator;
using Google.Apis.Auth.OAuth2.Web;

namespace GAPIs_Calendar_v3
{
    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // страница всегда знает куда нужно отправить в случае если авторизация не требуется
            string redirectIfNoNeed = "Page1.aspx?a=1&b=2&action=return";

            var userId = Session["GapiUserId"] as string;

            // проверка userId
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Не указан userId");
            }

            // пробую авторизовать
            var result = new GooCalV3Integrator().Authorize(Request, userId);
            if (result is AuthorizationCodeWebApp.AuthResult)
            {
                // нужно авторизоваться, проверяю
                Response.Redirect((result as AuthorizationCodeWebApp.AuthResult).RedirectUri,true);
            }
            else
            {
                // авторизация прошла или произошла ошибка, отправляю назад
                Response.Redirect(redirectIfNoNeed, true);
            }
        }
    }
}