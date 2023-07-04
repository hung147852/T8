using System;
using System.IO;
//using System.ComponentModel.Design.Serialization;
using System.Reflection.PortableExecutable;
using T8;

namespace T8
{
	public class EmployeeManager : BaseManager
	{
		public static int MAX = 10;
		private int currLenght = 0;
		private Dictionary<string, Employee> employees;


		public EmployeeManager() : base()
		{
			//this.employees = new employees [MAX];
			employees = new Dictionary<string, Employee>() {
				{ "101", new Employee("101", "hoangnm", "hoangnm@gmail.com", "123456", true) },
				{ "102", new Employee("102", "namph", "namph@gmail.com", "123456", false) },
				{ "103", new Employee("103", "minhnv", "minhnv@gmail.com", "123456", false) },
				{ "104", new Employee("104", "trungnv", "trungnv@gmail.com", "123456", false) },
			};
		}

		public EmployeeManager(string name, Dictionary<string, Employee> employees)
		{
			this.employees = employees;
		}

		public override void AddNew()
		{
			Employee e = new Employee();
			e.Input();

			if (employees.Count >= MAX)
			{
				Console.WriteLine("Da vuot qua so luong toi da.");
			}
			else
			{
				employees.Add(e.GetNo(), e);
			}
			Console.WriteLine("Them moi thanh cong.");
			Console.WriteLine("Bang du lieu sau khi them.");

		}

		public override void Update()
		{
			Console.Write("Enter EmpNo or Name: ");
			String searchKey = Console.ReadLine();

			if (employees.ContainsKey(searchKey))
			{
				Employee emp = employees[searchKey];
				Console.Write("Enter Name want change: ");
				string name = Console.ReadLine();
				Console.Write("Enter Email want change: ");
				string email = Console.ReadLine();
				emp.SetName(name);
				emp.SetEmail(email);
				Console.WriteLine(emp);
			}
			else
			{
				Console.WriteLine("Khong tim thay du lieu phu hop.");
			}
		}

		public override void Delete()
		{
			Console.Write("Enter EmpNo or Name: ");
			string searchKey = Console.ReadLine();

			if (employees.ContainsKey(searchKey))
			{
				Employee emp = employees[searchKey];
				employees.Remove(searchKey);
				Console.WriteLine("Da xoa thanh cong.");
				Console.WriteLine(emp);
			}
			else
			{
				Console.WriteLine("Khong tim thay thong tin phu hop.");
			}
		}
		//private int FindIndex()
		//{
		//	Console.Write("Enter EmpNo or Name: ");
		//	String searchKey = Console.ReadLine();
		//	for (int i = 0; i < employees.Length; i++)
		//	{
		//		Employee emp = employees[i];
		//		if (emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey))
		//		{
		//			return i;
		//		}
		//	}
		//	return -1;
		//}

		public override void Find()
		{

			Console.Write("Enter EmpNo or Name: ");
			String searchKey = Console.ReadLine();

			// search
			List<Employee> result = new List<Employee>();
			int count = 0;

			foreach (KeyValuePair<string, Employee> kvp in employees)
			{
				Employee emp = kvp.Value;
				Console.WriteLine($"{kvp.Value}");
				if (emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey))
				{
					result.Add(emp);
				}
			}
			// print
			if (result.Count > 0)
			{
				PrintList(result);
			}
			else
			{
				Console.WriteLine("Not Found!");
			}

		}

