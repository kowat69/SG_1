namespace sg_main;


using System.Runtime.InteropServices;
using sgDebug; 
using sg_main.game;
using System.Windows.Forms;

public partial class SG_Window : Form
{
    [DllImport("winmm.dll")]
    private static extern uint timeBeginPeriod(uint uMilliseconds);
    [DllImport("winmm.dll")]
    private static extern uint timeEndPeriod(uint uMilliseconds);
    
    SG_Game sgGame;
    //Debug OutPut
    sgConsole sgC;
    
    int fps = 120;

    public SG_Window(int width, int height)
    {
        //Form Init
        InitializeComponent(width, height);
        sgGame = new SG_Game(width, height);
        //Debug(sgConsole) Init
        sgC = new sgConsole();

        Console.WriteLine("init");
        Task.Run(Process);
    }
    private void Process(){
        timeBeginPeriod(1);
        Console.WriteLine("Process");
        FPS.init();
        while(true){
            FPS.start();


            sgGame.processUpdate();
//            Console.WriteLine(a++ );
            

            FPS.end();
//            Console.WriteLine(FPS.dt);
//            Console.WriteLine(FPS.getfps());
            this.Invalidate();
            Thread.Sleep(1000 / fps);

        }
    }
    protected override void OnLoad(EventArgs e){
        this.DoubleBuffered = true;
    }
    protected override void OnClosed(EventArgs e)
    {
        sgC.Close();
    }
    protected override void WndProc(ref Message m)
    {
        sgGame.wndProc(ref m);
        base.WndProc(ref m);
    }
    protected override void OnPaint(PaintEventArgs e){
        Graphics g = e.Graphics;
        sgGame.Clear(g);
        sgGame.draw(g);
    }
}
