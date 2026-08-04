namespace CarvedRock.End2End.Tests;

public static class Utilities
{
    public static string GetBaseUrl()
    {
        //return "https://localhost:7224";
        //return TestContext.Parameters.Get("BaseUrl", "https://localhost:7224");
        // CI: Read from environment variable
        var envUrl = Environment.GetEnvironmentVariable("TEST_BASEURL");
        if (!string.IsNullOrEmpty(envUrl))
        {
            return envUrl;
        }

        // Local dev: Try .runsettings parameters
        var paramUrl = TestContext.Parameters.Get("BaseUrl", null);
        if (!string.IsNullOrEmpty(paramUrl))
        {
            return paramUrl;
        }

        // Final fallback for local dev
        return "https://localhost:7224";
    }
    public static string GetApiUrl()
    {
        //return "https://localhost:7213";
        //return TestContext.Parameters.Get("ApiUrl", "https://localhost:7213");
        // CI: Read from environment variable
        var envUrl = Environment.GetEnvironmentVariable("TEST_APIURL");
        if (!string.IsNullOrEmpty(envUrl))
        {
            return envUrl;
        }

        // Local dev: Try .runsettings parameters
        var paramUrl = TestContext.Parameters.Get("ApiUrl", null);
        if (!string.IsNullOrEmpty(paramUrl))
        {
            return paramUrl;
        }

        // Final fallback for local dev
        return "https://localhost:7213";
    }
}
