using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Support
{
    public class UrlUtls
    {
        public static string URI(ControllerBase ctr, string controller, string method, object values)
            => ctr.Url.Action(method, controller.Replace("Controller", ""), values, ctr.Request.Scheme);
    }
}
