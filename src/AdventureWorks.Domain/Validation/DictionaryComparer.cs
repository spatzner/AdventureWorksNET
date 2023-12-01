namespace AdventureWorks.Domain.Validation
{
    public class DictionaryComparer : IEqualityComparer<Dictionary<string, object?>>
    {
        public bool Equals(Dictionary<string, object?>? x, Dictionary<string, object?>? y)
        {
            if (x == y)
                return true;
            
            if (x is null || y is null)
                return false;
            
            return x.Count == y.Count && !x.Except(y).Any();
        }

        public int GetHashCode(Dictionary<string, object?> obj)
        {
            int hash = 17;

            unchecked
            {
                foreach (var kvp in obj)
                    hash = hash * 23 + kvp.GetHashCode();
            }

            return hash;
        }
    }
}