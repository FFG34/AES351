using System;
using System.Drawing;
using System.Windows.Forms;

namespace AES351
{
    public partial class Form1 : Form
    {
        private CommandParser parser;

        public Form1()
        {
            InitializeComponent();
            parser = new CommandParser(codeTextBox, displayArea);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            displayArea.Paint += new PaintEventHandler(displayArea_Paint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialization that occurs when the form loads can be placed here.
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            try
            {
                string commandLine = commandTextBox.Text; 
                parser.ProcessCommand(commandLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing the program: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SyntaxButton_Click(object sender, EventArgs e)
        {
            try
            {
                parser.ValidateSyntax();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Syntax error: " + ex.Message, "Syntax Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void commandTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var commandText = commandTextBox.Text;
                try
                {
                    parser.ProcessCommand(commandText);
                    commandTextBox.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error executing command: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void displayArea_Paint(object sender, PaintEventArgs g)
        {
            parser.SetupGraphics(g.Graphics);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            parser.Cleanup();
        }
    }
}
