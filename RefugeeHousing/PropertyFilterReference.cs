using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;

namespace RefugeeHousing
{
    public class PropertyFilterReference
    {
        public static IEnumerable<SelectListItem> GetMinRoomsFilterList()
        {
            var minRoomOptions = new List<SelectListItem>
            {
                new SelectListItem()
            };
            for (var i = 1; i < 6; i++)
            {
                minRoomOptions.Add(new SelectListItem {Text = i.ToString(), Value = i.ToString()});
            }
            return minRoomOptions;
        }

        public static IEnumerable<SelectListItem> GetMaxMonthlyRentFilterList()
        {
            var minRoomOptions = new List<SelectListItem>
            {
                new SelectListItem()
            };
            for (var i = 1; i < 6; i++)
            {
                var rentValue = i*200;
                minRoomOptions.Add(new SelectListItem { Text = @"< €" + rentValue, Value = rentValue.ToString() });
            }
            return minRoomOptions;
        }

        public static IEnumerable<SelectListItem> GetFurnishedFilterList()
        {
            return CreateYesNoFilterList();
        }

        private static IEnumerable<SelectListItem> CreateYesNoFilterList()
        {
            var minRoomOptions = new List<SelectListItem>
            {
                new SelectListItem(),
                new SelectListItem {Text = LocalizedText.Yes, Value = true.ToString()},
                new SelectListItem {Text = LocalizedText.No, Value = false.ToString()}
            };
            return minRoomOptions;
        } 
    }
}