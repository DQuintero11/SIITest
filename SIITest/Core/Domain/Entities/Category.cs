using Newtonsoft.Json;

namespace SIITest.Core.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public string Image { get; private set; }
        public DateTime CreationAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        [JsonConstructor]
        private Category(int id, string name, string slug, string image)
        {
            Id = id;
            Name = name;
            Slug = slug;
            Image = image;
            CreationAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Category Create(int id, string name, string slug, string image)
        {
            return new Category(id, name, slug, image);
        }
    }

    public class CategoryBuilder
    {
        private int _id;
        private string _name;
        private string _slug;
        private string _image;

        public CategoryBuilder SetId(int id) { _id = id; return this; }
        public CategoryBuilder SetName(string name) { _name = name; return this; }
        public CategoryBuilder SetSlug(string slug) { _slug = slug; return this; }
        public CategoryBuilder SetImage(string image) { _image = image; return this; }

        public Category Build()
        {
            return Category.Create(_id, _name, _slug, _image);
        }
    }
}
