namespace VideoConverter.Jobs;

public interface IJob<T> where T : class
{
    //public string JobCron { get; }
    public void ScheduleJob();
}
