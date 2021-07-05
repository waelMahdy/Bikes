using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vroomnew.Extensions
{
    public static class IEnumerableEtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var sli = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            list.Add(sli);
            foreach (var item in items)
            {
                var sel = new SelectListItem
                {
                    Text = item.GetType().GetProperty("Name").GetValue(item, null).ToString(),
                    Value = item.GetType().GetProperty("Id").GetValue(item, null).ToString()
                };
                list.Add(sel);

            }
            return list;
        }
    }
}
