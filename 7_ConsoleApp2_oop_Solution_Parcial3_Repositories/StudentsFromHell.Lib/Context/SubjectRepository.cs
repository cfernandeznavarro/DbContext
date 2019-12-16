﻿using Academy.Lib.Infrastructure;
using Academy.Lib.Models;
using System.Collections.Generic;

namespace Academy.Lib.Context
{
    public class SubjectRepository : Repository<Subject>
    {
        public static Dictionary<string, Subject> SubjectsByName { get; set; } = new Dictionary<string, Subject>();

        public override SaveResult<Subject> Add(Subject entity)
        {
            var output = base.Add(entity);

            if (output.IsSuccess)
            {
                SubjectsByName.Add(output.Entity.Name, output.Entity);
            }

            return output;
        }

        public override SaveResult<Subject> Update(Subject entity)
        {
            var output = base.Update(entity);

            if (output.IsSuccess)
            {
                SubjectsByName[output.Entity.Name] = output.Entity;
            }

            return output;
        }

        public Subject GetSubjectByName(string Name)
        {
            if (SubjectsByName.ContainsKey(Name))
                return SubjectsByName[Name];

            return null;
        }
    }
}
