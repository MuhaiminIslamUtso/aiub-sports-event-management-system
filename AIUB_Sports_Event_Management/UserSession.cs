namespace AIUBSportsEventManager
{
    public static class UserSession
    {
        public static string Username { get; set; }
        public static string FullName { get; set; }
        public static string Role { get; set; }

        public static void Clear()
        {
            Username = null;
            FullName = null;
            Role = null;
        }
    }
}
