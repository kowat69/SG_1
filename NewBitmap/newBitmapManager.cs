namespace NewBitmap;
#pragma warning disable CS8603
public class newBitmapManager{
    static private Dictionary<string, newBitmap> bmpTable = new Dictionary<string, newBitmap>();
    public newBitmapManager(){}
    
    public newBitmap this[string id]{
        get {
            newBitmap? bmp = null;
            bmpTable.TryGetValue(id, out bmp);
            return bmp;
        }
    }
    public static newBitmap get(string id){
        newBitmap? bmp = null;
        bmpTable.TryGetValue(id, out bmp);
        return bmp;
    }
    static public void Add(newBitmap bmp, string id){
        bool res = bmpTable.TryAdd(id, bmp);
        if(!res)
            Console.WriteLine("This ID is already used! id is " + id );
    }
    static public void Remove(string id){
        bmpTable.Remove(id);
    }
    static public void list(){
        foreach(KeyValuePair<string, newBitmap> bmp in bmpTable){
            Console.WriteLine(bmp.Key);
        }
    }

   
}
