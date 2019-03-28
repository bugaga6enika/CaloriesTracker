namespace CaloriesTracker.Infrastructure.Rest
{
    public struct JsonResponse<T>
    {
        public T Content { get; set; }
        public bool Success { get; set; }
    }
}
