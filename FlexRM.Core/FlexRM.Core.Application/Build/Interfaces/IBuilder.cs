using FlexRM.Core.Domain.Entities.Build;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Application.Build.Interfaces
{
    internal interface IBuilder
    {
        public BuildInfo Build(BuildConfig buildConfig);
        public BuildInfo Build();
    }
}
