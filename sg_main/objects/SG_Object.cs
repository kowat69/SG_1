public class sgObject : IDisposable{
    protected int x = 0, y = 0;
    protected int defaultX, defaultY;
    protected double moveX, moveY;
    protected string name = "";
    protected bool isBroken = false;
    protected bool gameoverSkip = true;
    
    public virtual void update(){}
    public virtual void reset(){}
    /// This Method is unrecoomanded.
    public virtual void extraDraw(){}
    /// This Method is unrecoomanded.
    public virtual void DrawConnectednewBitmap(NewBitmap.newBitmap nbmp){}
    
    protected void Move(int x, int y){
        moveX += x;
        moveY += y;
        this.x = (int)moveX + defaultX;
        this.y = (int)moveY + defaultY;
    }
    protected void ApplyLocation(){
        x = (int)moveX + defaultX;
        y = (int)moveY + defaultY;
    }
    public sgObject(string name){
        this.name = name;
    }
    public int bmpWidth{
        get {return NewBitmap.newBitmapManager.get(name).Width;}
    }
    public int bmpHeight{
        get {return NewBitmap.newBitmapManager.get(name).Height;}

    }
    public void Break(){
        isBroken = true;
    }
    public bool isGameoverSkip() => gameoverSkip;
    public void Cure(){
        isBroken = false;
    }
    public bool IsBroken{
        get {return isBroken;}
    }
    public string Name{
        get {return name;}
    }
    public int X{
        get{return x;}
    }
    public int Y{
        get{return y;}
    }
    private bool _disposed = false;
    public void Dispose(){
        Dispose(true);
    }
    public void init(int x, int y){
        moveX = 0;
        moveY = 0;
        this.x = x;
        this.y = y;
        defaultX = x;
        defaultY = y;
    }
    protected virtual void Dispose(bool disposing){
        if(!_disposed){
            if(disposing){

            }
            _disposed = true;
        }
    }
}
/*  sgObject Exsample
 *  public class exsampleObject{
 *      private static firstInstance = true;
 *      public exsample(...) : base("exsample"){
 *          if(firstInstance){
 *              newBitmapManager.Add(new newBitmap("exsample.png"), "exsample");
 *              firstInstance = false;
 *          }
 *      }
 *  }
 * */
