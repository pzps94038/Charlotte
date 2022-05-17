using Charlotte.Model;

namespace Charlotte.VModel.ManagerLogin
{
    public class ManagerLoginVModel
    {
        public Token Token { get; set; }
        public int ManagerUserId { get; set; }
        public ManagerLoginVModel() 
        {
            this.Token = new Token();
        }
    }
}
