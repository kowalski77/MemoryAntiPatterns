using System;

namespace MemoryAntiPatterns
{
    public sealed class Product : IEquatable<Product>
    {
        public Product(int id, string name, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public int Id { get; }

        public string Name { get; }

        public decimal Price { get; internal set; }

        public bool Equals(Product other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Id == other.Id && this.Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Product other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Name);
        }

        public static bool operator ==(Product left, Product right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Product left, Product right)
        {
            return !Equals(left, right);
        }
    }
}