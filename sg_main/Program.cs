namespace sg_main;
static class Program
{
    public static readonly int width = 500;
    public static readonly int height = 800;
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>

    static Form window = new SG_Window(width, height);
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
//        Application.Run(new SG_Window(width, height));
        window.ShowDialog();
    }    
}
