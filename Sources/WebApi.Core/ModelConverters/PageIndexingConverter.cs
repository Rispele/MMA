namespace WebApi.Core.ModelConverters;

public static class PageIndexingConverter
{
    public static int MapPageNumberToBatchNumber(int pageNumber)
    {
        return Math.Max(pageNumber - 1, val2: 0);
    }
}