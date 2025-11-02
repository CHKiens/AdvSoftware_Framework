using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Mandatory2DGameFramework.logger
{
    public sealed class MyLogger
    {
        private static readonly MyLogger instance = new MyLogger();
        public static MyLogger Instance { get { return instance; } }
        private TraceSource ts;
        private MyLogger() 
        {
            ts = new TraceSource("GameLog");
            ts.Switch = new SourceSwitch("SourceSwitch", "All");
        }

        public void LogInfo(string message)
        {
            ts.TraceEvent(TraceEventType.Information, 0, message);
            ts.Flush();
        }

        public void LogWarning(string message)
        {
            ts.TraceEvent(TraceEventType.Warning, 0, message);
            ts.Flush();
        }

        public void LogError(string message)
        {
            ts.TraceEvent(TraceEventType.Error, 0, message);
            ts.Flush();
        }

        public void LogCritical(string message)
        {
            ts.TraceEvent(TraceEventType.Critical, 0, message);
            ts.Flush();
        }

        public void AddFileListener(string filePath)
        {
            ts.Listeners.Add(new TextWriterTraceListener(filePath));
        }

        public void AddXmlListener(string filePath)
        {
            ts.Listeners.Add(new XmlWriterTraceListener(filePath));
        }

        public void RemoveListener(TraceListener listener)
        {
            ts.Listeners.Remove(listener);
        }
    }
}
