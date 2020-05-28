using System;
using System.Collections.Generic;

namespace M3.DB
{
    public partial class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Class { get; set; }
    }
}
