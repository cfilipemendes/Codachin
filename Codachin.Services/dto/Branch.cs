using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codachin.Services.dto
{
    public class Branch
    {
        public List<Commit> CommitHistory { get; set; }
    }
}
