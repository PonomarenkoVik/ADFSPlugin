using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFSPlugin
{
    public static class EventLogger
    {
        public static void Log(string mess)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(mess, EventLogEntryType.Information);
            }
        }
        public static void Log(string mess, Exception ex)
        {
            using (EventLog eventLog = new EventLog("Error"))
            {
                eventLog.Source = "Error";
                eventLog.WriteEntry($"{mess} :: {ex.Message}, StackTrace: {ex.StackTrace}", EventLogEntryType.Error);
            }
        }

    }
}
