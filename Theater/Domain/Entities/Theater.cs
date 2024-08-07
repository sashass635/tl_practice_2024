namespace Domain.Entities
{
    public class Theater
    {
        public int Id { get; private init; }
        public string Name { get; private init; }
        public string Address { get; private init; }
        public DateTime OpeningDate { get; private init; }
        public List<WorkingHours> WorkingHours { get; private init; }
        public string Description { get; private init; }
        public string PhoneNumber { get; private init; }
        public List<Play> Plays { get; private init; } = new List<Play>();

        public Theater( string name, string address, DateTime openingDate, string description, string phoneNumber )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentNullException( $"{name} cannot be null" );
            }
            Name = name;

            if ( string.IsNullOrWhiteSpace( address ) )
            {
                throw new ArgumentNullException( $"{address} cannot be null" );
            }
            Address = address;
            OpeningDate = openingDate;

            if ( string.IsNullOrWhiteSpace( description ) )
            {
                throw new ArgumentNullException( $"{description} cannot be null" );
            }
            Description = description;

            if ( string.IsNullOrWhiteSpace( phoneNumber ) )
            {
                throw new ArgumentNullException( $"{phoneNumber} cannot be null" );
            }
            PhoneNumber = phoneNumber;
        }

        public void Update( string name, string description, string phoneNumber )
        {
            if ( !string.IsNullOrWhiteSpace( Name ) )
            {
                name = Name;
            }

            if ( !string.IsNullOrWhiteSpace( Description ) )
            {
                description = Description;
            }

            if ( !string.IsNullOrWhiteSpace( phoneNumber ) )
            {
                phoneNumber = PhoneNumber;
            }
        }
    }
}
