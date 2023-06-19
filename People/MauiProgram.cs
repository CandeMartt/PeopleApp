namespace People;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// El Singleton provee los datos a todos los componentes que lo utilizan que en nuestro caso es solo la interfaz de usuario
		string dbPath = FileAccessHelper.GetLocalFilePath("peopledb1.db3");

		builder.Services.AddSingleton<PersonRepository>
			(s => ActivatorUtilities.CreateInstance<PersonRepository>(s, dbPath));
			
        return builder.Build();
	}
}
