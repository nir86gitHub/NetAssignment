using FSC_BOL.Models.FoodProduct;
using FSC_DAL.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FSC_BLL.AdminPanel
{
    public class ClsCommon
    {
        #region Get Product Categories
        /// <summary>
        /// Method to Get Get Product Categories List
        /// </summary>
        /// <param name="categoryId"></param>
        public List<GbMCategory> GetCategory(int categoryId)
        {
            List<GbMCategory> objGbMCategoryList = null;
            try
            {
                SqlParameter[] parameter = { new SqlParameter("@CategoryId", categoryId) };

                using (SqlDataReader rdr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_COMMON_GETCATEGORY
                        , parameter))
                {

                    objGbMCategoryList = new List<GbMCategory>();
                    while (rdr.Read())
                    {
                        GbMCategory objGbMCategory = new GbMCategory();
                        objGbMCategory.CategoryId = rdr.GetInt32(0);
                        objGbMCategory.CategoryCode = rdr.GetString(1);
                        objGbMCategory.CategoryName = rdr.GetString(2);
                        objGbMCategoryList.Add(objGbMCategory);
                    }
                }
                return objGbMCategoryList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objGbMCategoryList = null;
            }
        }

        #endregion

        #region Get Vendors
        /// <summary>
        /// Method to Get Get Vendors List
        /// </summary>
        /// <param name="vendorId"></param>
        public List<GbMVendor> GetVendors(int vendorId)
        {
            List<GbMVendor> objGbMVendorList = null;
            try
            {
                SqlParameter[] parameter = { new SqlParameter("@VendorId", vendorId) };

                using (SqlDataReader rdr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_COMMON_GETVENDORS
                        , parameter))
                {

                    objGbMVendorList = new List<GbMVendor>();
                    while (rdr.Read())
                    {
                        GbMVendor objGbMVendor = new GbMVendor();
                        objGbMVendor.VendorId = rdr.GetInt32(0);
                        objGbMVendor.VendorName = rdr.GetString(1);
                        objGbMVendorList.Add(objGbMVendor);
                    }
                }
                return objGbMVendorList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objGbMVendorList = null;
            }
        }

        #endregion

        #region Get Currency
        /// <summary>
        /// Method to Get Currency List
        /// </summary>
        /// <param name="currencyId"></param>
        public List<GbMCurrency> GetCurrency(int currencyId)
        {
            List<GbMCurrency> objGbMCurrencyList = null;
            try
            {
                SqlParameter[] parameter = { new SqlParameter("@CurrencyId", currencyId) };

                using (SqlDataReader rdr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_COMMON_GETCURRENCY
                        , parameter))
                {

                    objGbMCurrencyList = new List<GbMCurrency>();
                    while (rdr.Read())
                    {
                        GbMCurrency objGbMCurrency = new GbMCurrency();
                        objGbMCurrency.CurrencyId = rdr.GetInt32(0);
                        objGbMCurrency.CurrencyCode = rdr.GetString(1);
                        objGbMCurrency.CurrencyName = rdr.GetString(2);
                        objGbMCurrencyList.Add(objGbMCurrency);
                    }
                }
                return objGbMCurrencyList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objGbMCurrencyList = null;
            }
        }

        #endregion

        #region Get Attributes
        /// <summary>
        /// Method to Get Attributes List
        /// </summary>
        /// <param name="attributeId"></param>
        /// <param name="categoryId"></param>
        /// <param name="attributeGrpCode"></param>
        public List<GbMAttributes> GetFoodAttributes(int attributeId,int categoryId,string attributeGrpCode)
        {
            List<GbMAttributes> objGbMAttributesList = null;
            try
            {
                SqlParameter[] parameter = {
                                                new SqlParameter("@AttributeId", attributeId),
                                                new SqlParameter("@CategoryId", categoryId),
                                                new SqlParameter("@AttributeGroupCode", attributeGrpCode)
                                           };

                using (SqlDataReader rdr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                        CommandType.StoredProcedure, DBConstant.PROC_COMMON_GETATTRIBUTES
                        , parameter))
                {

                    objGbMAttributesList = new List<GbMAttributes>();
                    while (rdr.Read())
                    {
                        GbMAttributes objGbMAttributes = new GbMAttributes();
                        objGbMAttributes.AttributeId = rdr.GetInt32(0);
                        objGbMAttributes.CategoryId = rdr.GetInt32(1);
                        objGbMAttributes.AttributeGroupCode = rdr.GetString(2);
                        objGbMAttributes.AttributeGroupDesc = rdr.GetString(3);
                        objGbMAttributes.AttributeValue = rdr.GetString(4);
                        objGbMAttributes.SrNo = rdr.GetInt32(5);
                        objGbMAttributesList.Add(objGbMAttributes);
                    }
                }
                return objGbMAttributesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objGbMAttributesList = null;
            }
        }

        #endregion
    }
}
