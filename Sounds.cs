using JyunrcaeaFramework;

namespace Jyunrcaea
{
    public static class Sounds
    {
        public static Sound button_hover, button_play_select;
        public static Sound dropdown_open,dropdown_close;
        public static Sound default_hover, default_select;
        public static Sound osd_change,osd_off,osd_on;
        public static Sound normal_hit;

        public const string sound_path = "sounds/";

        public static void Init()
        {
            button_hover = new(sound_path + "button-hover.wav");
            button_play_select = new(sound_path + "button-play-select.wav");
            dropdown_open = new(sound_path + "dropdown-open.wav");
            dropdown_close = new(sound_path + "dropdown-close.wav");
            default_hover = new(sound_path + "default-hover.wav");
            default_select = new(sound_path + "default-select.wav");
            osd_change = new(sound_path + "osd-change.wav");
            osd_off = new(sound_path + "osd-off.wav");
            osd_on = new(sound_path + "osd-on.wav");
            normal_hit = new(sound_path + "normal-hitnormal.wav");
        }
    }
}
