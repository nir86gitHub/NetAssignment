using System;

namespace FSC_BOL.Models.FoodProduct
{
    #region Common Models
    public class GbMCategory
    {
        public int CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
    }
    public class GbMVendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
    }
    public class GbMCurrency
    {
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }
    public class GbMAttributes
    {
        public int AttributeId { get; set; }
        public int CategoryId { get; set; }
        public string AttributeGroupCode { get; set; }
        public string AttributeGroupDesc { get; set; }
        public string AttributeValue { get; set; }
        public int SrNo { get; set; }
    }
    #endregion

    #region Food Product
    public class GbMProduct
    {
        public Int64 ProductId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal BasePrice { get; set; }
        public string BasePriceDesc { get; set; }
        public int BaseCurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string VegNonVeg { get; set; }
        public string ImageURL { get; set; }
        public string IsAllowedToCustomise { get; set; }
        public string IsActive { get; set; }
        public Int64 CrtBy { get; set; }
        public string CrtIp { get; set; }
    }
    public class GbMProductAttribute
    {
        public Int64 ProductDetailId { get; set; }
        public Int64 ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal BasePrice { get; set; }
        public string BasePriceDesc { get; set; }
        public string CurrencyCode { get; set; }
        public int AttributeId1 { get; set; }
        public int AttributeId2 { get; set; }
        public int AttributeId3 { get; set; }
        public int AttributeId4 { get; set; }
        public int AttributeId5 { get; set; }
        public int AttributeId6 { get; set; }
        public int AttributeId7 { get; set; }
        public int AttributeId8 { get; set; }
        public int AttributeId9 { get; set; }
        public int AttributeId10 { get; set; }
        public string AttributeName1 { get; set; }
        public string AttributeName2 { get; set; }
        public string AttributeName3 { get; set; }
        public string AttributeName4 { get; set; }
        public string AttributeName5 { get; set; }
        public string AttributeName6 { get; set; }
        public string AttributeName7 { get; set; }
        public string AttributeName8 { get; set; }
        public string AttributeName9 { get; set; }
        public string AttributeName10 { get; set; }

        public string AttributeGroupCode1 { get; set; }
        public string AttributeGroupCode2 { get; set; }
        public string AttributeGroupCode3 { get; set; }
        public string AttributeGroupCode4 { get; set; }
        public string AttributeGroupCode5 { get; set; }
        public string AttributeGroupCode6 { get; set; }
        public string AttributeGroupCode7 { get; set; }
        public string AttributeGroupCode8 { get; set; }
        public string AttributeGroupCode9 { get; set; }
        public string AttributeGroupCode10 { get; set; }

        public string IsDefault { get; set; }
        public string IsActive { get; set; }
        public Int64 CrtBy { get; set; }
        public string CrtIp { get; set; }
    }
    #endregion

}
