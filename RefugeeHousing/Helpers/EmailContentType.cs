namespace RefugeeHousing.Helpers
{
    public class EmailContentType
    {
        public static readonly EmailContentType Html = new EmailContentType("text/html");
        public static readonly EmailContentType Plain = new EmailContentType("text/plain");

        private readonly string value;

        private EmailContentType(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
