using System.Collections;
using System.Collections.Generic;

public class DisplayText
{
    public string text;
    public int priority;

    public DisplayText(string _text, int _priority)
    {
        text = _text;
        priority = _priority;
    }

    public DisplayText()
    {
        text = string.Empty;
        priority = 0;
    }

}
