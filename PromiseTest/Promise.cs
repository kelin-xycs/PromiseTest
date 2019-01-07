using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromiseTest
{
    class Promise
    {
        private Action<Promise> a1;
        private Action<Promise> a2;
        private Action<Promise> a3;

        private object obj = new object();

        private bool a1Completed;
        private bool a2Completed;
        private bool allCompleted;

        public object State1;
        public object State2;
        public object State3;

        //private Para para = new Para();

        private Promise(Action<Promise> a1, Action<Promise> a2)
        {
            this.a1 = a1;
            this.a2 = a2;
        }

        public static Promise When(Action<Promise> a1, Action<Promise> a2)
        {
            var p = new Promise(a1, a2);

            return p;
        }

        public void Then(Action<Promise> a3)
        {
            this.a3 = a3;

            a1(this);
            a2(this);
        }

        public void SetCompleted1()
        {
            this.a1Completed = true;

            DoCompleted();
        }

        public void SetCompleted2()
        {
            this.a2Completed = true;

            DoCompleted();
        }

        public void DoCompleted()
        {   
            lock (this.obj)
            {
                if (this.allCompleted)
                {
                    return;
                }

                if (this.a1Completed && this.a2Completed)
                {
                    allCompleted = true;
                }
            }

            if (this.allCompleted)
            {
                this.a3(this);
            }
        }

        //public class Para
        //{
        //    public object State1;
        //    public object State2;
        //}

        //public delegate void ThenAction(Promise p);

    }
}
