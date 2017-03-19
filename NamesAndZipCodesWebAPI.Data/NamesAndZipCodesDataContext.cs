using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamesAndZipCodesWebAPI.Data
{
    /// <summary>
    /// Data object for Demographic (if it was a database, this would be the database
    /// table.  Because this is a text file, this maps to each text field.
    /// </summary>
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
    }
    public class NamesAndZipCodesDataContext
    {
        private IEnumerable<Demographic> m_demographics;

        /// <summary>
        /// Object Constructor
        /// </summary>
        /// <param name="fileLocation">full path of the txt file that was sent for the sample.</param>
        public NamesAndZipCodesDataContext(string fileLocation)
        {
            List<Demographic> listDemographics = new List<Demographic>();

            //get text file (Sample (002).txt)
            string txtFileLocation = @fileLocation;
            string[] txtFileLines = System.IO.File.ReadAllLines(@txtFileLocation);

            //loop through each text file line and create a demographic object.
            foreach(string line in txtFileLines)
            {
                //split the string and remove empty values (will be caused by double spaces or more in the text file).
                string[] lineContent = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                Demographic demographicToAdd = new Demographic();

                //loop through the split line content to fill demographic object with data.
                for (int col = 0; col < lineContent.Length; col++)
                {
                    switch (col)
                    {
                        case 0:
                            //first name
                            demographicToAdd.FirstName = lineContent[col].ToString();
                            break;
                        case 1:
                            //last name
                            demographicToAdd.LastName = lineContent[col].ToString();
                            break;
                        case 2:
                            //zip code
                            demographicToAdd.ZipCode = lineContent[col].ToString();
                            break;
                    }
                }

                //add demographic object to the list
                listDemographics.Add(demographicToAdd);
            }

            //set demographics IEnumerable that is core to the data layer.
            m_demographics = listDemographics;
        }

        public IEnumerable<Demographic> Demographics
        {
            get { return m_demographics; }
        }
    }
}
 