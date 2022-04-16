namespace FizzBuzzLibrary.Options
{
    public class FizzBuzzOptions
    {
        public FizzBuzzOptions()
        {
            NumberSubstitutions = new List<NumberSubstitution>();
        }

        public List<NumberSubstitution> NumberSubstitutions { get; set; }
    }
}
