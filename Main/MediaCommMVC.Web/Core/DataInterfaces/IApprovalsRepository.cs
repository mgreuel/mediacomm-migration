using System.Collections.Generic;

using MediaCommMVC.Web.Core.Model;

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    public interface IApprovalsRepository
    {
        void AddAproval(Approval approval);

        Approval[] GetApprovalsForUrls(string[] approvalUrls);

        Approval[] GetNewestApprovals(int count);

        IEnumerable<KeyValuePair<string, int>> GetUrlsWithMostApprovals(int count);
    }
}