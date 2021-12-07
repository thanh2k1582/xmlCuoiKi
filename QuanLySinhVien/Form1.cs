using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QuanLyNhanVien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            show();
        }

        public void show()
        {
            XmlHandler handler = new XmlHandler(); 
            XmlDocument doc = new XmlDocument(); 
            string filename = @"F:\XMLWriter\qlsinhvien\QuanLySinhVien\sinhvien.xml";
            
            List<Employee> empList = new List<Employee>();
            
            handler.loadDataFromDoc(doc, filename, empList);
            
            dgvInfor.Rows.Clear(); 
            int rowIndex = 0;
            foreach (Employee emp in empList)
            {
                dgvInfor.Rows.Add();
                dgvInfor.Rows[rowIndex].Cells[0].Value = emp.EmpID;
                dgvInfor.Rows[rowIndex].Cells[1].Value = emp.EmpName;
                dgvInfor.Rows[rowIndex].Cells[2].Value = emp.Salary;
                dgvInfor.Rows[rowIndex].Cells[3].Value = emp.DeptName;
                dgvInfor.Rows[rowIndex].Cells[4].Value = emp.DeptTel;

                rowIndex++;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            XmlHandler handler = new XmlHandler();
            
            string empId, empName, deptName, deptTel;
            float salary;
            try
            {
                
                empId = txtEmpId.Text;
                empName = txtEmpName.Text;
                salary = float.Parse(txtSalary.Text);
                deptName = txtDeptName.Text;
                deptTel = txtDeptTel.Text;
                
                if (!notEmptyFields()) 
                    MessageBox.Show("Any fields must not be empty", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    Employee emp = new Employee(empId, empName, salary, deptName, deptTel);
                    if (!handler.add(emp)) 
                        MessageBox.Show("Can't not add duplicate Emp. ID: " + empId, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else 
                    {
                        MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        show(); 
                        clearTextBox(); 
                        txtEmpId.Focus(); 
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool notEmptyFields()
        {
            if (txtEmpId.Text.Trim() == "" || txtEmpName.Text.Trim() == "")
                return false;
            if (txtSalary.Text.Trim() == "" || txtDeptName.Text.Trim() == "")
                return false;
            if (lblDeptTel.Text.Trim() == "")
                return false;
            return true;
        }

        public void clearTextBox()
        {
            txtEmpId.Text = "";
            txtEmpName.Text = "";
            txtSalary.Text = "";
            txtDeptName.Text = "";
            txtDeptTel.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            XmlHandler handler = new XmlHandler();
           
            string empId, empName, deptName, deptTel;
            float salary;
            try
            {
               
                empId = txtEmpId.Text;
                empName = txtEmpName.Text;
                salary = float.Parse(txtSalary.Text);
                deptName = txtDeptName.Text;
                deptTel = txtDeptTel.Text;
               
                if (!notEmptyFields()) 
                    MessageBox.Show("Any fields must not be empty", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    Employee emp = new Employee(empId, empName, salary, deptName, deptTel);
                    if (!handler.edit(emp)) 
                        MessageBox.Show("Emp. ID: " + empId + " is not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        show(); 
                        clearTextBox(); 
                        txtEmpId.Focus(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void onCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            int rowSeleted = dgvInfor.CurrentCell.RowIndex; 
            txtEmpId.Text = (string)dgvInfor.Rows[rowSeleted].Cells[0].Value;
            txtEmpName.Text = (string)dgvInfor.Rows[rowSeleted].Cells[1].Value;
            float salary = (float)dgvInfor.Rows[rowSeleted].Cells[2].Value;
            txtSalary.Text = salary.ToString();
            txtDeptName.Text = (string)dgvInfor.Rows[rowSeleted].Cells[3].Value;
            txtDeptTel.Text = (string)dgvInfor.Rows[rowSeleted].Cells[4].Value;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string empId = txtEmpId.Text;
            if (empId.Trim() == "")
                MessageBox.Show("EmpId must not be empty", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                XmlHandler handler = new XmlHandler();
                if (!handler.delete(empId)) 
                    MessageBox.Show("Emp. ID: " + empId + " is not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else 
                {
                    MessageBox.Show("Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    show(); 
                    clearTextBox(); 
                    txtEmpId.Focus(); 
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
