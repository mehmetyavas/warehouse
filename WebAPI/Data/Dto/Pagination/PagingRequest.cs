namespace WebAPI.Data.Dto.Pagination;

public class PagingRequest : BasePagingRequest
{
    public string? Column { get; set; }

    public string? SearchParam { get; set; }
}