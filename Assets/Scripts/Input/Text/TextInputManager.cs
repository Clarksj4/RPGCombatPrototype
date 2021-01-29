
public class TextInputManager : MonoSingleton<TextInputManager>
{
    private TextCommandParser parser = new TextCommandParser();

    public void Input(string text)
    {
        Command command = parser.Parse(text);
        if (command != null)
            command.Do();
    }
}
