namespace VideoConverter;
public class AppSettings
{
    public required HangFire HangFire { get; set; }
}

public class HangFire
{
    public static string SectionName = "HangFire";
    public int DB { get; set; }
    public required string Prefix { get; set; }
    public required string ConnectionString { get; set; }
}