using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crossword.Models
{
    public class Clue
    {
        public int ClueId { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public string Direction { get; set; }
        public int Number { get; set; }
        public string Answer { get; set; }
        public string AnswerClue { get; set; }
    }
}