using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksSystem.Models
{
    public class BorrowBook
    {
        [Key]
        public int id {get; set;}
        public int usrId {get; set;}

        public int bookId {get; set;}
        public string bookName {get; set;}

        public DateTime borrowTime {get; set;} = DateTime.Now;

        public DateTime deadLineTime {get; set;}

        public BorrowBook()
        {
            deadLineTime = borrowTime.AddDays(2);  // 设置截止日期为借书日期 + 2 天
        }
    }
}