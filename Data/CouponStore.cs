using MinimalVilla.Models;

namespace MinimalVilla.Data
{
    public class CouponStore
    {
        public static List<Coupon> coupons = new List<Coupon>
        {
            new Coupon{Id=1,Name="aline",Persent=10,isActive=true},
            new Coupon{Id=2,Name="Axix",Persent=100,isActive=true}
         };

    }
}
