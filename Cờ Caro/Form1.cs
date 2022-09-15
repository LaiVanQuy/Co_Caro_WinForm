using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cờ_Caro
{
    public partial class Form1 : Form
    {
        private List<Player> players;

        public List<Player> Players { get => players; set => players = value; }
        private int currentPlayer;
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        public List<List<Button>> Maxtrix { get => maxtrix; set => maxtrix = value; }

        private List<List<Button>> maxtrix;
        public Form1()
        {
            InitializeComponent();
            this.Players = new List<Player>()
            {
                new Player("Player 1","X"),
                new Player("Player 2","O")
            };
            this.CurrentPlayer = 0;
            txtPlayerName.Text = Players[CurrentPlayer].Name;
            txtSign.Text = Players[CurrentPlayer].Sign;
            NewGame();

        }
        void DrawChessBoard()
        {
            pnlChessBoard.Controls.Clear();
            Maxtrix = new List<List<Button>>();
            Button oldbtn = new Button() { Width=0,Location=new Point(0,0)};
            for(int i = 0; i <= ChessBoardSize.ChessBoard_Height; i++)
            {
                Maxtrix.Add(new List<Button>());
                for (int j = 0; j <= ChessBoardSize.ChessBoard_Width; j++)
                {
                    Button btn = new Button();
                    btn.Width = ChessBoardSize.Chess_Width;
                    btn.Height = ChessBoardSize.Chess_Height;
                    btn.Location = new Point(oldbtn.Location.X + oldbtn.Width, oldbtn.Location.Y);
                    btn.Tag = i.ToString();
                    btn.Click += Btn_Click;
                    pnlChessBoard.Controls.Add(btn);
                    Maxtrix[i].Add(btn);
                    oldbtn = btn;
                }
                oldbtn.Location = new Point(0, oldbtn.Location.Y + ChessBoardSize.Chess_Height);
                oldbtn.Width = 0;
            }

        }
        public Point GetButtonPoint(Button btn)
        {
            
            int y = Convert.ToInt32(btn.Tag);
            int x = Maxtrix[y].IndexOf(btn);
            Point point = new Point(x,y);
            return point;
        }
        public void EndGame(Button btn)
        {
            MessageBox.Show("Trò chơi kết thúc!");
            if (MessageBox.Show("Bạn có muốn vào ván mới", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                NewGame();

        }
        public bool isEndGame(Button btn)
        {
            return isEndHorizontalLine(btn) || isEndVerticalLine(btn) 
                || isEndMainDiagonal(btn) || isEndAuxiliaryDiagonal(btn);
        }
        public bool isEndHorizontalLine(Button btn)
        {
            Point point = GetButtonPoint(btn);
            int count1 = 0;
            for(int i = point.X; i >= 0; i--)
            {
                if (Maxtrix[point.Y][i].Text == btn.Text)
                    count1++;
                else break;

            }
            int count2 = 0;
            for (int i = point.X+1; i <= ChessBoardSize.ChessBoard_Width; i++)
            {
                if (Maxtrix[point.Y][i].Text == btn.Text)
                    count2++;
                else break;
            }
            return count1 + count2 == 5;
        }
        public bool isEndVerticalLine(Button btn)
        {
            Point point = GetButtonPoint(btn);
            int count1 = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Maxtrix[i][point.X].Text == btn.Text)
                    count1++;
                else break;

            }
            int count2 = 0;
            for (int i = point.Y + 1; i <= ChessBoardSize.ChessBoard_Height; i++)
            {
                if (Maxtrix[i][point.X].Text == btn.Text)
                    count2++;
                else break;
            }
            return count1 + count2 == 5;
        }
        public bool isEndMainDiagonal(Button btn)
        {
            Point point = GetButtonPoint(btn);
            int count1 = 0;
            for (int i = 0; i <=point.X; i++)
            {
                if (point.Y - i < 0 || point.X - i < 0)
                    break;
                if (Maxtrix[point.Y-i][point.X-i].Text == btn.Text)
                    count1++;
                else break;

            }
            int count2 = 0;
            for (int i = 1; i <= ChessBoardSize.ChessBoard_Width-point.X; i++)
            {
                if (point.Y + i > ChessBoardSize.ChessBoard_Height || point.X + i > ChessBoardSize.ChessBoard_Width)
                    break;
                if (Maxtrix[point.Y+i][point.X+i].Text == btn.Text)
                    count2++;
                else break;
            }
            return count1 + count2 == 5;
        }
        public bool isEndAuxiliaryDiagonal(Button btn)
        {
            Point point = GetButtonPoint(btn);
            int count1 = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y - i < 0 || point.X + i > ChessBoardSize.ChessBoard_Width)
                    break;
                if (Maxtrix[point.Y - i][point.X + i].Text == btn.Text)
                    count1++;
                else break;

            }
            int count2 = 0;
            for (int i = 1; i <= ChessBoardSize.ChessBoard_Width - point.X; i++)
            {
                if (point.Y + i > ChessBoardSize.ChessBoard_Height || point.X - i <0)
                    break;
                if (Maxtrix[point.Y + i][point.X - i].Text == btn.Text)
                    count2++;
                else break;
            }
            return count1 + count2 == 5;
        }
        public void NewGame() 
        {
            
            DrawChessBoard();
            this.CurrentPlayer = 0;
            txtPlayerName.Text = Players[CurrentPlayer].Name;
            txtSign.Text = Players[CurrentPlayer].Sign;

        }
        public void Quit()
        {
            Application.Exit();
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text != "")
                return;
            btn.Text = Players[CurrentPlayer].Sign;

            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
            txtPlayerName.Text = Players[CurrentPlayer].Name;
            txtSign.Text = Players[CurrentPlayer].Sign;
            if(isEndGame(btn))
            {
                EndGame(btn);
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát", "Thông báo", MessageBoxButtons.YesNoCancel) != System.Windows.Forms.DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
