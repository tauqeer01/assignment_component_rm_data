namespace Assignment.Models
{
    public class ComponentWithRMViewModel
    {
        public string ComponentCode { get; set; }
        public string ComponentDescription { get; set; }
        public List<RMViewModel> RMData { get; set; } = new List<RMViewModel>();
    }
}
