namespace RefugeeHousing.ViewModels
{
    public class PropertyEnquiry
    {
        public int PropertyId { get; set; }
        public string EnquirerName { get; set; }
        public string EnquirerEmail { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationWebsite { get; set; }
        public bool EnquirerSpeaksEnglish { get; set; }
        public bool EnquirerSpeaksGreek { get; set; }
        public bool EnquirerSpeaksFrench { get; set; }
        public bool EnquirerSpeaksGerman { get; set; }
    }
}