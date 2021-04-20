using MaterialSkin.Controls;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PdfMerge
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "PDF files (*.pdf)|*.pdf";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileDialog oDialog = (FileDialog)sender;

            if (openFileDialog1.FileNames.Length > 0)
            {
                button2.Enabled = true;

                foreach (string pdfFile in openFileDialog1.FileNames)
                {
                    listBox1.Items.Add(pdfFile);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PdfDocument outputDocument = new PdfDocument();
            string oFileName = (String.IsNullOrEmpty(textBox1.Text)) ? "merged.pdf" : textBox1.Text + ".pdf";

            foreach (string pdfFile in listBox1.Items)
            {
                PdfDocument inputPDFDocument = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);
                outputDocument.Version = inputPDFDocument.Version;

                foreach (PdfPage page in inputPDFDocument.Pages)
                {
                    outputDocument.AddPage(page);
                }
            }
            outputDocument.Save(oFileName);

            string exeFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fullPath = Path.Combine(exeFile, oFileName);
            System.Diagnostics.Process.Start(fullPath);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            button2.Enabled = false;
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Delete)
            {
                ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(listBox1);
                selectedItems = listBox1.SelectedItems;

                if (listBox1.SelectedIndex != -1)
                {
                    for (int i = selectedItems.Count - 1; i >= 0; i--)
                        listBox1.Items.Remove(selectedItems[i]);
                }
            }
        }
    }
}