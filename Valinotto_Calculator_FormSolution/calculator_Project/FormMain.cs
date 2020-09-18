using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator_Project
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public struct ButtonStruct
        {
            public char Content;
            public bool IsBold;
            public ButtonStruct(char content, bool isBold)
            {
                this.Content = content;
                this.IsBold = isBold;
            }
            public override string ToString()
            {
                return Content.ToString();
            }
        }

        public ButtonStruct[,] buttons = {
            {new ButtonStruct(' ',false),new ButtonStruct(' ',false),new ButtonStruct(' ',false),new ButtonStruct(' ',false)},
            {new ButtonStruct(' ',false),new ButtonStruct(' ',false),new ButtonStruct(' ',false),new ButtonStruct('/',false)},
            {new ButtonStruct('7',true),new ButtonStruct('8',true),new ButtonStruct('9',true),new ButtonStruct('x',false)},
            {new ButtonStruct('4',true),new ButtonStruct('5',true),new ButtonStruct('6',true),new ButtonStruct('-',false)},
            {new ButtonStruct('1',true),new ButtonStruct('2',true),new ButtonStruct('3',true),new ButtonStruct('+',false)},
            {new ButtonStruct('±',false),new ButtonStruct('0',true),new ButtonStruct(',',false),new ButtonStruct('=',false)}
        };
        private RichTextBox resultBox;

    public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            makeResultBox(resultBox);
            makeButtons(buttons);
        }

        private void makeResultBox(RichTextBox resultBox)
        {
            resultBox = new RichTextBox();
            resultBox.ReadOnly = true;
            resultBox.SelectionAlignment = HorizontalAlignment.Right;
            resultBox.Font = new Font("Segue UI", 22);
            resultBox.Width = (this.Width)-20;
            resultBox.Text = "12345";
            resultBox.Height = 50;
            resultBox.Top = 20;
            this.Controls.Add(resultBox);
        }

        private void makeButtons(ButtonStruct[,] buttons)
        {
            int buttonWidth = 82, buttonHeight = 60; ///inp. grandezza buttons
            int posX = 0, posY = 101; ///posizione di partenza
            /// 0=righe, 1=colonne
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    Button newButton = new Button();
                    newButton.Font = new Font("Segue UI",14);
                    ///newButton.Text = buttons[i, j].Content.ToString();
                    ButtonStruct bs = buttons[i, j];
                    newButton.Text = bs.ToString();
                    if(bs.IsBold)
                    {
                        newButton.Font=new Font(newButton.Font, FontStyle.Bold);
                    }
                    newButton.Width = buttonWidth;
                    newButton.Height = buttonHeight;
                    newButton.Left= posX;
                    newButton.Top = posY;
                    this.Controls.Add(newButton); ///aggiungo il button alla form (this punta a form)
                    posX += buttonWidth;
                }
                posY += buttonHeight;
                posX = 0;
            }
        }
    }
}
