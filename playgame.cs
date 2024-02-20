//using JyunrcaeaFramework;
//using NAudio.Wave;

//namespace Jyunrcaea.GamePlay;

//public class InGame : Group, Events.KeyDown
//{
//    Box backline;
//    Box underline;

//    public InGame()
//    {
//        this.Hide = false;

//        backline = new(400, Window.Height, Color.Black);
//        backline.RelativeSize = false;

//        underline = new(400, 10, Color.White);
//        underline.RelativeSize = true;
//        underline.CenterY = 0.9;
//        underline.DrawY = VerticalPositionType.Bottom;

//        this.Objects.AddRange(
//            backline,
//            underline
//            );
//    }

//    public static int width;

//    public override void Resize()
//    {
//        zoom = Window.Height / InGame.NoteSpeed;
//        width = (int)(400 * Window.AppropriateSize);
//        base.Resize();
//        backline.Size.Width = width;
//        backline.Size.Height = Window.Height;
//    }

//    BeatMap map;
//    public static AudioFileReader source, tack;
//    WasapiOut musicplayer;
//    public static IWavePlayer player;
//    public static WaveMixerStream32 mixer;

//    public static Input.Keycode n1, n2, n3, n4;

//    public const int start_delay = 2000;

//    public static async void PlayHitSound()
//    {
//        await Task.Run(() =>
//        {
//            DirectSoundOut soundOut = new DirectSoundOut(1);
//            soundOut.Init(tack);
//            soundOut.PlaybackStopped += (_, _) => soundOut.Dispose();
//            soundOut.Play();
//        });
//    }

//    public void Init()
//    {
//        n1 = Input.Keycode.d;
//        n2 = Input.Keycode.f;
//        n3 = Input.Keycode.j;
//        n4 = Input.Keycode.k;

//        for (int i = 0; i < 4; i++)
//        {
//            notetiming[i] = new();
//            holdnotes[i] = new();
//        }

//        player = new DirectSoundOut();
//        mixer = new();
//        player.Init(mixer);
//        player.Play();
//        mixer.AutoStop = false;

//        musicplayer = new(NAudio.CoreAudioApi.AudioClientShareMode.Shared,2);
//        tack = new("sounds/normal-hitnormal.wav");
//    }

//    public Queue<NotePosition>[] notetiming = new Queue<NotePosition>[4];
//    public Queue<int>[] holdnotes = new Queue<int>[4];

//    public const double pure = 30, far = 100, longfar = 300;

//    public static int notecounting, pcount, fcount,lfcount;

//    public record NotePosition(int start,int len);

//    public void KeyDown(Input.Keycode k)
//    {

//        int index;
//        if (k == n1) index = 0;
//        else if (k == n2) index = 1;
//        else if (k == n3) index = 2;
//        else if (k == n4) index = 3;
//        else return;

//        //노트없
//        if (!notetiming[index].TryPeek(out var next)) return;

//        double len = Math.Abs(next.start - Now);

//        //너무멂
//        if (len > longfar) return;

//        //PlayHitSound();
//        //Sounds.normal_hit.Play();
//        mixer.AddInputStream(tack);

//        notetiming[index].Dequeue();
//        notecounting++;

//        if (len <= pure)
//        {
//            pcount++;
//        } else if (len <= far)
//        {
//            fcount++;
//        } else
//        {
//            lfcount++;
//        }
//    }

//    public void Deinit()
//    {
//        musicplayer.Stop();


//        source.Dispose();
//        musicplayer.Dispose();
//    }


//    public void Load(BeatMap map,string music)
//    {
//        this.map = map;
//        source = new(music);
//    }

//    double StartTime;
//    double Now => Framework.RunningTime - StartTime;
//    public static double NoteSpeed = 614;

//    public static int DisplayedNow;
//    public static int AppearTime;
//    public static double zoom = 1;

//    public void Play()
//    {
//        notecounting = pcount = fcount = lfcount = 0;

//        playing = true;
//        reading = true;


//        StartTime = Framework.RunningTime + start_delay;
//        musicplayer.Play();

//        //Scheduler.Add(() =>
//        //{
//        //}, StartTime);

//    }

//    int next = 0;
//    bool playing = false;
//    bool reading = false;

//    public void Refresh()
//    {
//        DisplayedNow = (int)Now;
//        AppearTime = (int)(DisplayedNow + NoteSpeed);

//        if (!reading) return;
//        do
//        {
//            if (map.map[next].start <= AppearTime)
//            {
//                notetiming[map.map[next].index].Enqueue(new(map.map[next].start, map.map[next].len));
//                this.Objects.Add(new Note(map.map[next]));
//                next++;
//                continue;
//            }
//            break;

//        } while (next < map.map.Length);

//        if (next >= map.map.Length)
//        {
//            reading = false;
//        }
//    }

//    public static int offset = 100;

//    public override void Update(float ms)
//    {
//        if (!playing) return;
//        Refresh();
//        base.Update(ms);
//    }

//    public static int notesize = 40;

//    public class Note : Box, Events.Update, Events.Resize
//    {
//        BeatMap.Line line;
//        int end;
//        bool longnote;

//        public Note(BeatMap.Line line) : base(100,notesize + line.len,Color.LightPeriwinkle)
//        {
//            longnote = line.len > 1;
//            this.DrawX = HorizontalPositionType.Right;

//            this.CenterY = 0.9;
//            this.line = line;
//            this.DrawY = VerticalPositionType.Top;

//            end = line.start + line.len;

//            Resize();
//        }

//        bool disapper = false;

       

//        public void Update(float f)
//        {

//            if (end < DisplayedNow && !disapper)
//            {
//                PlayHitSound();
//                Scheduler.Add(() =>
//                {
//                    this.Parent.Objects.Remove(this);
                    
//                }, -200);
//                disapper = true;
//            }

//            //if (end < DisplayedNow)
//            //{
//            //    if (!disapper)
//            //    {
//            //        Animation.Add(new Animation.Info.Opacity(this, 0, null, 100, TimeCalculator: Animation.Type.EaseOutQuad, FunctionWhenFinished: (me) =>
//            //        {
//            //            me.Parent.Objects.Remove(me);
//            //        }));
//            //        disapper = true;
//            //    }
//            //}



//            this.Y = (int)( (-this.line.start + DisplayedNow) * zoom);
//        }

//        public void Resize()
//        {
//            this.Scale.X = Window.AppropriateSize;
//            this.Scale.Y = zoom;
//            int onewidth = InGame.width >> 1;
//            this.X = -onewidth;
//            onewidth = onewidth >> 1;
//            this.X += onewidth * this.line.index;
//        }
//    }
//}