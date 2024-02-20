using Jyunrcaea.MusicSelector;
using JyunrcaeaFramework;

namespace Jyunrcaea
{
    public class Loading : Group
    {
        Box dark;

        Text MusicName;
        Text Artist;
        Text mapper;

        public Loading(BitmapInfo bitmapInfo)
        {


            dark = new(Window.Width, Window.Height, new(100, 100, 100, 100));
            dark.RelativeSize = false;

            MusicName = new(bitmapInfo.name, 24,Color.White);
            Artist = new(bitmapInfo.artist, 20, Color.White);
            mapper = new(bitmapInfo.mapper, 16, Color.White);

            this.Objects.AddRange(
                dark,
                MusicName,
                Artist,
                mapper
                );
        }

        public override void Resize()
        {
            base.Resize();
            dark.Size = new(Window.Width, Window.Height); 
        }
    }
}
