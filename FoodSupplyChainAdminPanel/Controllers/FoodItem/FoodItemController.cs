using FoodSupplyChainAdminPanel.App_Code;
using FoodSupplyChainAdminPanel.Models;
using FSC_BLL.AdminPanel;
using FSC_BOL.Models.Common;
using FSC_BOL.Models.FoodProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FoodSupplyChainAdminPanel.Controllers.FoodItem
{
    public class FoodItemController : BaseController
    {
        // GET: FoodItem Item List
        public ActionResult ManageFoodItem()
        {
            try
            {
                ViewBag.Title = "Food Products";
                if (Request.Form["hdMsg"] != null)
                {
                    Message objMessage = new Message();
                    objMessage = (ClsWebCommon.JsonDeserialize<Message>(Request.Form["hdMsg"])) as Message;
                    @ViewBag.Msg = objMessage.Msg;
                    @ViewBag.MsgType = objMessage.MsgType;
                }
                ViewBag.VendorsFoodProductWL = new SelectList(new ClsCommon().GetVendors(0), "VendorId", "VendorName");
                ViewBag.CategoryFoodProductWL = new SelectList(new ClsCommon().GetCategory(0), "CategoryId", "CategoryName");
                return View("ManageFoodItem");
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public ActionResult FoodItemIU(string product)
        {
            try
            {
                Int64 productId = 0;
                GbMProduct objProductDetails = new GbMProduct();

                if (product != null && product != string.Empty)
                {
                    try
                    {
                        Int64.TryParse(product, out productId);
                    }
                    catch (Exception exc)
                    {
                        productId = 0;
                    }
                }
                if (productId > 0)
                {
                    ViewBag.Title = "Edit Food Item";
                    objProductDetails = new ClsFoodProduct().GetProductDetail(productId);
                }
                else ViewBag.Title = "Add Food Item";
                GetFoodProductViewBagData(productId, objProductDetails.CategoryId);

                return View("FoodItemIU", objProductDetails);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        #region GetDefaultData
        private void GetFoodProductViewBagData(Int64 productId,int categoryId)
        {
            ClsCommon clsCommon = new ClsCommon();
            ViewBag.FoodProductIUCategory = clsCommon.GetCategory(0);
            ViewBag.FoodProductIUVendor = clsCommon.GetVendors(0);
            ViewBag.FoodProductIUCurrency = clsCommon.GetCurrency(0);

            if (productId > 0)
            {
                List<GbMProductAttribute> objProductAttributes = new ClsFoodProduct().GetProductAttributes(productId, 0);
                if(objProductAttributes!=null && objProductAttributes.Count > 0)
                {
                    ViewBag.FoodProductIUAttributes = objProductAttributes;
                }

                if (categoryId > 0 && objProductAttributes.Count > 0)
                {
                    List<GbMAttributes> listGbMAttributes = new List<GbMAttributes>();
                    List<FSCFoodAttributes> objFoodAttributes = new List<FSCFoodAttributes>();

                    #region Get Master Attributes
                    listGbMAttributes = new ClsCommon().GetFoodAttributes(0, categoryId, "");
                    if (listGbMAttributes != null && listGbMAttributes.Count > 0)
                    {
                        var grpAttributes = listGbMAttributes.GroupBy(n => new { n.AttributeGroupCode, n.AttributeGroupDesc })
                          .Select(g => new
                          {
                              g.Key.AttributeGroupCode,
                              g.Key.AttributeGroupDesc
                          }).ToList();
                        foreach (var attr in grpAttributes)
                        {
                            FSCFoodAttributes fscAttribute = new FSCFoodAttributes();
                            fscAttribute.AttributeGroupCode = attr.AttributeGroupCode;
                            fscAttribute.AttributeGroupDesc = attr.AttributeGroupDesc;
                            List<FSCFoodAttributeValues> attributeValuesList = new List<FSCFoodAttributeValues>();
                            foreach (var att in listGbMAttributes)
                            {
                                if (att.AttributeGroupCode == attr.AttributeGroupCode)
                                {
                                    FSCFoodAttributeValues attValue = new FSCFoodAttributeValues();
                                    attValue.AttributeId = att.AttributeId;
                                    attValue.AttributeValue = att.AttributeValue;
                                    attValue.AttributeGroupCode = att.AttributeGroupCode;
                                    attValue.AttributeGroupDesc = att.AttributeGroupDesc;
                                    attributeValuesList.Add(attValue);
                                }
                            }
                            fscAttribute.FoodAttributes = attributeValuesList;
                            objFoodAttributes.Add(fscAttribute);
                        }
                    }
                    #endregion

                    if(objFoodAttributes!=null && objFoodAttributes.Count > 0)
                    {
                        ViewBag.FoodProductIUAllAttributes = objFoodAttributes;
                    }
                }
            }
        }
        #endregion
    }
}