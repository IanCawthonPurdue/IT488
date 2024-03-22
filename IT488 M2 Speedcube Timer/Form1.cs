namespace IT488_M2_Speedcube_Timer
{
    public partial class Form1 : Form
    {
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        bool timerOn = false;
        int hours = 0;
        int minutes = 0;
        int seconds = 0;
        int milliseconds = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private String getDisplayTime(TimeSpan ts)
        {
            if (ts.Hours > 0)
            {
                return String.Format("{0:0}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            }

            else if (ts.Minutes > 0)
            {
                return String.Format("{0:0}:{1:00}.{2:000}", ts.Minutes, ts.Seconds, ts.Milliseconds);
            }

            else
            {
                return String.Format("{0:0}.{1:000}", ts.Seconds, ts.Milliseconds);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timerOn){
                timerOn = true;
                timer.Start();
                textBox1.Text = "timer running...";
                button1.Text = "STOP";
            }
            else
            {
                timerOn = false;
                timer.Stop();
                button1.Text = "START";

                TimeSpan solveTime = timer.Elapsed;
                textBox1.Text = getDisplayTime(solveTime);

                timer.Reset();
            }
        }
    }
}
