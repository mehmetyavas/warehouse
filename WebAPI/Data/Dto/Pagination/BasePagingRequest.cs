using WebAPI.Data.Entity.Base;
using WebAPI.Data.Enum;

namespace WebAPI.Data.Dto.Pagination;

public class BasePagingRequest : IDto
{
    public BasePagingRequest()
    {
        Page = 1;
        Limit = 20;
    }

    private int _limit;
    private string? _sortBy;
    public int Page { get; set; }

    public int Limit
    {
        get => _limit;
        set => _limit = value > 50 ? 50 : value;
    }

    public string SortBy
    {
        get => _sortBy ?? nameof(IEntity.CreatedAt);
        set => _sortBy = value;
    }

    public SortDirection SortDirection { get; set; } = SortDirection.Asc;
}