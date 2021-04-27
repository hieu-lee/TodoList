using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Services
{
    public class DateService
    {
        public DateTime? Date { get; set; } = DateTime.Now.ToLocalTime().Date;
    }
}
