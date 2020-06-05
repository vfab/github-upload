using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        public Model.ToDoItem[] Items = new Model.ToDoItem[] { };

        private static string backendDNSName = $"{Environment.GetEnvironmentVariable("ToDoServiceName")}";
        private static Uri backendUrl = new Uri($"http://{backendDNSName}:{Environment.GetEnvironmentVariable("ApiHostPort")}/api/todo");

        public void OnGet()
        {
            HttpClient client = new HttpClient();

            var variables = Environment.GetEnvironmentVariables();

            using (HttpResponseMessage response = client.GetAsync(backendUrl).GetAwaiter().GetResult())
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Items = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.ToDoItem[]>(response.Content.ReadAsStringAsync().Result);
                }
            }
        }
    }
}
