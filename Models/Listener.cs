namespace Models
{
    public class Listener
    {
        private int userID;
        private string fullName;
        private string userName;      
        private string emailAddress;
        private string passwordHash;

        public Listener(int userID, string fullName, string userName, string emailAddress, string passwordHash)
        {
            this.UserID = userID;
            this.FullName = fullName;
            this.UserName = userName;
            this.EmailAddress = emailAddress;
            this.PasswordHash = passwordHash;
        }
        public Listener() {}

        public int UserID { get => userID; set => userID = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string UserName { get => userName; set => userName = value; }
        public string EmailAddress { get => emailAddress; set => emailAddress = value; }
        public string PasswordHash { get => passwordHash; set => passwordHash = value; }
    }
}
