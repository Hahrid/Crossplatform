using System;
using System.Collections.Generic;
using System.Text;

namespace Crossplatform_lab1
{
    class Node
    {
        public Node(char letter,bool taken,string word)
        {
            Letter = letter;
            Taken = taken;
            Word = word;
        }
        public string? Word { get; set; }
        public char Letter { get; set; }
        public bool Taken { get; set; }
    }
}
