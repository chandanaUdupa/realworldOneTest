using Microsoft.Extensions.Logging;
using realworldOneTest.Service;
using realworldOneTest.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace realworldOneTest.BusinessLogic
{
    public class CatBusinessLogic : ICatBusinessLogic
    {
        ILogger _log;
        ICatService _catService;
        public CatBusinessLogic(ILogger<CatBusinessLogic> logger, ICatService catService)
        {
            _log = logger;
            _catService = catService;
        }


        /// <summary>
        /// Basic version of API call
        /// </summary>
        /// <returns></returns>
        public byte[] GetRandomCatImageDefault()
        {
            try
            {
                string apiURL = WebAppSettings.GetBaseAPIURL() + WebAppSettings.GetDefaultPath();
                byte[] kittenImageInBytes = _catService.GetRandomCatImage(apiURL);
                return ImageHelper.RotateImage(kittenImageInBytes);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Fetch cat image by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public byte[] GetRandomCatImageByType(string type)
        {
            try
            {
                string[] types = WebAppSettings.GetTypes().Split(',');

                if (types.Select(x => x.Trim().ToLower()).Contains(type.ToLower()))
                {
                    string apiURL = WebAppSettings.GetBaseAPIURL() + WebAppSettings.GetDefaultPath() + "?type=" + type;
                    byte[] kittenImageInBytes = _catService.GetRandomCatImage(apiURL);
                    return ImageHelper.RotateImage(kittenImageInBytes);
                }
                else
                {
                    throw new ArgumentException("Invalid type", "type");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Fetch cat image by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public byte[] GetRandomCatImageByFilter(string filter)
        {
            try
            {
                string[] filters = WebAppSettings.GetFilter().Split(',');

                if (filters.Select(x => x.Trim().ToLower()).Contains(filter.ToLower()))
                {
                    string apiURL = WebAppSettings.GetBaseAPIURL() + WebAppSettings.GetDefaultPath() + "?filter=" + filter;
                    byte[] kittenImageInBytes = _catService.GetRandomCatImage(apiURL);
                    return ImageHelper.RotateImage(kittenImageInBytes);
                }
                else
                {
                    throw new ArgumentException("Invalid filter", "filter");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Cat json data
        /// </summary>
        /// <returns></returns>
        public string GetCatJsonData()
        {
            try
            {
                string apiURL = WebAppSettings.GetBaseAPIURL() + WebAppSettings.GetTagsPath();
                return _catService.GetCatJsonData(apiURL);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
