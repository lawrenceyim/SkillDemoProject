using Godot;

public class Logger
{
    public enum DebugLevel {
        NONE = 0,
        DEBUG = 1,
        INFO = 2,
        WARNING = 3,
        ERROR = 4,
        FATAL = 5,
    }
    private static DebugLevel debugLevel = DebugLevel.DEBUG;
    
    public static void Print(object sender, DebugLevel level, string message) {
        if (level >= debugLevel) {
            GD.Print(sender.GetType() + " " + message);
        }
    }
}
