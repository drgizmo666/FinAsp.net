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

namespace cossword
{
    public partial class Form1 : Form
    {
        clues clue_windiow = new clues();
        List<id_cells> idc = new List<id_cells>();
        public string puzzle_file = Application.StartupPath + "\\Puzzles\\puzzle1.pzl";

        public Form1()
        {
            buildWordList();
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CrossWord puzzle by Gizmo's Endless Programming Wonders", "Help About" );
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializaBoard();
            clue_windiow.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
            clue_windiow.StartPosition = FormStartPosition.Manual;
            clue_windiow.Show();
            clue_windiow.clue_table.AutoResizeColumns();
        }

        private void InitializaBoard()
        {
            board.BackgroundColor = Color.Black;
            board.DefaultCellStyle.BackColor = Color.Black;

            for (int i = 0; i < 21; i++)
                board.Rows.Add();

            //set the width of colum
            foreach (DataGridViewColumn c in board.Columns)
                c.Width = board.Width / board.Columns.Count;

            //set the height of row
            foreach (DataGridViewRow r in board.Rows)
                r.Height = board.Height / board.Rows.Count;

             //make all cells readonly
            for (int row = 0; row < board.Rows.Count; row++)
            {
                for (int col = 0; col < board.Columns.Count; col++)
                {
                    board[col, row].ReadOnly = true;
                }
            }

            foreach (id_cells i in idc)
            {
                int start_col = i.X;
                int start_row = i.Y;
                char[] word = i.word.ToCharArray();

                for (int j = 0; j < word.Length; j++)
                {
                    if (i.direction.ToUpper() == "ACROSS")
                        formatCell(start_row, start_col + j, word[j].ToString());
                    if (i.direction.ToUpper() == "DOWN")
                        formatCell(start_row + j, start_col, word[j].ToString());
                }
            }
        }

        private void formatCell(int row, int col, string letter)
        {
            DataGridViewCell c = board[col, row];
            c.Style.BackColor = Color.White;
            c.ReadOnly = false;
            c.Style.SelectionBackColor = Color.Tomato;
            c.Tag = letter;
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            clue_windiow.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y); 
        }

        private void buildWordList()
        {
            string line = "";
            using (StreamReader s = new StreamReader(puzzle_file))
            {
                line = s.ReadLine(); //ignores first line
                while((line = s.ReadLine()) != null)
                {
                    string[] l = line.Split('|');
                    idc.Add(new id_cells(Int32.Parse(l[0]),Int32.Parse(l[1]),l[2],l[3],l[4],l[5]));
                    clue_windiow.clue_table.Rows.Add(new string[] {l[3], l[2], l[5] });
                }
            }
        }

        private void board_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Make letter uppercase
            try
            {
                board[e.ColumnIndex, e.RowIndex].Value = board[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
            }
            catch { }

            //truncate to one letter
            try
            {
                if(board[e.ColumnIndex, e.RowIndex].Value.ToString().Length > 1)
                    board[e.ColumnIndex, e.RowIndex].Value = board[e.ColumnIndex, e.RowIndex].Value.ToString().Substring(0, 1);
            }
            catch { }

            //format color if correct
            try
            {
                if (board[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper().Equals(board[e.ColumnIndex, e.RowIndex].Tag.ToString().ToUpper()))
                    board[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.DarkGreen;
                else
                    board[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.DarkRed;
            }
            catch { }
        }

        private void openPuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Puzzle Files|*.pzl";
            if (ofd.ShowDialog().Equals(DialogResult.OK))
            {
                puzzle_file = ofd.FileName;

                board.Rows.Clear();
                clue_windiow.clue_table.Rows.Clear();
                idc.Clear();

                buildWordList();
                InitializaBoard();

            }
        }

        private void board_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            string number = "";
            //foreach(item c in list of items)
            if (idc.Any(c => (number = c.number) != "" && c.X == e.ColumnIndex && c.Y == e.RowIndex))
            {
                Rectangle r = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                e.Graphics.FillRectangle(Brushes.White, r);
                Font f = new Font(e.CellStyle.Font.FontFamily, 7);
                e.Graphics.DrawString(number, f, Brushes.Black, r);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
        }
    }

    public class id_cells
    {
        public int X;
        public int Y;
        public string direction;
        public string number;
        public string word;
        public string clue;

        public id_cells(int x, int y, string d, string n, string w, string c)
        {
            this.X = x;
            this.Y = y;
            this.direction = d;
            this.number = n;
            this.word = w;
            this.clue = c;
        }
    }
}
