using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamesAndZipCodesWebAPI.DAL.Entities
{
    public class Demographic
    {
        private string m_firstName;
        private string m_lastName;
        private string m_zipCode;

        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }

        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }

        public string ZipCode
        {
            get { return m_zipCode; }
            set { m_zipCode = value; }
        }

        public string FullName
        {
            get { return m_firstName + " " + m_lastName; }
        }
    }
}
