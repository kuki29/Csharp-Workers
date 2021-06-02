using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PolimorfKurWork
{
	class OfficeWorker : Employee
	{
		protected uint _Salary;


		public OfficeWorker(string surname, string name, string oatronymic, Position position, uint salary)
			: base(surname, name, oatronymic, position, SalaryType.Fixed)
		{
			Salary = salary;
		}

		public OfficeWorker(string surname, string name, string patronymic, string position, uint salary)
			: this(surname, name, patronymic, Employee.GetPositionFromString(position), salary)
		{
		}


		public override uint PayMonth() => Salary;

		public override bool Equals([AllowNull] Employee other)
		{
			return base.Equals(other);
		}

		public bool Equals([AllowNull] OfficeWorker other)
		{
			return base.Equals(other) && Salary == other.Salary;
		}

		public uint Salary
		{
			get { return _Salary; }
			set { _Salary = value; }

		}
	}
}
