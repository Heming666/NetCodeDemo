using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web.Common
{
    public static class Common
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(Type enumType,int? selectValue, string DefaultName="==全部==", string DefaultValue="")
        {
            IList<SelectListItem> listItem = new List<SelectListItem>();
            Array values = Enum.GetValues(enumType);
            listItem.Add(new SelectListItem(DefaultName, DefaultValue));
            if (null != values && values.Length > 0)
            {
                foreach (int item in values)
                {
                    var selectitem = new SelectListItem { Value = item.ToString(), Text = Enum.GetName(enumType, item) };
                    if (selectValue.HasValue) selectitem.Selected = item == selectValue ? true : false;
                    listItem.Add(selectitem);
                }
            }
            return listItem;
        }
    }
}
