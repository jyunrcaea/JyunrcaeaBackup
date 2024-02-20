using JyunrcaeaFramework;

namespace Jyunrcaea;

public class ErrorScene : Group, Events.KeyDown
{
    private Text title, des,tip;

    private string message;
    
    public ErrorScene(string error)
    {
        message = error;
        title = new Text("예상치 못한 오류가 발생했습니다!",20);
        des = new("오류 메시지:\n" + message,18);
        tip = new Text("스페이스 바를 눌러 오류 메시지를 복사할수 있습니다.",12);

        title.CenterY = 0.1;
        title.DrawY = VerticalPositionType.Bottom;

        tip.CenterY = 1;
        tip.DrawY = VerticalPositionType.Top;
        
        this.Objects.AddRange(
            title,
            des,
            tip
            );
    }

    public void KeyDown(Input.Keycode key)
    {
        if (key == Input.Keycode.SPACE)
        {
            if (Text.SetClipboard(this.message)) this.tip.Content = "클립보드에 복사 완료!";
            else this.tip.Content = "클립보드에 복사 실패했습니다. (권한이 없는것 같습니다.)";
        }
    }
    
    public override void Resize()
    {
        des.WrapWidth = (uint)(Window.Width * 0.614);
        base.Resize();
    }
}