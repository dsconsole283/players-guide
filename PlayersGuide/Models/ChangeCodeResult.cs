using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayersGuide.Models
{
  public class ChangeCodeResult
  {
    public bool Succeeded { get; set; }
    public string? FailureReason { get; set; }
  }
}