		private void PrintList(List<Employee> list)
		{
			foreach (Employee item in list)
			{
				if (item != null)
				{
					Console.WriteLine(item);
				}
			}
		}
        public override void Importdata()
        {
            Console.WriteLine("Enter the file path to import:");
            string filePath = Console.ReadLine();

            try
            {
                StreamReader reader = new StreamReader(filePath);
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    if (data.Length == 5)
                    {
                        string empNo = data[0].Trim();
                        string empName = data[1].Trim();
                        string empEmail = data[2].Trim();
                        string empPassword = data[3].Trim();
                        bool empIsManager = bool.Parse(data[4].Trim());

                        Employee employee = new Employee(empNo, empName, empEmail, empPassword, empIsManager);
                        AddEmployee(employee);
                        Console.WriteLine("Import1!");
                    }
                }

                reader.Close();
                Console.WriteLine("Import successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Import failed with error: " + ex.Message);
            }
        }
        private void AddEmployee(Employee employee)
        {
            {
                employees[employee.GetNo()] = employee;
            }
        }

        public override void ExportList()
        {
            try
            {
                Console.Write("Enter Located: ");
                string Filepath = Console.ReadLine();
                StreamWriter writer = new StreamWriter(Filepath);
                foreach (KeyValuePair<string, Employee> kvp in employees)
                {
                    Employee emp = kvp.Value;
                    writer.WriteLine(emp.GetNo() + "," + emp.GetName() + "," + emp.GetEmail() + "," + emp.GetPassword() + "," + emp.GetisManager());

                }
                Console.WriteLine("Export thanh cong!");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Khong thanh cong voi loi: " + ex.Message);
            }
        }

        //log in module

        public override void UserLogin()

		{
			bool isLoggedIn = false;
			Employee loggedInUser = null;



			while (!isLoggedIn)
			{
				Console.WriteLine("***EMPLOYEE MANAGER***");
				Console.Write("Username (Gmail): ");
				string username = Console.ReadLine();
				Console.Write("Password: ");
				string password = Console.ReadLine();
				//int result;

				foreach (KeyValuePair<string, Employee> kvp in employees)
				{
					Employee emp = kvp.Value;
					if ((emp.GetEmail() == username) && (emp.GetPassword() == password))
					{
						//if (emp.IsManager())
						//{
						isLoggedIn = true;
						loggedInUser = emp;

						//Console.WriteLine("Login successful! Welcome Admin\n");
						//result = 1;
						break;
						//}
					}
				}

				if (!isLoggedIn)
				{
					Console.WriteLine("Invalid username or password. Please try again.\n");
				}
				if (isLoggedIn)
				{
					Console.WriteLine("Login successful! Welcome " + (loggedInUser.GetisManager() ? "Admin" : "User") + "\n");

					if (loggedInUser.IsManager)
					{
						ManageEmployees();
					}
					else
					{
						UserEmployee();
					}
				}
			}
		}
		public void ManageEmployees()
		{
			int selected = 0;

			do
			{
				Console.WriteLine("***EMPLOYEE MANAGER***");
				Console.WriteLine("1. Search Employee by Name or EmpNo");
				Console.WriteLine("2. Add New Employee");
				Console.WriteLine("3. Update Employee");
				Console.WriteLine("4. Delete Employee");
				Console.WriteLine("5. Export Employee to File");
				Console.WriteLine("6. Import Employee to File");
				Console.WriteLine("7. Exit");
				Console.Write("   Select (1-7): ");
				selected = Convert.ToInt16(Console.ReadLine());

				switch (selected)
				{
					case 1:
						Find();
						break;
					case 2:
						AddNew();
						break;
					case 3:
						Update();
						break;
					case 4:
						Delete();
						break;
					case 5:
						ExportList();
						break;
					case 6:
						//Console.WriteLine("ABC");
						Importdata();
						break;
					case 7:
						Console.WriteLine("-------- END ---------");
						break;
					default:
						Console.WriteLine("Invalid");
						break;
				}
			} while (selected != 7);
		}

		public void UserEmployee()
		{
			int selected = 0;
			do
			{
				Console.WriteLine("***EMPLOYEE MANAGER***");
				Console.WriteLine("1. Search Employee by Name or EmpNo");
				Console.WriteLine("2. Export Employee to File");
				Console.WriteLine("3. Exit");
				Console.Write("   Select (1-3): ");
				selected = Convert.ToInt32(Console.ReadLine());

				switch (selected)
				{
					case 1:
						Find();
						break;
					case 2:
						ExportList();
						break;
					case 3:
						Console.WriteLine("-------- END ---------");
						break;
					default:
						Console.WriteLine("Invalid selection.");
						break;
				}
			} while (selected != 3);
		}
		
	}
}







