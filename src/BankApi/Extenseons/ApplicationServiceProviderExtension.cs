public static class ApplicationServiceProviderExtensions
{
    public static async Task<IApplicationBuilder> InitializeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var menuInitializer = scope.ServiceProvider.GetRequiredService<HeaderMenuInitializer>();
        var locationInitializer = scope.ServiceProvider.GetRequiredService<LocationMenuInitializer>();
        await menuInitializer.SeedHeaderMenu();
        await locationInitializer.SeedLocationMenu();
        return app;
    }
}