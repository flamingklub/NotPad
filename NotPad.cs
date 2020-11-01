using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotPad
{
    public partial class NotPad : Form
    {
        public NotPad()
        {
            InitializeComponent();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //Check if it's all ok  
            {
                name = saveFileDialog1.FileName;  

                richTextBox1.SaveFile(name, RichTextBoxStreamType.RichText);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //Checks if it's all ok
            {
                richTextBox1.LoadFile(openFileDialog1.FileName);
            }
            else //If something goes wrong...  
            {
                MessageBox.Show("The file you've chosen is f*'d");
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //Check if it's all ok  
            {
                string name = saveFileDialog1.FileName + ".txt"; //Just to make sure the extension is .txt  
                File.WriteAllText(name, richTextBox1.Text); //Writes the text to the file and saves it               
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font,
                richTextBox1.SelectionFont.Style ^ FontStyle.Bold);

        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font,
                richTextBox1.SelectionFont.Style ^ FontStyle.Italic);
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font,
                richTextBox1.SelectionFont.Style ^ FontStyle.Strikeout);
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font,
                richTextBox1.SelectionFont.Style ^ FontStyle.Underline);
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            var selectedText = richTextBox1.SelectionFont;

            float newsize = selectedText.Size + 1;
            Font currentFont = richTextBox1.SelectionFont;
            FontStyle newFontStyle = (FontStyle)(currentFont.Style);
            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, newsize, newFontStyle);
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            var selectedText = richTextBox1.SelectionFont;

            float newsize = selectedText.Size - 1;
            Font currentFont = richTextBox1.SelectionFont;
            FontStyle newFontStyle = (FontStyle)(currentFont.Style);
            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, newsize, newFontStyle);
        }
    }
}
