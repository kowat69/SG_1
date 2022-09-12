namespace NewBitmap;


#pragma warning disable CA1416

public class newBitmap
{
    int width, height;
    byte[] argbData;    // R:8 G:8 B:8 A:8
    public newBitmap(int width, int height, uint rgba){
        this.width = width;
        this.height = height;
        argbData = new byte[width * height * 4];
        for(int i = 0; i < width * height; i++){
            argbData[i * 4] = (byte)((rgba & 0xff000000) >> 24);
            argbData[i * 4 + 1] = (byte)((rgba & 0x00ff0000) >> 16);
            argbData[i * 4 + 2] = (byte)((rgba & 0x0000ff00) >> 8);
            argbData[i * 4 + 3] = (byte)(rgba & 0x000000ff);
        }
    }
    public void Clear(uint rgba){
        for(int i = 0; i < width * height; i++){
            argbData[i * 4] = (byte)((rgba & 0xff000000) >> 24);
            argbData[i * 4 + 1] = (byte)((rgba & 0x00ff0000) >> 16);
            argbData[i * 4 + 2] = (byte)((rgba & 0x0000ff00) >> 8);
            argbData[i * 4 + 3] = (byte)(rgba & 0x000000ff);
        }
    }
    public int Width{
        get{return width;}
    }
    public int Height{
        get{return height;}
    }
    public newBitmap(string path){
        var bmp = new System.Drawing.Bitmap(path);
        width = bmp.Width;
        height = bmp.Height;
        var rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
        var bmpdata  = bmp.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        IntPtr ptr = bmpdata.Scan0;

        int bytes = bmp.Width * bmp.Height * 4;
        argbData = new byte[bytes];
        
        System.Runtime.InteropServices.Marshal.Copy(ptr, argbData, 0, bytes);

        bmp.UnlockBits(bmpdata);
        

    }
    public newBitmap(System.Drawing.Bitmap bmp){
        width = bmp.Width;
        height = bmp.Height;
        var rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
        var bmpdata  = bmp.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        IntPtr ptr = bmpdata.Scan0;

        int bytes = bmp.Width * bmp.Height * 4;
        argbData = new byte[bytes];
        
        System.Runtime.InteropServices.Marshal.Copy(ptr, argbData, 0, bytes);

        bmp.UnlockBits(bmpdata);
        
    }
    public System.Drawing.Bitmap getThis(){
        var bmp = new System.Drawing.Bitmap(width, height);
        var rect = new System.Drawing.Rectangle(0, 0, width, height);
        var bmpdata  = bmp.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        IntPtr ptr = bmpdata.Scan0;

        System.Runtime.InteropServices.Marshal.Copy(argbData, 0, ptr, argbData.Length);

        bmp.UnlockBits(bmpdata);

        return bmp;
    }
    public void Draw(newBitmap nbmp, int x, int y){
        if(( this.width <= x) || (this.height <= y) ) return;

        int wLen = System.Math.Min(nbmp.width, this.width - x);
        int hLen = System.Math.Min(nbmp.height, this.height - y);
        
        for(int i = 0; i < hLen; i++){
            for(int j = 0; j < wLen; j++ ){
                if(nbmp.argbData[4 * (j + i * nbmp.width) + 3] == 0) continue;
                if(x + j < 0 || x + j >= width || y + i < 0 || y + i >= height) continue;
                this.argbData[4 * (x + j + (y + i) * this.width)] = nbmp.argbData[4 * (j + i * nbmp.width)];
                this.argbData[4 * (x + j + (y + i) * this.width) + 1] = nbmp.argbData[4 * (j + i * nbmp.width) + 1];
                this.argbData[4 * (x + j + (y + i) * this.width) + 2] = nbmp.argbData[4 * (j + i * nbmp.width) + 2];
                this.argbData[4 * (x + j + (y + i) * this.width) + 3] = nbmp.argbData[4 * (j + i * nbmp.width) + 3];
            }
        }
    }


}
