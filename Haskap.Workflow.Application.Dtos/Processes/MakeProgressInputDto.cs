﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskap.Workflow.Application.Dtos.Processes;
public class MakeProgressInputDto
{
    public Guid RequestId { get; set; }
    public Guid CommandId { get; set; }
}
