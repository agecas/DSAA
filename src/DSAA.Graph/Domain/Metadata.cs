namespace DSAA.Graph.Domain
{
    public sealed class Metadata
    {
        public Metadata(GraphType type, int verticesCount, int edgeCount, bool connected,
            bool weighted,
            bool hasNegativeWeights)
        {
            Connected = connected;
            Type = type;
            VerticesCount = verticesCount;
            EdgeCount = edgeCount;
            Weighted = weighted;
            HasNegativeWeights = hasNegativeWeights;
        }

        public GraphType Type { get; set; }

        public int VerticesCount { get; }
        public int EdgeCount { get; }
        public bool Disconnected => Connected == false;
        public bool Connected { get; }
        public bool Weighted { get; }
        public bool Unweighted => Weighted == false;
        public bool HasNegativeWeights { get; }
        public bool AllPositiveWeights => HasNegativeWeights == false;

        public static Metadata Empty(GraphType type)
        {
            return new Metadata(type, 0, 0, false, false, false);
        }


        public override string ToString()
        {
            return
                $"{nameof(Type)}: {Type}, " +
                $"{nameof(VerticesCount)}: {VerticesCount}, " +
                $"{nameof(EdgeCount)}: {EdgeCount}, " +
                $"{nameof(Disconnected)}: {Disconnected}, " +
                $"{nameof(Connected)}: {Connected}, " +
                $"{nameof(Weighted)}: {Weighted}, " +
                $"{nameof(Unweighted)}: {Unweighted}, " +
                $"{nameof(HasNegativeWeights)}: {HasNegativeWeights}, " +
                $"{nameof(AllPositiveWeights)}: {AllPositiveWeights}";
        }
    }
}