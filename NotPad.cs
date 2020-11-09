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
using System.Security.Cryptography;

namespace NotPad
{
    public partial class NotPad : Form
    {
        public NotPad()
        {
            InitializeComponent();
        }

        static string key = "0123456789ABCDEF";

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name;
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //Check if it's all ok  
            {
                name = saveFileDialog1.FileName;  

                richTextBox1.SaveFile(name, RichTextBoxStreamType.RichText); //saves file

                string enc = Encrypt(richTextBox1.Rtf);

                File.WriteAllText(name, enc);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) //Checks if it's all ok
            {
                //richTextBox1.LoadFile(openFileDialog1.FileName);

                string txt = File.ReadAllText(openFileDialog1.FileName);

                string dec = Decrypt(txt);

                richTextBox1.Rtf = dec;
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

        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}
