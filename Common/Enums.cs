namespace WebBase.Common
{
    public class Enums
    {
        public enum Status : int
        {
            Deleted = -2,
            UnActived = 0,
            Activied = 1
        }

        public enum FunctionCode
        {
            DASHBOARD,

            STATISTIC,
            STATISTIC_MONTHLY_NEWMEMBER,

            SYSTEM,
            SYSTEM_USER,
            SYSTEM_ROLE,
            SYSTEM_FUNCTION,
            SYSTEM_PERMISSION,
        }

        public enum CommandCode
        {
            CREATE,
            UPDATE,
            DELETE,
            VIEW,
            APPROVE
        }
    }
}