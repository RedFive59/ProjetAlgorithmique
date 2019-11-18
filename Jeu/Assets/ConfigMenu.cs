public static class ConfigMenu
{
    private static bool seen = false;

    public static bool Seen
    {
        get
        {
            return seen;
        }
        set
        {
            seen = value;
        }
    }

}
