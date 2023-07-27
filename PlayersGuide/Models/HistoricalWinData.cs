using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlayersGuide.Models
{
  internal class HistoricalWinData
  {
    public int PlayerOneWins { get; set; }
    public int PlayerTwoWins { get; set; }
  }
}
