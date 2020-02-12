using FSC_BLL.Login.Constant;
using FSC_BOL.Models.Login;
using FSC_DAL.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FSC_BLL.Login
{
    public class ClsLogin
    {
        #region Authenticate User
        public FSCLogin AuthenticateUser(string LoginId)
        {
            FSCLogin objFSCLogin = new FSCLogin();
            try
            {
                SqlParameter[] parameters ={
                    new SqlParameter("@UserEmail",LoginId)
                };
                using (SqlDataReader dr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                    CommandType.StoredProcedure, DBConstant.PROC_COMMON_AUTHENTICATE, parameters))
                {
                    while (dr.Read())
                    {
                        objFSCLogin.UserId = dr.GetInt64(0);
                        objFSCLogin.Email = dr.GetString(1);
                        objFSCLogin.Password = dr.GetString(2);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objFSCLogin;
        }

        #endregion

        #region Get User Details

        public FSCUserDetail GetUserDetails(Int64 userId)
        {
            FSCUserDetail objFSCUserDetail = new FSCUserDetail();
            try
            {
                SqlParameter[] parameters ={
                    new SqlParameter("@UserId",userId)
                    };
                using (SqlDataReader dr = FSC_Helper.ExecuteReader(FSC_Helper.ConnectionStringLocalTransaction,
                    CommandType.StoredProcedure, DBConstant.PROC_COMMON_GETUSERDETAIL, parameters))
                {
                    while (dr.Read())
                    {
                        objFSCUserDetail.UserId = dr.GetInt64(0);
                        objFSCUserDetail.UserEmail = dr.GetString(1);
                        objFSCUserDetail.PhoneNo = dr.GetString(2);
                        objFSCUserDetail.AlternateNo = dr.GetString(3);
                        objFSCUserDetail.FirstName = dr.GetString(4);
                        objFSCUserDetail.LastName = dr.GetString(5);
                        objFSCUserDetail.IsAnonymous = dr.GetString(6);
                        objFSCUserDetail.IsAdminPanel = dr.GetString(7);
                        objFSCUserDetail.SignUpDate = dr.GetString(8);
                        objFSCUserDetail.IsVerified = dr.GetString(9);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objFSCUserDetail;
        }

        #endregion
    }
}
