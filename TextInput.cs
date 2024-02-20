using JyunrcaeaFramework;

namespace Jyunrcaea.Test
{
    class TextInput : Group //, Events.KeyDown, Events.KeyUp
    {
        Text text = new(null,28);

        public TextInput()
        {
            text.Content = Input.Text.Content = "Test";
            this.Objects.Add(text);
            text.TextColor = Color.Black;
        }

        public override void Prepare()
        {
            base.Prepare();
            Input.Text.Enable = true;
        }

        public void KeyDown(Input.Keycode key)
        {
            text.Content = Input.Text.Content;
            //Console.WriteLine(Input.Text.Content);
        }

        public void KeyUp(Input.Keycode key)
        {
            KeyDown(key);
        }

        float addtime = 0;
        bool underbar = false;

        public override void Update(float ms)
        {
            addtime += ms;
            if (addtime >= 614)
            {
                addtime -= 614;
                underbar = !underbar;
            }
            text.Content = Input.Text.Content + (underbar ? '_' : ' ');
            base.Update(ms);
        }
    }
}
