﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Student_Enquiry
{
    public partial class Form1 : Form
    {
        SortedList<string, string[]> programs;

        string[] ITPrograms = { "Programming", "Security", "Networking" };
        string[] EngineeringPrograms = { "Electrical", "Mechanical" };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            programs = new SortedList<string, string[]>
            {
                {"Information Technology", ITPrograms },
                {"Enginnering", EngineeringPrograms },
            };

            // This combobox has DropDownStyle=DropDownList, user can only pick one of the available choices
            cbxDepartment.Items.AddRange(programs.Keys.ToArray());
            cbxDepartment.SelectedIndex = 0;  // Select the first choice

            // This combobox has DropDownStyle=DropDown, so user can enter their own text
            cbxHowDidYouHear.Items.Add("Another student");
            cbxHowDidYouHear.Items.Add("Another school");
            cbxHowDidYouHear.Items.Add("Internet search");

        }

        private void cbxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstDegrees.ClearSelected();  // fires the selection changed event
            // Otherwise, the number of degrees selected deosn't reset to 0

            lstDegrees.Items.Clear();

            string department = cbxDepartment.Text;

            if (department != null)
            {
                // Fetch the Array of degrees from the SortedList, show in ListBox
                // Remember the keys in the SortedList are the names of programs
                // The value for each key, is an array of degrees for that program
                string[] degrees = programs[department];
                lstDegrees.Items.AddRange(degrees);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Generate a list of each error with the form 
            List<string> errors = new List<string>();

            if (cbxDepartment.SelectedIndex == -1)
            {
                errors.Add("Select a semester");
            }

            if (lstDegrees.SelectedIndex == -1)
            {
                errors.Add("Select at least one degree");
            }

            // Because user can type, whatever they type is considered to be index -1
            if (String.IsNullOrEmpty(cbxHowDidYouHear.Text))
            {
                errors.Add("Type or select how you heard about us");
            }

            if (errors.Count > 0)
            {
                MessageBox.Show(String.Join("\n", errors), "Error");
                return;
            }

            StringBuilder summaryBuilder = new StringBuilder();

            summaryBuilder.Append("Summary");
            summaryBuilder.Append("\n\nDepartment");
            summaryBuilder.Append(cbxDepartment.Text);

            summaryBuilder.Append("\n\nPrograms: \n");

            foreach (object degree in lstDegrees.SelectedItems)
            {
                summaryBuilder.Append(degree);
                summaryBuilder.Append("\n");
            }

            summaryBuilder.Append("\nYou heard about us from: ");
            summaryBuilder.Append(cbxHowDidYouHear.Text);

            string summary = summaryBuilder.ToString();

            MessageBox.Show(summary, "Thank you!");

            this.Close();
        }

        private void lstDegrees_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numberSelected = lstDegrees.SelectedItems.Count;
            lblDegreeCount.Text = $"{numberSelected} selected";
        }
    }
}
