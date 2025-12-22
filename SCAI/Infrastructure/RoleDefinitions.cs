namespace SCAI.Infrastructure
{
    public static class RoleDefinitions
    {
        public const string Trooper = "Trooper";
        public const string Commander = "Commander";
        public const string Sith = "Sith";

        public const int TrooperLevel = 3;
        public const int CommanderLevel = 2;
        public const int SithLevel = 1;

        public static int GetRoleLevel(string role)
        {
            return role switch
            {
                Sith => SithLevel,
                Commander => CommanderLevel,
                Trooper => TrooperLevel,
                _ => TrooperLevel
            };
        }

        public static bool HasAccess(string userRole, int minimalRoleLevel)
        {
            return GetRoleLevel(userRole) <= minimalRoleLevel;
        }
    }
}