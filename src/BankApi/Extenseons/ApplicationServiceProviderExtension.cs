public static class ApplicationServiceProviderExtensions
{
    public static async Task<IApplicationBuilder> InitializeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();     
        var initializer = scope.ServiceProvider.GetRequiredService<HeaderMenuInitializer>();
        await initializer.SeedMenu();      
        return app;
    }
}