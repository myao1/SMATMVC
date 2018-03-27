using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMATMVC.Models
{
    public class SiteInfo
    {
        public int ID { get; set; }
        public string WebUrl { get; set; }
        public string SiteUrl { get; set; }
        public string Creator { get; set; }
        public string SiteCollectionAdmins { get; set; }
        public string Owners { get; set; }
        [Display(Name = "Last Item Modified Date")]
        [DataType(DataType.Date)]
        public DateTime? LastItemModifiedDate { get; set; }
        public SMATEnums.Disposition ProposedDisposition { get; set; }

        public SiteInfo(){

        }
        public SiteInfo(string WebUrl, string SiteUrl, string creator, string admins, string owners, DateTime lastItemModified)
        {
            this.WebUrl = WebUrl;
            this.SiteUrl = SiteUrl;
            this.Creator = creator;
            this.SiteCollectionAdmins = admins;
            this.Owners = owners;
            this.LastItemModifiedDate = lastItemModified;
            DetermineDisposition();

        }

        public SiteInfo(string WebUrl, string SiteUrl, string creator, string admins, string owners)
        {
            this.WebUrl = WebUrl;
            this.SiteUrl = SiteUrl;
            this.Creator = creator;
            this.SiteCollectionAdmins = admins;
            this.Owners = owners;
            this.LastItemModifiedDate = null;
        }

        public SiteInfo(string WebUrl, string SiteUrl, string creator, string admins)
        {
            this.WebUrl = WebUrl;
            this.SiteUrl = SiteUrl;
            this.Creator = creator;
            this.SiteCollectionAdmins = admins;
            this.Owners = string.Empty;
            this.LastItemModifiedDate = null;
        }

        public SiteInfo(string WebUrl, string SiteUrl, string creator)
        {
            this.WebUrl = WebUrl;
            this.SiteUrl = SiteUrl;
            this.Creator = creator;
            this.SiteCollectionAdmins = string.Empty;
            this.Owners = string.Empty;
            this.LastItemModifiedDate = null;
        }

        public SiteInfo(string WebUrl, string SiteUrl)
        {
            this.WebUrl = WebUrl;
            this.SiteUrl = SiteUrl;
            this.Creator = string.Empty;
            this.SiteCollectionAdmins = string.Empty;
            this.Owners = string.Empty;
            this.LastItemModifiedDate = null;
        }

        public SiteInfo(string WebUrl)
        {
            this.WebUrl = WebUrl;
            this.SiteUrl = string.Empty;
            this.Creator = string.Empty;
            this.SiteCollectionAdmins = string.Empty;
            this.Owners = string.Empty;
            this.LastItemModifiedDate = null;
        }

        private void DetermineDisposition()
        {
            DateTime expiration = DateTime.Now.AddMonths(-18);
            if (DateTime.Compare(expiration, LastItemModifiedDate.Value) < 0)
            {
                ProposedDisposition = SMATEnums.Disposition.Keep;
            }
            else
            {
                ProposedDisposition = SMATEnums.Disposition.Delete;
            }
        }
    }
}