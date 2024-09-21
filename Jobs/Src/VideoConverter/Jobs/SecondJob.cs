using Hangfire;

namespace VideoConverter.Jobs;

public class SecondJob : IJob<SecondJob>
{
    public void ScheduleJob()
    {
        RecurringJob.AddOrUpdate(nameof(FirstJob),
                                 () => RunJob(),
                                 Cron.Hourly(10)
                                 );
    }
    public void RunJob()
    {
        //jobCode
    }
}
