using FoodSupplyChainAdminPanel.App_Code;
using FSC_BLL.Login;
using FSC_BOL.Models.Login;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace FoodSupplyChainAdminPanel.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            if (TempData["Msg"] != null)
            {
                @ViewBag.Msg = TempData["Msg"].ToString();
                @ViewBag.MsgType = TempData["MsgType"].ToString();
                // Reset temporary session
                TempData["Msg"] = null;
                TempData["MsgType"] = null;
            }
            return View("Login");
        }

        #region Authinticate User

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DoLogin(FSCLogin objLogin)
        {
            string msgType = "Error";
            string msg = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    ClsLogin objClsLogin = new ClsLogin();
                    FSCLogin objLoginDetail = objClsLogin.AuthenticateUser(objLogin.Email);
                    if (objLoginDetail != null && objLoginDetail.UserId > 0)
                    {
                        if (objLoginDetail.Password == FSCSecurity.EncryptPassword(objLogin.Password))
                        {
                            //Initialise Session
                            if (FSCWebRespository.InitialiseSession(objLoginDetail.UserId))
                            {
                                FormsAuthentication.SetAuthCookie(objLogin.Email.ToUpper(), true);
                                return RedirectToAction("../Home/Home");
                            }
                            else
                                msg = "Technical Error!";
                        }
                        else
                        {
                            msg = "Invalid Password!";
                        }
                    }
                    else
                    {
                        msg = "Invalid User";
                    }
                    ViewBag.MsgType = msgType;
                    ViewBag.Msg = msg;
                }
                return View("Login");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region LogOff
        public ActionResult LogOff(string msg)
        {
            try
            {
                if (msg != null && msg!=string.Empty)
                {
                    TempData["Msg"] = msg;
                    TempData["MsgType"] = "Error";
                }
                FormsAuthentication.SignOut();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return RedirectToAction("Login");
        }
        #endregion
    }
}