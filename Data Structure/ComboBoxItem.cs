using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class ComboBoxItem
    {
        public string Name;
        public string Value;
        public ComboBoxItem(string name, string value)
        {
            Name = name; Value = value;
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }
    }
}
