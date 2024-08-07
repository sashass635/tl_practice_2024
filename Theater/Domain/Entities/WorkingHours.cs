namespace Domain.Entities
{
    public class WorkingHours
    {
        public int Id { get; private init; }
        public DateTime OpeningDate { get; private init; }
        public DateTime ClosingDate { get; private init; }
        public bool IsWeekend { get; private init; }
        public int TheaterId { get; private init; }

        public WorkingHours( DateTime openingDate, DateTime closingDate, bool isWeekend )
        {
            if ( openingDate > closingDate )
                throw new ArgumentException( "Opening date cannot be later than closing date." );

            if ( !isWeekend )
            {
                throw new ArgumentException( $"The theater is closed on this day because it's a day off." );
            }

            OpeningDate = openingDate;
            ClosingDate = closingDate;
            IsWeekend = isWeekend;
        }
    }
}
