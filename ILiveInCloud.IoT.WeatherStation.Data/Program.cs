using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.ServiceBus.Messaging;
using ILiveInCloud.IoT.WeatherStation.Data.TelemetryModel;
using Newtonsoft.Json;
using static ILiveInCloud.IoT.WeatherStation.Data.Range;

namespace ILiveInCloud.IoT.WeatherStation.Data
{
    class Program
    {

        private static DateTime _fakeDateTime = new DateTime(2016, 08, 01, 0,0,0);
        private static Random _randomTemp = new Random();
        static string eventHubName = "ilic-iot";
        static string connectionString = "Endpoint=sb://ilic-iot-ns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hGOl/2A0MUvzf0KN1z2Svgc+C7EzlTPj64fAD/h+QKg=";
        static void Main(string[] args)
        {
            Console.WriteLine("Press Ctrl-C to stop the sender process");
            Console.WriteLine("Press Enter to start now");
            Console.ReadLine();
            SendingRandomMessages();
        }

        static void SendingRandomMessages()
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
            while (true)
            {
                try
                {

                    _fakeDateTime = _fakeDateTime.AddMinutes(5);
                    var message = getRandomTelemetry();
                    
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, message);
                    // eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(getRandomTelemetry())));
                    
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }

                Thread.Sleep(200);
            }
        }

        static string getRandomTelemetry() {
            var bmphModel = new BMPHModel
            {
                Temperature = getTempTelemetry(),
                TelmetryDateTime = _fakeDateTime
                
            };
            return JsonConvert.SerializeObject(bmphModel);
        }

        /// <summary>
        /// Create Next float 
        /// </summary>
        /// <param name="random"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static float NextFloat(Random random,int start,int end)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, random.Next(-126, 128));
            return (float)(mantissa * exponent);
        }
        /// <summary>
        /// Get random temperature from the predefined rule
        /// </summary>
        /// <returns></returns>
        private static float getTempTelemetry()
        {
            var tempRange = getTempRangeInTime(_fakeDateTime.TimeOfDay);
            return NextFloat(_randomTemp, Convert.ToInt32(tempRange.Start), Convert.ToInt32(tempRange.End));
        }
        /// <summary>
        /// Get Predefined rule for temperature
        /// </summary>
        /// <returns></returns>
        private static Dictionary<TimeRange,TempRange> getDataGenerateRule(){
            var data = new Dictionary<TimeRange, TempRange>();
            data.Add(new TimeRange { StartTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(1, 0, 0) }, new TempRange { Start = 16, End = 16 });
            data.Add(new TimeRange { StartTime = new TimeSpan(1, 0, 0), EndTime = new TimeSpan(2, 0, 0) }, new TempRange { Start = 15, End = 16 });
            data.Add(new TimeRange { StartTime = new TimeSpan(2, 0, 0), EndTime = new TimeSpan(3, 0, 0) }, new TempRange { Start = 16, End = 17 });
            data.Add(new TimeRange { StartTime = new TimeSpan(3, 0, 0), EndTime = new TimeSpan(4, 0, 0) }, new TempRange { Start = 15, End = 16 });
            data.Add(new TimeRange { StartTime = new TimeSpan(4, 0, 0), EndTime = new TimeSpan(5, 0, 0) }, new TempRange { Start = 17, End = 17 });
            data.Add(new TimeRange { StartTime = new TimeSpan(5, 0, 0), EndTime = new TimeSpan(6, 0, 0) }, new TempRange { Start = 17, End = 18 }); data.Add(new TimeRange { StartTime = new TimeSpan(6, 0, 0), EndTime = new TimeSpan(7, 0, 0) }, new TempRange { Start = 16, End = 17 });
            data.Add(new TimeRange { StartTime = new TimeSpan(7, 0, 0), EndTime = new TimeSpan(8, 0, 0) }, new TempRange { Start = 17, End = 18 });
            data.Add(new TimeRange { StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(9, 0, 0) }, new TempRange { Start = 17, End = 20 });
            data.Add(new TimeRange { StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(10, 0, 0) }, new TempRange { Start = 21, End = 22 });
            data.Add(new TimeRange { StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 0, 0) }, new TempRange { Start = 25, End = 26 });
            data.Add(new TimeRange { StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0) }, new TempRange { Start = 29, End = 30 }); data.Add(new TimeRange { StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(13, 0, 0) }, new TempRange { Start = 30, End = 31 });
            data.Add(new TimeRange { StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0) }, new TempRange { Start = 30, End = 31 });
            data.Add(new TimeRange { StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(15, 0, 0) }, new TempRange { Start = 26, End = 27 });
            data.Add(new TimeRange { StartTime = new TimeSpan(15, 0, 0), EndTime = new TimeSpan(16, 0, 0) }, new TempRange { Start = 26, End = 27 });
            data.Add(new TimeRange { StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 0, 0) }, new TempRange { Start = 25, End = 26 });
            data.Add(new TimeRange { StartTime = new TimeSpan(17, 0, 0), EndTime = new TimeSpan(18, 0, 0) }, new TempRange { Start = 25, End = 24 }); data.Add(new TimeRange { StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(19, 0, 0) }, new TempRange { Start = 24, End = 25 });
            data.Add(new TimeRange { StartTime = new TimeSpan(19, 0, 0), EndTime = new TimeSpan(20, 0, 0) }, new TempRange { Start = 23, End = 24 });
            data.Add(new TimeRange { StartTime = new TimeSpan(20, 0, 0), EndTime = new TimeSpan(21, 0, 0) }, new TempRange { Start = 22, End = 23 });
            data.Add(new TimeRange { StartTime = new TimeSpan(21, 0, 0), EndTime = new TimeSpan(22, 0, 0) }, new TempRange { Start = 20, End = 22 });
            data.Add(new TimeRange { StartTime = new TimeSpan(22, 0, 0), EndTime = new TimeSpan(23, 0, 0) }, new TempRange { Start = 20, End = 21 });
            data.Add(new TimeRange { StartTime = new TimeSpan(23, 0, 0), EndTime = new TimeSpan(24, 0, 0) }, new TempRange { Start = 20, End = 21 });
            return data;
        }
        /// <summary>
        /// Get temperature in defined range 
        /// </summary>
        /// <param name="timeSpan">Time range </param>
        /// <returns></returns>
        private static TempRange getTempRangeInTime(TimeSpan timeSpan) {
            var tmeSpan = getDataGenerateRule();
            var tme = tmeSpan.Where(x => x.Key.StartTime >= timeSpan ).Where(x=>x.Key.EndTime <= timeSpan).ToList();
            return tme.FirstOrDefault().Value;
        }


    }
}
