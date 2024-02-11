namespace EStoreAPI.DTOs;

public class PaginatedResponse<T>
{
    public IEnumerable<T> Items { set; get; } = [];
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }
    public int  TotalItems { get; set; }
    public int? NextPage { get; set; }
}