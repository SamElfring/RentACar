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

        public static readonly string LowClass = "Low";
        public static readonly string MidClass = "Mid";
        public static readonly string HighClass = "High";

        public static List<SelectListItem> GetClassesForDropDown()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem {Value = Helper.LowClass, Text = "Lage klasse"},
                new SelectListItem {Value = Helper.MidClass, Text = "Middel klasse"},
                new SelectListItem {Value = Helper.HighClass, Text = "Luxe klasse"}
            };
            return items.ToList();
        }
    }
}
