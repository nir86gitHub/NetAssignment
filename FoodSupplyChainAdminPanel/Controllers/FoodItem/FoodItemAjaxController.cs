using FoodSupplyChainAdminPanel.App_Code;
using FoodSupplyChainAdminPanel.CustomAttributes;
using FoodSupplyChainAdminPanel.Models;
using FSC_BLL.AdminPanel;
using FSC_BLL.AdminPanel.Constant;
using FSC_BOL.Models.Common;
using FSC_BOL.Models.FoodProduct;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace FoodSupplyChainAdminPanel.Controllers.FoodItem
{
    [FSCHasSession]
    public class FoodItemAjaxController : Controller
    {
        #region Ajax Action : Food Product WF
        public ActionResult GetFoodProduct(DataTablesRequest pageRequest)
        {
            var pageResponse = new ClsFoodProduct().GetFoodProducts(pageRequest);
            return Json(pageResponse);
        }
        #endregion

        #region Get Attributes Based on Category
        public JsonResult GetProductAttributes(string category)
        {
            List<GbMAttributes> listGbMAttributes = null;
            List<FSCFoodAttributes> objFoodAttributes = new List<FSCFoodAttributes>();
            try
            {
                int categoryId = 0;
                listGbMAttributes = new List<GbMAttributes>();
                try
                {
                    Int32.TryParse(category, out categoryId);
                }
                catch (Exception exc)
                {
                    categoryId = 0;
                }
                listGbMAttributes = new ClsCommon().GetFoodAttributes(0, categoryId, "");
                if (listGbMAttributes != null && listGbMAttributes.Count > 0)
                {
                    var grpAttributes = listGbMAttributes.GroupBy(n => new { n.AttributeGroupCode, n.AttributeGroupDesc })
                      .Select(g => new
                      {
                          g.Key.AttributeGroupCode,
                          g.Key.AttributeGroupDesc
                      }).ToList();
                    foreach(var attr in grpAttributes)
                    {
                        FSCFoodAttributes fscAttribute = new FSCFoodAttributes();
                        fscAttribute.AttributeGroupCode = attr.AttributeGroupCode;
                        fscAttribute.AttributeGroupDesc = attr.AttributeGroupDesc;
                        List<FSCFoodAttributeValues> attributeValuesList = new List<FSCFoodAttributeValues>();
                        foreach(var att in listGbMAttributes)
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
                return Json(objFoodAttributes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                listGbMAttributes = null;
            }
        }
        #endregion

        #region Ajax Action : Insert Update Food Product
        public JsonResult InsUpdFoodProduct(string jsonProduct,string jsonProductDetails)
        {
            GbMProduct objProduct = null;
            List<KeyValuePair<string, string>> objStatus = null;
            List<GbMProductAttribute> listProductAttributes = null;
            string MsgType = "Error";
            string Msg = string.Empty;
            try
            {
                string strDateValidation = string.Empty;

                objProduct = (ClsWebCommon.JsonDeserialize<GbMProduct>(jsonProduct)) as GbMProduct;

                DataTable dtProductAttributeDetails = GetProductAttributeTable();

                if (objProduct.IsActive == "Y")
                {
                    #region Food Product Detail

                    listProductAttributes = new List<GbMProductAttribute>();
                    if (jsonProductDetails != "" && jsonProductDetails != null)
                        listProductAttributes = (ClsWebCommon.JsonDeserialize<List<GbMProductAttribute>>(jsonProductDetails)) as List<GbMProductAttribute>;

                    #region Get Product Attributes Datatble
                    DataRow dr = null;
                    foreach (GbMProductAttribute attributes in listProductAttributes)
                    {
                        dr = dtProductAttributeDetails.NewRow();
                        dr["BasePrice"] = attributes.BasePrice;
                        dr["AttributeId1"] = attributes.AttributeId1;
                        dr["AttributeId2"] = attributes.AttributeId2;
                        dr["AttributeId3"] = attributes.AttributeId3;
                        dr["AttributeId4"] = attributes.AttributeId4;
                        dr["AttributeId5"] = attributes.AttributeId5;
                        dr["AttributeId6"] = attributes.AttributeId6;
                        dr["AttributeId7"] = attributes.AttributeId7;
                        dr["AttributeId8"] = attributes.AttributeId8;
                        dr["AttributeId9"] = attributes.AttributeId9;
                        dr["AttributeId10"] = attributes.AttributeId10;
                        dr["IsDefault"] = attributes.IsDefault;
                        dr["IsActive"] = attributes.IsActive;
                        dtProductAttributeDetails.Rows.Add(dr);
                    }
                    #endregion

                    #endregion
                }

                #region Ins Upd Food Product

                if (strDateValidation == string.Empty)
                {
                    objProduct.CrtBy = GetSession.GetSessionFromContext().UserId;
                    objProduct.CrtIp = FSCSecurity.GetIPAddress();

                    string[] Out = new ClsFoodProduct().InsUpdFoodProduct(objProduct, dtProductAttributeDetails);

                    if (Out[0] != string.Empty)
                    {
                        switch (Out[0])
                        {
                            case CodeConstant.Code_InsertSuccess:
                            case CodeConstant.Code_UpdateSuccess:
                            case CodeConstant.Code_DeleteSuccess:
                                MsgType = "Success";
                                Msg = HttpContext.GetGlobalResourceObject("AdminPanel", "FoodProduct_" + Out[0]).ToString();
                                break;
                            case CodeConstant.Code_FOOD_ITEM_NAME_DUPLICATE:
                            case CodeConstant.Code_FOOD_ITEM_ATTRIBUTE_MANDATORY:
                            case CodeConstant.Code_FOOD_PRODUCT_ATTRIBUTE_NOT_EXISTS:
                                Msg = HttpContext.GetGlobalResourceObject("AdminPanel", "FoodProduct_" + Out[0]).ToString();
                                break;
                            default:
                                Msg = HttpContext.GetGlobalResourceObject("AdminPanel", "T1100").ToString();
                                break;
                        }
                    }
                    else
                    {
                        Msg = HttpContext.GetGlobalResourceObject("AdminPanel", "T1100").ToString();
                    }
                }
                else
                {
                    Msg = strDateValidation;
                }

                #endregion

                objStatus = new List<KeyValuePair<string, string>>();
                objStatus.Add(new KeyValuePair<string, string>("MsgType", MsgType));
                objStatus.Add(new KeyValuePair<string, string>("Msg", Msg));
            }
            catch (Exception ex)
            {
                objStatus = new List<KeyValuePair<string, string>>();
                objStatus.Add(new KeyValuePair<string, string>("MsgType", MsgType));
                Msg = HttpContext.GetGlobalResourceObject("AdminPanel", "T1100").ToString();
                objStatus.Add(new KeyValuePair<string, string>("Msg", Msg));
                throw ex;
            }
            return Json(objStatus);
        }

        #region Privte: Product Attribute Detail Table
        private DataTable GetProductAttributeTable()
        {
            DataTable dt = new DataTable();
            DataColumn col = new DataColumn("BasePrice"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId1"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId2"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId3"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId4"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId5"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId6"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId7"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId8"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId9"); dt.Columns.Add(col);
            col = new DataColumn("AttributeId10"); dt.Columns.Add(col);
            col = new DataColumn("IsDefault"); dt.Columns.Add(col);
            col = new DataColumn("IsActive"); dt.Columns.Add(col);
            return dt;

        }

        #endregion

        #endregion
    }
}