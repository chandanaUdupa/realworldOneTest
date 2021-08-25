using System;

namespace realworldOneTest.Utility
{
    public static class WebAppSettings
    {
        /// <summary>
        /// Returns Cats API URL
        /// </summary>
        /// <returns></returns>
        public static string GetBaseAPIURL()
        {
            return Environment.GetEnvironmentVariable("BaseAPIURL").ToString();
        }

        public static string GetSecretKey()
        {
            return Environment.GetEnvironmentVariable("Secret").ToString();
        }

        public static string GetDefaultPath()
        {
            return Environment.GetEnvironmentVariable("DefaultPath").ToString();
        }
        public static string GetAPIPath()
        {
            return Environment.GetEnvironmentVariable("APIPath").ToString();
        }
        public static string GetTagsPath()
        {
            return Environment.GetEnvironmentVariable("TagsPath").ToString();
        }

        public static string GetTypes()
        {
            return Environment.GetEnvironmentVariable("Types").ToString();
        }
        public static string GetFilter()
        {
            return Environment.GetEnvironmentVariable("Filter").ToString();
        }

    }
}
