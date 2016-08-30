using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILiveInCloud.IoT.WeatherStation.Data
{
    public class Range {
        public class TimeRange
        {
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
        }

        public class TempRange {
            public float Start { get; set; }
            public float End { get; set; }
        }
    }
    
}
