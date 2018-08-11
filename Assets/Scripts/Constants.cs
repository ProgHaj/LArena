using UnityEngine;

public static class Inputs
{
    private static string _horizontal = "Horizontal";
    private static string _vertical = "Vertical";
    private static string _aButton = "AButton";
    private static string _bButton = "BButton";
    private static string _startButton = "StartButton";

    public static string HorizontalCID(int controllerId) 
    {
        return _horizontal + controllerId;
    }

    public static string VerticalCID(int controllerId) 
    {
        return _vertical + controllerId;
    }

    public static string AButtonCID(int? controllerId = null)
    {
        return _aButton + controllerId;
    }

    public static string BButtonCID(int? controllerId = null)
    {
        return _bButton + controllerId;
    }

    public static string StartButtonCID(int? controllerId = null)
    {
        return _startButton + controllerId;
    }

    public static bool AButton(int playerId) {
        return Input.GetButtonDown(AButtonCID(GameManager.controllerId[playerId]));
    }

    public static bool BButton(int playerId) {
        return Input.GetButton(BButtonCID(GameManager.controllerId[playerId]));
    }

    public static bool StartButton(int playerId) {
        return Input.GetButtonDown(StartButtonCID(GameManager.controllerId[playerId]));
    }

    public static float Horizontal(int playerId) {
        return Input.GetAxisRaw(HorizontalCID(GameManager.controllerId[playerId]));
    }

    public static float Vertical(int playerId) {
        return Input.GetAxisRaw(VerticalCID(GameManager.controllerId[playerId]));
    }
}