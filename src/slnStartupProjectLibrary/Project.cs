using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace slnStartupProjectLibrary
{
    class Project
    {
        public string   ProjectName;
        public string   CDATA;
        public int      Offset;

        public Project(string cdata, int index)
        {
            CDATA = cdata;
            Offset = index;
            MatchCollection projectQuotesMatches = Regex.Matches(CDATA, @"""[^""\\]*(?:\\.[^""\\]*)*""");
            if (projectQuotesMatches.Count == 0)
                throw new Exception("Could not extract quoted strings from the project definition while looking for its name");
            if (projectQuotesMatches.Count < 2)
                throw new Exception("Could not locate project's name from the project definition");
            ProjectName = projectQuotesMatches[1].Value.Trim('"');
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", ProjectName, Offset);
        }
    }
}
