using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keypad
{
    public partial class Form1 : Form
    {
        string serialDataIn;

        string[] validInputs = { "A01", "A02", "A03", "A04", "A05",
                                 "A06", "A07" , "A08", "A09", "A10"};        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM4";
            serialPort1.BaudRate = 9600;

            serialPort2.PortName = "COM5";
            serialPort2.BaudRate = 9600;

            serialPort1.Open();
            textBox1.Enabled = false;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            serialDataIn = serialPort1.ReadExisting(); 
            this.Invoke(new EventHandler(ShowData));
        }

        private async void ShowData(object sender, EventArgs e)
        {
            textBox1.Text += serialDataIn;

            if (serialDataIn == "B" || textBox1.TextLength > 3)
            {
                label1.Text = "Invalid";
                textBox1.Clear();
                textBox1.Focus();
            }

            if (Array.Exists<string>(validInputs, (Predicate<string>)delegate (string s)
            {
                return textBox1.Text.IndexOf(s, StringComparison.OrdinalIgnoreCase) > -1;
            }))
            {
                label1.Text = $"{textBox1.Text} Selected!";
                textBox1.Clear();
                textBox1.Focus();

                this.Invoke(new EventHandler(button1_Click));

            }
            else
            {
                await Task.Delay(700);
                label1.Text = "Make Your Selection";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}