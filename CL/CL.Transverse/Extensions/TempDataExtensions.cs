using System.Web.Mvc;
using CL.Transverse.Enums;

namespace CL.Transverse.Extensions
{
    public static class TempDataExtensions
    {
        public static void SetStatusMessage(this TempDataDictionary tempData, string message, StatusMessageType messageType = StatusMessageType.Success)
        {
            tempData[Constants.GeneralAdminConfigs.StatusMessageText] = message;
            tempData[Constants.GeneralAdminConfigs.StatusMessageType] = messageType;
        }
    }
}
