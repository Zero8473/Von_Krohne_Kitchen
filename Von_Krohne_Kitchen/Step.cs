using System;
using System.Collections.Generic;
using System.Text;

namespace Von_Krohne_Kitchen
{
    class Step
    {
        public Recipe Rec;

        public int No;

        public string Description;

        public Step(Recipe rec, int no, string description)
        {
            this.Rec = rec;
            this.No = no;
            this.Description = description;
        }
    }
}
