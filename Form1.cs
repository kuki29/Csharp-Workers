using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolimorfKurWork
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		LinkedList<Teacher> listTeacher = new LinkedList<Teacher>();
		LinkedListNode<Teacher> nodeT;
		LinkedList<OfficeWorker> listOfficeWorker = new LinkedList<OfficeWorker>();
		LinkedListNode<OfficeWorker> nodeW;

		private void Form1_Load(object sender, EventArgs e)
		{
			radioButton1.Enabled = true;
			CreateLists();
		}

		public void CreateLists()
		{
			List<Employee> workers = ReadWorkersFromFile("D:\\Projects\\PolimorfKurWork\\PolimorfKurWorkGUI\\input.txt");

			foreach (Employee employee in workers)
			{
				if (employee.GetType() == typeof(OfficeWorker))
				{
					listOfficeWorker.AddFirst((OfficeWorker) employee);
				}
				else
				{
					listTeacher.AddFirst((Teacher) employee);
				}
			}

			nodeT = listTeacher.First;
			nodeW = listOfficeWorker.First;
			SetNode();
		}

		private void SetNode()
		{
			MonthPayTextBox.Text = "";

			if (radioButton1.Checked)
			{
				label6.Visible = true;
				label5.Visible = true;
				RateTextBox.Visible = true;
				WorkedHoursTextBox.Visible = true;

				label7.Visible = false;
				SalaryTextBox.Visible = false;

				SurnameTextBox.Text = nodeT.Value.Surname;
				NameTextBox.Text = nodeT.Value.Name;
				PatronymicTextBox.Text = nodeT.Value.Patronymic;
				PositionTextBox.Text = nodeT.Value.Position.ToString();
				RateTextBox.Text = nodeT.Value.Rate.ToString();
				WorkedHoursTextBox.Text = nodeT.Value.HoursWorked.ToString();
			}
			else
			{
				label6.Visible = false;
				label5.Visible = false;
				RateTextBox.Visible = false;
				WorkedHoursTextBox.Visible = false;

				label7.Visible = true;
				SalaryTextBox.Visible = true;

				SurnameTextBox.Text = nodeW.Value.Surname;
				NameTextBox.Text = nodeW.Value.Name;
				PatronymicTextBox.Text = nodeW.Value.Patronymic;
				PositionTextBox.Text = nodeW.Value.Position.ToString();
				SalaryTextBox.Text = nodeW.Value.Salary.ToString();
			}
		}

		private void GetNode()
		{
			if (radioButton1.Checked)
			{
				nodeT.Value.Surname = SurnameTextBox.Text;
				nodeT.Value.Name = NameTextBox.Text;
				nodeT.Value.Patronymic = PatronymicTextBox.Text;
				nodeT.Value.Position = Employee.GetPositionFromString(PositionTextBox.Text);
				nodeT.Value.Rate = UInt32.Parse(RateTextBox.Text);
				nodeT.Value.HoursWorked = UInt32.Parse(WorkedHoursTextBox.Text);
			}
			else
			{
				nodeW.Value.Surname = SurnameTextBox.Text;
				nodeW.Value.Name = NameTextBox.Text;
				nodeW.Value.Patronymic = PatronymicTextBox.Text;
				nodeW.Value.Position = Employee.GetPositionFromString(PositionTextBox.Text);
				nodeW.Value.Salary = UInt32.Parse(SalaryTextBox.Text);
			}
		}

		private void ClearForm()
		{
			SurnameTextBox.Text = "";
			NameTextBox.Text = "";
			PatronymicTextBox.Text = "";
			PositionTextBox.Text = "";
			RateTextBox.Text = "";
			WorkedHoursTextBox.Text = "";
			SalaryTextBox.Text = "";
		}

		static List<Employee> ReadWorkersFromFile(string filename)
		{
			List<Employee> workers = new List<Employee>();

			using StreamReader sr = File.OpenText(filename);
			{
				string s;
				string[] args;

				while ((s = sr.ReadLine()) != null)
				{
					args = s.Split("*");

					if (args.Length < 6 || args.Length > 8)
					{
						Console.WriteLine("Error: Corrupted file!!!");
						sr.Close();
						Environment.Exit(1);
						return null;
					}

					if (Int32.Parse(args[0]) == 0 && args.Length > 6)
					{
						workers.Add(new Teacher(args[1].Trim(), args[2].Trim(), args[3].Trim(), args[4].Trim(), UInt32.Parse(args[5]), UInt32.Parse(args[6])));
					}
					else if (Int32.Parse(args[0]) == 1 && args.Length < 8)
					{
						workers.Add(new OfficeWorker(args[1].Trim(), args[2].Trim(), args[3].Trim(), args[4].Trim(), UInt32.Parse(args[5])));
					}
					else if (workers.Count > 0)
					{
						sr.Close();
						return workers;
					}
					else
					{
						Console.WriteLine("Error: Corrupted file!!!");
						sr.Close();
						Environment.Exit(1);
						return null;
					}
				}
			}

			sr.Close();

			return workers;
		}

		private void UpdateFile()
		{
			string line;

			using (StreamWriter sw = new StreamWriter("input.txt"))
			{
				foreach (var node in listTeacher)
				{
					line = "0" + "*" + node.Surname + "*" + node.Name + "*" + node.Patronymic + "*" + 
						node.Rate + "*" + node.HoursWorked + "*";
					sw.WriteLine(line);
				}

				foreach(var node in listOfficeWorker)
				{
					line = "1" + "*" + node.Surname + "*" + node.Name + "*" + node.Patronymic + "*" +
						node.Salary + "*";
					sw.WriteLine(line);
				}

				sw.Close();
				MessageBox.Show("Файл оновлено");
			}
		}

		private void UpdateFile(string line)
		{
			using (StreamWriter sw = new StreamWriter("input.txt", append: true))
			{
				sw.WriteLine(line);
				MessageBox.Show("Запис додано в кінець файлу");
			}
		}

		private void FirstButton_Click(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
				nodeT = listTeacher.First;
			else
				nodeW = listOfficeWorker.First;

			SetNode();
		}

		private void PrevButton_Click(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
			{
				if (nodeT != listTeacher.First)
					nodeT = nodeT.Previous;
			}
			else
			{

				if (nodeW != listOfficeWorker.First)
					nodeW = nodeW.Previous;
			}

			SetNode();
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
			{
				if (nodeT != listTeacher.Last)
					nodeT = nodeT.Next;
			}
			else
			{

				if (nodeW != listOfficeWorker.Last)
					nodeW = nodeW.Next;
			}

			SetNode();
		}

		private void LastButton_Click(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
				nodeT = listTeacher.Last;
			else
				nodeW = listOfficeWorker.Last;

			SetNode();
		}

		private void InsertButton_Click(object sender, EventArgs e)
		{
			string lineToAdd = radioButton1.Checked ? "0" : "1" + "*" + SurnameTextBox.Text + "*" +
				NameTextBox.Text + "*" + PatronymicTextBox.Text + "*";
			if (radioButton1.Checked)
			{
				listTeacher.AddLast(new Teacher(SurnameTextBox.Text, NameTextBox.Text,
					PatronymicTextBox.Text, Employee.GetPositionFromString(PositionTextBox.Text),
					UInt32.Parse(RateTextBox.Text), UInt32.Parse(WorkedHoursTextBox.Text)));

				lineToAdd += RateTextBox.Text + "*" + WorkedHoursTextBox.Text + "*";
			}
			else
			{
				listOfficeWorker.AddLast(new OfficeWorker(SurnameTextBox.Text, NameTextBox.Text,
					PatronymicTextBox.Text, Employee.GetPositionFromString(PositionTextBox.Text),
					UInt32.Parse(SalaryTextBox.Text)));

				lineToAdd += SalaryTextBox.Text + "*";
			}

			UpdateFile(lineToAdd);
			ClearForm();
		}

		private void UpdateButton_Click(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
			{
				nodeT.Value.Surname = SurnameTextBox.Text;
				nodeT.Value.Name = NameTextBox.Text;
				nodeT.Value.Patronymic = PatronymicTextBox.Text;
				nodeT.Value.Rate = UInt32.Parse(RateTextBox.Text);
				nodeT.Value.HoursWorked = UInt32.Parse(WorkedHoursTextBox.Text);
			}
			else
			{
				nodeW.Value.Surname = SurnameTextBox.Text;
				nodeW.Value.Name = NameTextBox.Text;
				nodeW.Value.Patronymic = PatronymicTextBox.Text;
				nodeW.Value.Salary = UInt32.Parse(SalaryTextBox.Text);
			}

			UpdateFile();
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (SurnameTextBox.Text.Length != 0)
			{
				if (radioButton1.Checked)
				{
					listTeacher.Remove(nodeT);
				}
				else
				{
					listOfficeWorker.Remove(nodeW);
				}
			}
			else
			{
				MessageBox.Show("Немає запису для виділення");
			}
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			SetNode();
		}

		private void MonthPayButton_Click(object sender, EventArgs e)
		{
			uint Salary;

			if (radioButton1.Checked)
			{
				nodeT.Value.Rate = UInt32.Parse(RateTextBox.Text);
				nodeT.Value.HoursWorked = UInt32.Parse(WorkedHoursTextBox.Text);
				Salary = nodeT.Value.PayMonth();
			}
			else
			{
				nodeW.Value.Salary = UInt32.Parse(SalaryTextBox.Text);
				Salary = nodeW.Value.PayMonth();
			}

			MonthPayTextBox.Text = Salary.ToString() + "UAH";
		}

		private void AskDocentsButton_Click(object sender, EventArgs e)
		{
			var docents = from docent in listTeacher
						  where docent.Position == Position.Docent
						  select docent;

			richTextBox1.Text = "";
			
			foreach (var docent in docents)
			{
				richTextBox1.Text += docent.Surname + " " + docent.Name + " " + "зарплата за 1 місяць = " +
					docent.PayMonth().ToString() + "\n";
			}
		}
	}
}
