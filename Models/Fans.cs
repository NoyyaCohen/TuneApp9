using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Fans
    {
        private int fanID;
        private string userName;
        private string email;
        private int eventID;

        public Fans() { }

        public Fans(int fanID, string userName, string email, int eventID)
        {
            this.fanID = fanID;
            this.userName = userName;
            this.email = email;
            this.eventID = eventID;
        }

        public int FanID { get => fanID; set => fanID = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Email { get => email; set => email = value; }
        public int EventID { get => eventID; set => eventID = value; }
    }

}
