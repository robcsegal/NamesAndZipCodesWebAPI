using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamesAndZipCodesWebAPI.Data;
using NamesAndZipCodesWebAPI.Common;

namespace NamesAndZipCodesWebAPI.DAL.Repositories
{
    public class DemographicRepository
    {
        private NamesAndZipCodesDataContext m_dcNamesAndZipCodes;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="FileLocation">file location for the sample (002) text file.</param>
        public DemographicRepository(string FileLocation)
        {
            m_dcNamesAndZipCodes = new NamesAndZipCodesDataContext(@FileLocation);
        }

        /// <summary>
        /// Converts Data.Demographics data object to the DAL.Entities.Demographic object.
        /// </summary>
        /// <param name="DemographicDataObject">Demographic direct data object.</param>
        /// <returns></returns>
        private Entities.Demographic GetDemographicDataEntity(Data.Demographic DemographicDataObject)
        {
            return new Entities.Demographic { FirstName = DemographicDataObject.FirstName, LastName = DemographicDataObject.LastName, ZipCode = DemographicDataObject.ZipCode };
        }

        /// <summary>
        /// Get Demographics by Full Name
        /// </summary>
        /// <param name="FullName">Full Name (first and last name)</param>
        /// <returns></returns>
        public List<string> GetDemographicsByFullName(string FullName)
        {
            try
            {
                return (from demo in m_dcNamesAndZipCodes.Demographics
                        where FullName.Trim() == "" || (FullName.Trim().ToLower() != "" && (demo.FirstName.Trim().ToLower() + " " + demo.LastName.Trim().ToLower() == FullName.ToLower() || demo.FirstName.ToLower() == FullName.ToLower() || demo.LastName.ToLower() == FullName.ToLower()))
                        select demo.ZipCode).Distinct().ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Demographics by zip code.
        /// </summary>
        /// <param name="ZipCode">Zip Code (12345 or 12345-1234)</param>
        /// <returns></returns>
        public List<string> GetDemographicsbyZipCode(string ZipCode)
        {
            try
            {
                //returns a list of demographic entities data, tries to use 5 digit
                //zip code comparison if parameter value does not contain a '-'.
                return (from demo in m_dcNamesAndZipCodes.Demographics
                        where ZipCode.Trim() == ""
                        || (ZipCode.Trim() != "" && ZipCode.IndexOf('-') < 1 && DemographicUtils.Get5DigitZipCode(demo.ZipCode.Trim()) == ZipCode.Trim())
                        || (ZipCode.Trim() != "" && ZipCode.IndexOf('-') > 0 && demo.ZipCode.Trim() == ZipCode.Trim())
                        select demo.FirstName + " " + demo.LastName).Distinct().ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check if demographic record exists for given full name and zip code.
        /// </summary>
        /// <param name="FullName">Full name (first and last name)</param>
        /// <param name="ZipCode">Zip Code (12345 or 12345-1234)</param>
        /// <returns></returns>
        public bool IsExists(string FullName, string ZipCode)
        {
            bool exists = false;
            int recordCount = 0;

            try
            {
                recordCount = (from demo in m_dcNamesAndZipCodes.Demographics
                               where (FullName.Trim() == "" || (FullName.Trim().ToLower() != "" && demo.FirstName.Trim().ToLower() + " " + demo.LastName.Trim().ToLower() == FullName.ToLower()))
                               && (ZipCode.Trim() == ""
                               || (ZipCode.Trim() != "" && ZipCode.IndexOf('-') < 1 && DemographicUtils.Get5DigitZipCode(demo.ZipCode.Trim()) == ZipCode.Trim())
                               || (ZipCode.Trim() != "" && ZipCode.IndexOf('-') > 0 && demo.ZipCode.Trim() == ZipCode.Trim()))
                               select GetDemographicDataEntity(demo)).ToList().Count();

                if (recordCount > 0)
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }

                return exists;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
