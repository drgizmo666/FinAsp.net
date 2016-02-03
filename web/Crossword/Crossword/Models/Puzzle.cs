using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crossword.Models
{
    public class Puzzle
    {
        public int PuzzleId { get; set; }
        List<Clue> clues = new List<Clue>();
        public string PuzzleName { get; set; }
        public List<Clue> Clues
        {
            get { return clues; }
        }
    }
}