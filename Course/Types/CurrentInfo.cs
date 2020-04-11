using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLib.Types
{
    public class CurrentInfo
    {
        public string CompanyName { get; }//название компании
        public double CompanyId { get; }//айди внутри компании
        public string InformationPath { get; }
        public CurrentInfo(string company, double id, string informationPath)
        {
            CompanyId = id;
            CompanyName = company;
            InformationPath = informationPath;
        }

    }
}
