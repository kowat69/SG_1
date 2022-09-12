namespace sgDebug;
#nullable disable

using System.Diagnostics;
using System.Runtime.InteropServices;
public class sgLog{
    DefaultTraceListener dtl;
    public sgLog(string path){
        if(Trace.Listeners["Default"] != null)
            dtl = (DefaultTraceListener)Trace.Listeners["Default"];
        File.Delete(path);

        if(dtl != null)
            dtl.LogFileName = path;
        
        Debug.WriteLine("これはデバッグ出力メッセージです。");
 
        // 変数をデバッグ出力することもできる
        Debug.WriteLine("NowTime：" + DateTime.Now);
    }
    
}
public class sgConsole{
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();
    [DllImport("kernel32.dll")]
    private static extern bool FreeConsole();
    public sgConsole(){
        AllocConsole();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()){AutoFlush = true}); 
    }
    public void Close(){
        FreeConsole();
    }
}
