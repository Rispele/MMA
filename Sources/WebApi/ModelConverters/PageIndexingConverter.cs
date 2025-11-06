namespace WebApi.ModelConverters;

public static class PageIndexingConverter
{
    public static int MapPageNumberToBatchNumber(int pageNumber)
    {
        return Math.Max(pageNumber - 1, 0);
    }
}