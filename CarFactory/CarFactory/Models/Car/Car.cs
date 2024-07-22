using CarFactory.Models.BodyShape;
using CarFactory.Models.CarColor;
using CarFactory.Models.Engine;
using CarFactory.Models.Gearboxes;
using CarFactory.Models.SteeringPosition;

namespace CarFactory.Models.Cars
{
    public class Car : ICar
    {
        public IBodyShape _bodyShape;
        public ICarColor _color;
        public IEngine _engine;
        public IGearboxes _gearbox;
        public ISteeringPosition _steeringPosition;

        public string Name { get; private set; }

        public Car( string name, IBodyShape bodyShape, ICarColor color, IEngine engine, IGearboxes gearbox, ISteeringPosition steeringPosition )
        {
            Name = name;
            _bodyShape = bodyShape;
            _color = color;
            _engine = engine;
            _gearbox = gearbox;
            _steeringPosition = steeringPosition;
        }

        public int GetMaxSpeed() => _engine.MaxSpeed;

        public int GetGearsNumber() => _gearbox.GearsNumber;

        public void ShowCarConfigurations()
        {
            Console.WriteLine( $"\nНазвание машины: {Name}" );
            Console.WriteLine( $"Форма кузова: {_bodyShape.Name}" );
            Console.WriteLine( $"Цвет машины: {_color.Name}" );
            Console.WriteLine( $"Тип двигателя: {_engine.Name}" );
            Console.WriteLine( $"Коробка передач: {_gearbox.Name}" );
            Console.WriteLine( $"Позиция руля: {_steeringPosition.Name}" );
            Console.WriteLine( $"Максимальная скорость: {GetMaxSpeed()} км/ч" );
            Console.WriteLine( $"Количество передач: {GetGearsNumber()}" );
        }
    }
}