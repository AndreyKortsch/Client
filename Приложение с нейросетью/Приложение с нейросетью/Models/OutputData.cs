using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Models
{
    public class OutputData
    {
        [ColumnName("dense")]
        public String  Scores
        {
            get; set;
        }
    }
}
