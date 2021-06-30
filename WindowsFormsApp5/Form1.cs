using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        private string checkcode;
        public Form1()
        {
            InitializeComponent();
        }

        private string CheckBox()  //生成所需的验证码
        {
            int number;
            char code;
            Random random = new Random();
            this.checkcode = String.Empty;
            string showcheckcode = String.Empty;
            for(int i=0;i<4;i++)
            {
                number = random.Next(36);
                if(number>10)
                {
                    code = (char)('A' + (char)((number-9) % 26));
                }
                else
                {
                    code = (char)('0' + (char)(number));
                }
                showcheckcode += " " + code.ToString();
                this.checkcode += code.ToString();
            }
            return showcheckcode;
        }
        private void CodeImage(string showcheckcode)  //显示验证码图片
        {
            /*所需要的操作包括：
             * 1.画背景噪音线
             * 2.画验证码字符串
             * 3.画前景噪音点
             * 4.画边框线
             */
            if (showcheckcode == null || showcheckcode.Trim() == String.Empty) return;
            Bitmap imit = new Bitmap((int)Math.Ceiling((showcheckcode.Length*10.5)), 20);
            Graphics g = Graphics.FromImage(imit);
            Random random = new Random();  //生成随机生成器对象
            g.Clear(Color.White); //清空图片背景色
            //画背景噪音线
            for(int i=0;i<3; i++)
            {
                int x1 = random.Next(imit.Width);
                int x2 = random.Next(imit.Width);
                int x3 = random.Next(imit.Width);
                int y1 = random.Next(imit.Height);
                int y2 = random.Next(imit.Height);
                int y3 = random.Next(imit.Height);
                g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
            }
            //画验证码字符串
            Font font = new Font("Arial", 12, (FontStyle.Bold));
            g.DrawString(showcheckcode, font, new SolidBrush(Color.Red), 2, 2);
            //画前景噪声点
            for(int i=0;i<100;i++)
            {
                int x3 = random.Next(imit.Width);
                int y1 = random.Next(imit.Height);
                imit.SetPixel(x3, y1, Color.FromArgb(random.Next()));
            }
            //画边框线
            g.DrawRectangle(new Pen(Color.Blue), 0,0, imit.Width - 1, imit.Height - 1);
            this.pictureBox1.Width = imit.Width;
            this.pictureBox1.Height = imit.Height;
            this.pictureBox1.BackgroundImage = imit;
        }
        private void Form1_Load(object sender, EventArgs e)

        {
            CodeImage(CheckBox());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CodeImage(CheckBox());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.checkcode==textBox1.Text)
            {
                Form2 frm2 = new Form2();
                //frm2.ShowDialog();
                frm2.Show();
                //this.Hide();
                //this.Dispose();
            }
            else
            {
                MessageBox.Show("验证码错误，请重新输入！");
                CodeImage(CheckBox());
            }
        }


    }

}
