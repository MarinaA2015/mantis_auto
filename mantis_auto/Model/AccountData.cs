﻿namespace mantis_auto
{
    public class AccountData
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public AccountData(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }
        public AccountData()
        {
        }
    }
}