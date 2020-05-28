using System;
using System.Collections.Generic;

namespace M3.DB
{
    public partial class Grades
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
    }
}
