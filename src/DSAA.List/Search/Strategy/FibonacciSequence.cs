namespace DSAA.List.Search.Strategy
{
    public sealed class FibonacciSequence
    {
        private FibonacciSequence(int n2, int n1)
        {
            N2 = n2;
            N1 = n1;
        }

        public int N2 { get; }
        public int N1 { get; }
        public int N => N1 + N2;

        public FibonacciSequence Next()
        {
            return new FibonacciSequence(N1, N);
        }

        public FibonacciSequence Previous()
        {
            return new FibonacciSequence(N1 - N2, N2);
        }

        public static FibonacciSequence First()
        {
            return new FibonacciSequence(0, 1);
        }
    }
}