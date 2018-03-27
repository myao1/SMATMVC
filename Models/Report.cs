using System;
using System.Collections.Generic;

namespace SMATMVC.Models
{
    public class Report
    {
        public SMATEnums.Category Category { get; set; }
        public int Count { get { return dictionary.Count; } }
        private Dictionary<String, int> dictionary;

        public Report(SMATEnums.Category category)
        {
            this.Category = category;
            dictionary = new Dictionary<string, int>();
        }

        public int getCountOfSites(string url)
        {
            return dictionary[url];
        }

        private void CountSites(){

        }
    }
}