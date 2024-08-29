namespace Domain.Entities
{
    public class Composition
    {
        public int Id { get; private init; }
        public string Name { get; private init; }
        public string ShortDescription { get; private init; }
        public string CharactersInfo { get; private init; }
        public int AuthorId { get; private init; }
        public Author Author { get; private init; }
        public List<Play> Plays { get; private init; } = new List<Play>();

        public Composition( string name, string shortDescription, string charactersInfo, int authorId )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentNullException( "Name cannot be null or empty" );
            }
            Name = name;

            if ( string.IsNullOrWhiteSpace( shortDescription ) )
            {
                throw new ArgumentNullException( "Short description cannot be null or empty" );
            }
            ShortDescription = shortDescription;

            if ( string.IsNullOrWhiteSpace( charactersInfo ) )
            {
                throw new ArgumentNullException( "Information about characters cannot be null or empty" );
            }
            CharactersInfo = charactersInfo;
            AuthorId = authorId;
        }
    }
}