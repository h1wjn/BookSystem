using System;
using System.ComponentModel.DataAnnotations;

namespace BooksSystem.Models
{
     public class BookCard
    {

        public int Id { get; set; } 

        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }


        [Required(ErrorMessage = "联系电话不能为空")]
        [Phone(ErrorMessage = "联系电话格式不正确")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "地址不能为空")]
        public string Address { get; set; }

        public DateTime CreateTime {get ; set;} = DateTime.Now;

    }
}
