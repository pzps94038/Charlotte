using Charlotte.Model;

namespace Charlotte.VModel.ManagerLogin
{
    public class ManagerLoginVModel
    {
        public Token token { get; set; }
        public int managerUserId { get; set; }
        public ManagerLoginVModel() 
        {
            this.token = new Token();
        }
    }
}
