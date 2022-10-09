using System.Diagnostics;
using Timer = System.Timers.Timer;

namespace ProcessMonitorUtility
{
    class ProcessMonitor
    {
        static Boolean monitoring = true;
        static Boolean stopMonitor = false;
        static Timer timer = new Timer();
        static void Main(string[] arguments)
        {
            string processOfInterest = arguments[0];
            TimeSpan allowedLifeTimeInSeconds = TimeSpan.FromSeconds(int.Parse(arguments[1]));
            int frequencyIntervalInSeconds = int.Parse(arguments[2]);
            timer.Interval = frequencyIntervalInSeconds * 1000;
            timer.Elapsed += MonitorProcessEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();

            DateTime End = DateTime.Now.AddSeconds(10);
            while(true)
                if (monitoring)
                {
                    Console.WriteLine($"Monitoring for {processOfInterest}....");
                    Process[] ps = Process.GetProcessesByName(processOfInterest);
                    if (ps.Count() == 0)
                    {
                        Console.WriteLine($"No instance of {processOfInterest} is running. Continue Monitoring(yes/no)");
                        timer.Enabled=false;
                        string? confirm = Console.ReadLine();
                        if (confirm!=null && confirm.Equals("yes"))
                        {
                            timer.Enabled = true;
                            continue;
                          
                        }
                        else
                        {
                            monitoring = false;
                            stopMonitor = true;
                            goto End;

                        }
                    }
                    foreach (Process p in ps)
                    {
                        DateTime processStartTime = p.StartTime;
                        TimeSpan currentTimespan = DateTime.Now - processStartTime;
                        if (currentTimespan > allowedLifeTimeInSeconds)
                        {
                            Console.WriteLine($"Time exceeded {allowedLifeTimeInSeconds}. So killing process {p.ProcessName}");
                            p.Kill();
                        }

                    }
                    monitoring = false;
                }

            End: StopMonitoring();

        }


        static void MonitorProcessEvent(Object s, System.Timers.ElapsedEventArgs e)
        {
            if (stopMonitor)
            {
                StopMonitoring();
            }
            else
            {
                Console.WriteLine("Time to Monitor......");
                monitoring = true;
            }


        }


        static void StopMonitoring()
        {
            Console.WriteLine("Monitoring is stopped. Press any key to exit");
            timer.Stop();
            Console.ReadKey();
        }




    }
}