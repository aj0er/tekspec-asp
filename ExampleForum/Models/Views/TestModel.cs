using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExampleForum.Models.Views
{
    public class TestModel
    {

        public List<string> Names { get; set; }

        public List<SelectListItem> GetNameList()
        {
            return Names.Select(d => new SelectListItem { Text = d, Value = d }).ToList();
        }

    }
}
