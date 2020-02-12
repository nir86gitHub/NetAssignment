using FSC_BOL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodSupplyChainAdminPanel.App_Code
{
    public class DataTablesBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var pageRequest = new DataTablesRequest
            {
                Echo = BindDataTablesRequestParam<Int32>(bindingContext, "sEcho"),
                DisplayStart = BindDataTablesRequestParam<Int32>(bindingContext, "iDisplayStart"),
                DisplayLength = BindDataTablesRequestParam<Int32>(bindingContext, "iDisplayLength"),
                ColumnNames = BindDataTablesRequestParam<string>(bindingContext, "sColumns"),
                Columns = BindDataTablesRequestParam<Int32>(bindingContext, "iColumns"),
                Search = BindDataTablesRequestParam<string>(bindingContext, "sSearch"),
                Regex = BindDataTablesRequestParam<bool>(bindingContext, "bRegex"),
                SortingCols = BindDataTablesRequestParam<Int32>(bindingContext, "iSortingCols"),
                DataProp = BindDataTablesRequestParam<string>(controllerContext.HttpContext.Request, "mDataProp_"),
                RegexColumns = BindDataTablesRequestParam<bool>(controllerContext.HttpContext.Request, "bRegex_"),
                Searchable = BindDataTablesRequestParam<bool>(controllerContext.HttpContext.Request, "bSearchable_"),
                Sortable = BindDataTablesRequestParam<bool>(controllerContext.HttpContext.Request, "bSortable_"),
                SortCol = BindDataTablesRequestParam<Int32>(controllerContext.HttpContext.Request, "iSortCol_"),
                SearchColumns = BindDataTablesRequestParam<string>(controllerContext.HttpContext.Request, "sSearch_"),
                SortDir = BindDataTablesRequestParam<string>(controllerContext.HttpContext.Request, "sSortDir_"),

                CategoryId = BindDataTablesRequestParamstring(bindingContext, "CategoryId"),
                VendorId = BindDataTablesRequestParamstring(bindingContext, "VendorId")
            };
            return pageRequest;
        }

        private static T BindDataTablesRequestParam<T>(System.Web.Mvc.ModelBindingContext context, string propertyName)
        {
            return (T)context.ValueProvider.GetValue(propertyName).ConvertTo(typeof(T));
        }

        private static List<T> BindDataTablesRequestParam<T>(HttpRequestBase request, string keyPrefix)
        {
            return (from k in request.Params.AllKeys
                    where k.StartsWith(keyPrefix)
                    orderby int.Parse(k.Replace(keyPrefix, string.Empty))
                    select (T)Convert.ChangeType(request.Params[k], typeof(T))
                   ).ToList();

        }

        private static string BindDataTablesRequestParamstring(System.Web.Mvc.ModelBindingContext context, string propertyName)
        {
            string value = string.Empty;
            if (context.ValueProvider.GetValue(propertyName) != null)
            {
                value = context.ValueProvider.GetValue(propertyName).AttemptedValue;
            }
            return value;
        }

        private static List<T> BindDataTableRequestJsonParam<T>(System.Web.Mvc.ModelBindingContext context, string keyPrefix)
        {
            List<T> value = new List<T>();
            if (context.ValueProvider.GetValue(keyPrefix) != null)
            {
                string JsonValue = context.ValueProvider.GetValue(keyPrefix).AttemptedValue;
                if (JsonValue != string.Empty && JsonValue != null)
                {
                    value = (ClsWebCommon.JsonDeserialize<List<T>>(JsonValue)) as List<T>;
                }
            }
            return value;

        }
    }
}