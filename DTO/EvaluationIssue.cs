using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSense.DTO;

public class EvaluationIssue
{
    public string Message { get; set; } = string.Empty;
    public string FixUrl { get; set; } = string.Empty;
    public int Priority { get; set; }
}
