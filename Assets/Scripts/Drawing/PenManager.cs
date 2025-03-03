using UnityEngine;

public class PenManager : MonoBehaviour
{
    public Color penColor = Color.black;
    public int penSize = 5;

    /// ===[ SETTER ]=== \\\
    ///==================\\\
    public void SetColor(int color)
    {
        switch(color) {
            case 1:
                penColor = Color.red;
                break;
            case 2:
                penColor = Color.blue;
                break;
            case 3:
                penColor = Color.yellow;
                break;
            case 4:
                penColor = Color.green;
                break;
            case 5:
                penColor = Color.black;
                break;
        }
    }

    public void SetSize(int newSize)
    {
        penSize = newSize;
    }

    /// ===[ GETTER ]=== \\\
    ///==================\\\
    public Color GetPenColor()
    {
        return penColor;
    }

    public int GetPenSize()
    {
        return penSize;
    }
}
