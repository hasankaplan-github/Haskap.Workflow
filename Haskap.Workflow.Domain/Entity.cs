﻿using Haskap.DddBase.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haskap.Workflow.Domain;

public abstract class Entity : Entity<Guid>, IEntity
{
    protected Entity()
    {

    }

    protected Entity(Guid id)
        : base(id)
    {

    }
}
