using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SIITest.Core.Domain.Entities
{
    public class Products
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public Category Category { get; private set; }
        public IReadOnlyList<string> Images { get; private set; }
        public DateTime CreationAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        [JsonConstructor]
        private Products(int id, string title, string slug, decimal price, string description, Category category, List<string> images, DateTime creationAt, DateTime updatedAt)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            Id = id;
            Title = title;
            Slug = slug;
            Price = price;
            Description = description;
            Category = category;
            Images = images ?? new List<string>(); 
            CreationAt = creationAt;
            UpdatedAt = updatedAt;
        }

        public static Products Create(int id, string title, string slug, decimal price, string description, Category category, List<string> images)
        {
            return new Products(id, title, slug, price, description, category, images, DateTime.UtcNow, DateTime.UtcNow);
        }
    }
}
