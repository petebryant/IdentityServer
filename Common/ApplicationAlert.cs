namespace IdentityServer.Common
{
    public class ApplicationAlert
    {
        public ApplicationAlert(AlertType type, string message)
        {
            AlertType = type;
            Message = message;
        }
        public string Message {get;set;}
        public AlertType AlertType {get;set;}
    }
     
}