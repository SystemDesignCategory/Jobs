//using Hangfire;

namespace VideoConverter.Jobs;

public class RegisterJobsHostService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var jobInterface = typeof(IJob<>);

        var jobTypes = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .SelectMany(x => x.GetTypes())
                                .Where(type => !type.IsInterface && !type.IsAbstract)
                                .Where(type => type.GetInterfaces()
                                                   .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == jobInterface));

        foreach (var jobType in jobTypes)
        {
            var method = jobType?.GetMethod("ScheduleJob");
            var classInstance = Activator.CreateInstance(jobType);
            method?.Invoke(classInstance, null);

            /*
            var prop = jobType.GetProperty("JobCron");

            if (method == null || prop == null)
            {
                continue;
            }

            var classInstance = Activator.CreateInstance(jobType);
            var cronValue = prop.GetValue(classInstance);

            RecurringJob.AddOrUpdate(jobType.Name,
                                       () => classInstance.RunJob(),
                                       cronValue?.ToString());
            */
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
