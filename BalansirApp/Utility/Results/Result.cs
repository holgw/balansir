namespace BalansirApp.Utility.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }

        // METHODS: Public
        public void SetFail(string message)
        {
            IsSuccess = false;
            Message = message;
        }
    }

    public class Result<T> : Result
    {
        public T Object { get; set; }
    }
}
