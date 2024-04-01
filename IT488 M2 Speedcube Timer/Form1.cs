using System.Data.SqlClient;

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

        String connectionString = "Data Source=IANWUBBY;Initial Catalog=IT488_Speedcube_App;Integrated Security=SSPI";

        private void submitSolveData(String session, DateTime dateAndTime, String Puzzle, TimeSpan solveTime, String Scramble)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "INSERT INTO dbo.Solves (Session, Date_and_Time, Puzzle, SolveTime, Scramble) VALUES ('"+session+"', '"+dateAndTime.ToString()+"', '"+Puzzle+"', '"+solveTime.ToString()+"', '"+Scramble+"')";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Form1()
        {
            InitializeComponent();

            textBox2.Text = getRandomMoveScramble_3x3(25);
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

        private String getRandomMoveScramble_3x3(int numMoves)
        {
            Random r = new Random();
            int currMove = r.Next(0, 6);

            String[] moves = ["R", "L", "U", "D", "F", "B"];
            String[] moveTypes = ["", "i", "2"];

            String scramble = moves[currMove] + moveTypes[r.Next(0, 3)];

            for (int i = 0; i < numMoves - 1; i++)
            {
                currMove = (currMove + r.Next(1, 5)) % 6;
                scramble = scramble + " " + moves[currMove] + moveTypes[r.Next(0, 3)];
            }

            return scramble;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timerOn)
            {
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

                submitSolveData("3x3Sesh", DateTime.Now, "3x3x3", solveTime, textBox2.Text);

                timer.Reset();
                textBox2.Text = getRandomMoveScramble_3x3(25);
            }
        }

        private bool tryConnection(string connectionString)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
            }

            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
