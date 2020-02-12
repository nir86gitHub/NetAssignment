using FoodSupplyChain.App_Code;
using FoodSupplyChain.Models;
using FSC_BLL.AdminPanel;
using FSC_BOL.Models.FoodProduct;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FoodSupplyChain.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Home(string Category)
        {
            try
            {
                List<GbMProduct> foodproductsList = new List<GbMProduct>();
                int categoryId = 0;
                if(Category!=null && Category!=string.Empty)
                {
                    try
                    {
                        Int32.TryParse(Category, out categoryId);
                    }
                    catch (Exception exc)
                    {
                        categoryId = 0;
                    }
                }
                foodproductsList = new ClsFoodProduct().GetProductList(0, categoryId);
                return View("Home", foodproductsList);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
        public ActionResult CustomiseFoodProduct(string Product)
        {
            try
            {
                GbMProduct foodproductsDetails = new GbMProduct();
                ClsFoodProduct objClsFoodProduct = new ClsFoodProduct();
                Int64 productId = 0;
                if (Product != null && Product != string.Empty)
                {
                    try
                    {
                        Int64.TryParse(Product, out productId);
                    }
                    catch (Exception exc)
                    {
                        productId = 0;
                    }
                }
                foodproductsDetails = objClsFoodProduct.GetProductDetail(productId);
                ViewBag.FoodProductAttributes = objClsFoodProduct.GetProductAttributes(productId, 0);

                #region Get Topping And Other Products
                ViewBag.OtherFoodProducts = objClsFoodProduct.GetProductsByCategories("3,4,6,7,8");
                #endregion

                return PartialView("CustomiseFoodProduct", foodproductsDetails);
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        #region Add Food Item To Cart
        [HttpPost]
        public JsonResult AddToCart(string jsonProduct,string jsonTopping)
        {
            ProductCart objProduct = null;
            List<ProductCartToppings> objProductCartToppings = null;
            List<KeyValuePair<string, string>> objStatus = null;
            string MsgType = "Error";
            string Msg = string.Empty;
            try
            {
                objProduct = (ClsWebCommon.JsonDeserialize<ProductCart>(jsonProduct)) as ProductCart;

                objProductCartToppings = new List<ProductCartToppings>();
                if (jsonTopping != "" && jsonTopping != null)
                    objProductCartToppings = (ClsWebCommon.JsonDeserialize<List<ProductCartToppings>>(jsonTopping)) as List<ProductCartToppings>;

                if (objProduct.ProductId > 0)
                {
                    objProduct.SessionId = SessionManager.GetSessionId();
                    if(objProductCartToppings!=null && objProductCartToppings.Count>0)
                    {
                        objProduct.Toppings = objProductCartToppings;
                    }

                    List<ProductCart> objCurrentSessionCart = SessionManager.GetCartSession();
                    if (objCurrentSessionCart != null && objCurrentSessionCart.Count > 0)
                    {
                        objCurrentSessionCart.Add(objProduct);
                    }
                    else
                    {
                        objCurrentSessionCart = new List<ProductCart>();
                        objCurrentSessionCart.Add(objProduct);
                    }
                    SessionManager.SetCartSession(objCurrentSessionCart);

                    Msg = "Product Added To Cart!";
                    MsgType = "Success";
                }
                else
                {
                    Msg = "Error!";
                }
                objStatus = new List<KeyValuePair<string, string>>();
                objStatus.Add(new KeyValuePair<string, string>("MsgType", MsgType));
                objStatus.Add(new KeyValuePair<string, string>("Msg", Msg));
            }
            catch (Exception ex)
            {
                objStatus = new List<KeyValuePair<string, string>>();
                objStatus.Add(new KeyValuePair<string, string>("MsgType", MsgType));
                objStatus.Add(new KeyValuePair<string, string>("Msg", "Error"));
                throw ex;
            }
            return Json(objStatus);
        }
        #endregion
    }
}