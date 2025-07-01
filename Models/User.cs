namespace RideSharingSystem.Models
{
    abstract class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        protected User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
