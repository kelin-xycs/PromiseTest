using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace PromiseTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Promise.When((p) => { Read1(p); }, (p) => { Read2(p);  }).Then( (p) => { Read3(p); } );
        }

        // 如果 参数 p 传 null， 则作为 普通方法 调用
        private void Read1(Promise p)
        {
            byte[] b = new byte[2048];

            var s = File.Open("aa1.txt", FileMode.Open);

            p.State1 = s;   //  可在这里设置要传给 ThenAction 的 state 对象

            s.BeginRead(b, 0, 2048, CompletedRead1, p);
        }

        private void CompletedRead1(IAsyncResult ar)
        {

            WriteMsg("Read aa1.txt 完成");

            Promise p = (Promise)ar.AsyncState;

            if (p != null)
            {
                ((Stream)p.State1).Dispose();

                p.SetCompleted1();
            }
        }

        private void Read2(Promise p)
        {
            byte[] b = new byte[2048];

            var s = File.Open("aa2.txt", FileMode.Open);

            p.State2 = s;   //  可在这里设置要传给 ThenAction 的 state 对象

            s.BeginRead(b, 0, 2048, CompletedRead2, p);
        }

        private void CompletedRead2(IAsyncResult ar)
        {

            WriteMsg("Read aa2.txt 完成");

            Promise p = (Promise)ar.AsyncState;

            if (p != null)
            {
                ((Stream)p.State2).Dispose();

                p.SetCompleted2();
            }
        }

        private void Read3(Promise p)
        {
            byte[] b = new byte[2048];

            var s = File.Open("aa3.txt", FileMode.Open);

            p.State3 = s;   //  可在这里设置要传给 ThenAction 的 state 对象

            s.BeginRead(b, 0, 2048, CompletedRead3, p);
        }

        private void CompletedRead3(IAsyncResult ar)
        {

            WriteMsg("Read aa3.txt 完成");


            Promise p = (Promise)ar.AsyncState;

            if (p != null)
            {
                ((Stream)p.State3).Dispose();
                // 可通过 p.State1 和 p.State2 获取 Action1 和 Action2 传过来的 State 对象
            }
        }

        private void WriteMsg(string msg)
        {
            txtMsg.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + " " + msg + "\r\n");
        }
    }
}
