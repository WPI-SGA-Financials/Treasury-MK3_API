using System.Linq;

namespace Treasury.WebAPI.Util;

public class HelperFunctions
{
    public static string[] CleanName(string[] requestName)
    {
        return requestName.Length > 0 ? requestName.Where(s => !string.IsNullOrEmpty(s)).ToArray() : requestName;
    }

    public static string[] CleanAcronym(string[] requestAcronym)
    {
        return requestAcronym.Length > 0
            ? requestAcronym.Where(s => !string.IsNullOrEmpty(s)).ToArray()
            : requestAcronym;
    }

    public static string[] CleanClassification(string[] requestClassification)
    {
        return requestClassification.Length > 0
            ? requestClassification.Where(s => !string.IsNullOrEmpty(s)).ToArray()
            : requestClassification;
    }

    public static string[] CleanType(string[] requestType)
    {
        return requestType.Length > 0 ? requestType.Where(s => !string.IsNullOrEmpty(s)).ToArray() : requestType;
    }

    public static string[] CleanDescription(string[] requestDescription)
    {
        return requestDescription.Length > 0
            ? requestDescription.Where(s => !string.IsNullOrEmpty(s)).ToArray()
            : requestDescription;
    }

    public static string[] CleanFiscalYear(string[] requestFiscalYear)
    {
        return requestFiscalYear.Length > 0
            ? requestFiscalYear.Where(s => !string.IsNullOrEmpty(s)).ToArray()
            : requestFiscalYear;
    }

    public static string[] CleanFiscalClass(string[] requestFiscalClass)
    {
        return requestFiscalClass.Length > 0
            ? requestFiscalClass.Where(s => !string.IsNullOrEmpty(s)).ToArray()
            : requestFiscalClass;
    }
}