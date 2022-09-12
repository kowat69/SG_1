namespace sg_main.objects.enemy;

public class sgEnemy : sgObject{
    private double health;
    public sgEnemy(string name, double health) : base(name){
        this.health = health;
    }
    public void Damaged(double damage){
        health -= damage;
    }
    public override void update()
    {
        Move(0, 2);
        if(health <= 0) isBroken = true;
        if(x < -100 || x > Program.width + 100 || y < -100 || y > Program.height + 100){
            this.isBroken = true;
        }
    }
}
