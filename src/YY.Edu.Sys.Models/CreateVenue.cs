﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Models
{
    public class CreateVenue : Venue
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "联系人邮箱不能为空")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "邮箱格式不正确")]
        public string LinkManEmail { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
