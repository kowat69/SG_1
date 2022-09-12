namespace sg_main.objects.player;


using NewBitmap;
using sg_main.objects.enemy;

public class sgPlayer : sgObject{
    ///BMP Information
    ///if fps is changed this also will be cahanged
    double Fspeed = 4;
    ///speed 
    double _speed = 4;
    double sneekPer = 0.4;
    ///Location
    
    double health = 2.0;
    readonly double DEFAULT_HEALTH = 2.0;
    int damageInterval = 0;
    readonly int DAMAGE_INTERVAL = 1200;
    public Dictionary<string, int> status = new Dictionary<string, int>();
    
    private static bool firstInstance = true;
    public sgPlayer() : base("player"){
        if(firstInstance){
            newBitmapManager.Add(new newBitmap("assets/player/player1.png"), name);
            firstInstance = false;
        }
        status.Add("moveX", 0);
        status.Add("moveY", 0);
        status.Add("shot", 0);
        status.Add("shotInterval", 0);
        status.Add("sneek", 0);
        status.Add("death", 0);
        ///
    }
    
    
    public override void update(){
        shot();
        move();
        damage();
        
        if(health <= 0){
            status["death"] = 1;
        }
//        Console.WriteLine(moveX + " " + moveY);
//        Console.WriteLine(mX + " " + mY);
//        Console.WriteLine(Fspeed);
    }
    private void damage(){
        int thisCenterX = x + (bmpWidth >> 1);
        int thisCenterY = y + (bmpHeight >> 1);
        foreach(sgObject sgobj in sgObjectManager.get("sg").ObjectsGet()){
            if(!(sgobj is sgEnemy)) continue;
            sgEnemy sge = (sgEnemy)sgobj;
            int sgobjCenterX = sgobj.X + (sgobj.bmpWidth >> 1);
            int sgobjCenterY = sgobj.Y + (sgobj.bmpHeight >> 1);
            int dx = Math.Abs(thisCenterX - sgobjCenterX);
            int dy = Math.Abs(thisCenterY - sgobjCenterY);
            if(damageInterval <= 0 && 2 * dx < sgobj.bmpWidth + this.bmpWidth && 2 * dy < sgobj.bmpHeight + this.bmpHeight){
                Console.WriteLine("hit");
                health--;
                damageInterval = DAMAGE_INTERVAL;
            }else{
                if(damageInterval > 0) damageInterval--;
            }
        }
    }
    private void shot(){
        if(status["shotInterval"] > 0) status["shotInterval"]--;
        if(status["shot"] == 0 || status["shotInterval"] > 0) return;
         sgObjectManager.get("sg").Add( new sgPlayerBullet(x + (bmpWidth >> 1), y) );
        status["shotInterval"] = 10;
    }
    private void move(){
        Fspeed = _speed;
        if(status["sneek"] == 1) Fspeed = _speed * sneekPer;
        double len = Math.Sqrt( square(status["moveX"]) + square(status["moveY"]));
        if(len != 0){
         
            moveX += status["moveX"] / len * Fspeed;
            moveY += status["moveY"] / len * Fspeed;
        }
        posFixM3();
        ApplyLocation();
    }
    
    ///Recommanded
    private void posFixM2(){
        if( Math.Abs(moveX) > Math.Abs((Program.width - bmpWidth) >> 1) ) moveX = getsign(moveX) * ((Program.width - bmpWidth) >> 1);
        if( Math.Abs(moveY) > Math.Abs((Program.height - bmpHeight) >> 1) ) moveY = getsign(moveY) * ((Program.height - bmpHeight) >> 1);
    }
    private void posFixM3(){
        if(defaultX + moveX < 0) moveX = -defaultX;
        else if(defaultX + moveX + bmpWidth > Program.width) moveX = Program.width - defaultX - bmpWidth;
        if(defaultY + moveY < 0) moveY = -defaultY;
        else if(defaultY + moveY + bmpHeight > Program.height) moveY = Program.height - defaultY - bmpHeight;
    }
    public override void reset(){
        
        status["moveX"] = 0;
        status["moveY"] = 0;
        status["shot"] = 0;
        status["shotInterval"] = 0;
        status["sneek"] = 0;
        status["death"] = 0;
        damageInterval = 0;
        health = DEFAULT_HEALTH;
    }
    private int getsign(double a){
        if(a < 0) return -1;
        if(0 < a) return 1;
        return 0;
    }
    private int square(int a){
        return a * a;
    }
}
