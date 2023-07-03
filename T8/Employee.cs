using System;
namespace T8
{
	public class Employee
	{
        private string no;
        private string name;
		private string email;
        private string password;
        private bool isManager;
        private Employee loggedInUser;

        public Employee()
        {
        }


        public string No
        {
            get { return no; }
            set { no = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool IsManager
        {
            get { return isManager; }
            set { isManager = value; }
        }


        public Employee(string no, string name, string email, string password, bool isManager)
        {
            this.no = no;
            this.name = name;
            this.email = email;
            this.password = password;
            this.isManager = isManager;
        }

        public Employee LoggedInUser
        {
            get { return loggedInUser; }
            set { loggedInUser = value; }
        }
        public void SetNo(string no)
        {
            this.no = no;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public void SetPassword(string password)
        {
            this.password = password;
        }
        public void SetisManager(bool isManager)
        {
            this.isManager = isManager;
        }
        internal string GetNo()
        {
            return no;
        }
        internal string GetName()
        {
            return name;
        }
        internal string GetEmail()
        {
            return email;
        }
        internal string GetPassword()
        {
            return password;
        }
        internal bool GetisManager()
        {
            return isManager;
        }
        public void Input()
        {
            Console.Write("Enter No: ");
            this.no = Console.ReadLine();
            Console.Write("Enter Name: ");
            this.name = Console.ReadLine();
            Console.Write("Enter Email: ");
            this.email = Console.ReadLine();
            Console.Write("Enter Password: ");
            this.password = Console.ReadLine();
            Console.Write("Is Manager (true/false): ");
            this.isManager = Convert.ToBoolean(Console.ReadLine());
        }


        public override string ToString()
        {
            return no + ", " + name + ", " + email + ", " + password + ", " + isManager;
        }

    }
}

