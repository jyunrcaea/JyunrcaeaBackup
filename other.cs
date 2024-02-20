using JyunrcaeaFramework;
namespace Jyunrcaea
{
    public class FullBox : Box, Events.Resize
    {

        public FullBox(Color? color = null) : base(Window.Width, Window.Height, color)
        {
            this.RelativeSize = false;
        }

        public FullBox(byte r,byte g,byte b,byte a=255) : this(new(r,g,b,a))
        {

        }

        public void Resize()
        {
            this.Size.Width = Window.Width;
            this.Size.Height = Window.Height;
        }

    }
}
