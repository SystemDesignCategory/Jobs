using Hangfire;

namespace VideoConverter.Jobs;

public class FirstJob : IJob<FirstJob>
{
    //public static string _JobCron = Cron.Hourly();
    //public string JobCron => _JobCron;

    public void ScheduleJob()
    {
        RecurringJob.AddOrUpdate(nameof(FirstJob),
                                 () => RunJob(),
                                 /*JobCron,*/
                                 Cron.Hourly()
                                 );
    }
    public void RunJob()
    {
        //jobCode
    }
}
