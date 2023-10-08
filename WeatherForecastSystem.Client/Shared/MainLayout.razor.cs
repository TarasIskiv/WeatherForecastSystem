using MudBlazor;

namespace WeatherForecastSystem.Client.Shared;

partial class MainLayout
{
    public MudThemeProvider MudThemeProvider { get; set; } = default!;
    public MudTheme MudTheme { get; set; } = default!;
    private bool _isDarkMode;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            ConfigureCustomPalette();
            _isDarkMode = await MudThemeProvider.GetSystemPreference();
            StateHasChanged();
        }
    }
    public void ChangeTheme()
    {
        _isDarkMode = !_isDarkMode;
    }
    
    public void ConfigureCustomPalette()
    {
        MudTheme = new MudTheme()
        {
            Palette = new PaletteLight()
            {
                Primary = Colors.Blue.Darken4,
                Secondary = Colors.Blue.Darken1,
                Info = Colors.Blue.Lighten5
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Blue.Lighten3,
                Secondary = Colors.Blue.Lighten1,
                Info = Colors.Shades.Black
            }
        };
    }
}