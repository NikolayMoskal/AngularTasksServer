namespace MediaItemsServer.Models
{
    public class SecurityRelation
    {
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public InclusionType InclusionType { get; set; }
    }
}
