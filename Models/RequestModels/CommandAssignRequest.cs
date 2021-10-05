namespace WebBase.Models.RequestModels
{
    public class CommandAssignRequest
    {
        public string[] CommandIds { get; set; }

        public bool AddToAllFunctions { get; set; }
    }
}