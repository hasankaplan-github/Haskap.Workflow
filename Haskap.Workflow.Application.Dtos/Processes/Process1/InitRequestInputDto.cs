﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Dtos.Processes.Process1;
public class InitRequestInputDto
{
    public Guid ProcessId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
