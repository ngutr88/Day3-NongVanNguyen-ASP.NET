using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Day3_NongVanNguyen.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Tên danh mục phải từ 3 đến 100 ký tự!")]
        public string? Name { get; set; }
    }
}