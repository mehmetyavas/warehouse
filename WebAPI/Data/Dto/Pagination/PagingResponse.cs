using WebAPI.Data.Entity.Base;

namespace WebAPI.Data.Dto.Pagination;

public class PagingResponse<T> : IDto
    where T : new()
{
    public PagingResponse(T data, int currentPage, int pageSize, int totalItems)
    {
        Data = data;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalItems = totalItems;
    }

    public PagingResponse()
    {
    }

    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    public T Data { get; set; }
}