using System.Linq;

namespace Treasury.WebAPI.Util
{
    public class HelperFunctions
    {
        public static string[] CleanName(string[] requestName)
        {
            return requestName.Length > 0 ? requestName.Where(s => !string.IsNullOrEmpty(s)).ToArray() : requestName;
        }
        
        public static string[] CleanAcronym(string[] requestAcronym)
        {
            return requestAcronym.Length > 0 ? requestAcronym.Where(s => !string.IsNullOrEmpty(s)).ToArray() : requestAcronym;
        }
        
        public static string[] CleanClassification(string[] requestClassification)
        {
            return requestClassification.Length > 0 ? requestClassification.Where(s => !string.IsNullOrEmpty(s)).ToArray() : requestClassification;
        }
        
        public static string[] CleanType(string[] requestType)
        {
            return requestType.Length > 0 ? requestType.Where(s => !string.IsNullOrEmpty(s)).ToArray() : requestType;
        }
    }
}