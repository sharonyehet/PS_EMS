public class ListingApiResponse<T>
{
    public int TotalRecord { get; set; } = 0;
    public int PageNo { get; set; } = 0; 
    public int PageSize { get; set; } = 10; 
    public T Data { get; set; }

    public ListingApiResponse(T data, int pageSize, int pageNo, int totalRecords)
    {
        TotalRecord = totalRecords;
        PageSize = pageSize;
        PageNo = pageNo;
        Data = data;
    }
}
