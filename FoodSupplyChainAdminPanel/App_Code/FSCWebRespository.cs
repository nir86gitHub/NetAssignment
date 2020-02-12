using FoodSupplyChainAdminPanel.Models;
using FSC_BLL.Login;
using FSC_BOL.Models.Login;
using System;
using System.Web;

namespace FoodSupplyChainAdminPanel.App_Code
{
    public static class FSCWebRespository
    {
        public static bool InitialiseSession(Int64 UserId)
        {
            bool isSessionInitialised = false;
            FSCUserSession objFSCUserSession = new FSCUserSession();
            try
            {
                FSCUserDetail userDetails = new ClsLogin().GetUserDetails(UserId);
                objFSCUserSession.UserId = userDetails.UserId;
                objFSCUserSession.UserName = userDetails.FirstName + " " + userDetails.LastName;
                HttpContext.Current.Session["UserDetails"] = objFSCUserSession;
                isSessionInitialised = true;
            }
            catch (Exception exc)
            {
                isSessionInitialised = false;
                throw exc;
            }
            return isSessionInitialised;
        }
    }
}