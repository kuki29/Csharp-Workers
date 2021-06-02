using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PolimorfKurWork
{
	public enum Position { Teacher, Docent, Professor, Metodist, Electric, Rector, Invalid }
	public enum SalaryType { Hourly, Fixed }

	abstract class Employee : IEquatable<Employee>, IComparable<Employee>
	{
		protected string _Surname;
		protected string _Name;
		protected string _Patronymic;
		protected Position _Position;
		private SalaryType _SalaryType;


		public Employee(string surname, string name, string patronymic, Position position, SalaryType salaryType)
		{
			_Surname = surname;
			_Name = name;
			_Patronymic = patronymic;
			_Position = position;
			_SalaryType = salaryType;
		}

		public Employee(string surname, string name, string patronymic, string position, SalaryType salaryType)
		{
			Surname = surname;
			Name = name;
			Patronymic = patronymic;
			Position = GetPositionFromString(position);
			_SalaryType = salaryType;
		}

		public Employee()
		{
		}


		public abstract uint PayMonth();
		public override string ToString()
		{
			return Surname + "\t" + Name + "\t" + "\t" + Patronymic + "\t-\t" + Position.ToString() + "\t-\t" +
				PayMonth() + "UAH/month";
		}

		public virtual bool Equals([AllowNull] Employee other)
		{
			return Surname == other.Surname && Name == other.Name && Patronymic == other.Patronymic &&
				Position == other.Position && _SalaryType == other._SalaryType;
		}

		public int CompareTo(Employee compareWorker)
		{
			if (compareWorker == null)
				return 1;
			else if (PayMonth() == compareWorker.PayMonth())
				return Surname.CompareTo(compareWorker.Surname);
			else
				return PayMonth().CompareTo(compareWorker.PayMonth());
		}

		public static Position GetPositionFromString(string s)
		{
			switch (s)
			{
				case "teacher":
					return Position.Teacher;

				case "docent":
					return Position.Docent;

				case "professor":
					return Position.Professor;

				case "metodist":
					return Position.Metodist;

				case "electric":
					return Position.Electric;

				case "rector":
					return Position.Rector;

				default:
					return Position.Invalid;
			}
		}


		public string Surname
		{
			get { return _Surname; }
			set { _Surname = value; }
		}

		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		public string Patronymic
		{
			get { return _Patronymic; }
			set { _Patronymic = value; }
		}

		public Position Position
		{
			get { return _Position; }
			set { _Position = value; }
		}
	}
}
