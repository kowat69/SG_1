namespace sg_main.game;

using NewBitmap;
using sg_main.objects;
using sg_main.objects.player;
using sg_main.objects.enemy;
using sg_main.objects.button;

public partial class SG_Game{
    newBitmap stagedata;
    newBitmap objectsdata;
    Bitmap? _stage = null;
    Bitmap? _objects = null;
    sgObjectManager sgobjManager = new sgObjectManager("sg");
    SG_GameStatus sgStatus = SG_GameStatus.SG_START_INIT;
    Random rand = new Random();
    sgPlayer player = new sgPlayer();
    
    
    ///Start Screen
    private sgTitle gameTitle = new sgTitle();
    private sgButton startbutton;

    private bool isInit(){
        return (stagedata != null) && (objectsdata != null);
    }
    public SG_Game(int width, int height){
         stagedata = new newBitmap(width, height, 0xffffffff);
        objectsdata = new newBitmap(width, height, 0xffffff00);
        
        startbutton = new sgButton("startbutton",200, 400, 300, 450, 0x000000ff);
        startbutton.ClickHandler += startbutton_Click;

        statusSet();
    }
    void gameInit(){
//        player.init((Program.width - newBitmapManager.get("player").Width) >> 1, (Program.height - newBitmapManager.get("player").Height) >> 1);
        player.init((Program.width - newBitmapManager.get("player").Width) >> 1, (int)((Program.height - newBitmapManager.get("player").Height) * 0.8));
//        Console.WriteLine(player.defaultY);
        this.Add(player);
    }
    public void wndProc(ref Message m){
        switch(m.Msg){
            case WM_KEYDOWN:
            case WM_KEYUP:
            {
                this.playerKeyProcess();
                break;
            }
            case WM_MOUSEMOVE:
            {
                Status.set("mouseX", m.LParam.ToInt32() & 0xffff);
                Status.set("mouseY", (int)(m.LParam.ToInt32() & 0xffff0000) >> 16);
                break;
            }
            case WM_LBUTTONDOWN:
            {
                Status.set("LClickng", 1);
                Status.set("LNowClick", 1);
                __click = false;
                break;
            }
            case WM_LBUTTONUP:
            {
                player.status["LClicking"] = 0;
                break;
            }
            default:
            break;
        }
    }
    private void startbutton_Click(object? sender, sgButtonEventArgs e){
        sgStatus = SG_GameStatus.SG_PALYGAME;
        startbutton.Break();
        Console.WriteLine("ButtonClick");
        
    }
    bool __click = false;
    private void Refreash(){
        if(__click) {
            Status.set("LNowClick", 0);
        }else{
            __click = true;
        }
        switch(sgStatus){
            
            case SG_GameStatus.SG_START_INIT:
            {
                sgobjManager.Clear();
                player.reset();
                Status.reset();
                sgStatus = SG_GameStatus.SG_START;
                this.Add(startbutton);

                gameTitle.init((Program.width - gameTitle.bmpWidth) >> 1, 50);
                this.Add(gameTitle);
                break;
            }
            case SG_GameStatus.SG_START:
            {
                break;
            }
            case SG_GameStatus.SG_PALYGAME:
            {
                sgobjManager.Clear();
                gameInit();
                sgStatus = SG_GameStatus.SG_INGAME;
                break;
            }
            case SG_GameStatus.SG_INGAME:
            {
                if(rand.Next(0, 120) == 0) {
                    this.Add(new sgEnemy_normal1(rand.Next(0, 480), -100 ));
                }
                if(player.status["death"] == 1){
                    Status.set("isGameOver", 1);
                    sgStatus = SG_GameStatus.SG_GAMEOVER;
                }
                break;
            }
            case SG_GameStatus.SG_GAMEOVER:
            {
                break;
            }
        }
    }
    private void playerKeyProcess(){
        /// Player Movement

        switch(sgStatus){
            
            case SG_GameStatus.SG_START:
            {
                break;
            }
            case SG_GameStatus.SG_PALYGAME:
            {
                break;
            }
            case SG_GameStatus.SG_INGAME:
            {
                
                if(XOR(sg_main.KeyState.get(VKeys.VK_RIGHT), sg_main.KeyState.get(VKeys.VK_LEFT)) ){
                    if(KeyState.get(VKeys.VK_RIGHT)) player.status["moveX"] = 1;
                    else player.status["moveX"] = -1;
                }else{
                    player.status["moveX"] = 0;
                }
                if( XOR(KeyState.get(VKeys.VK_UP), KeyState.get(VKeys.VK_DOWN)) ){
                    if(KeyState.get(VKeys.VK_UP))  player.status["moveY"] = -1;
                    else player.status["moveY"] = 1;
                }else{
                    player.status["moveY"] = 0;
                }

                /// Player Speed
                if(KeyState.get(VKeys.VK_SHIFT))
                    player.status["sneek"] = 1;
                else player.status["sneek"] = 0;

                if(KeyState.get(VKeys.VK_SPACE))
                    player.status["shot"] = 1;
                else player.status["shot"] = 0;
                break;
            }
            case SG_GameStatus.SG_GAMEOVER:
            {
                if(KeyState.get(VKeys.VK_ESCAPE)) {
                    sgStatus = SG_GameStatus.SG_START_INIT;
                }
                break;
            }
        }
    }
    private void statusSet(){
        Status.Add("LNowClick");
        Status.Add("LClicking");
        Status.Add("mouseX");
        Status.Add("mouseY");
        Status.Add("isGameOver");
    }
    public void processUpdate(){
        Refreash();
        sgobjManager.Update();
        
    }

    private void update(){
        sgobjManager.Draw(objectsdata);
   }
    public void draw(System.Drawing.Graphics g){
        update();
        if(_stage != null) _stage.Dispose();
        if(_objects != null) _objects.Dispose();

        _stage = stagedata.getThis();
        _objects = objectsdata.getThis();

        g.DrawImage(_stage, 0, 0);
        g.DrawImage(_objects, 0, 0);
    }
    public void Add(sgObject sgo){
        sgobjManager.Add(sgo);
    }
    public void Clear(System.Drawing.Graphics g){
        g.Clear(System.Drawing.Color.White);
        
        objectsdata.Clear(0xffffff00);
    } 
    private bool XOR(bool a, bool b){
        return (a || b) && !(a && b);
    }
    private bool XOR(int a, int b){
        return (a * b == 0) && (a + b > 0);
    }
    private int boolToint(bool t){
        return t?1:0;
    }

}
enum SG_GameStatus : int{
    SG_START_INIT,
    SG_START,
    SG_PALYGAME,
    SG_INGAME,
    SG_GAMEOVER,
}
