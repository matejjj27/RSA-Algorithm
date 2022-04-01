using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace Kursov_Proekt_Kriptografija
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Help Functions

        private static bool IsNumber(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != ' ')
                    return false;
            }
            return true;
        }

        private static int Check_Prime(string s)
        {
            if (IsNumber(s) == false)
                return 0;
            else 
            {
            BigInteger number = Convert.ToInt64(s);
            if (number == 1 || number == 0 || number < 0)
                return 0;

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                    return 0;
            }
            return 1;
            }
        }

        private static BigInteger NZD(BigInteger a, BigInteger b)
        {
            BigInteger rem;

            while (b > 0)
            {
                rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }

        #endregion

        #region ButtonClick Functions

        private void PrimeButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox4.Text))
                MessageBox.Show("Please Enter a Number");

            else if (textBox5.Text == textBox4.Text)
            {
                MessageBox.Show("Please Enter Different Numbers");
                textBox4.BackColor = Color.Pink;
            }

            else
            {
                string s = textBox4.Text;
                if (Check_Prime(s) == 0)
                    textBox4.BackColor = Color.Pink;
                else
                    textBox4.BackColor = Color.LightGreen;
            }
        }

        private void PrimeButton2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox5.Text))
                MessageBox.Show("Please Enter a Number");

            else if (textBox5.Text == textBox4.Text)
            {
                MessageBox.Show("Please Enter Different Numbers");
                textBox5.BackColor = Color.Pink;
            }

            else
            {
                string s = textBox5.Text;
                if (Check_Prime(s) == 0)
                    textBox5.BackColor = Color.Pink;
                else
                    textBox5.BackColor = Color.LightGreen;
            }
        }

        private void CalculatePhiButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrEmpty(textBox4.Text) ||
                textBox5.BackColor == Color.Pink || textBox5.BackColor == Color.Pink)
                MessageBox.Show("Please Enter Valid Prime Numbers!");

            else
            {
                BigInteger a, b, c;
                a = Convert.ToInt64(textBox4.Text);
                b = Convert.ToInt64(textBox5.Text);
                c = (a - 1) * (b - 1);

                textBox6.Text = Convert.ToString(c);
                textBox6.BackColor = Color.LightGreen;
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (String.IsNullOrEmpty(textBox6.Text))
                MessageBox.Show("Please Calculate Phi First!");
            else
            {
                BigInteger a;
                a = Convert.ToInt64(textBox6.Text);
                
                for (BigInteger i = 2; i < a; i++)
                {
                    if (NZD(i, a) == 1)
                        listBox1.Items.Add(i);
                }
            }
        }

        private void Generate2Button_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (String.IsNullOrEmpty(listBox1.Text) || String.IsNullOrEmpty(listBox1.SelectedItem.ToString()))
                MessageBox.Show("Please Choose a value for 'e'!");
            else
            {
                BigInteger a = Convert.ToInt64(listBox1.SelectedItem.ToString());
                BigInteger b = Convert.ToInt64(textBox6.Text);

                for (BigInteger i = 2; i < 9999; i++)
                {
                    if (((i*a) % b) == 1)
                        listBox2.Items.Add(i);
                }
            }
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(listBox2.Text) || String.IsNullOrEmpty(listBox2.SelectedItem.ToString()) ||
                String.IsNullOrEmpty(textBox6.Text) || String.IsNullOrEmpty(textBox1.Text))
                MessageBox.Show("Please Choose a value for 'd' and Enter Text Code for Encryption");
            else
            {
                int a = Convert.ToInt32(listBox1.SelectedItem.ToString());
                BigInteger p = Convert.ToInt64(textBox4.Text);
                BigInteger q = Convert.ToInt64(textBox5.Text);
                BigInteger str = Convert.ToInt64(textBox1.Text);

                BigInteger c = BigInteger.Pow(str, a);
                textBox2.Text = Convert.ToString(c % (p * q));
            }
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox1.Text))
                MessageBox.Show("Please Enter Text Code and Encrypt");
            else
            {
                BigInteger p = Convert.ToInt64(textBox4.Text);
                BigInteger q = Convert.ToInt64(textBox5.Text);
                BigInteger c = Convert.ToInt64(textBox2.Text);
                int d = Convert.ToInt32(listBox2.SelectedItem.ToString());

                BigInteger m = BigInteger.Pow(c, d);
                BigInteger k = m % (p * q);
                textBox3.Text = Convert.ToString(k);

                if (textBox3.Text == textBox1.Text)
                    textBox3.BackColor = Color.LightGreen;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox3.BackColor = Color.White;
            textBox4.Clear();
            textBox4.BackColor = Color.White;
            textBox5.Clear();
            textBox5.BackColor = Color.White;
            textBox6.Clear();
            textBox6.BackColor = Color.White;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }
        #endregion

        #region KeyPress Functions

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            PrimeButton1_Click(sender, e);
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PrimeButton2_Click(sender, e);
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CalculatePhiButton_Click(sender, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                EncryptButton_Click(sender, e);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DecryptButton_Click(sender, e);
        }

        #endregion
    }
}
