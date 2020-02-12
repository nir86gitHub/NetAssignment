using FSC_BOL.Models.Common;
using FSC_BOL.Models.FoodProduct;
using FSC_DAL.DBUtility;
using FSC_Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FSC_BLL.AdminPanel
{
    public class ClsFoodProduct
    {
        #region Get Food Product List
        /// <summary>
        /// Method to Get Food Product List
        /// </summary>
        public object GetFoodProducts(DataTablesRequest request)
        {
            List<GbMProduct> objListGbMProduct = new List<GbMProduct>();
            try
            {

                var startPage = (request.DisplayLength == 0) ? 1 : request.DisplayStart / request.DisplayLength + 1;

                var orderBy = string.Join(",", DTHelper.GetOrderByClause(request));

                int skip = (startPage - 1) * request.DisplayLength;

                int take = skip + request.DisplayLength;

                int TotalItems = 0;


                SqlParameter[] parameter = 
                {
                    new SqlParameter("@CategoryId", request.CategoryId),
                    new SqlParameter("@VendorId", request.VendorId),
                    new SqlParameter("@Start",skip),
                    new SqlParameter("@End",take),
                    new SqlParameter("@Search",request.Search),
                    new SqlParameter("@OrderBy",orderBy),
                };

                using (SqlDataReader rdr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_PRODUCT_GETFOODPRDUCTS, parameter))
                {
                    while (rdr.Read())
                    {
                        if (rdr["Count"] != DBNull.Value)
                        {
                            TotalItems = Convert.ToInt32(rdr["Count"]);
                        }

                        rdr.NextResult();

                        while (rdr.Read())
                        {
                            GbMProduct objGbMProduct = new GbMProduct();
                            objGbMProduct.ProductId = rdr.GetInt64(1);
                            objGbMProduct.VendorName = rdr.GetString(2); 
                            objGbMProduct.CategoryName = rdr.GetString(3);
                            objGbMProduct.ProductName = rdr.GetString(4);
                            objGbMProduct.ProductDesc = rdr.GetString(5);
                            objGbMProduct.BasePriceDesc = rdr.GetString(6);
                            objListGbMProduct.Add(objGbMProduct);
                        }
                    }
                }

                return new
                {
                    sEcho = request.Echo,
                    iTotalRecords = TotalItems,
                    iTotalDisplayRecords = TotalItems,
                    sColumns = request.ColumnNames,
                    aaData = (from i in objListGbMProduct
                              select new[]
                              {
                                Convert.ToString(i.ProductId),
                                Convert.ToString(i.ProductName),
                                Convert.ToString(i.ProductDesc),
                                Convert.ToString(i.BasePriceDesc),
                                Convert.ToString(i.CategoryName),
                                Convert.ToString(i.VendorName)
                              }).ToList()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objListGbMProduct = null;
            }
        }

        #endregion

        #region Get Food Product Details
        /// <summary>
        /// Method to Get Food Product Details
        /// </summary>
        /// <param name="productId"></param>
        public GbMProduct GetProductDetail(Int64 productId)
        {
            GbMProduct objGbMProduct = null;
            try
            {
                SqlParameter[] parameter = {
                                                new SqlParameter("@ProductId", productId),
                                           };

                using (SqlDataReader rdr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_PRODUCT_GETFOODPRDUCTDETAIL
                        , parameter))
                {
                    while (rdr.Read())
                    {
                        objGbMProduct = new GbMProduct();
                        objGbMProduct.ProductId = rdr.GetInt64(0);
                        objGbMProduct.CategoryId = rdr.GetInt32(1);
                        objGbMProduct.CategoryName = rdr.GetString(2);
                        objGbMProduct.VendorId = rdr.GetInt32(3);
                        objGbMProduct.VendorName = rdr.GetString(4);
                        objGbMProduct.ProductName = rdr.GetString(5);
                        objGbMProduct.ProductDesc = rdr.GetString(6);
                        objGbMProduct.BasePrice = rdr.GetDecimal(7);
                        objGbMProduct.BaseCurrencyId = rdr.GetInt32(8);
                        objGbMProduct.CurrencyName = rdr.GetString(9);
                        objGbMProduct.VegNonVeg = rdr.GetString(10);
                        objGbMProduct.ImageURL = rdr.GetString(11);
                        objGbMProduct.IsAllowedToCustomise = rdr.GetString(12);
                    }
                }
                return objGbMProduct;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objGbMProduct = null;
            }
        }

        #endregion

        #region Get Food Product Attributes
        /// <summary>
        /// Method to Get Food Product Attributes Details
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productDetailId"></param>
        public List<GbMProductAttribute> GetProductAttributes(Int64 productId,Int64 productDetailId)
        {
            List<GbMProductAttribute> objGbMProductAttributeList = null;
            try
            {
                SqlParameter[] parameter = {
                                                new SqlParameter("@ProductId", productId),
                                                new SqlParameter("@ProductDetailId", productDetailId),
                                           };

                using (SqlDataReader rdr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_PRODUCT_GETFOODPRDUCTATTRIBUTES
                        , parameter))
                {
                    objGbMProductAttributeList = new List<GbMProductAttribute>();
                    while (rdr.Read())
                    {
                        GbMProductAttribute objGbMProductAttribute = new GbMProductAttribute();
                        objGbMProductAttribute.ProductName = rdr.GetString(0);
                        objGbMProductAttribute.ProductDetailId = rdr.GetInt64(1);
                        objGbMProductAttribute.BasePrice = rdr.GetDecimal(2);
                        objGbMProductAttribute.CurrencyCode = rdr.GetString(3);
                        objGbMProductAttribute.AttributeName1 = rdr.GetString(4);
                        objGbMProductAttribute.AttributeName2 = rdr.GetString(5);
                        objGbMProductAttribute.AttributeName3 = rdr.GetString(6);
                        objGbMProductAttribute.AttributeName4 = rdr.GetString(7);
                        objGbMProductAttribute.AttributeName5 = rdr.GetString(8);
                        objGbMProductAttribute.AttributeName6 = rdr.GetString(9);
                        objGbMProductAttribute.AttributeName7 = rdr.GetString(10);
                        objGbMProductAttribute.AttributeName8 = rdr.GetString(11);
                        objGbMProductAttribute.AttributeName9 = rdr.GetString(12);
                        objGbMProductAttribute.AttributeName10 = rdr.GetString(13);
                        objGbMProductAttribute.AttributeId1 = rdr.GetInt32(14);
                        objGbMProductAttribute.AttributeId2 = rdr.GetInt32(15);
                        objGbMProductAttribute.AttributeId3 = rdr.GetInt32(16);
                        objGbMProductAttribute.AttributeId4 = rdr.GetInt32(17);
                        objGbMProductAttribute.AttributeId5 = rdr.GetInt32(18);
                        objGbMProductAttribute.AttributeId6 = rdr.GetInt32(19);
                        objGbMProductAttribute.AttributeId7 = rdr.GetInt32(20);
                        objGbMProductAttribute.AttributeId8 = rdr.GetInt32(21);
                        objGbMProductAttribute.AttributeId9 = rdr.GetInt32(22);
                        objGbMProductAttribute.AttributeId10 = rdr.GetInt32(23);
                        objGbMProductAttribute.AttributeGroupCode1 = rdr.GetString(24);
                        objGbMProductAttribute.AttributeGroupCode2 = rdr.GetString(25);
                        objGbMProductAttribute.AttributeGroupCode3 = rdr.GetString(26);
                        objGbMProductAttribute.AttributeGroupCode4 = rdr.GetString(27);
                        objGbMProductAttribute.AttributeGroupCode5 = rdr.GetString(28);
                        objGbMProductAttribute.AttributeGroupCode6 = rdr.GetString(29);
                        objGbMProductAttribute.AttributeGroupCode7 = rdr.GetString(30);
                        objGbMProductAttribute.AttributeGroupCode8 = rdr.GetString(31);
                        objGbMProductAttribute.AttributeGroupCode9 = rdr.GetString(32);
                        objGbMProductAttribute.AttributeGroupCode10 = rdr.GetString(33);
                        objGbMProductAttributeList.Add(objGbMProductAttribute);
                    }
                }
                return objGbMProductAttributeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objGbMProductAttributeList = null;
            }
        }

        #endregion

        #region Insert Update Food Product
        /// <summary>
        /// Method to insert and update Food Product
        /// </summary>
        /// <param name="objGbMProduct"></param>
        public string[] InsUpdFoodProduct(GbMProduct objGbMProduct,DataTable dtProductAttributes)
        {
            try
            {
                SqlParameter[] parameter = {
                                            new SqlParameter("@ProductId",objGbMProduct.ProductId),
                                            new SqlParameter("@CategoryId",objGbMProduct.CategoryId),
                                            new SqlParameter("@VendorId",objGbMProduct.VendorId),
                                            new SqlParameter("@ProductName",objGbMProduct.ProductName),
                                            new SqlParameter("@ProductDesc",objGbMProduct.ProductDesc),
                                            new SqlParameter("@BasePrice",objGbMProduct.BasePrice),
                                            new SqlParameter("@BaseCurrencyId",objGbMProduct.BaseCurrencyId),
                                            new SqlParameter("@VegNonVeg",objGbMProduct.VegNonVeg),
                                            new SqlParameter("@ImageURL",objGbMProduct.ImageURL),
                                            new SqlParameter("@IsAllowedToCustomise",objGbMProduct.IsAllowedToCustomise),
                                            new SqlParameter("@Udt_FoodProduct_Attributes",dtProductAttributes),
                                            new SqlParameter("@IsActive",objGbMProduct.IsActive),
                                            new SqlParameter("@CrtBy",objGbMProduct.CrtBy),
                                            new SqlParameter("@CrtIP",objGbMProduct.CrtIp),
                                            new SqlParameter("@ErrorMsg", SqlDbType.VarChar,50) {
                                                    Direction = ParameterDirection.Output},
                                            new SqlParameter("@ErrorDesc", SqlDbType.VarChar,500) {
                                                    Direction = ParameterDirection.Output}
                                            };

                return FSC_Helper.ExecuteNonQueryOutputResult(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_PRODUCT_INSUPDFOODPRODUCT, new string[] { "@ErrorMsg", "@ErrorDesc" }, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update Food Product Attribute
        /// <summary>
        /// Method to update Food Product Attribute
        /// </summary>
        /// <param name="objGbMProductAttribute"></param>
        public string[] UpdFoodProductAttribute(GbMProductAttribute objGbMProductAttribute)
        {
            try
            {
                SqlParameter[] parameter = {
                                            new SqlParameter("@ProductDetailId",objGbMProductAttribute.ProductId),
                                            new SqlParameter("@BasePrice",objGbMProductAttribute.BasePrice),
                                            new SqlParameter("@IsDefault",objGbMProductAttribute.IsDefault),
                                            new SqlParameter("@IsActive",objGbMProductAttribute.IsActive),
                                            new SqlParameter("@CrtBy",objGbMProductAttribute.CrtBy),
                                            new SqlParameter("@CrtIP",objGbMProductAttribute.CrtIp),
                                            new SqlParameter("@ErrorMsg", SqlDbType.VarChar,50) {
                                                    Direction = ParameterDirection.Output},
                                            new SqlParameter("@ErrorDesc", SqlDbType.VarChar,500) {
                                                    Direction = ParameterDirection.Output}
                                            };

                return FSC_Helper.ExecuteNonQueryOutputResult(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_PRODUCT_UPDFOODPRODUCTATTRIBUTE, new string[] { "@ErrorMsg", "@ErrorDesc" }, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get Food Products
        /// <summary>
        /// Method to Get Food Product Details
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="categoryId"></param>
        public List<GbMProduct> GetProductList(Int64 productId,int categoryId)
        {
            List<GbMProduct> objGbMProductList = null;
            try
            {
                SqlParameter[] parameter = {
                                                new SqlParameter("@ProductId", productId),
                                                new SqlParameter("@CategoryId", categoryId),
                                           };

                using (SqlDataReader rdr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_PRODUCT_GETLISTOFFOODPRDUCTS
                        , parameter))
                {
                    objGbMProductList = new List<GbMProduct>();
                    while (rdr.Read())
                    {
                        GbMProduct objGbMProduct = new GbMProduct();
                        objGbMProduct.ProductId = rdr.GetInt64(0);
                        objGbMProduct.CategoryId = rdr.GetInt32(1);
                        objGbMProduct.CategoryName = rdr.GetString(2);
                        objGbMProduct.VendorId = rdr.GetInt32(3);
                        objGbMProduct.VendorName = rdr.GetString(4);
                        objGbMProduct.ProductName = rdr.GetString(5);
                        objGbMProduct.ProductDesc = rdr.GetString(6);
                        objGbMProduct.BasePrice = rdr.GetDecimal(7);
                        objGbMProduct.BaseCurrencyId = rdr.GetInt32(8);
                        objGbMProduct.CurrencyName = rdr.GetString(9);
                        objGbMProduct.VegNonVeg = rdr.GetString(10);
                        objGbMProduct.ImageURL = rdr.GetString(11);
                        objGbMProduct.IsAllowedToCustomise = rdr.GetString(12);
                        objGbMProductList.Add(objGbMProduct);
                    }
                }
                return objGbMProductList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objGbMProductList = null;
            }
        }

        #endregion

        #region Get Food Products By Categories
        /// <summary>
        /// Method to Get Food Products By Categories Comma Seperated
        /// </summary>
        /// <param name="categories"></param>
        public List<GbMProduct> GetProductsByCategories(string categories)
        {
            List<GbMProduct> objGbMProductList = null;
            try
            {
                SqlParameter[] parameter = {
                                                new SqlParameter("@CategoryId", categories)
                                           };

                using (SqlDataReader rdr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_PRODUCT_GETFOODPRDUCTSBYCATEGORIES
                        , parameter))
                {
                    objGbMProductList = new List<GbMProduct>();
                    while (rdr.Read())
                    {
                        GbMProduct objGbMProduct = new GbMProduct();
                        objGbMProduct.ProductId = rdr.GetInt64(0);
                        objGbMProduct.CategoryId = rdr.GetInt32(1);
                        objGbMProduct.CategoryName = rdr.GetString(2);
                        objGbMProduct.VendorId = rdr.GetInt32(3);
                        objGbMProduct.VendorName = rdr.GetString(4);
                        objGbMProduct.ProductName = rdr.GetString(5);
                        objGbMProduct.ProductDesc = rdr.GetString(6);
                        objGbMProduct.BasePrice = rdr.GetDecimal(7);
                        objGbMProduct.BaseCurrencyId = rdr.GetInt32(8);
                        objGbMProduct.CurrencyName = rdr.GetString(9);
                        objGbMProduct.VegNonVeg = rdr.GetString(10);
                        objGbMProduct.ImageURL = rdr.GetString(11);
                        objGbMProduct.IsAllowedToCustomise = rdr.GetString(12);
                        objGbMProductList.Add(objGbMProduct);
                    }
                }
                return objGbMProductList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objGbMProductList = null;
            }
        }

        #endregion
    }
}
