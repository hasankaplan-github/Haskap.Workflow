using Haskap.DddBase.Presentation.CustomAuthorization;

namespace Haskap.Workflow.Ui.MvcWebUi.CustomAuthorization;

public class WorkflowPermissionProvider : PermissionProvider
{
    public override void Define()
    {
        AddPermission(nameof(Permissions.Workflow), Permissions.Workflow.User);
        AddPermission(nameof(Permissions.Workflow), Permissions.Workflow.Admin);
    }
}