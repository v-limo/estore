namespace Backend.Profile;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();

        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();

        CreateMap<CartDto, Cart>().ReverseMap();
        CreateMap<CartCreateDto, Cart>();
        CreateMap<CartUpdateDto, Cart>();

        CreateMap<CustomerDto, Customer>().ReverseMap();
        CreateMap<CustomerCreateDto, Customer>();
        CreateMap<CustomerUpdateDto, Customer>();

        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();
    }
}