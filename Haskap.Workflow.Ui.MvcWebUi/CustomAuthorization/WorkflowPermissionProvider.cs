using Haskap.DddBase.Presentation.CustomAuthorization;

namespace Haskap.Workflow.Ui.MvcWebUi.CustomAuthorization;

public class WorkflowPermissionProvider : PermissionProvider
{
    public override void Define()
    {
        AddPermission(nameof(Permissions.Recipe), Permissions.Recipe.Editor);
        AddPermission(nameof(Permissions.Recipe), Permissions.Recipe.Admin);
    }
}