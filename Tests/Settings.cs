namespace Tests;

internal static class Settings
{
    public static string ConnectionString => _instance!.ConnectionString!;

    private static AppSettings? _instance;

    internal static void SetInstance(AppSettings? settings)
    {
        _instance = settings;
    }
}