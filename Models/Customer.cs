using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksSystem.Models
{
    
    public class Customer
    {

        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

      
        [Required]
        [StringLength(50)]
        [Display(Name = "用户密码")]
        public string Password { get; set; }

        
        /// 用户类型：0管理员，1普通用户
        [Required]
        [Range(0, 1)]
        [Display(Name = "用户类型")]
        public int Category { get; set; }



        [Display(Name = "电话")]
        public string PhoneNumber { get; set; }

        public int MaxNum {get; set;} = 3;

        public int CurrentNum {get; set;}

        public int HasCard {get; set;}


       
       
    }
}

