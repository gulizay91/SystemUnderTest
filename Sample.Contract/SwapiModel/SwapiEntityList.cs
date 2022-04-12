namespace Sample.Contract.SwapiModel
{
    public class SwapiEntityList<T>
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public IEnumerable<T> results { get; set; }

        public bool isNext => !String.IsNullOrEmpty(next);

        public bool isPrev => !String.IsNullOrEmpty(previous);
    }
}
