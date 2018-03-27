using System;

namespace SMATMVC.Models
{
    public static class SMATEnums
    {
        public enum Disposition { Keep, Delete };
        public enum Category {
            Alerts,
            Apps,
            BCSApplications,
            CheckedOutFiles,
            CustomizedPAges,
            EmailEnabledLists,
            FileVersions,
            FullTrustSolution_ContentType,
            FullTrustSolution_CustomFields,
            FullTrustSolution_EventReceivers,
            FullTrustSolution_FarmFeaturesUsage,
            FullTrustSolution_SiteFeaturesUsage,
            FullTrustSolution_WebAppFeaturesUsage,
            FullTrustSolution_ListTemplatesUsage,
            FullTrustSolution_DeployedPageLayouts,
            FullTrustSolution_PageLayoutsUsage,
            FullTrustSolution_WebPartsUsage,
            InfoPath,
            LargeExcelFiles,
            LargeLists,
            LargeListViews,
            LockedSites,
            LongODBUrl_NavNodes,
            NonDefaultMasterPages,
            SandboxSolution,
            SecureStoreApplications,
            UnsupportedWebTemplate,
            WebApplicationPolicy,
            WorkflowAssociation2010,
            WorkflowAssociation2013,
            WorkflowRunning2010,
            WorkflowRunning2013
         };
    }
}