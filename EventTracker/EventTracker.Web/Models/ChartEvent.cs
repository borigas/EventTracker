using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventTracker.Web.Models
{
    public class ChartEvent
    {
        public DateTime EventTime { get; set; }
        public double Value { get; set; }
        public string Label { get; set; }

        public double MillisecondsSinceEpoch
        {
            get
            {
                return (EventTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
            }
        }

        public static List<ChartEvent> GetLoggedInChart()
        {
            string sql = @"SELECT EventTime, CAST(IsLoggedOn AS DECIMAL) AS Value, CASE WHEN IsLoggedOn = 1 THEN 'Logged On' ELSE 'Logged Off' END AS Label FROM LogOnEvents";

            var logonEvents = GetEvents(sql);
            for (int i = 0; i < logonEvents.Count(); i += 2)
            {
                var inverseItem = new ChartEvent()
                {
                    EventTime = logonEvents[i].EventTime,
                    Label = logonEvents[i].Label,
                    Value = Convert.ToDouble(logonEvents[i].Value == 0),
                };
                logonEvents.Insert(i, inverseItem);
            }
            return logonEvents;
        }

        private static List<ChartEvent> GetEvents(string sql)
        {
            var results = new List<ChartEvent>();

            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["EventTracker"].ConnectionString))
            using (var cmd = new SqlCommand(sql, db))
            {
                db.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new ChartEvent()
                        {
                            EventTime = Convert.ToDateTime(reader["EventTime"]),
                            Value = Convert.ToDouble(reader["Value"]),
                            Label = reader["Label"].ToString(),
                        });
                    }
                }
            }
            return results;
        }
    }
}