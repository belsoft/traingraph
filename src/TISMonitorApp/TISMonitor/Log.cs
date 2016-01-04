namespace TISMonitor
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Runtime.CompilerServices;

    public class Log
    {
        static Log()
        {
            LogFilename = string.Format("{0}.log", DateTime.Today.ToString("dd-MM-yyyy"));
            UseStackFrame = false;
        }

        public static void Fatal(string format, params object[] args)
        {
            WriteLine(LoggingLevel.Fatal, format, args);
        }

        public static void Info(string format, params object[] args)
        {
            WriteLine(LoggingLevel.Info, format, args);
        }

        public static string ReadLog()
        {
            IsolatedStorageFile userStoreForSite = IsolatedStorageFile.GetUserStoreForSite();
            if (userStoreForSite.FileExists(LogFilename))
            {
                try
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(LogFilename, FileMode.OpenOrCreate, userStoreForSite))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
                catch (IOException)
                {
                }
            }
            return "";
        }

        public static void Warn(string format, params object[] args)
        {
            WriteLine(LoggingLevel.Warn, format, args);
        }

        public static void WriteLine(LoggingLevel level, string format, params object[] args)
        {
            string message = string.Format(format, args);
            if (UseStackFrame)
            {
                string name = new StackFrame(2).GetMethod().Name;
                message = string.Format(string.Format("[{0} - {1}] ", level, name) + format, args);
            }
            Debug.WriteLine(message);
            WriteToFile(message);
        }

        private static void WriteToFile(string message)
        {
            try
            {
                IsolatedStorageFile userStoreForSite = IsolatedStorageFile.GetUserStoreForSite();
                bool flag = userStoreForSite.FileExists(LogFilename);
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(LogFilename, FileMode.Append, userStoreForSite))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("[{0}] {1}", DateTime.UtcNow.ToString(), message);
                    }
                }
            }
            catch (IOException)
            {
            }
        }

        //public static string LogFilename
        //{
        //    [CompilerGenerated]
        //    get
        //    {
        //        return <LogFilename>k__BackingField;
        //    }
        //    [CompilerGenerated]
        //    set
        //    {
        //        <LogFilename>k__BackingField = value;
        //    }
        //}

        //public static bool UseStackFrame
        //{
        //    [CompilerGenerated]
        //    get
        //    {
        //        return <UseStackFrame>k__BackingField;
        //    }
        //    [CompilerGenerated]
        //    set
        //    {
        //        <UseStackFrame>k__BackingField = value;
        //    }
        //}
    }
}

