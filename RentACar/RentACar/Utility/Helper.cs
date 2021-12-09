using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace RentACar.Utility
{
    public static class Helper
    {
        public static readonly string Admin = "Admin";
        public static readonly string Employee = "Employee";
        public static readonly string User = "User";

        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem {Value = Helper.Admin, Text = "Beheerder"},
                new SelectListItem {Value = Helper.Employee, Text = "Medewerker"},
                new SelectListItem {Value = Helper.User, Text = "Gebruiker"}
            };
            return items.OrderBy(s => s.Text).ToList();
        }
    }
}
