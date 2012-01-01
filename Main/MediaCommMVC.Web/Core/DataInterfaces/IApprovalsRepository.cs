using MediaCommMVC.Web.Core.Model;

namespace MediaCommMVC.Web.Core.DataInterfaces
{
    public interface IApprovalsRepository
    {
        void AddAproval(Approval approval);

        Approval[] GetApprovalsForUrls(string[] approvalUrls);
    }
}