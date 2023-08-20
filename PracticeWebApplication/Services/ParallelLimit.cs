namespace PracticeWebApplication.Services
{
    public class ParallelLimit
    {
        public int CurrentLimit { get; set; }
        public int MaxLimit { get; set; }

        public ParallelLimit(int maxLimit) 
        { 
            MaxLimit = maxLimit;
            CurrentLimit = 0;
        }
    }
}
