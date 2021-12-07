using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanVien
{
    class Employee
    {
        private string empID; //ma nhan vien
        private string empName; //ten nhan vien
        private float salary; //luong
        private string deptName; //ten phong ban
        private string deptTel; //so dt phong ban

        public Employee()
        {

        }

        public Employee(string empID, string empName, float salary, string deptName, string deptTel)
        {
            this.empID = empID;
            this.empName = empName;
            this.salary = salary;
            this.deptName = deptName;
            this.deptTel = deptTel;
        }

        public string EmpID
        {
            get
            {
                return empID;
            }

            set
            {
                empID = value;
            }
        }

        public string EmpName
        {
            get
            {
                return empName;
            }

            set
            {
                empName = value;
            }
        }

        public float Salary
        {
            get
            {
                return salary;
            }

            set
            {
                salary = value;
            }
        }

        public string DeptName
        {
            get
            {
                return deptName;
            }

            set
            {
                deptName = value;
            }
        }

        public string DeptTel
        {
            get
            {
                return deptTel;
            }

            set
            {
                deptTel = value;
            }
        }
    }
}
