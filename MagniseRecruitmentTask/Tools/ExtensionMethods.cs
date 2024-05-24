namespace MagniseRecruitmentTask.Tools
{
    public static  class ExtensionMethods
    {
        public static string[] QueryParams(this string query)
        {
            return query.Split(',');
        }
    }
}
