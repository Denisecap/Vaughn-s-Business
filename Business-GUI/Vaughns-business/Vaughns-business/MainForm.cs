﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vaughns_business.Classes;
using static Vaughns_business.LoginForm;

namespace Vaughns_business
{
    public partial class MainForm : Form
    {
        private List<Customer> customersList = new List<Customer>();
        private List<Staff> staffList = new List<Staff>();
        private List<Order> ordersList = new List<Order>();
        public MainForm(int userId)
        {
            InitializeComponent();

            // initializing form
            ReadInPeople();
            SetUserLabel(userId);
            DisplayCustomers();
        }
        private void ReadInPeople()
        {
            // reading customers
            string customerFilePath = "..\\..\\Text_files\\customers.txt";
            Utils.ReadFromFile(customerFilePath, customersList);

            // reading in staff
            string staffFilePath = "..\\..\\Text_files\\staff.txt";
            Utils.ReadFromFile(staffFilePath, staffList);

            // reading in staff
            string orderFilePath = "..\\..\\Text_files\\orders.txt";
            Utils.ReadFromFile(orderFilePath, ordersList);

            //// adding an order 
            //int orderId = 1;
            //int customerId = 1;
            //string productName = "1x ride";
            //double price = 50;
            //string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //string orderString = $"{orderId},{customerId},{productName},{price},{dateTime}";

            //using (StreamWriter sw = new StreamWriter("..\\..\\Text_files\\orders.txt", append: true))
            //{
            //    sw.WriteLine(orderString);
            //}

        }
        private void button_customer_Click(object sender, EventArgs e)
        {
            Form form = new CustomerForm(this, customersList, ordersList);
            OpenContainerForm(form);
        }
        public void UpdateCustomerList(List<Customer> newCustomerList)
        {
            customersList = null;
            customersList = newCustomerList;
        }
        private void SetUserLabel(int userId)
        {
            // initalising text for users
            string fullName = "";
            string userAccess = "";
            foreach (Staff staff in staffList)
            {
                if (staff.Id == userId)
                {
                    fullName = $"{staff.FirstName} {staff.LastName}";
                    userAccess = $"{staff.UserRole}";
                }
            }
            label_user_info.Text = $"{fullName}({userAccess}) Currently Signed";
        }
        public int GetUniqueId(int selection)
        {
            int personId;
            int idIteration;
            List<int> idList = new List<int>();
            if (selection == 0)
            {
                foreach (Customer customer in customersList)
                {
                    idList.Add(customer.Id); // iterates through learners and adds the id value to array
                }
                idIteration = customersList.Count + 1; // new id for learner based on count
            }
            else
            {
                foreach (Staff staff in staffList)
                {
                    idList.Add(staff.Id);
                }
                idIteration = staffList.Count + 1;
            }
            while (idList.Contains(idIteration))
            {
                idIteration++;
            }
            personId = idIteration; // id for txt file
            return personId;
        }

        private Form activeForm = null;
        private void OpenContainerForm(Form containerForm)
        {
            activeForm?.Close(); // closes current form
            activeForm = containerForm;
            containerForm.TopLevel = false;
            containerForm.FormBorderStyle = FormBorderStyle.None;
            containerForm.Dock = DockStyle.Fill;
            panel_container.Controls.Add(containerForm);
            panel_container.Tag = containerForm;
            containerForm.BringToFront();
            containerForm.Show();
        }
    }
}
