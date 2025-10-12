namespace E_Commerce.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public IEnumerable<CouponViewModel>AppliedCoupons { get; set; }
        public IEnumerable<ProductViewModel>Items { get; set; }
        public decimal InvoiceBeforeCoupons { get; set; }
        public decimal InvoiceAfterCoupons { get; set; }

    }
}
