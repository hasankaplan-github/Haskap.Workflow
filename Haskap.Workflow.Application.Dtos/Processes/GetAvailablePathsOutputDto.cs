using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Dtos.Processes;
public class GetAvailablePathsOutputDto
{
    public IList<PathOutputDto> AvailablePaths { get; set; }
}
