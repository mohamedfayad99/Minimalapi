using AutoMapper;
using MinimalVilla.Models;

namespace MinimalVilla
{
    public class MAppingConfic :Profile
    {
        public MAppingConfic()
        {
            CreateMap<Coupon, CuoponDTO>().ReverseMap();
            CreateMap<Coupon, couponcreateddto>().ReverseMap();
        }

    }
}
