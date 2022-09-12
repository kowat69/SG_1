namespace sg_main.objects;
#pragma warning disable CS8603

public class sgObjectManager{
    private List<sgObject> sgobjects = new List<sgObject>();
    /// Connect SG_Game SG_Object
    private static Dictionary<string, sgObjectManager> sgobjManager = new Dictionary<string, sgObjectManager>();
    private string id = "";
    public static sgObjectManager get(string id){
        sgObjectManager? ret = null;
        sgObjectManager.sgobjManager.TryGetValue(id, out ret);
        return ret;
    }
    public sgObjectManager(string id){
        this.id = id;
        sgObjectManager.sgobjManager.Add(id, this);
        
    }
    public List<sgObject> ObjectsGet(){
        return sgobjects;
    }
    public string getId(){
        return id;
    }
    public void Clear(){
        sgobjects.Clear();
    }
    public void Add(sgObject sgobj){
        sgobjects.Add(sgobj);
    }
    public void Update(){
        for(int i = 0; i < sgobjects.Count; i++){
            sgObject sgobj = sgobjects[i];
            if(Status.get("isGameOver") == 1 && sgobj.isGameoverSkip()) continue;
            sgobj.update();
        }
        for(int i = 0; i < sgobjects.Count; i++){
            sgObject sgobj = sgobjects[i];
            if(sgobj.IsBroken){
                sgobj.Cure();
                sgobjects.RemoveAt(i);
                i--;
            }
        }
    }
    public void Draw(NewBitmap.newBitmap data){
        for(int i = 0; i < sgobjects.Count; i++){
            sgObject sgobj = sgobjects[i];
            sgobj.DrawConnectednewBitmap(data);
            data.Draw(NewBitmap.newBitmapManager.get(sgobj.Name), sgobj.X, sgobj.Y);
        }
    }

}
