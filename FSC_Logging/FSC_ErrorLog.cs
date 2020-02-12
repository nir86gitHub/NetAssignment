using FSC_DAL.DBUtility;
using FSC_Logging.Constant;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace FSC_Logging
{
    public class FSC_ErrorLog
    {
        public void LogError(ErrorLogType objErrorLogType, string controllerName, string actionName, string errorType, string errorMsg, string errorDate, string userCode, string ipAdd)
        {
            switch (objErrorLogType.LogMedia)
            {
                case "SQL":
                    LogErrorInSql(controllerName, actionName, errorType, errorMsg, errorDate, userCode, ipAdd);
                    break;
                case "TXT":
                    LogErrorInText(controllerName, actionName, errorType, errorMsg, errorDate, userCode, ipAdd);
                    break;
            }
        }

        public void LogError(ErrorLogType objErrorLogType, string controllerName, string errorType, string errorMsg, string errorDate, string userCode, string ipAdd)
        {
            switch (objErrorLogType.LogMedia)
            {
                case "SQL":
                    LogErrorInSql(controllerName, string.Empty, errorType, errorMsg, errorDate, userCode, ipAdd);
                    break;
                case "TXT":
                    LogErrorInText(controllerName, string.Empty, errorType, errorMsg, errorDate, userCode, ipAdd);
                    break;
            }
        }

        public void LogErrorInSql(string controllerName, string actionName, string errorType, string errorMsg, string errorDate, string userCode, string ipAdd)
        {
            try
            {
                SqlParameter[] parameters ={
                    new SqlParameter("@ControllerName",controllerName),
                    new SqlParameter("@ActionName",actionName),
                    new SqlParameter("@ErrorType",errorType),
                    new SqlParameter("@ErrorMsg",errorMsg),
                    new SqlParameter("@ErrorDate",errorDate),
                    new SqlParameter("@UserCode",userCode),
                    new SqlParameter("@IpAdd",ipAdd)
                    };

                FSC_Helper.ExecuteNonQuery(FSC_Helper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, DBConstant.PROC_COMMON_LOGERROR, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LogErrorInText(string controllerName, string actionName, string errorType, string errorMsg, string errorDate, string userCode, string ipAdd)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = Convert.ToString(ConfigurationManager.AppSettings["ErrorLogFilePath"]);

                logFilePath = logFilePath + "FSCLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) return;

                #region Create the Log file directory if it does not exists
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);

                #region Create Log Message

                StringBuilder messageBuilder = new StringBuilder();

                try
                {
                    messageBuilder.Append(Environment.NewLine);
                    messageBuilder.Append("=================================================================================================" + Environment.NewLine);
                    messageBuilder.Append("Controller:: " + controllerName + Environment.NewLine);
                    messageBuilder.Append("Action_Name :: " + actionName + Environment.NewLine);
                    messageBuilder.Append("Exception_Type :: " + errorType + Environment.NewLine);
                    messageBuilder.Append("Exception_Msg :: " + errorMsg + Environment.NewLine);
                    messageBuilder.Append("exception_Date :: " + errorDate + Environment.NewLine);
                    messageBuilder.Append("Exception_User :: " + userCode + Environment.NewLine);
                    messageBuilder.Append("Exception_IP :: " + ipAdd + Environment.NewLine);
                    messageBuilder.Append("=================================================================================================");
                }
                catch (Exception exc)
                {
                    messageBuilder.Append("Exception:: Unknown Exception." + exc.Message);
                }

                #endregion

                streamWriter.WriteLine(messageBuilder);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }
    }

    public class ErrorLogType
    {
        public ErrorLogType()
        {
            this.LogMedia = Convert.ToString(ConfigurationManager.AppSettings["LogMedia"]);
        }
        public string LogMedia { get; private set; }
    }
}
