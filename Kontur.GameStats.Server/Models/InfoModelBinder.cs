using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;
using Newtonsoft.Json;


namespace Kontur.GameStats.Server.Models
{
    [ModelBinder(typeof(InfoModelBinder))]
    public class Info
    {
        public string name { get; set; }
        public string[] gameModes { get; set; }
    }


    public class InfoModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(Info))
            {
                return false;
            }
            Info result = JsonConvert.DeserializeObject<Info>
               (actionContext.Request.Content.ReadAsStringAsync().Result);
            bindingContext.Model = result;
            return true;
        }

    }
}
