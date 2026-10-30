using System.ComponentModel.DataAnnotations;

namespace Day3_NongVanNguyen.Models
{
    public class Product
    {
        [Required(ErrorMessage = "ID sản phẩm không được để trống")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string? Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Giá sản phẩm không được âm.")]
        public decimal Price { get; set; }

        public string? Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "ID danh mục không được để trống")]
        //foreign key to Category
        public int CategoryId { get; set; }

        //navigation property to Category -thuoc tinh dieu huong
        public Category? Category { get; set; }
    }
}

