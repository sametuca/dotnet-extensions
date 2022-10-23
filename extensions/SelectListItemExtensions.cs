using Marti.Core.Utilities;
using Marti.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Marti.Core.Extensions;

public static class SelectListItemExtensions
{
    public static List<SelectListItem> AddALLOption(this List<SelectListItem> list)
    {
        list.Insert(0, new SelectListItem { Text = "-- Tümü --", Value = "0" });
        return list;
    }

    public static List<SelectListItem> AddEmptyOption(this List<SelectListItem> list, string prefix, bool disabled = false, bool selected = false)
    {
        list.Insert(0, new SelectListItem { Text = $"-- {prefix ?? string.Empty} Yok --", Value = null, Disabled = disabled, Selected = selected});
        return list;
    }

    public static List<SelectListItem> AddSELECTOption(this List<SelectListItem> list)
    {
        list.Insert(0, new SelectListItem { Text = "-- Seçiniz -- ", Value = string.Empty });
        return list;
    }

    public static List<SelectListItem> AddNewStatus(this List<SelectListItem> list)
    {
        List<SelectListItem> enumlist = typeof(ScooterEmergencyStatus).GetEnumValuesWithDescription<ScooterEmergencyStatus>()
            .Select(x => new SelectListItem
            {
                Text = x.Value,
                Value = ((int)x.Key).ToString()
            }).ToList();
        foreach (var item in enumlist)
        {
            list.Add(new SelectListItem() { Text = item.Text, Value = item.Value });
        }

        return list;
    }


    public static List<SelectListItem> AddOtherOption(this List<SelectListItem> list)
    {
        list.Insert(0, new SelectListItem { Text = "-- Other -- ", Value = "999" });
        return list;
    }

    public static List<SelectListItem> PopulateLanguages(this List<SelectListItem> list)
    {
        list.Add(new SelectListItem() { Text = "Türkçe", Value = "tr" });
        list.Add(new SelectListItem() { Text = "English", Value = "en" });
        return list;
    }
}