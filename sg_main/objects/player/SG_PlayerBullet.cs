namespace sg_main.objects.player;

using NewBitmap;
using sg_main.objects.enemy;


public class sgPlayerBullet : sgObject{
    private static bool firstInstance = true;
    public sgPlayerBullet(int x, int y) : base("playerBullet") {
        if(firstInstance){
            newBitmapManager.Add(new newBitmap("assets/player/bullet.png"), name);
            firstInstance = false;
        }
        this.defaultX = x;
        this.defaultY = y;
    }
    public override void update(){
        var sgobjs = sgObjectManager.get("sg").ObjectsGet();
        
        Move(0, -8);
        int thisCenterX = x + (bmpWidth >> 1);
        int thisCenterY = y + (bmpHeight >> 1);
        foreach(sgObject sgobj in sgobjs){
            if(!(sgobj is sgEnemy)) continue;
            sgEnemy sge = (sgEnemy)sgobj;
            int sgobjCenterX = sgobj.X + (sgobj.bmpWidth >> 1);
            int sgobjCenterY = sgobj.Y + (sgobj.bmpHeight >> 1);
            int dx = Math.Abs(thisCenterX - sgobjCenterX);
            int dy = Math.Abs(thisCenterY - sgobjCenterY);
            if(2 * dx < sgobj.bmpWidth + this.bmpWidth && 2 * dy < sgobj.bmpHeight + this.bmpHeight){
                sge.Damaged(1);
                this.Break();
            }
        }
        if(x < -100 || x > Program.width + 100 || y < -100 || y > Program.height + 100){
            this.Break();
        }
    }
}
