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
            public bool IsNumber;
            public bool IsDecimalSeparator;
            public bool IsPlusMinusSign;
            public ButtonStruct(char content, bool isBold, bool isNumber= false, bool isDecimalSeparator= false, bool isPlusMinusSign=false)
            {
                this.Content = content;
                this.IsBold = isBold;
                this.IsNumber = isNumber;
                this.IsDecimalSeparator = isDecimalSeparator;
                this.IsPlusMinusSign = isPlusMinusSign;
            }
            public override string ToString()
            {
                return Content.ToString();
            }
        }

        public ButtonStruct[,] buttons = {
            {new ButtonStruct(' ',false),new ButtonStruct(' ',false),new ButtonStruct('C',false),new ButtonStruct('<',false)},
            {new ButtonStruct(' ',false),new ButtonStruct(' ',false),new ButtonStruct(' ',false),new ButtonStruct('/',false)},
            {new ButtonStruct('7',true,true),new ButtonStruct('8',true,true),new ButtonStruct('9',true,true),new ButtonStruct('x',false)},
            {new ButtonStruct('4',true,true),new ButtonStruct('5',true,true),new ButtonStruct('6',true,true),new ButtonStruct('-',false)},
            {new ButtonStruct('1',true,true),new ButtonStruct('2',true,true),new ButtonStruct('3',true,true),new ButtonStruct('+',false)},
            {new ButtonStruct('±',false,false,false,true),new ButtonStruct('0',true,true),new ButtonStruct(',',false,false,true),new ButtonStruct('=',false)}
        };
        private RichTextBox resultBox;

    public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            makeResultBox();
            makeButtons(buttons);
        }

        private void makeResultBox()
        {
            resultBox = new RichTextBox();
            resultBox.ReadOnly = true;
            resultBox.SelectionAlignment = HorizontalAlignment.Right;
            resultBox.Font = new Font("Segue UI", 22);
            resultBox.Width = (this.Width)-20;
            resultBox.Text = "0";
            resultBox.TabStop = false;
            resultBox.Height = 50;
            resultBox.Top = 20;
            resultBox.TextChanged += ResultBox_TextChanged;
            this.Controls.Add(resultBox);
        }

        private void ResultBox_TextChanged(object sender, EventArgs e)
        {
            int newSize = 22 + (15 - resultBox.Text.Length);
            if(newSize>8 && newSize<23)
            {
                int delta = 15 - resultBox.Text.Length;
                resultBox.Font = new Font("Segue UI", newSize);
            }
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
                    newButton.Tag = bs;
                    newButton.Click += Button_Click;
                    this.Controls.Add(newButton); ///aggiungo il button alla form (this punta a form)
                    posX += buttonWidth;
                }
                posY += buttonHeight;
                posX = 0;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            ButtonStruct bs = (ButtonStruct)clickedButton.Tag;
            if (bs.IsNumber)
            {
                if (resultBox.Text == "0")
                {
                    resultBox.Text = " ";
                }
                resultBox.Text += clickedButton.Text;
            }
            else
            {
                if (bs.IsDecimalSeparator)
                {
                    if (!(resultBox.Text.Contains(bs.Content))) //sintassib generica che funzioni qualsiasi separatore
                    {
                        resultBox.Text += clickedButton.Text;
                    }
                }
                if (bs.IsPlusMinusSign)
                {
                    if (!(resultBox.Text.Contains("-")))
                    {
                        resultBox.Text = "-" + resultBox.Text;
                    }
                    else
                    {
                        resultBox.Text=resultBox.Text.Substring(1);
                    }
                }
                else
                {
                    switch(bs.Content)
                    {
                        case 'C':
                            resultBox.Text = "0";
                            break;
                        case '<':
                            if (resultBox.Text.Length >= 1)
                            {
                                resultBox.Text = resultBox.Text.Remove(resultBox.Text.Length - 1);
                            }
                            if (resultBox.Text.Length ==1 || resultBox.Text=="-0" || resultBox.Text == "-")
                            {
                                resultBox.Text = "0";
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
