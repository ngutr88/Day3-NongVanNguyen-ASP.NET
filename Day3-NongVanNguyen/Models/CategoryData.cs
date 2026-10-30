namespace Day3_NongVanNguyen.Models
{
    // Dữ liệu danh mục dùng chung giữa các controller.
    internal static class CategoryData
    {
        internal static readonly List<Category> Items = new()
        {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" },
            new Category { Id = 3, Name = "Category 3" }
        };
    }
}
