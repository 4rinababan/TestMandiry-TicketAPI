using Test_Mandiri.Responses;

namespace Test_Mandiri.Pagination;

public class PaginationResponse<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Uri FirstPage { get; set; }
    public Uri LastPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public Uri NextPage { get; set; }
    public Uri PreviousPage { get; set; }
    public PaginationResponse(T data, int pageNumber, int pageSize, int totalPages, int totalRecords, string message)
    {
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
        this.TotalPages = totalPages;
        this.TotalRecords = totalRecords;
        this.Data = data;
        this.Message = message;
    }
}