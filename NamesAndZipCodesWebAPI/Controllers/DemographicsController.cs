using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using NamesAndZipCodesWebAPI.DAL;

namespace NamesAndZipCodesWebAPI.Controllers
{
    public class DemographicsController : ApiController
    {
        private string m_dataFileLocation = @HttpRuntime.AppDomainAppPath + @"\DataSource\" + System.Configuration.ConfigurationManager.AppSettings["DATASOURCE_FILE_NAME"].ToString();

        //GET: api/Demographics?Name=val
        public HttpResponseMessage GetDemographicsByName(string Name)
        {
            List<string> retValues = new List<string>();
            HttpResponseMessage message = new HttpResponseMessage();
            DAL.Repositories.DemographicRepository repoDemographic = new DAL.Repositories.DemographicRepository(m_dataFileLocation);

            try
            {
                retValues = repoDemographic.GetDemographicsByFullName(Name);

                if (retValues.Count > 0)
                {
                    message = Request.CreateResponse(HttpStatusCode.OK, retValues);
                }
                else
                {
                    //error code 400
                    message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, Name + " does not exist.");
                }

            }
            catch (Exception ex)
            {

                message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return message;
        }

        //GET: api/Demographics?ZipCode=val
        public HttpResponseMessage GetDemographicsByZipCode(string ZipCode)
        {
            List<string> retValues = new List<string>();
            HttpResponseMessage message = new HttpResponseMessage();
            DAL.Repositories.DemographicRepository repoDemographic = new DAL.Repositories.DemographicRepository(m_dataFileLocation);

            try
            {
                retValues = repoDemographic.GetDemographicsbyZipCode(ZipCode);

                if (retValues.Count > 0)
                {
                    message = Request.CreateResponse(HttpStatusCode.OK, retValues);
                }
                else
                {
                    //error code 400
                    message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ZipCode + " does not exist.");
                }

            }
            catch (Exception ex)
            {

                message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return message;
        }

        //GET: api/Demographics?Name=val&ZipCode=val
        public HttpResponseMessage GetDemographicsExists(string Name, string ZipCode)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            DAL.Repositories.DemographicRepository repoDemographic = new DAL.Repositories.DemographicRepository(m_dataFileLocation);

            try
            {
                message = Request.CreateResponse(HttpStatusCode.OK, (repoDemographic.IsExists(Name, ZipCode).ToString().ToLower()));
            }
            catch(Exception ex)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return message;
        }
    }
}
