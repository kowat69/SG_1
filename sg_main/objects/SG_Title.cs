namespace sg_main.objects;
using NewBitmap;
public class sgTitle : sgObject{
    private static bool firstInstance = true;
    public sgTitle() : base("title"){
        if(firstInstance){
            newBitmapManager.Add(new newBitmap("assets/title/gametitle.png"), name);
            firstInstance = false;
        }
    }
}

