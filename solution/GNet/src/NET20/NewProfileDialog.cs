﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GNet
{
    public partial class NewProfileDialog : Form
    {
        public NewProfileDialog()
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;
            Executables = new List<string>();
        }

        private void NewProfileDialog_Load(object sender, EventArgs e)
        {
            cbxLanguage.SelectedIndex = 0;

            if (cbxLanguage.Items.Count <= 1)
                cbxLanguage.Enabled = false;

            cbxDevice.SelectedIndex = 0;

            if (cbxDevice.Items.Count <= 1)
                cbxDevice.Enabled = false;

            cbxCopyExisting.SelectedIndex = 0;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Language { get; private set; }
        public string Device { get; private set; }
        public List<string> Executables { get; private set; }
        public bool LockForExecutables { get; private set; }
        public string CopyFrom { get; private set; }

        public bool OkClicked { get; private set; }

        private void lbxExecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxExecs.SelectedItem != null)
                btnRemove.Enabled = true;
            else
                btnRemove.Enabled = false;
        }

        private void btnExecBrowse_Click(object sender, EventArgs e)
        {
            var result = ofdBrowse.ShowDialog();

            if (result == DialogResult.OK)
                foreach (var exe in ofdBrowse.FileNames)
                    lbxExecs.Items.Add(exe);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Stack<int> indices = new Stack<int>(lbxExecs.SelectedIndices.Count);

            foreach (int index in lbxExecs.SelectedIndices)
                indices.Push(index);

            while (indices.Count > 0)
                lbxExecs.Items.RemoveAt(indices.Pop());
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            Name = tbxName.Text;
            Description = tbxDescription.Text;
            Language = cbxLanguage.SelectedItem.ToString();
            Device = cbxDevice.SelectedItem.ToString();
            LockForExecutables = chkLock.Checked;

            if (chkCopyExisting.Checked)
                CopyFrom = cbxCopyExisting.SelectedItem.ToString();

            foreach (var item in lbxExecs.Items)
                Executables.Add(item.ToString());

            OkClicked = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OkClicked = false;
            Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {

        }

        private void chkCopyExisting_CheckedChanged(object sender, EventArgs e)
        {
            cbxCopyExisting.Enabled = chkCopyExisting.Checked;
        }
    }
}