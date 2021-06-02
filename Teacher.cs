using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PolimorfKurWork
{
	class Teacher : Employee
	{
		private uint _HoursWorked;
		private uint _Rate;


		public Teacher(string surname, string name, string partonymic, Position position, uint rate, uint hoursWorked)
			: base(surname, name, partonymic, position, SalaryType.Fixed)
		{
			HoursWorked = hoursWorked;
			Rate = rate;

			if (Position == Position.Teacher)
				_Rate = 50;
			else if (Position == Position.Docent)
				_Rate = 85;
			else if (Position == Position.Professor)
				_Rate = 100;
		}

		public Teacher(string surname, string name, string patronymic, string position, uint rate, uint hoursWorked)
			: this(surname, name, patronymic, Employee.GetPositionFromString(position), rate, hoursWorked)
		{
		}


		public override uint PayMonth() => HoursWorked * _Rate;

		public override bool Equals([AllowNull] Employee other)
		{
			return base.Equals(other);
		}

		public bool Equals([AllowNull] Teacher other)
		{
			return base.Equals(other) && Rate == other.Rate && HoursWorked == other.HoursWorked;
		}


		public uint HoursWorked
		{
			get { return _HoursWorked; }

			set
			{
				if (Position == Position.Teacher)
				{
					if (value >= 10 && value <= 100)
					{
						_HoursWorked = value;
					}
					else
					{
						_HoursWorked = value < 10 ? (uint) 10 : (uint) 100;
					}
				}
				else
				{
					_HoursWorked = value;
				}

			}
		}

		public uint Rate
		{
			get { return _Rate; }
			set { _Rate = value; }
		}
	}
}
