namespace sg_main.objects.enemy;

using NewBitmap;

class sgEnemy_normal1 : sgEnemy{
    private static bool firstInstance = true;
    public sgEnemy_normal1(int x, int y) : base("enemy_normal1", 3){
        if(firstInstance){
            newBitmapManager.Add(new newBitmap("assets/enemies/normal1.png"), name);
            firstInstance = false;
        }
        this.defaultX = x;
        this.defaultY = y;
    }
}
