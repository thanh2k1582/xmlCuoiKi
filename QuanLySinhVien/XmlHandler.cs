using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QuanLyNhanVien
{
    class XmlHandler
    {
        XmlDocument doc;
        string filename = @"F:\XMLWriter\qlsinhvien\QuanLySinhVien\sinhvien.xml";
        List<Employee> empList;

        public void create(List<Employee> empList)
        {
            XmlNode decNode;
            XmlElement company, employee, dept, empName,
                        salary, deptName, deptTel;
            
            XmlAttribute empId;
            doc = new XmlDocument();

            decNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(decNode);

            company = doc.CreateElement("sinhvien");

            foreach (Employee emp in empList)
            {

                employee = doc.CreateElement("nhanvien");
                empId = doc.CreateAttribute("masv"); 
                empId.Value = emp.EmpID; 
                employee.Attributes.Append(empId);

                empName = doc.CreateElement("hoten"); 
                empName.InnerText = emp.EmpName; 

                salary = doc.CreateElement("masv");
                salary.InnerText = emp.Salary.ToString(); 
                

                dept = doc.CreateElement("thongtinlienlac"); 

                deptName = doc.CreateElement("email");
                deptName.InnerText = emp.DeptName;

                deptTel = doc.CreateElement("dienthoai");
                deptTel.InnerText = emp.DeptTel;

                dept.AppendChild(deptName);
                dept.AppendChild(deptTel);

                employee.AppendChild(empName);
                employee.AppendChild(salary);
                employee.AppendChild(dept);

                company.AppendChild(employee);
                                                
            }
            doc.AppendChild(company);

            doc.Save(filename); 
        }

        public bool add(Employee emp)
        {
            doc = new XmlDocument(); 
            doc.Load(filename); 
            empList = new List<Employee>();
            loadDataFromDoc(doc, filename, empList);
            if (isExistId(empList, emp.EmpID)) 
                return false; 

            XmlElement employee, dept, empName,
                        salary, deptName, deptTel;
            XmlAttribute empId;

            employee = doc.CreateElement("nhanvien");
            empId = doc.CreateAttribute("masv");
            empId.Value = emp.EmpID;
            employee.Attributes.Append(empId);

            empName = doc.CreateElement("hoten"); 
            empName.InnerText = emp.EmpName; 

            salary = doc.CreateElement("masv"); 
            salary.InnerText = emp.Salary.ToString();
            dept = doc.CreateElement("thongtinlienlac"); 

            deptName = doc.CreateElement("email"); 
            deptName.InnerText = emp.DeptName; 

            deptTel = doc.CreateElement("dienthoai");
            deptTel.InnerText = emp.DeptTel;

            dept.AppendChild(deptName);
            dept.AppendChild(deptTel);

            employee.AppendChild(empName);
            employee.AppendChild(salary);
            employee.AppendChild(dept);

            doc.DocumentElement.AppendChild(employee);
            doc.Save(filename);

            return true;
        }

        public bool edit(Employee emp)
        {
            doc = new XmlDocument(); 
            doc.Load(filename);
            empList = new List<Employee>(); 
            loadDataFromDoc(doc, filename, empList); 
            if (!isExistId(empList, emp.EmpID)) 
                return false; 


            XmlNodeList empNode = doc.GetElementsByTagName("nhanvien");
            
            foreach (XmlNode node in empNode)
            {
                string empId = node.Attributes["masv"].Value;
               
                if (empId == emp.EmpID)
                {
                    
                    node.ChildNodes[0].InnerText = emp.EmpName;
                    node.ChildNodes[1].InnerText = emp.Salary.ToString();
                    node.ChildNodes[2].ChildNodes[0].InnerText = emp.DeptName;
                    node.ChildNodes[2].ChildNodes[1].InnerText = emp.DeptTel;
                    break; 
                }
            }

            doc.Save(filename);

            return true; 
        }

        public bool delete(string empId)
        {
            doc = new XmlDocument();
            doc.Load(filename); 
            empList = new List<Employee>();
            loadDataFromDoc(doc, filename, empList);
            if (!isExistId(empList, empId)) 
                return false; 


            XmlNodeList empNode = doc.GetElementsByTagName("nhanvien");
          
            foreach (XmlNode node in empNode)
            {
                string id = node.Attributes["masv"].Value;
                
                if (id == empId) 
                {
                    doc.DocumentElement.RemoveChild(node);
                    break;
                }
            }

            doc.Save(filename);

            return true;
        }

        public void loadDataFromDoc(XmlDocument doc, string filename, List<Employee> empList)
        {
            doc = new XmlDocument(); 
            doc.Load(filename); 
            XmlNodeList empNode = doc.GetElementsByTagName("nhanvien");
           
            foreach (XmlNode node in empNode)
            {
               
                string empId, empName, deptName, deptTel;
                float salary;

                empId = node.Attributes["masv"].Value; 
                empName = node.ChildNodes[0].InnerText;
               
                salary = float.Parse(node.ChildNodes[1].InnerText);
                
                deptName = node.ChildNodes[2].ChildNodes[0].InnerText;
              
                deptTel = node.ChildNodes[2].ChildNodes[1].InnerText;
               

                Employee e = new Employee(empId, empName, salary, deptName, deptTel);
                
                empList.Add(e);
            }
        }

        public bool isExistId(List<Employee> empList, string id)
        {
            bool isExist = false;

            foreach (Employee emp in empList)
            {
                string empId = emp.EmpID;
                if (empId.ToLower() == id.ToLower()) 
                {
                    
                    isExist = true; 
                    break; 
                }
            }

            return isExist;
        }
    }
}
