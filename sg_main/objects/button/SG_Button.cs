namespace sg_main.objects.button;

using NewBitmap;

public class sgButtonEventArgs : sgEventArgs{
    private int left, top, right, bottom, w, h;
    public int Left{
        get  {return left;}
    }
    public int Right{
        get{return right;}
    }
    public int Top{
        get  {return top;}
    }
    public int Bottom{
        get{return bottom;}
    }
    public int Width{
        get{return w;}
    }
    public int Height{
        get{return h;}
    }
    public sgButtonEventArgs(int left, int top, int right, int bottom){
        this.left = left;
        this.top = top;
        this.right = right;
        this.bottom = bottom;
        this.w = right - left;
        this.h = bottom - top;
    }
}
public class sgButton : sgObject{
    protected int bWidth, bHeight, right, bottom;
    protected uint color;
    private static bool firstInstance = true;

    protected virtual void OnClick(sgButtonEventArgs e){
        var handler = ClickHandler;
        if(handler != null){
            handler(this, e);
        }
    }
    protected virtual bool isClicked(){
        return Status.get("LNowClick") == 1;
    }
    public event EventHandler<sgButtonEventArgs>? ClickHandler;
    public sgButton(string name, int left, int top, int right, int bottom, uint rgba) : base(name){
        this.defaultX = left;
        this.defaultY = top;
        bWidth = right - left;
        bHeight = bottom - top;
        this.right = right;
        this.bottom = bottom;
        gameoverSkip = false;

        color = rgba;
        if(firstInstance){
            newBitmapManager.Add(new newBitmap(right - left, bottom - top, rgba), name);
            firstInstance = false;
        }
    }
    public override void update(){
        ApplyLocation();
        var events = new sgButtonEventArgs(x, y, right, bottom);
        int mouseX = Status.get("mouseX");
        int mouseY = Status.get("mouseY");
        if(isClicked() &&mouseX > this.x && mouseX < this.right && mouseY > this.y && mouseY < this.bottom){
            OnClick(events);
        }
    }
}
