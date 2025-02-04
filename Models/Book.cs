using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksSystem.Models
{
        public class Book
    {
        
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="分类名称不可为空")]
        [StringLength(50)]
        [Display(Name = "分类名称")]
        public string Category { get; set; }
        
        [Required(ErrorMessage = "书籍名称不可为空")]
        [StringLength(50)]
        [Display(Name = "书名")]
        public string Title { get; set; }
        
        [StringLength(500)]
        [Display(Name = "图片")]
        public string Img { get; set; } = "D:\\png\\pic.png";
        
        [Required(ErrorMessage = "作者不可为空")]
        [StringLength(50)]
        [Display(Name = "作者")]
        public string Author { get; set; }
       
        [StringLength(50)]
        [Display(Name = "出版社")]
        public string Press { get; set; }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "出版时间")]
        public DateTime Pubtime { get; set; }
        
        
        [Required(ErrorMessage = "简介不可为空")]
        [StringLength(500)]
        [Display(Name = "简介")]
        public string Description { get; set; }
       
        
        [Required]
        [Display(Name = "是否可用")]
        public bool Enabled { get; set; }
        
        
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "修改时间")]
        public DateTime EditTime { get; set; }

    }
}
