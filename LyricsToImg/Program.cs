// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;

class Program
{
    static int Main(string[] args)
    {
        if (!File.Exists(@"./lyrics.txt"))
        {
            Console.WriteLine("File not found");
            return 1;
        }
        
        string[] lines = File.ReadAllLines(@"./lyrics.txt");
        Font font = new Font("FUTENE", 100);
        int index = 0;
        Console.OutputEncoding = Encoding.UTF8;
        
        if (!Directory.Exists(@"./images"))
        {
            Directory.CreateDirectory(@"./images");
        }
        else
        {
            Directory.Delete(@"./images", true);
            Directory.CreateDirectory(@"./images");
        }
        
        foreach (string line in lines)
        {
            if(line == "") continue;
            Console.WriteLine("Drawing: " + line);
            Image image = DrawText(line, font, Color.Black, Color.Transparent);
            image.Save(@"./images/" + index + ".png");
            index++;
        }

        return 0;
    }
    
    private static Image DrawText(String text, Font font, Color textColor, Color backColor)
    {
        //first, create a dummy bitmap just to get a graphics object
        Image img = new Bitmap(1, 1);
        Graphics drawing = Graphics.FromImage(img);

        //measure the string to see how big the image needs to be
        SizeF textSize = drawing.MeasureString(text, font);

        //free up the dummy image and old graphics object
        img.Dispose();
        drawing.Dispose();

        //create a new image of the right size
        img = new Bitmap((int) textSize.Width, (int)textSize.Height);

        drawing = Graphics.FromImage(img);
        drawing.TextRenderingHint = TextRenderingHint.AntiAlias;
        drawing.SmoothingMode = SmoothingMode.HighQuality;
        //paint the background
        drawing.Clear(backColor);

        //create a brush for the text
        Brush textBrush = new SolidBrush(textColor);

        drawing.DrawString(text, font, textBrush, 0, 0);

        drawing.Save();

        textBrush.Dispose();
        drawing.Dispose();

        return img;

    }
}