using System;
using System.Web.Script.Serialization;

namespace FoodSupplyChainAdminPanel.App_Code
{
    public static class ClsWebCommon
    {
        #region JsonDeserialize
        public static T JsonDeserialize<T>(string json)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                return serializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}