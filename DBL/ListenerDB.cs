using System.Text.RegularExpressions;
using Models;


namespace DBL
{
    public class ListenerDB : BaseDB<Listener>
    {
        protected async override Task<List<Listener>> CreateListModelAsync(List<object[]> rows)
        {
            List<Listener> ListenersList = new List<Listener>();
            foreach (object[] item in rows)
            {
                Listener c;
                c = (Listener)await CreateModelAsync(item);
                ListenersList.Add(c);
            }

            return ListenersList;
        }

        protected async override Task<Listener> CreateModelAsync(object[] row)
        {
            Listener L = new Listener();
            L.UserID = int.Parse(row[0].ToString());
            L.FullName = row[1].ToString();
            L.UserName = row[2].ToString();
            L.EmailAddress = row[3].ToString();
            L.PasswordHash = StringHasher.GetSHA256Hash(row[4].ToString());
            return L;
        }

        protected override string GetPrimaryKeyName()
        {
            return "userID";
        }

        protected override string GetTableName()
        {
            return "users";
        }

        public async Task<List<Listener>> selectAllAsync()
        {
            return await base.SelectAllAsync();
        }

        public async Task<Listener> GetListenerByID(long id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["userID"] = id;
            List<Listener> ListenerDTOs = await base.SelectAllAsync(parameters);
            if (ListenerDTOs != null && ListenerDTOs.Count == 1)
                return ListenerDTOs[0];
            else
                return null;
        }

        public async Task<Listener> InsertGetObjAsync(Listener listener, string password)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "fullName", listener.FullName },
                { "userName", listener.UserName },
                { "emailAddress", listener.EmailAddress },
                { "passwordHash", listener.PasswordHash }
            };
            return (Listener)await base.InsertGetObjAsync(fillValues);
        }

        public async Task<List<Listener>> GetAllAsync()
        {
            return ((List<Listener>)await SelectAllAsync());
        }

        public async Task<int> DeleteAsync(Listener listener)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object> { { "userID", listener.UserID } };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<int> UpdateAsync(Listener listener)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            fillValues.Add("fullName", listener.FullName);
            fillValues.Add("userName", listener.UserName);
            fillValues.Add("emailAddress", listener.EmailAddress);
            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<Listener> LoginAsync(string name, string password)
        {
            string sql = @"SELECT * FROM tuneapp.users where userName = @name and passwordHash=@pass;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("name", name);
            p.Add("pass", password);
            List<Listener> list = await SelectAllAsync(sql, p);

            //If the query found exactly one user matching the username and password, return that user; otherwise, return null

            if (list.Count == 1)
                return list[0];
            else
                return null;
        }

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            try
            {
                string sql = "SELECT * FROM tuneapp.users WHERE userName = @username";
                var parameters = new Dictionary<string, object> { { "username", username } };

                // Add await before your database call
                var users = await SelectAllAsync(sql, parameters);

                // Return true if count > 0
                return users.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking username: {ex.Message}");
                // Always return a value even in the catch block
                return false;
            }
        }

        public async Task<bool> DoesEmailExist(string email)
        {
            try
            {
                string sql = "select * from tuneapp.users where emailAddress=@email";
                var parameters = new Dictionary<string, object> { { "@email", email } };

                var users = await SelectAllAsync(sql, parameters);

                return users.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking whether email exists: {ex.Message}");
                // Always return a value even in the catch block
                return false;
            }
        }

        public async Task<bool> DoesUsernameExist(string userName)
        {
            try
            {
                string sql = "select * from tuneapp.users where userName=@userName";
                var parameters = new Dictionary<string, object> { { "userName", userName } };

                var users = await SelectAllAsync(sql, parameters);

                return users.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking whether username exists: {ex.Message}");
                // Always return a value even in the catch block
                return false;
            }

        }
    }
}